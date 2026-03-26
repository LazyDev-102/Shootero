// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("2dYaz/XokmcGoEcuEUd6SODcOA+qgLlR0uxv0n6SY4ceeN7evvI2/tSOXaeWcwpXgaWVskXcJju95+Jzo3rJQgzVCaFGt6wPeWiIKnifQcEY8SMM7ASHR/Eoeot5VgR/jYlXxxsrHr1zOD5Ke6hUIuCs+DzdY2Q5nS+sj52gq6SHK+UrWqCsrKyora5uJAGQ4+qZtyDq1ZUvbAazlpTqiV3G6tn1lsoja06IgDRbeKY7XGdIChJX2hBnhmFowaoS84lGYj7KUbcMYFsl/IBBZUOqDX2ecM/WUkCEwC+soq2dL6ynry+srK0l/zGSTKgKatPkW6PiRBJoeXykPj8XFVJKkVIJ44qYxKXr3FuwiuQFGnv4f+9Cwdn3B4FGQfPShK+urK2s");
        private static int[] order = new int[] { 4,2,11,6,6,12,6,13,13,9,13,11,12,13,14 };
        private static int key = 173;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
