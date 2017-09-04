using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileFulltextSearch {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
        }

 

        private void btSearch_Click(object sender, EventArgs e) {
            toolStripStatusLabel1.Text = "Searching...";
            listView1.Items.Clear();
            toolStripProgressBar1.Value = 0;
            ScanDir();
            int file = 0;

            foreach (var _file in _files) {
                file++;
                string atxt = File.ReadAllText(_file.FullName);
                var ix =atxt.ToLowerInvariant().AllIndexesOf(txtTerm.Text.Trim().ToLowerInvariant());
                foreach (var ixx in ix) {

                    listView1.Items.Add(new ListViewItem(new string[] {
                    "" +_file.Length / 1024,
                    _file.Name
                    , ixx.ToString(),
                    atxt.Substring(ixx > 10 ? ixx - 10 : ixx, Math.Min( 50, atxt.Length - ixx))
                })).Tag = _file;

                }

                toolStripProgressBar1.Value = 100 * ( file / _files.Count());

                System.Windows.Forms.Application.DoEvents();
            }
            toolStripStatusLabel1.Text = "ready.";

        }

     

        private void button1_Click(object sender, EventArgs e) {
            folderBrowserDialog1.ShowNewFolderButton = false;
            folderBrowserDialog1.SelectedPath = txtFolder.Text;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                txtFolder.Text = folderBrowserDialog1.SelectedPath;
            }

            ScanDir();

        }

        private void exitrToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }
 

        private void frmMain_Load(object sender, EventArgs e) {
            txtFolder.Text = Environment.CurrentDirectory;
        }

        private void txtFolder_TextChanged(object sender, EventArgs e) {
        }

        List<FileInfo> _files;

        private void ScanDir(string dir = null) {
            bool topCall = dir == null;
            if (topCall) {
                dir = txtFolder.Text;
                _files = new List<FileInfo>();
            }
            var di = new DirectoryInfo(dir);

            System.Windows.Forms.Application.DoEvents();
            
            _files.AddRange( di.EnumerateFiles(
                txtFilePattern.Text, 
                chkRecursive.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList());

            foreach (var dii in di.GetDirectories()) {
                try {
                    ScanDir(dii.FullName);
                } catch (UnauthorizedAccessException ae) {
                    statusStrip1.Text = "Unauthorized to read " + dii.Name;
                }
            }

            if (topCall)
                lbFiles.Text = $"Selected {_files.Count()} files.";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void listView1_DoubleClick(object sender, EventArgs e) {
            if (listView1.SelectedItems.Count == 1) {
                var si = listView1.SelectedItems[0];
                var fi = (FileInfo)si.Tag;
                System.Diagnostics.Process.Start(new ProcessStartInfo {
                     Arguments = fi.FullName
                     , FileName = "notepad.exe"
                });

            }
        }
    }
}
