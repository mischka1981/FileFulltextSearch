using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Kontacts.FileFulltextSearch {
    internal class MySettings {
        

        private FileInfo GetSettingsFile() {
            var f= new FileInfo(Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "FileFullTextSearch.xml"));
            if (!f.Exists) {
                File.WriteAllText(f.FullName, new Dictionary<string, string>().ToXML());
                f.Refresh();
            }
            return f;
        }

        internal string GetSetting(string key) {
            var dict = File.ReadAllText(GetSettingsFile().FullName).Xml2Dictionary();
            if (dict.ContainsKey(key)) return dict[key];
            return null;
        }
        internal void SaveSetting(string key, string val) {
            var dict = File.ReadAllText(GetSettingsFile().FullName).Xml2Dictionary();
            dict[key] = val;
            File.WriteAllText(GetSettingsFile().FullName, dict.ToXML());
        }


    }
}
