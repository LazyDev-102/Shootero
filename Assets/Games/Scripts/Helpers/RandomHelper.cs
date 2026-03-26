using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Helper {
    public static class RandomHelper {
        public static T RandomInCollection<T>(List<T> list) {
            if (list == null || list.Count == 0) {
                return default(T);
            }
            return list[Random.Range(0, list.Count)];
        }
        public static T RandomInCollection<T>(T[] array) {
            if (array == null || array.Length == 0) {
                return default(T);
            }
            return array[Random.Range(0, array.Length)];
        }
        public static T RandomInCollection<T>(T[] array, out int index) {
            if (array == null || array.Length == 0) {
                index = 0;
                return default(T);
            }
            index = Random.Range(0, array.Length);
            return array[index];
        }
        public static T RandomInCollection<T>(int maxLength, T[] array) {
            if (array == null || array.Length == 0) {
                return default(T);
            }
            return array[Random.Range(0, array.Length > maxLength ? maxLength : array.Length)];
        }

        public static T[] RandomInCollection<T>(T[] array, int number, bool duplicate = false) {
            if (!duplicate && array.Length < number) {
                Logs.LogError("Can't Random Collection because out of range");
                return null;
            }
            T[] result = new T[number];
            List<int> indexs = new List<int>();
            for (int i = 0; i < number; ++i) {
                int randomIndex = Random.Range(0, array.Length);
                if (!duplicate) {
                    while (indexs.Contains(randomIndex)) {
                        randomIndex = Random.Range(0, array.Length);
                    }
                    indexs.Add(randomIndex);
                }
                else {
                    indexs.Add(randomIndex);
                }
                result[i] = array[randomIndex];
            }

            return result;
        }

        public static List<T> RandomInCollection<T>(List<T> list, int number, bool duplicate = false) {
            if (!duplicate && list.Count < number) {
                Logs.LogError("Can't Random Collection because out of range");
                return null;
            }
            List<T> result = new List<T>();
            List<int> indexs = new List<int>();
            for (int i = 0; i < number; ++i) {
                int randomIndex = Random.Range(0, list.Count);
                if (!duplicate) {
                    while (indexs.Contains(randomIndex)) {
                        randomIndex = Random.Range(0, list.Count);
                    }
                    indexs.Add(randomIndex);
                }
                else {
                    indexs.Add(randomIndex);
                }
                result.Add(list[randomIndex]);
            }

            return result;
        }

        public static bool RandomWithProbability(int value) {
            return Random.Range(1, 101) <= value;
        }

        public static bool RandomWithPercent(float value) {
            return Random.Range(0, 100.0f) <= value;
        }

        public static int RandomInRange(RangeIntValue range) {
            return Random.Range(range.startValue, range.endValue + 1);
        }
        public static float RandomChoose(float a, float b) {
            return Random.Range(0, 2) == 0 ? a : b;
        }

        public static float RandomInRange(RangeFloatValue range) {
            return Random.Range(range.startValue, range.endValue);
        }

        public static int RandomWithPercent(int[] probabilities) { // random trả về index chứa xác suất trúng
            int randomNumber = UnityEngine.Random.Range(1, 101);
            int currentProbability = 0;
            for (int i = 0; i < probabilities.Length; ++i) {
                if (randomNumber <= currentProbability + probabilities[i]) {
                    return i;
                }
                currentProbability += probabilities[i];
            }
            return -1;
        }

        public static T RandomWithPercent<T>(T[] probabilities) where T : IPercentable {
            int randomNumber = UnityEngine.Random.Range(1, 101);
            int currentProbability = 0;
            for (int i = 0; i < probabilities.Length; ++i) {
                if (randomNumber <= currentProbability + probabilities[i].GetPercent()) {
                    return probabilities[i];
                }
                currentProbability += probabilities[i].GetPercent();
            }
            return default(T);
        }

        public static void Shuffle<T>(List<T> list) {
            int n = list.Count;
            while (n > 1) {
                int k = Random.Range(0, n);
                n--;
                T temp = list[k];
                list[k] = list[n];
                list[n] = temp;
            }
        }

        public static void Shuffle<T>(T[] array) {
            int n = array.Length;
            while (n > 1) {
                int k = Random.Range(0, n);
                n--;
                T temp = array[k];
                array[k] = array[n];
                array[n] = temp;
            }
        }
        public static void Shuffle<T, U>(T[] array, U[] array1) {
            int n = array.Length;
            while (n > 1) {
                int k = Random.Range(0, n);
                n--;
                T temp = array[k];
                array[k] = array[n];
                array[n] = temp;

                U temp1 = array1[k];
                array1[k] = array1[n];
                array1[n] = temp1;
            }
        }

        public static bool IsTrueOrFalse() {
            return Random.value < 0.5f;
        }
        public static bool CompareDistance(Vector3 pos1, Vector3 pos2, float distance) {
            return (pos2 - pos1).sqrMagnitude < distance * distance;
        }
    }


    public interface IPercentable {
        int GetPercent();
    }
}
