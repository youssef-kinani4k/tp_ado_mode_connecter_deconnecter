using System.Data.SqlClient;
using System;
using System.Data; 
using System.Windows.Forms;

namespace tp7
{
    public partial class Form1 : Form
    {
        private SqlConnection oCon;
        private SqlDataAdapter adapter;
        private int id;
        private DataTable clientTable;

        public Form1()
        {
            InitializeComponent();

            try
            {
                oCon = new SqlConnection(@"Data Source=THINKCENTER\SQLEXPRESS;Initial Catalog=GestionClients;Integrated Security=True");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connexion : " + ex.Message);
            }

            LoadData(); 
        }

        private void LoadData()
        {
            try
            {
                clientTable = new DataTable();

                string query = "SELECT * FROM clients";
                adapter = new SqlDataAdapter(query, oCon);

                
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                adapter.Fill(clientTable); 
                dataGridView1.DataSource = clientTable; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des données : " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) ||
                    string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) ||
                    string.IsNullOrEmpty(textBox5.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs.");
                    return;
                }

                DataRow newRow = clientTable.NewRow();
                newRow["nom"] = textBox1.Text;
                newRow["prenom"] = textBox2.Text;
                newRow["dateNaissance"] = dateTimePicker1.Value;
                newRow["adresse"] = textBox3.Text;
                newRow["email"] = textBox4.Text;
                newRow["telephone"] = textBox5.Text;
                clientTable.Rows.Add(newRow); 

                adapter.Update(clientTable);

                MessageBox.Show("Client ajouté avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout : " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    int index = selectedRow.Index;

                    clientTable.Rows[index]["nom"] = textBox1.Text;
                    clientTable.Rows[index]["prenom"] = textBox2.Text;
                    clientTable.Rows[index]["dateNaissance"] = dateTimePicker1.Value;
                    clientTable.Rows[index]["adresse"] = textBox3.Text;
                    clientTable.Rows[index]["email"] = textBox4.Text;
                    clientTable.Rows[index]["telephone"] = textBox5.Text;

                    adapter.Update(clientTable);
                    MessageBox.Show("Client mis à jour avec succès !");
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner un client.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la mise à jour : " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    int index = selectedRow.Index;

                    clientTable.Rows[index].Delete();

                    adapter.Update(clientTable);
                    MessageBox.Show("Client supprimé avec succès !");
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner un client.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression : " + ex.Message);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    textBox1.Text = selectedRow.Cells["nom"].Value.ToString();
                    textBox2.Text = selectedRow.Cells["prenom"].Value.ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(selectedRow.Cells["dateNaissance"].Value);
                    textBox3.Text = selectedRow.Cells["adresse"].Value.ToString();
                    textBox4.Text = selectedRow.Cells["email"].Value.ToString();
                    textBox5.Text = selectedRow.Cells["telephone"].Value.ToString();

                    id = Convert.ToInt32(selectedRow.Cells["id"].Value); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la sélection : " + ex.Message);
            }
        }
    }
}
