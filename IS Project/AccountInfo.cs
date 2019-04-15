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
    public partial class AccountInfo : Form
    {
        public AccountInfo()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(@"Data Source=SARAH-PC\SQLEXPRESS;Initial Catalog=Food_Ordering_System;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;");
            con.Open();
            SqlCommand cmd = new SqlCommand(@"select order_id , price_paid , order_date , quantity ,
            food_id from orders where client_id = '"+Global.Globalvar+"'" , con);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable tbl_orders = new DataTable();
            tbl_orders.Columns.Add("Order ID");
            tbl_orders.Columns.Add("Price Paid");
            tbl_orders.Columns.Add("Order Date");
            tbl_orders.Columns.Add("Food ID");
            tbl_orders.Columns.Add("Quantity");
            DataRow row;
            while (reader.Read())
            {
                row = tbl_orders.NewRow();
                row["Order ID"] = reader["order_id"];
                row["Price Paid"] = reader["price_paid"];
                row["Order Date"] = reader["order_date"];
                row["Quantity"] = reader["quantity"];
                row["Food ID"] = reader["food_id"]; 
                tbl_orders.Rows.Add(row);
            }
            reader.Close();
            con.Close();
            dataGridView1.DataSource = tbl_orders;
        }

        private void button2_Click(object sender, EventArgs e)//update button
        {
            SqlConnection con = new SqlConnection(@"Data Source=SARAH-PC\SQLEXPRESS;Initial Catalog=Food_Ordering_System;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;");
            con.Open();
            SqlCommand cmd = new SqlCommand(@"update clients set first_name = @FirstName , last_name = @LastName , 
            client_email = @email , password = @password, client_address = @Address , credit_card = @CreditCard where client_email = @email", con);
            cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
            cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
            cmd.Parameters.AddWithValue("@email", textBox3.Text);
            cmd.Parameters.AddWithValue("@password", textBox4.Text);
            cmd.Parameters.AddWithValue("@Address", textBox5.Text);
            cmd.Parameters.AddWithValue("@CreditCard", textBox6.Text);
            cmd.ExecuteNonQuery();
            SqlCommand cmd3 = new SqlCommand("select client_id from clients where client_email ='" + textBox3.Text + "'", con);
            int clientID = (int)cmd3.ExecuteScalar();
            SqlCommand cmd2 = new SqlCommand(@"update client_phone set country_code = @CountryCode, carrier = @Carrier , number = @Number where client_id = @clientID", con);
            cmd2.Parameters.AddWithValue("@clientID", clientID);
            cmd2.Parameters.AddWithValue("@CountryCode", textBoxPhone1.Text);
            cmd2.Parameters.AddWithValue("@Carrier", textBoxPhone2.Text);
            cmd2.Parameters.AddWithValue("@Number", textBoxPhone3.Text);
            cmd2.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Your account was updated successfully!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=SARAH-PC\SQLEXPRESS;Initial Catalog=Food_Ordering_System;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;");
            con.Open();

            SqlCommand cmd = new SqlCommand("select client_id from clients where client_email ='" + textBox3.Text + "'", con);
            int clientID = (int)cmd.ExecuteScalar();
            SqlCommand cmd2 = new SqlCommand(@"delete from client_phone where client_id = '" + clientID + "'", con); //deletes the client's phone by using the client's id.
            cmd2.ExecuteNonQuery();
            SqlCommand cmd4 = new SqlCommand(@"delete from orders where client_id = '" + clientID + "'", con); //deletes the client's orders
            cmd4.ExecuteNonQuery();
            SqlCommand cmd3 = new SqlCommand(@"delete from clients where client_email ='" + textBox3.Text + "'", con);
            cmd3.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Your account was deleted successfully!");

            this.Hide();
            Welcome frm = new Welcome();
            frm.Show();
        }

        private void textBoxPhone2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            PlaceOrder frm = new PlaceOrder();
            frm.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
