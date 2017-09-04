using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFulltextSearch {
    public static class FullTextSearcherExtensionMethods {

        /// <summary>
        /// https://stackoverflow.com/questions/2641326/finding-all-positions-of-a-substring-in-a-large-string-in-c-sharp
        /// from
        /// https://stackoverflow.com/users/227267/matti-virkkunen
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<int> AllIndexesOf(this string str, string value) {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length) {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }

    }
}
