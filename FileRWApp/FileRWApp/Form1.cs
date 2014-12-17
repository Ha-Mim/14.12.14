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

namespace FileRWApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string path = @"D:\asp.net\info.txt";
        private void saveButton_Click(object sender, EventArgs e)
        {
            FileStream aFileStream = new FileStream(path,FileMode.Append);
            StreamWriter aStreamWriter = new StreamWriter(aFileStream);
            aStreamWriter.Write(nameTextBox.Text);
            aStreamWriter.WriteLine();
            aStreamWriter.Close();
            aFileStream.Close();
            nameTextBox.Text = string.Empty;
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            FileStream aFileStream = new FileStream(path,FileMode.Open);
            StreamReader aStreamReader = new StreamReader(aFileStream);

            showListBox.Items.Clear();
            while (!aStreamReader.EndOfStream )
            {
                string name = aStreamReader.ReadLine();
                showListBox.Items.Add(name);
            }
            aStreamReader.Close();
            aFileStream.Close();
           

        }
    }
}
