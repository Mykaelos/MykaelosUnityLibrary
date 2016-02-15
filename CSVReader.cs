using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
//using ScottGarland;

public class CSVReader {
    private const string rowIgnore = "ignore";
    private const char fieldDelimiter = ',';
    private const char lineDelimiter = '\n';

    public List<string> Header;
    public Dictionary<string, int> HeaderMap = new Dictionary<string, int>();
    public List<List<string>> Data = new List<List<string>>();

    public int RowCount {
        get { return Data.Count; }
    }


    public CSVReader(string fileText) {
        string[] rows = fileText.Split(lineDelimiter);
        bool foundHeader = false;

        for (int i = 0; i < rows.Length; i++) {
            string[] cols = ParseRow(rows[i]).ToArray();//.Split(fieldDelimiter);

            if (rowIgnore.Equals(cols[0]) || string.Empty.Equals(cols[0])) {
                continue;
            }

            if (!foundHeader) {
                Header = new List<string>(cols);
                foundHeader = true;
                continue;
            }

            Data.Add(new List<string>(cols));
        }

        for (int i = 0; i < Header.Count; i++) {
            HeaderMap.Add(Header[i], i);
        }
    }

    public int GetCol(string name) {
        int col = 0;
        if (HeaderMap.TryGetValue(name, out col)) {
            return col;
        }
        Debug.Log("CSVReader: Failed to find Column " + name);
        return -1;
    }

    public string GetString(int row, string field, string defaultValue = default(string)) {
        int col = GetCol(field);
        if (col == -1 || row < 0 || row >= Data.Count) {
            return null;
        }

        return Data[row][col];
    }

    public int GetInt(int row, string field, int defaultValue = default(int)) {
        string valueString = GetString(row, field);
        if (valueString == null || string.Empty.Equals(valueString)) {
            return defaultValue;
        }

        int value = defaultValue;
        try {
            value = int.Parse(valueString);
        }
        catch (System.Exception e) {
            Debug.Log("CSVReader: BAD INT.PARSE ON " + valueString + "\n" + e.Message);
        }
        return value;
    }

    public float GetFloat(int row, string field, float defaultValue = default(float)) {
        string valueString = GetString(row, field);
        if (valueString == null || string.Empty.Equals(valueString)) {
            return defaultValue;
        }

        float value = defaultValue;
        try {
            value = float.Parse(valueString);
        }
        catch (System.Exception e) {
            Debug.Log("CSVReader: BAD FLOAT.PARSE ON " + valueString + "\n" + e.Message);
        }
        return value;
    }

    //public BigInteger GetBigInt(int row, string field) {
    //    string valueString = GetString(row, field);
    //    if (valueString == null || string.Empty.Equals(valueString)) {
    //        return null;
    //    }

    //    return new BigInteger(valueString);
    //}

    public bool GetBool(int row, string field) {
        string valueString = GetString(row, field).ToLower();
        return "1".Equals(valueString) || "t".Equals(valueString) || "true".Equals(valueString) || "yes".Equals(valueString);
    }

    private List<string> ParseRow(string rowString) {
        List<string> colList = new List<string>();
        bool inQuotes = false;
        StringBuilder currentCol = new StringBuilder();

        foreach (char currentChar in rowString.ToCharArray()) {
            switch (currentChar) {
                case fieldDelimiter:
                    if (inQuotes) {
                        currentCol.Append(currentChar);
                    }
                    else {
                        colList.Add(currentCol.ToString());
                        currentCol = new StringBuilder();
                    }
                    break;
                case '\"':
                    inQuotes = !inQuotes;
                    break;//removes quote
                case '\r':
                    break;//ignores return character
                default:
                    currentCol.Append(currentChar);
                    break;
            }
        }
        colList.Add(currentCol.ToString().ConvertNewlineStringToChar());

        return colList;
    }
}
