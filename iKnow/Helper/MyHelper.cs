﻿namespace iKnow.Helper {
    public class MyHelper {
        public static string UppercaseWords(string value) {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1) {
                if (char.IsLower(array[0])) {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++) {
                if (array[i - 1] == ' ') {
                    if (char.IsLower(array[i])) {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        public static string CapitalizeWords(string value) {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1) {
                if (char.IsLower(array[0])) {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            
            return new string(array);
        }
    }
}