using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Kontacts.FileFulltextSearch {
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

        public static Dictionary<string,string> Xml2Dictionary(this string xml) {
            var xdoc = XDocument.Parse(xml);
            var dictionary = new Dictionary<string, string>();
            var rootElement = xdoc.Nodes().First() as System.Xml.Linq.XElement;
            var settinx = rootElement.Nodes().ToList();

            foreach (System.Xml.Linq.XElement node in settinx) {  //
                dictionary[node.Name.ToString()] = node.Value;
            }
            return dictionary;
        }

        public static string ToXML(this Dictionary<string, string> inputDict) {


            var xdoc = new XDocument(new XElement("root",
                inputDict.Select(entry => new XElement(entry.Key, entry.Value))));

            return xdoc.ToString();
        }
        }
}
