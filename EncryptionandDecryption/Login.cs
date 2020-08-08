using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace EncryptionandDecryption
{
    public partial class Login : Form
    {
        Thread th;
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AU8FHEB\SQLSERVER;Initial Catalog=LoginDB;Integrated Security=True");

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click_Click_1(object sender, EventArgs e)
        {
            string Password = "";
            bool IsExist = false;
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from tblUserRegistration where UserName='" + txtUserName.Text + "'", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Password = sdr.GetString(2);  //get the user password from db if the user name is exist in that.  
                IsExist = true;
            }
            con.Close();
            if (IsExist)  //if record exis in db , it will return true, otherwise it will return false  
            {
                if (Cryptography.Decrypt(Password).Equals(txtPassword.Text))
                {
                    MessageBox.Show("Login Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);                   
                    
                    this.Close();
                    
                    th = new Thread(openform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
                else
                {
                    MessageBox.Show("Password is wrong!...", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else  //showing the error message if user credential is wrong  
            {
                MessageBox.Show("Please enter the valid credentials", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        private void openform()
        {
            Application.Run(new TelaInicial());
        }
    }
}
