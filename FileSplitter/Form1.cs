using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSplitter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            int _splitRowCount = int.Parse(txtLineNumber.Text);
            OpenFileDialog op1 = new OpenFileDialog();

            op1.Multiselect = true;

            op1.ShowDialog();

            op1.Filter = "allfiles|*.csv";
            txtInputURL.Text = op1.FileName;
            int count = 0;
            string[] FName;
            foreach (string s in op1.FileNames)
            {
                
                FName = s.Split('\\');
                //File.Copy(s, txtOutput.Text +"\\" + FName[FName.Length - 1]);


                var headerLine = File.ReadLines(txtInputURL.Text);

                var nHeaderLine = headerLine.Take(1);
                string FHeader = nHeaderLine.ToString();


                string[] lines = File.ReadAllLines(txtInputURL.Text);

                int _rowCountOutput = 1;
                int _rowCount = 1;
                bool _isHeader = false;
                string firstHeader = "";
                List<string> fileRow = new List<string>(); ;
                foreach (string _row  in lines)
                {
                    if (_rowCount == 1 && _isHeader == false)
                    {
                        firstHeader = _row;
                        fileRow.Add(_row);
                    }
                    else
                    {
                        fileRow.Add(_row);
                    }

                    if (_rowCount == _splitRowCount)
                    {   
                        File.WriteAllLines(txtOutput.Text +"\\" + _rowCountOutput + ".csv", fileRow.ToArray());
                        _rowCountOutput++;
                        fileRow = new List<string>();
                        fileRow.Add(firstHeader);
                        _rowCount = 0;
                    }
                    if (_isHeader == false)
                        _isHeader = true;

                    _rowCount++;
                }


                count++;
            }

            





            MessageBox.Show(Convert.ToString(count) + " File(s) copied");

        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtOutput.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

    }
}
