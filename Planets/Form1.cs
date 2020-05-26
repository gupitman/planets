using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planets
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public double Gplanets = 6.67430E-20;

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\ПРОГРАМЫ\PLANETS\PLANETS.MDF;Integrated Security=True");
            connect.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT id, name_planet, M, R FROM Planets", connect);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds, "Planets");

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].HeaderText = "Идентификатор";
            dataGridView1.Columns[1].HeaderText = "Название планеты";
            dataGridView1.Columns[2].HeaderText = "M";
            dataGridView1.Columns[3].HeaderText = "R";
            connect.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            label_M.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            label_R.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString();
            //// result
            string MFirst = label_M.Text;
            string RFirst = label_R.Text;
            double MM = Convert.ToDouble(MFirst);
            double RR = Convert.ToDouble(RFirst);
            double answerFirst = Math.Sqrt((Gplanets * MM)/RR);
            double answerSecond = Math.Sqrt(((2 *Gplanets) * MM) / RR);
            label_first_cosm.Text = answerFirst.ToString();
            label_second_cosm.Text = answerSecond.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\ПРОГРАМЫ\PLANETS\PLANETS.MDF;Integrated Security=True");
            string upsql = "INSERT INTO Planets (name_planet, M, R) VALUES (@name_planet, @m, @r)";
            SqlCommand commandUP = new SqlCommand(upsql, connect);
            connect.Open();

            commandUP.Parameters.AddWithValue("name_planet", textBox1.Text);
            commandUP.Parameters.AddWithValue("m", textBox2.Text);
            commandUP.Parameters.AddWithValue("r", textBox3.Text);
            commandUP.ExecuteNonQuery();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            connect.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label10.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\ПРОГРАМЫ\PLANETS\PLANETS.MDF;Integrated Security=True");
            string upsql = "DELETE FROM Planets WHERE name_planet = @name";
            SqlCommand commandUP = new SqlCommand(upsql, connect);
            connect.Open();

            commandUP.Parameters.AddWithValue("name", label10.Text);

            commandUP.ExecuteNonQuery();
            connect.Close();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            label14.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString(); ////6
            string namePlanet = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString(); ////6
            string Mstring = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            string Rstring = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString();
            double Mdouble = Convert.ToDouble(Mstring); ///// 5
            double Rdouble = Convert.ToDouble(Rstring); ///// 4
            textBox6.Text = namePlanet;
            textBox5.Text = Mdouble.ToString();
            textBox4.Text = Rdouble.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\ПРОГРАМЫ\PLANETS\PLANETS.MDF;Integrated Security=True");
            string upsql = "UPDATE Planets SET name_planet = @name, M = @m, R = @r WHERE id = @id";
            SqlCommand commandUP = new SqlCommand(upsql, connect);
            connect.Open();
            commandUP.Parameters.AddWithValue("id", label14.Text);
            commandUP.Parameters.AddWithValue("name", textBox6.Text);
            commandUP.Parameters.AddWithValue("m", textBox5.Text);
            commandUP.Parameters.AddWithValue("r", textBox4.Text);
            commandUP.ExecuteNonQuery();
            textBox6.Clear();
            textBox5.Clear();
            textBox4.Clear();
            connect.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label_G.Text = Gplanets.ToString();
        }
    }
}
