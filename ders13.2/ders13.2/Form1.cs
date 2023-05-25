using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using ders13._2_dll;

namespace ders13._2
{
    public partial class Form1 : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        DataTable dt = new DataTable();
        maashesapla hesapla = new maashesapla();
        public Form1()
        {
            InitializeComponent();
        }
        void baglanti()
        {
            dt.Clear();
            listView1.Items.Clear();
            con = new OleDbConnection("Provider=Microsoft.JET.Oledb.4.0; Data Source=C:\\Users\\halid\\source\\repos\\maasbodrosu.mdb");
            da = new OleDbDataAdapter("Select * from personel", con);
            da.Fill(dt);
            int count = dt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                listView1.Items.Add(dt.Rows[i]["personel_id"].ToString());
                listView1.Items[i].SubItems.Add(dt.Rows[i]["ad"].ToString());
                listView1.Items[i].SubItems.Add(dt.Rows[i]["soyad"].ToString());
                listView1.Items[i].SubItems.Add(dt.Rows[i]["medenidurum"].ToString());
                listView1.Items[i].SubItems.Add(dt.Rows[i]["cocuksayisi"].ToString());
                listView1.Items[i].SubItems.Add(dt.Rows[i]["engeldurum"].ToString());
                listView1.Items[i].SubItems.Add(dt.Rows[i]["brut"].ToString());
                listView1.Items[i].SubItems.Add(dt.Rows[i]["kesinti"].ToString());
                listView1.Items[i].SubItems.Add(dt.Rows[i]["net"].ToString());
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Personel ID");
            listView1.Columns.Add("Ad");
            listView1.Columns.Add("Soyad");
            listView1.Columns.Add("Medeni Durum");
            listView1.Columns.Add("Çocuk Sayısı");
            listView1.Columns.Add("Engel Durum");
            listView1.Columns.Add("Brüt Maaş");
            listView1.Columns.Add("Kesintiler");
            listView1.Columns.Add("Net Maaş");
            numericUpDown1.Enabled = false;
            comboBox2.Enabled = false;
            baglanti();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Bekar")
            {
                numericUpDown1.Enabled = false;
                numericUpDown1.Value = 0;
            }
            else if(comboBox1.SelectedItem.ToString() == "Evli")
            {
                numericUpDown1.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboBox2.Enabled = true;
            }
            else
            {   
                comboBox2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int brut = Convert.ToInt32(textBox3.Text);
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            if (checkBox1.Checked)
            {
                float kesinti = hesapla.kesintiler(brut, comboBox1.SelectedItem.ToString(), Convert.ToInt32(numericUpDown1.Value), (comboBox2.SelectedIndex+1));
                float net = brut - kesinti;
                cmd.CommandText = "insert into personel(ad,soyad,medenidurum,cocuksayisi,engeldurum,brut,kesinti,net) values('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.SelectedItem + "'," + numericUpDown1.Value + "," + (comboBox2.SelectedIndex + 1) + ","+brut+",'"+kesinti+"','"+net+"')";
            }
            else
            {
                float kesinti = hesapla.kesintiler(brut, comboBox1.SelectedItem.ToString(), Convert.ToInt32(numericUpDown1.Value), 0);
                float net = brut - kesinti;
                cmd.CommandText = "insert into personel(ad,soyad,medenidurum,cocuksayisi,engeldurum,brut,kesinti,net) values('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.SelectedItem + "'," + numericUpDown1.Value + "," + (0) + "," + brut + ",'" + kesinti + "','" + net + "')";
            }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            baglanti();
        }
    }
}
