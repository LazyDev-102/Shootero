using UnityEngine;
using System.Collections.Generic;

namespace GameSystem.Common.Utilities {
    public class ShuffleBag<T> {
        private List<T> bag;
        private int cursor = 0;

        public ShuffleBag() : this(4) {
        }

        public ShuffleBag(int capacity) {
            bag = new List<T>(capacity);
        }

        public ShuffleBag(T[] initalValues) : this(initalValues.Length) {
            for (int i = 0; i < initalValues.Length; i++) {
                Add(initalValues[i]);
            }
        }

        public T this[int index] {
            get { return bag[index]; }
            set { bag[index] = value; }
        }

        public T Next() {
            if (cursor < 1) {
                cursor = bag.Count - 1;
                if (bag.Count < 1) {
                    return default(T);
                }
                return bag[0];
            }

            int grab = Mathf.FloorToInt(Random.value * (cursor + 1));
            T temp = bag[grab];
            bag[grab] = this.bag[this.cursor];
            bag[cursor] = temp;
            cursor--;
            return temp;
        }

        public int Count {
            get { return bag.Count; }
        }

        public int IndexOf(T item) {
            return bag.IndexOf(item);
        }

        public bool Contains(T item) {
            return bag.Contains(item);
        }

        public void Add(T item) {
            bag.Add(item);
            cursor = bag.Count - 1;
        }

        public bool Remove(T item) {
            cursor = bag.Count - 2;
            return bag.Remove(item);
        }

        public void RemoveAt(int index) {
            cursor = bag.Count - 2;
            bag.RemoveAt(index);
        }

        public void Clear() {
            bag.Clear();
        }
    }
}
