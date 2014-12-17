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

namespace AddressBookApp
{
    public partial class AddressBook : Form
    {
        public AddressBook()
        {
            InitializeComponent();
        }

        private string path = @"E:\AddressBookInfo.csv";

        private void AddressBook_Load(object sender, EventArgs e)
        {
            showAllListView.Columns.Add(label1.Text);
            showAllListView.Columns.Add(label2.Text);
            showAllListView.Columns.Add(label3.Text);
            showAllListView.Columns.Add(label4.Text);
            showAllListView.Columns.Add(label5.Text);
            searchComboBox.Text = searchComboBox.Items[0].ToString();
            catagoryLabel.Text = searchComboBox.Text;

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (uniqueContactNumber(personalContactNumberTextBox.Text))
            {
                string name = nameTextBox.Text;
                string email = emailTextBox.Text;
                string mobile = personalContactNumberTextBox.Text;
                string tnt = homeContactNumberTextBox.Text;
                string address = addressTextBox.Text;
                FileStream aFileStream = new FileStream(path, FileMode.Append);
                CsvFileWriter aWriter = new CsvFileWriter(aFileStream);
                List<string> addressBook = new List<string>();
                addressBook.Add(name);
                addressBook.Add(email);
                addressBook.Add(mobile);
                addressBook.Add(tnt);
                addressBook.Add(address);
                aWriter.WriteRow(addressBook);
                aFileStream.Close();

            }
            else
            {
                MessageBox.Show("Please, provide another contact number");
            }
        }

        private bool uniqueContactNumber(string ContactNo)
        {
            FileStream aFileStream = new FileStream(path, FileMode.OpenOrCreate);
            CsvFileReader aReader = new CsvFileReader(aFileStream);
            List<string> addressBook = new List<string>();
            while (aReader.ReadRow(addressBook))
            {
                if (addressBook[2] == ContactNo)
                    return false;
            }
            aFileStream.Close();
            return true;


        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            showAllListView.Items.Clear();
            FileStream aFileStream = new FileStream(path, FileMode.Open);
            CsvFileReader aCsvFileReader = new CsvFileReader(aFileStream);
            List<string> personalInfo = new List<string>();

            while (aCsvFileReader.ReadRow(personalInfo))
            {
                ListViewItem myView = new ListViewItem(personalInfo[0]);
                for (int i = 0; i < 5; i++)
                {
                    myView.SubItems.Add(personalInfo[i]);
                }
                showAllListView.Items.Add(myView);
            }
            aFileStream.Close();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            FileStream aStream = new FileStream(path, FileMode.Open);
            CsvFileReader csvReader = new CsvFileReader(aStream);
            List<string> infoList = new List<string>();
            if (catagoryTextBox.Text != string.Empty)
            {
                if (searchComboBox.Text == "Name")
                {
                    while (csvReader.ReadRow(infoList))
                    {
                        bool check = false;
                        ListViewItem myView = new ListViewItem(infoList[0]);
                        foreach (char c in catagoryTextBox.Text)
                        {
                            if (infoList[0].ToLower().Contains(c.ToString().ToLower()))
                            {
                                check = true;
                                break;
                            }
                        }
                        if (check)
                        {
                            for (int i = 1; i < 5; i++)
                                myView.SubItems.Add(infoList[i]);
                            showAllListView.Items.Add(myView);
                        }

                    }
                    aStream.Close();
                }
                else if (searchComboBox.Text == "Email")
                {
                    while (csvReader.ReadRow(infoList))
                    {
                        bool check = false;
                        ListViewItem myView = new ListViewItem(infoList[0]);
                        foreach (char c in catagoryTextBox.Text)
                        {
                            if (infoList[1].ToLower().Contains(c.ToString().ToLower()))
                            {
                                check = true;
                                break;
                            }
                        }
                        if (check)
                        {
                            for (int i = 1; i < 5; i++)
                                myView.SubItems.Add(infoList[i]);
                            showAllListView.Items.Add(myView);
                        }

                    }
                    aStream.Close();
                }
                else
                {
                    while (csvReader.ReadRow(infoList))
                    {
                        bool check = false;
                        ListViewItem myView = new ListViewItem(infoList[0]);

                        if (infoList[2] == catagoryTextBox.Text)
                        {
                            check = true;
                        }
                        if (check)
                        {
                            for (int i = 1; i < 5; i++)
                                myView.SubItems.Add(infoList[i]);
                            showAllListView.Items.Add(myView);
                        }

                    }
                    aStream.Close();
                }
            }
        }
    }
}
