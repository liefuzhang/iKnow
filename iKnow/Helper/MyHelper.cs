using System.Text.RegularExpressions;
using System.Web;

namespace iKnow.Helper {
    public class MyHelper {
        public static string CapitalizeAllWords(string value) {
            if (value == null) {
                return null;
            }

            var str = CapitalizeFirstWord(value);

            return CapitalizeWordsFollowingSpace(str);
        }

        public static string CapitalizeFirstWord(string value) {
            if (value == null) {
                return null;
            }

            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1) {
                if (char.IsLower(array[0])) {
                    array[0] = char.ToUpper(array[0]);
                }
            }

            return new string(array);
        }

        private static string CapitalizeWordsFollowingSpace(string str) {
            char[] array = str.ToCharArray();

            for (int i = 1; i < array.Length; i++) {
                if (array[i - 1] == ' ') {
                    if (char.IsLower(array[i])) {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }
    }
}