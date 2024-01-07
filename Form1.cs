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

namespace CRUD
{
    public partial class Form1 : Form
    {

        // to creata local db: C:\WINDOWS\system32>sqlloacldb create "local"

        // CREATE TABLE[dbo].[student]
        // (

        //[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,

        //[name] NVARCHAR(50) NULL, 
        //[age] NVARCHAR(50) NULL, 
        // [gender] NVARCHAR(50) NULL
        //)

        private const string connectionSrting = "Data Source=(localdb)\\local;Initial Catalog=students;Integrated Security=True;Pooling=False";
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadDgv()
        {
            SqlConnection con = new SqlConnection(connectionSrting);
            con.Open();
            SqlDataAdapter cmd = new SqlDataAdapter("SELECT * FROM student", con);

            DataSet ds = new DataSet();
            cmd.Fill(ds, "student");
            dgv_students.DataSource = ds.Tables["student"].DefaultView;

            con.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDgv();
        }

        private void emptyFields()
        {
            name.Text = "";
            age.Text = "";
            gender.Text = "";
        }

        private void button_insert_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionSrting);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO student VALUES (@Name, @Age, @Gender)", con);
            cmd.Parameters.AddWithValue("@Name", name.Text);
            cmd.Parameters.AddWithValue("@Age", age.Text);
            cmd.Parameters.AddWithValue("@Gender", gender.Text);
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Saved successfully!");
            LoadDgv();
            emptyFields();

        }
        private int id;

        private void dgv_students_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int row = e.RowIndex;
            id = Convert.ToInt32(dgv_students[0, row].Value);
            name.Text = Convert.ToString(dgv_students[1, row].Value);
            age.Text = Convert.ToString(dgv_students[2, row].Value);
            gender.Text = Convert.ToString(dgv_students[3, row].Value);
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionSrting);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE student SET name = @Name, age = @Age, gender = @Gender WHERE id = @ID", con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", name.Text);
            cmd.Parameters.AddWithValue("@Age", age.Text);
            cmd.Parameters.AddWithValue("@Gender", gender.Text);
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Updated successfully!");
            LoadDgv();
            emptyFields();

        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionSrting);
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM student WHERE id = @ID", con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", name.Text);
            cmd.Parameters.AddWithValue("@Age", age.Text);
            cmd.Parameters.AddWithValue("@Gender", gender.Text);
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Deleted successfully!");
            LoadDgv();
            emptyFields();
        }
    }
}
