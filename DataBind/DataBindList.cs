using System;
using System.Collections.Generic;

namespace MykaelosUnityLibrary.DataBind {

    /**
     * A simple List wrapper that calls the ListChanged event when updated.
     * The objects in the list DO NOT trigger the event, only when the list itself changes (add/remove/clear). DataBindProperty can be used with this class to also listen for object change events.
     * 
     * Partially borrowed from http://unity-coding.slashgames.org/the-magic-behind-data-binding-part-1/ with some tweaks.
     */
    public class DataBindList<T> {
        private List<T> hiddenList = new List<T>();

        public List<T> List {
            get { return hiddenList; }
            set {
                if (Equals(hiddenList, value)) {
                    return;
                }

                hiddenList = value;
                OnListChanged();
            }
        }


        public DataBindList() { }

        public DataBindList(List<T> list) {
            List = list; // Triggers OnListChanged().
        }

        #region ListChanged Event
        public event Action ListChanged;

        protected void OnListChanged() {
            ListChanged?.Invoke();
        }
        #endregion ListChanged Event

        #region List Overrides
        public T this[int index] {
            get { return hiddenList[index]; }
            set {
                hiddenList[index] = value;
                OnListChanged();
            }
        }

        public void Add(T item) {
            List.Add(item);
            OnListChanged();
        }

        public void Remove(T item) {
            List.Remove(item);
            OnListChanged();
        }

        public void Clear() {
            List.Clear();
            OnListChanged();
        }

        // TODO Add overrides.
        //public void AddRange(IEnumerable<T> collection);
        //public void Insert(int index, T item);
        //public void InsertRange(int index, IEnumerable<T> collection);
        //public int RemoveAll(Predicate<T> match);
        //public void RemoveAt(int index);
        //public void RemoveRange(int index, int count);
        //public void Reverse(int index, int count);
        //public void Reverse();
        //public void Sort(Comparison<T> comparison);
        //public void Sort(int index, int count, IComparer<T> comparer);
        //public void Sort();
        //public void Sort(IComparer<T> comparer);
        //public void TrimExcess();
        // and everything else...

        public int Count {
            get { return hiddenList.Count; }
        }

        public bool Contains(T item) {
            return hiddenList.Contains(item);
        }

        // IEnumerable
        public IEnumerator<T> GetEnumerator() {
            return hiddenList.GetEnumerator();
        }
        #endregion List Overrides
    }

    public static class DataBindListExtensions {
        public static bool IsNullOrEmpty<T>(this DataBindList<T> dataBindList) {
            return dataBindList == null || dataBindList.List.IsNullOrEmpty();
        }

        public static bool IsNotEmpty<T>(this DataBindList<T> dataBindList) {
            return dataBindList != null && dataBindList.List.IsNotEmpty();
        }
    }
}