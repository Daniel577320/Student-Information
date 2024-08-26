using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        string path = "login.txt";
        string UserName;
        string Password;

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserName = txtUName.Text;
            Password = txtPass.Text;
            bool found = false;

            foreach (string line in File.ReadLines(path))
            {
                string[] sections = line.Split(' ');
                if (sections.Length == 2)
                {
                    string storedName = sections[0];
                    string storedPassword = sections[1];

                    if (UserName == storedName && Password == storedPassword)
                    {
                        MessageBox.Show("Succesfully logged in");
                        Windows_GUI main = new Windows_GUI();
                        main.Show();
                        this.Hide();                       
                        found = true;
                        return;
                    }
                }
            }

            if (found == false)
            {
                MessageBox.Show("Incorrect Username or Password");
            }
        }

        private void lkCreate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserName = txtUName.Text;
            Password = txtPass.Text;

            if (Exists(UserName))
            {
                MessageBox.Show("User already exists");
                return;
            }

            NewUser(UserName, Password);
            MessageBox.Show("User succesfully added");

            Windows_GUI main = new Windows_GUI();
            main.Show();
            this.Hide();
        }

        private bool Exists(string uname)
        {
            return File.ReadLines(path).Any(line => line.StartsWith(uname + " "));
        }

        private void NewUser(string uname, string password)
        {
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine($"{uname} {password}");
            }
        }

        private void txtUName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
