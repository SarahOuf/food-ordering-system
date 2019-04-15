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
    public partial class NewAccount : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=SARAH-PC\SQLEXPRESS;Initial Catalog=Food_Ordering_System;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;");
        public NewAccount()
        {
            InitializeComponent();
        }

        private void NewAccount_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"insert into clients (first_name , last_name , client_email , password , client_address , credit_card) 
            values (@FirstName , @LastName , @email , @password , @Address , @CreditCard)", con);
            cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
            cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
            cmd.Parameters.AddWithValue("@email", textBox3.Text);
            cmd.Parameters.AddWithValue("@password", textBox4.Text);
            cmd.Parameters.AddWithValue("@Address", textBox5.Text);
            cmd.Parameters.AddWithValue("@CreditCard", textBox6.Text);
            cmd.ExecuteNonQuery(); //executes without returning a value

            SqlCommand cmd2 = new SqlCommand(@"insert into client_phone (country_code , carrier , number)
            values (@CountryCode , @Carrier , @Number)", con);
            cmd2.Parameters.AddWithValue("@CountryCode", textBoxPhone1.Text);
            cmd2.Parameters.AddWithValue("@Carrier", textBoxPhone2.Text);
            cmd2.Parameters.AddWithValue("@Number", textBoxPhone3.Text);
            cmd2.ExecuteNonQuery();
            SqlCommand cmd3 = new SqlCommand("select client_id from clients where client_email='" + textBox3.Text + "'", con);
            Global.Globalvar = (int)cmd3.ExecuteScalar();

            con.Close();

            MessageBox.Show("Your account was created successfully!");

            this.Hide();
            PlaceOrder frm = new PlaceOrder();
            frm.Show();

        }
    }
}
