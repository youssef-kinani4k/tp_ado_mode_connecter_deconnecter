using System.Data.SqlClient;
using System;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace TP6
{
    public partial class Form1 : Form
    {


        int id;
        SqlConnection oCon;

        public Form1()
        {
            try
            {
                oCon = new SqlConnection(@"Data Source=THINKCENTER\SQLEXPRESS;Initial Catalog=GestionClients;Integrated Security=True");

            }
            catch (Exception ex)
            {
                MessageBox.Show("erro de cnx");
            }

            InitializeComponent();
            afficher();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            oCon.Open();
            string query = "UPDATE clients " +
                           "SET nom = @nom, " +
                           "prenom = @prenom, " +
                           "dateNaissance = @dateNaissance, " +
                           "adresse = @adresse, " +
                           "email = @email, " +
                           "telephone = @telephone " +
                           "WHERE id = @id";
            SqlCommand cmd = new SqlCommand(query, oCon);
            cmd.Parameters.AddWithValue("@nom", textBox1.Text);
            cmd.Parameters.AddWithValue("@prenom", textBox2.Text);
            cmd.Parameters.AddWithValue("@dateNaissance", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@adresse", textBox3.Text.ToLower());
            cmd.Parameters.AddWithValue("@email", textBox4.Text.ToLower());
            cmd.Parameters.AddWithValue("@telephone", textBox5.Text.ToLower());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            oCon.Close();
            afficher();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void afficher()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("Nom", "Nom");
            dataGridView1.Columns.Add("Prenom", "Prénom");
            dataGridView1.Columns.Add("DateNaissance", "Date de Naissance");
            dataGridView1.Columns.Add("Adresse", "Adresse");
            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns.Add("Telephone", "Téléphone");
            oCon.Open();
            SqlCommand cmd = new SqlCommand("select * from clients", oCon);
            SqlDataReader red = cmd.ExecuteReader();
            while (red.Read())
            {
                dataGridView1.Rows.Add(
                                red["id"].ToString(),
                                red["nom"].ToString(),
                                red["prenom"].ToString(),
                                Convert.ToDateTime(red["dateNaissance"]).ToShortDateString(),
                                red["adresse"].ToString(),
                                red["email"].ToString(),
                                red["telephone"].ToString()
                            );

            }
            oCon.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nom1 = textBox1.Text.ToString();
            string prenom1 = textBox2.Text.ToString();
            string adresse1 = textBox3.Text.ToString();
            string date1 = dateTimePicker1.Value.ToShortDateString();
            string email1 = textBox4.Text.ToString();
            string phone1 = textBox5.Text.ToString();
            if (nom1.Equals("") || prenom1.Equals("") || adresse1.Equals("") || email1.Equals("") || phone1.Equals("")) MessageBox.Show("insrer tout les champ");
            else
            {
                oCon.Open();
                string query = "INSERT INTO clients (nom, prenom, dateNaissance, adresse, email, telephone) " +
                                           "VALUES (@Nom, @Prenom, @DateNaissance, @Adresse, @Email, @Telephone)";

                SqlCommand cmd = new SqlCommand(query, oCon);

                cmd.Parameters.AddWithValue("@Nom", nom1);
                cmd.Parameters.AddWithValue("@Prenom", prenom1);
                cmd.Parameters.AddWithValue("@DateNaissance", date1);
                cmd.Parameters.AddWithValue("@Adresse", adresse1);
                cmd.Parameters.AddWithValue("@Email", email1);
                cmd.Parameters.AddWithValue("@Telephone", phone1);
                cmd.ExecuteNonQuery();
                oCon.Close();
                afficher();
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";
            }


        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView1.SelectedRows[0];
                id = int.Parse(dataGridViewRow.Cells["id"].Value.ToString());
                textBox1.Text = dataGridViewRow.Cells["Nom"].Value.ToString();
                textBox2.Text = dataGridViewRow.Cells["Prenom"].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridViewRow.Cells["DateNaissance"].Value);
                textBox3.Text = dataGridViewRow.Cells["Adresse"].Value.ToString();
                textBox4.Text = dataGridViewRow.Cells["Email"].Value.ToString();
                textBox5.Text = dataGridViewRow.Cells["Telephone"].Value.ToString();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            oCon.Open();
            string query = "DELETE FROM clients where id=@id";
            SqlCommand cmd = new SqlCommand(query, oCon);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
            oCon.Close();
            afficher();
        }
    }
}
