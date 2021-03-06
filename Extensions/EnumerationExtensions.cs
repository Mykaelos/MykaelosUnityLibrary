﻿using System;

/**
    Much of this is borrowed from http://www.codeproject.com/Articles/37921/Enums-Flags-and-C-Oh-my-bad-pun.
*/

public static class EnumerationExtensions {
    //checks if the value contains the provided type
    public static bool Has<T>(this Enum type, T value) {
        try {
            return (((int)(object)type & (int)(object)value) == (int)(object)value);
        }
        catch {
            return false;
        }
    }

    //checks if the value is only the provided type
    public static bool Is<T>(this Enum type, T value) {
        try {
            return (int)(object)type == (int)(object)value;
        }
        catch {
            return false;
        }
    }

    //appends a value
    public static T Add<T>(this Enum type, T value) {
        try {
            int typeInt = (int)(object)type;
            int valueInt = (int)(object)value;
            int output = typeInt | valueInt;
            //Because of the way this extension works, BESURE TO SET YOUR ENUM VARIABLE ON THE RETURN OF THIS METHOD.
            //It cannot be set in here because the output cannot be converted to the Enum type.

            return (T)(object)output;
        }
        catch (Exception ex) {
            throw new ArgumentException(string.Format("Could not append value from enumerated type '{0}'.", typeof(T).Name), ex);
        }
    }

    //completely removes the value
    public static T Remove<T>(this Enum type, T value) {
        try {
            int typeInt = (int)(object)type;
            int valueInt = (int)(object)value;
            int output = typeInt & ~valueInt;
            //Because of the way this extension works, BESURE TO SET YOUR ENUM VARIABLE ON THE RETURN OF THIS METHOD.
            //It cannot be set in here because the output cannot be converted to the Enum type.

            return (T)(object)output;
        }
        catch (Exception ex) {
            throw new ArgumentException(string.Format("Could not remove value from enumerated type '{0}'.", typeof(T).Name), ex);
        }
    }

    //toggles a value
    public static T Toggle<T>(this Enum type, T value) {
        return type.Has(value) ? type.Remove(value) : type.Add(value);
    }
}

/* http://stackoverflow.com/questions/8447/what-does-the-flags-enum-attribute-mean-in-c
A fantastic way to setup an enum:
[Flags]
public enum MyEnum
{
    None   = 0,
    First  = 1 << 0,
    Second = 1 << 1,
    Third  = 1 << 2,
    Fourth = 1 << 3
}

if(enumThingy.Has(Thingy.Banana | Thingy.Apple)) { } // pipe acts as a way to add them together. In this case, with Has, it acts as an 'or'.
*/
