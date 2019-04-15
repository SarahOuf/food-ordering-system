using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IS_Project
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();

        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.White;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e) // create account button
        {
            this.Hide();
            NewAccount frm = new NewAccount();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e) //Log in button
        {
            SqlConnection con = new SqlConnection(@"Data Source=SARAH-PC\SQLEXPRESS;Initial Catalog=Food_Ordering_System;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;");
            con.Open();
            SqlCommand cmd = new SqlCommand(" select count(*) from Clients where client_email='"+textBox1.Text+"'and password='" + textBox2.Text + "'", con);
            int count = (int)cmd.ExecuteScalar();
            if (count.ToString() == "1")
            {
                this.Hide();                                 //hides this form and opens the placeOrder form.
                PlaceOrder frm = new PlaceOrder();
                frm.Show();
                SqlCommand cmd2 = new SqlCommand("select client_id from clients where client_email='" + textBox1.Text + "'" , con);
                Global.Globalvar = (int)cmd2.ExecuteScalar(); //execute scalar as it returns one value only. 

            }


            else
            {
                MessageBox.Show("Please enter a correct E-mail and Password.");
            }
            con.Close();
            
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }
        
    }
}
