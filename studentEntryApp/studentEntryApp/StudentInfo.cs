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
using CSVLib;

namespace studentEntryApp
{
    public partial class StudentInfo : Form
    {
        public StudentInfo()
        {
            InitializeComponent();
        }

        private string path = @"D:\studentInfo.csv";

        private void saveButton_Click(object sender, EventArgs e)
        {
            string regNo = regNoTextBox.Text;
            string name = nameTextBox.Text;
            bool uniqueReg = UniqueRegNoCheker(regNo);
            
                if (uniqueReg == false)
                {
                    MessageBox.Show("Please,Provide another reg No.");
                    
                    
                }
                else 
                {
                    
                  
                    FileStream aFileStream = new FileStream(path, FileMode.Append);
                    CsvFileWriter aCsvFileWriter = new CsvFileWriter(aFileStream);
                    List<string> student = new List<string>();
                    student.Add(regNo);
                    student.Add(name);
                    aCsvFileWriter.WriteRow(student);
                    aFileStream.Close();
                    nameTextBox.Text = string.Empty;
                    regNoTextBox.Text = string.Empty;
                    MessageBox.Show("Information has Been Added");
                }
            

        }
        private bool UniqueRegNoCheker(string studentReg)
        {
            int studentRegInt = Convert.ToInt32(studentReg);
            FileStream aFileStream = new FileStream(path, FileMode.Open);
            CsvFileReader aCsvFileReader = new CsvFileReader(aFileStream);
            List<string> aStudentDataRead = new List<string>();
            showAllListBox.Items.Clear();
            bool a = true;
            while (aCsvFileReader.ReadRow(aStudentDataRead))
            {
                int studentRegNo = Convert.ToInt32(aStudentDataRead[0]);
                if (studentRegInt == studentRegNo)
                {
                    a = false;
                    break;
                }
                else
                {
                    a = true;
                }
            }
            aFileStream.Close();
            return a;
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            FileStream aStream = new FileStream(path,FileMode.Open);
            CsvFileReader aFileReader = new CsvFileReader(aStream);
            List<string> aStudentDataRead = new List<string>();
            showAllListBox.Items.Clear();
            while (aFileReader.ReadRow(aStudentDataRead))
            {
                string regNo = aStudentDataRead[0];
                string name = aStudentDataRead[1];
                showAllListBox.Items.Add(regNo + " : " + name);
            }
            aStream.Close();
        }
    }
}
