using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;


namespace EncryptionandDecryption
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Database=LoginDB;Username=postgres;Password=40028922;");


        private void Guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text != "" && txtPassword.Text != "" && txtConfirmPassword.Text != "")  //validating the fields whether the fields or empty or not  
            {
                if (txtPassword.Text.ToString().Trim().ToLower() == txtConfirmPassword.Text.ToString().Trim().ToLower()) //validating Password textbox and confirm password textbox is match or unmatch    
                {
                    string UserName = txtUserName.Text;
                    string Password = Cryptography.Encrypt(txtPassword.Text.ToString());   // Passing the Password to Encrypt method and the method will return encrypted string and stored in Password variable.  
                    con.Open();

                    
                    NpgsqlCommand insert = new NpgsqlCommand("INSERT INTO tblUserRegistration(UserName,Password)values('" + UserName + "','" + Password + "')", con);

                    insert.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else
                {
                    MessageBox.Show("Password and Confirm Password doesn't match!.. Please Check..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);  //showing the error message if password and confirm password doesn't match  
                }
            }
            else
            {
                MessageBox.Show("Please fill all the fields!..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);  //showing the error message if any fields is empty  
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
