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
    public partial class PlaceOrder : Form
    {
        public PlaceOrder()  //this function displays the cook's id and the cook's location
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(@"Data Source=SARAH-PC\SQLEXPRESS;Initial Catalog=Food_Ordering_System;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;");
            con.Open();
            SqlCommand cmd = new SqlCommand("select cook_id , cook_address from cooks", con);
            cmd.CommandType = CommandType.Text; //we need to identify the command type bec we are using data reader
            SqlDataReader rdr = cmd.ExecuteReader(); //we use execute reader bec we are viewing data
            DataTable tbl_location = new DataTable();  //we create a table named tbl_location
            tbl_location.Columns.Add("ID");       //here we name two columns ID and Location
            tbl_location.Columns.Add("Location");
            DataRow row;                          //we create a new row for the table
            while (rdr.Read())                    //while the reader is reading the data from the database
            {
                row = tbl_location.NewRow();            //add a new row to table tbl_location
                row["ID"] = rdr["cook_id"];             //each row is filled with the data that the reader reads from the database
                row["Location"] = rdr["cook_address"];
                tbl_location.Rows.Add(row);             //add a new row
            }
            rdr.Close();
            con.Close();
            dataGridView1.DataSource = tbl_location;      //view the tbl_location in the dataGridView

        }

        private void PlaceOrder_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.White;
        }



        private void button2_Click(object sender, EventArgs e) // Enter
        {
            SqlConnection con = new SqlConnection(@"Data Source=SARAH-PC\SQLEXPRESS;Initial Catalog=Food_Ordering_System;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from food where category='" + textBox2.Text + "' and cook_id = '" + textBox1.Text + "'", con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable tbl_food = new DataTable();
            tbl_food.Columns.Add("food_id");
            tbl_food.Columns.Add("food_name");
            tbl_food.Columns.Add("price");
            tbl_food.Columns.Add("category");
            tbl_food.Columns.Add("rating");
            DataRow row;
            while (rdr.Read())
            {
                row = tbl_food.NewRow();
                row["food_id"] = rdr["food_id"];
                row["food_name"] = rdr["food_name"];
                row["price"] = rdr["price"];
                row["category"] = rdr["category"];
                row["rating"] = rdr["rating"];
                tbl_food.Rows.Add(row);
            }
            rdr.Close();
            con.Close();
            dataGridView1.DataSource = tbl_food;
        }

        private void button4_Click(object sender, EventArgs e)// order
        {
            SqlConnection con = new SqlConnection(@"Data Source=SARAH-PC\SQLEXPRESS;Initial Catalog=Food_Ordering_System;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;");
            con.Open();
            SqlCommand cmd3 = new SqlCommand("select price from food where food_id='" + textBox3.Text + "'", con);
            int price = (int)cmd3.ExecuteScalar();
            SqlCommand cmd = new SqlCommand("NewOrder", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FoodID", textBox3.Text));
            cmd.Parameters.Add(new SqlParameter("@Quant", textBox4.Text));
            cmd.Parameters.Add(new SqlParameter("@ClientID", Global.Globalvar));
            cmd.Parameters.Add(new SqlParameter("@Price", price));
            cmd.ExecuteScalar();
            MessageBox.Show("We have recieved you order and we sent and email with the delivery time");
            con.Close();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            AccountInfo frm = new AccountInfo();
            frm.Show();
        }


    }
}

