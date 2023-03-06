using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test2_conexiune
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Left = this.Width / 2;
            label1.Top = this.Height / 6;

            button1.Left = this.Width / 2 + 323;
            button1.Top = this.Height / 2 + this.Height / 4 + 100;

            button2.Left = this.Width / 2;
            button2.Top = this.Height / 4 + 20;

            button3.Left = this.Width / 2 + 323;
            button3.Top = this.Height / 4 + 20;

            button4.Left = this.Width / 2 + 323;
            button4.Top = this.Height / 2 + 60;

            button5.Left = this.Width / 2;
            button5.Top = this.Height / 2 + 60;

            button6.Left = this.Width / 2;
            button6.Top = this.Height / 2 + this.Height / 4 + 100;
        }

        bool logare_ok = false;
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetFullPath(executable));
            path = path.Replace("\\Test2_conexiune.exe", "");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            //string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = |DataDirectory|\BazaDeDate.accdb;Persist Security Info=True";
            string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + path + "\\BazaDeDate.accdb;Persist Security Info=True";
            OleDbConnection con = new OleDbConnection(connection);
            con.Open();
            MessageBox.Show("Conexiune reusita");*/
            Application.Exit();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (logare_ok == false)
            {
                string username = " ";
                string var = "";
                if (InputBox("Logare", "Introduceti username-ul:", ref var) == DialogResult.OK)
                    username = var;

                string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string path = (System.IO.Path.GetFullPath(executable));
                path = path.Replace("\\Test2_conexiune.exe", "");
                AppDomain.CurrentDomain.SetData("DataDirectory", path);
                //string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = |DataDirectory|\BazaDeDate.accdb;Persist Security Info=True";
                string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + path + "\\BazaDeDate.accdb;Persist Security Info=True";
                

                OleDbConnection con = new OleDbConnection(connection);
                string query = "SELECT * FROM Users";
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                OleDbDataReader reader = cmd.ExecuteReader();
                bool ok = false;
                while (reader.Read())
                {
                    if (reader.GetString(1) == username)
                    {
                        do
                        {
                            string parola = "";
                            string val = "";
                            if (InputBox("Logare", "Introduceti parola:", ref val) == DialogResult.OK)
                                parola = val;
                            if (parola == reader.GetString(2))
                                ok = true;
                            else MessageBox.Show("Parola gresita!");
                        } while (ok == false);
                    }
                }
                if (ok == true)
                {
                    logare_ok = true;
                    MessageBox.Show("Logare reusita!");
                }
                else MessageBox.Show("Nu exista un cont cu acest username!");
            }
            else MessageBox.Show("Sunteti deja autentificat!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string titlu = "";
            string value = " ";
            if (InputBox("Titlu album", "Dati un Titlu de album pentru a verifica", ref value) == DialogResult.OK)
                titlu = value;

            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetFullPath(executable));
            path = path.Replace("\\Test2_conexiune.exe", "");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            //string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = |DataDirectory|\BazaDeDate.accdb;Persist Security Info=True";
            string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + path + "\\BazaDeDate.accdb;Persist Security Info=True";


            String query = "SELECT Inventar.Titlu , Inventar.NrBucati FROM Inventar";
            OleDbConnection con = new OleDbConnection(connection);
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            OleDbDataReader reader = cmd.ExecuteReader();
            string output = "";
            while (reader.Read())
            {

                if (reader.GetString(0) == titlu)
                {
                    if (reader.GetInt32(1) > 0)
                    {
                        output += reader.GetInt32(1);
                    }
                }

            }

            if (output == "")
            {
                MessageBox.Show("Acest Album nu este in magazinul nostru!");
            }
            else
                MessageBox.Show("Mai sunt " + output + " de bucati disponibile!");
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (logare_ok == true)
            {
                string titlu = "";
                string value = " ";
                if (InputBox("Titlu album", "Dati un Titlu de album pentru a cumpara", ref value) == DialogResult.OK)
                    titlu = value;

                string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string path = (System.IO.Path.GetFullPath(executable));
                path = path.Replace("\\Test2_conexiune.exe", "");
                AppDomain.CurrentDomain.SetData("DataDirectory", path);
                //string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = |DataDirectory|\BazaDeDate.accdb;Persist Security Info=True";
                string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + path + "\\BazaDeDate.accdb;Persist Security Info=True";


                String query = "UPDATE Inventar Set NrBucati = NrBucati -1 WHERE Titlu = @t ";
                OleDbConnection con = new OleDbConnection(connection);
                con.Open();
                OleDbDataAdapter update = new OleDbDataAdapter();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@t", titlu);
                update.InsertCommand = cmd;
                update.InsertCommand.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Cumparare Reusita");
            }
            else
                MessageBox.Show("Trebuie sa va logati pentru a cumpara un articol!");
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
