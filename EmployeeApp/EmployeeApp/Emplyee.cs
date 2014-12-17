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

namespace EmployeeApp
{
    public partial class Emplyee : Form
    {
        public Emplyee()
        {
            InitializeComponent();
        }
        string path = @"D:\asp.net\employee.text";
         string allInfo = "";
        private void saveButton_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            string ID = IDTextBox.Text;
            string salary = salaryTextBox.Text;
            FileStream aStream = new FileStream(path,FileMode.Open);
            StreamReader aReader = new StreamReader(aStream);
            allInfo = aReader.ReadLine();
            if (allInfo.Contains(ID) && IDTextBox.Text!=string.Empty)
            {
                MessageBox.Show("ID already been used, Please provide another ID");
                aReader.Close();
                aStream.Close();
            }
            else if (nameTextBox.Text!=string.Empty && salaryTextBox.Text!=string.Empty)
            {
                aReader.Close();
                aStream.Close();
                FileStream aFileStream = new FileStream(path, FileMode.Append);
                StreamWriter aStreamWriter = new StreamWriter(aFileStream);
                aStreamWriter.Write(name + " , " + ID + " , " + salary);
                aStreamWriter.WriteLine();
                aStreamWriter.Close();
                aFileStream.Close();
                nameTextBox.Text = string.Empty;
                IDTextBox.Text = string.Empty;
                salaryTextBox.Text = string.Empty;
            }
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            double sum = 0;
            FileStream aFileStream = new FileStream(path,FileMode.Open);
            StreamReader aStreamReader = new StreamReader(aFileStream);
            showListBox.Items.Clear();
            while (!aStreamReader.EndOfStream)
            {
                allInfo = aStreamReader.ReadLine();
                showListBox.Items.Add(allInfo);
                string[] amount = allInfo.Split(',');
                sum += Convert.ToDouble(amount[2]);
            }
            totalAmountTextBox.Text = Convert.ToString(sum);
            
            aStreamReader.Close();
            aFileStream.Close();
        }
       
    }
}
