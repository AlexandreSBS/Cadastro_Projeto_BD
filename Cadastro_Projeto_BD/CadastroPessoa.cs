using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Cadastro_Projeto_BD {
    public partial class frmPessoa : Form {


        public frmPessoa() {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }


        private void bntConcluir_Click(object sender, EventArgs e) {

            DateTime data = DateTime.Parse(mtxtData.Text);

            string connectionString = @"Data Source=AS-NOTEBOOK\AS_BD;Initial Catalog=agile_market;Integrated Security=True";

            SqlConnection conn = new SqlConnection(connectionString);

            string sql = "INSERT INTO Pessoa (nome, cpf, dt_nasc,sexo,email,rg_numero,rg_uf,rg_org_emi) " +
                         "VALUES (@nome, @cpf, @dt_nasc,@sexo,@email,@rg_numero,@rg_uf,@rg_org_emi)";

            try {
                SqlCommand C = new SqlCommand(sql, conn);

                C.Parameters.Add(new SqlParameter("@nome", this.txtNome.Text));
                C.Parameters.Add(new SqlParameter("@cpf", this.mtxtCpf.Text));
                C.Parameters.Add(new SqlParameter("@dt_nasc", data));
                C.Parameters.Add(new SqlParameter("@sexo", this.cmbSexo.Text));
                C.Parameters.Add(new SqlParameter("@email", this.txtEmail.Text));
                C.Parameters.Add(new SqlParameter("@rg_numero", this.mtxtRg.Text));
                C.Parameters.Add(new SqlParameter("@rg_uf", this.cmbUf.Text));
                C.Parameters.Add(new SqlParameter("@rg_org_emi", this.txtOrgEmis.Text));

                conn.Open();

                C.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Cadastro realizado com sucesso!");

                txtNome.Text = "";
                mtxtCpf.Text = "";
                mtxtData.Text = "";
                cmbSexo.Text = "";
                txtEmail.Text = "";
                mtxtRg.Text = "";
                cmbUf.Text = "";
                txtOrgEmis.Text = "";
                lblValidaCpf.Text = "";
            }
            catch (SqlException ex) {
                MessageBox.Show("ERROR: " + ex.Message);
            }
            catch (Exception ex) {
                MessageBox.Show("ERRO: " + ex.Message);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) {
        }
        private void MtxtData_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) {
        }

        private void MtxtCpf_TextChanged(object sender, EventArgs e) {
            if (mtxtCpf.Text.Length == 11) {
                try {
                    string cpf, cpfFinal, digitos;
                    int dig1 = 0, dig2 = 0, soma = 0;

                    cpf = mtxtCpf.Text;
                    cpfFinal = cpf;
                    cpf = cpfFinal.Substring(0, 9);

                    for (int i = 0; i < 9; i++) {
                        digitos = cpf.Substring(i, 1);
                        soma = (int.Parse(digitos) * (10 - i));
                        dig1 += soma;
                    }
                    dig1 = dig1 % 11;

                    if (dig1 < 2) {
                        dig1 = 0;
                    }
                    else {
                        dig1 = 11 - dig1;
                    }

                    cpf += Convert.ToString(dig1);

                    for (int i = 0; i < 10; i++) {
                        digitos = (cpf.Substring(i, 1));
                        soma = (int.Parse(digitos) * (11 - i));
                        dig2 += soma;
                    }
                    dig2 = dig2 % 11;

                    if (dig2 < 2) {
                        dig2 = 0;
                    }
                    else {
                        dig2 = 11 - dig2;
                    }

                    cpf += Convert.ToString(dig2);

                    if (cpfFinal == cpf) {
                        lblValidaCpf.ForeColor = Color.Green;
                        lblValidaCpf.Text = "CPF Válido";
                        bntConcluir.Enabled = true;
                    }

                    else {
                        lblValidaCpf.ForeColor = Color.Red;
                        lblValidaCpf.Text = "CPF Inválido ";
                        bntConcluir.Enabled = false;
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show("ERRO: " + ex.Message);
                    mtxtCpf.Text = "";
                }
            }
        }

        private void FrmPessoa_Load(object sender, EventArgs e) {

        }
    }
}
