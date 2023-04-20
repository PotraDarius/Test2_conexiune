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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Test2_conexiune
{
    public partial class Form1 : Form
    {
        bool logare_ok = false;
        bool logare_admin = false;
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

            this.button7.Visible = false;
            button7.Left = this.Width / 2;
            button7.Top = this.Height / 2 + this.Height / 4 + 200;

            this.button8.Visible = false;
            button8.Left = this.Width / 2 + 323;
            button8.Top = this.Height / 2 + this.Height / 4 + 200;

            button9.Top = this.Height / 4 +20;
            button9.Left = this.Width / 2 + 623;

            logare_ok = false;
            logare_admin = false;
        }

        
        string utilizator;
        public static DialogResult InputBox(string title, string promptText, ref string value,int opt)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            if(opt == 1)
                textBox.PasswordChar= '*';
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
            Application.Exit();
        }


        private void button2_Click(object sender, EventArgs e)
        {

                if (logare_ok == false && logare_admin == false)
                {
                    string username = " ";
                    string var = "";
                    if (InputBox("Logare", "Introduceti username-ul:", ref var,0) == DialogResult.OK)
                        username = var;

                    string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    executable = executable.Replace("\\bin\\Debug\\Test2_conexiune.exe", "\\Resurse\\BazaDeDate.accdb");
                    string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + executable + ";Persist Security Info=True";


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
                                if (InputBox("Logare", "Introduceti parola:", ref val,1) == DialogResult.OK)
                                    parola = val;
                                if (parola == reader.GetString(2))
                                {
                                    utilizator = username;
                                    ok = true;
                                }
                                else MessageBox.Show("Parola gresita!");
                            } while (ok == false);
                        }
                    }
                    if (ok == true)
                    {
                        if (username == "admin")
                        {
                            logare_admin = true;
                            MessageBox.Show("Logare de admin reusita!");
                            this.button7.Visible = true;
                            this.button8.Visible = true;
                        }
                        else
                        {
                            logare_ok = true;
                            MessageBox.Show("Logare reusita!");
                        }
                    }
                    else MessageBox.Show("Nu exista un cont cu acest username!");
                }
                else MessageBox.Show("Sunteti deja autentificat!");
            
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string titlu = "";
            string value = " ";
            if (InputBox("Titlu album", "Dati un Titlu de album pentru a verifica", ref value,0) == DialogResult.OK)
                titlu = value;

            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            executable = executable.Replace("\\bin\\Debug\\Test2_conexiune.exe", "\\Resurse\\BazaDeDate.accdb");
            string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + executable + ";Persist Security Info=True";

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
                    if (reader.GetInt16(1) > 0)
                    {
                        output += reader.GetInt16(1);
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
            if (logare_ok == true || logare_admin == true)
            {
                logare_ok = false;
                logare_admin = false;
                MessageBox.Show("Delogare reusita!");
                if(this.button7.Visible == true)
                    this.button7.Visible = false;
                if(this.button8.Visible == true)
                    this.button8.Visible = false;
            }
            else MessageBox.Show("Sunteti deja delogat!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (logare_ok == true)
            {
                string titlu = "";
                string value = " ";
                if (InputBox("Titlu album", "Dati un Titlu de album pentru a cumpara", ref value, 0) == DialogResult.OK)
                    titlu = value;

                string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
                executable = executable.Replace("\\bin\\Debug\\Test2_conexiune.exe", "\\Resurse\\BazaDeDate.accdb");
                string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + executable + ";Persist Security Info=True";


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
                MessageBox.Show("Trebuie sa va logati cu cont de utilizator pentru a cumpara un articol!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string opt = " ";
            string value = "";
            if (InputBox("Creare/Stergere Cont ", "Pentru creare cont tastati 1. Pentru stergere cont tastati 2:", ref value, 0) == DialogResult.OK)
                opt = value;
            if (opt == "1")
            {
                if (logare_ok == false)
                {
                    string username = "";
                    string parola = "";
                    value = "";

                    string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    executable = executable.Replace("\\bin\\Debug\\Test2_conexiune.exe", "\\Resurse\\BazaDeDate.accdb");
                    string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + executable + ";Persist Security Info=True";

                    OleDbConnection con = new OleDbConnection(connection);
                    con.Open();
                    bool ok = true;
                    do
                    {
                        ok = true;
                        if (InputBox("Creare Cont", "Introduceti username-ul dorit:", ref value, 0) == DialogResult.OK)
                            username = value;

                        string Query = "SELECT Users.Username FROM Users";
                        OleDbCommand comd = new OleDbCommand(Query, con);
                        OleDbDataReader reader = comd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.GetString(0) == username)
                                ok = false;
                        }
                        if (ok == false)
                        {
                            MessageBox.Show("Exista un cont cu acest username!");
                            value = "";
                        }

                    } while (ok == false);

                    value = "";

                    if (InputBox("Creare Cont", "Introduceti parola dorita:", ref value, 0) == DialogResult.OK)
                        parola = value;



                    string query = "INSERT INTO Users(Username,Parola) VALUES (@u, @p)";


                    OleDbDataAdapter cont = new OleDbDataAdapter();
                    OleDbCommand cmd = new OleDbCommand(query, con);

                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", parola);

                    cont.InsertCommand = cmd;

                    cont.InsertCommand.ExecuteNonQuery();

                    con.Close();

                    MessageBox.Show("Contul s-a creat!");
                    logare_ok = true;
                    utilizator = username;


                }
                else MessageBox.Show("Nu puteti crea un cont daca sunteti logat cu alt cont!");
            }
            else if (opt == "2")
            {
                if (logare_admin == false)
                {

                    if (logare_ok == false)
                        MessageBox.Show("Trebuie sa va logati pentru a sterge un cont!");
                    else if (logare_ok == true)
                    {
                        MessageBoxButtons buton = MessageBoxButtons.YesNo;
                        DialogResult result;
                        result = MessageBox.Show("Sunteti sigur ca doriti sa va stergeti contul?", "Stergere cont", buton);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
                            executable = executable.Replace("\\bin\\Debug\\Test2_conexiune.exe", "\\Resurse\\BazaDeDate.accdb");
                            string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + executable + ";Persist Security Info=True";

                            string query = "DELETE FROM Users WHERE Username = '" + utilizator + "'";

                            OleDbConnection con = new OleDbConnection(connection);
                            OleDbCommand cmd = new OleDbCommand();
                            con.Open();
                            cmd.CommandText = query;
                            cmd.Connection = con;
                            OleDbDataAdapter sterg = new OleDbDataAdapter();
                            sterg.DeleteCommand = cmd;
                            sterg.DeleteCommand.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Cont sters!");
                            logare_ok= false;
                            utilizator = "";
                        }
                    }
                }
                else if (logare_admin == true)
                {
                    string id = "";
                    value = "";
                    if (InputBox("Stergere cont", "Dati id-ul contului dorit:", ref value, 0) == DialogResult.OK)
                    {
                        id = value;
                    }

                    string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    executable = executable.Replace("\\bin\\Debug\\Test2_conexiune.exe", "\\Resurse\\BazaDeDate.accdb");
                    string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + executable + ";Persist Security Info=True";

                    string query = "DELETE FROM Users WHERE ID = " + id + "";

                    OleDbConnection con = new OleDbConnection(connection);
                    OleDbCommand cmd = new OleDbCommand();
                    con.Open();
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    OleDbDataAdapter sterg = new OleDbDataAdapter();
                    sterg.DeleteCommand = cmd;
                    sterg.DeleteCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Cont sters!");



                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string id = " ";
            string val = "";
            if (InputBox("Modificare articol", "Introduceti ID-ul articoluli dorit:", ref val, 0) == DialogResult.OK)
                id = val;
            val = "";

            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            executable = executable.Replace("\\bin\\Debug\\Test2_conexiune.exe", "\\Resurse\\BazaDeDate.accdb");
            string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + executable + ";Persist Security Info=True";

            OleDbConnection con = new OleDbConnection(connection);
            con.Open();
            string query = "SELECT * FROM INVENTAR";

            OleDbCommand cmd = new OleDbCommand(query, con);
            OleDbDataReader reader = cmd.ExecuteReader();

            bool ok = false;

            while (reader.Read())
            {
                if (reader.GetInt32(0) == Convert.ToInt32(id))
                {
                    ok = true;
                    string col = "";
                    string new_val = "";
                    if (InputBox("Modificare articol", "Ce doriti sa modificati la acest articol?", ref val, 0) == DialogResult.OK)
                        col = val;
                    val = "";
                    if (InputBox("Modificare articol", "Introduceti noua valoare:", ref val, 0) == DialogResult.OK)
                        new_val = val;
                    string Query = "UPDATE Inventar SET "+ col +" = '"+ new_val +"' WHERE Inventar.ID = "+ id +" ";
                    OleDbCommand cmd1 = new OleDbCommand();
                    OleDbDataAdapter mod = new OleDbDataAdapter();

                    cmd1.CommandText = Query;
                    cmd1.Connection = con;


                    mod.UpdateCommand = cmd1;
                    mod.UpdateCommand.ExecuteNonQuery();
                    MessageBox.Show("Modificare reusita!");
                }
                
            }
            if (ok == false)
                MessageBox.Show("Nu exista articol cu acest ID!");
            con.Close();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string opt = "";
            string val = "";
            if (InputBox("Modificare inventar", "Pentru adugare articol tastati 1. Pentru stergere articol tastati 2:", ref val, 0) == DialogResult.OK)
                opt = val;
            val = "";

            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            executable = executable.Replace("\\bin\\Debug\\Test2_conexiune.exe", "\\Resurse\\BazaDeDate.accdb");
            string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + executable + ";Persist Security Info=True";

            OleDbConnection con = new OleDbConnection(connection);
            con.Open();


            if (opt == "1")
            {
                string artist = "";
                string titlu = "";
                string genmuzical = "";
                string anlansare = "";
                string nrbucati = "";

                if (InputBox("Adaugare articol", "Dati artistul articolului:", ref val, 0) == DialogResult.OK)
                    artist = val;
                val = "";
                
                
                bool ok = false;
                do
                {
                    ok = false;
                    titlu = "";
                    if (InputBox("Adaugare articol", "Dati titlul articolului:", ref val, 0) == DialogResult.OK)
                        titlu = val;
                    val = "";

                    string q = "SELECT Titlu FROM Inventar";
                    OleDbCommand comd = new OleDbCommand(q, con);
                    OleDbDataReader read = comd.ExecuteReader();
                    while (read.Read())
                    {
                        if (read.GetString(0) == titlu)
                            ok = true;
                    }
                    if (ok == true)
                        MessageBox.Show("Exista deja acest album!");
                } while (ok == true);





                if (InputBox("Adaugare articol", "Dati genul muzical al articolului:", ref val  , 0) == DialogResult.OK)
                    genmuzical = val;
                val = "";

                if (InputBox("Adaugare articol", "Dati anul de lansare al articolului:", ref val, 0) == DialogResult.OK)
                    anlansare = val;
                val = "";

                if (InputBox("Adaugare articol", "Dati numarul de bucati al articolului:", ref val, 0) == DialogResult.OK)
                    nrbucati = val;
                val = "";
                string query = "INSERT INTO Inventar(Artist,Titlu,GenMuzical,AnLansare,NrBucati) VALUES('"+ artist +"', '" + titlu +"', '"+ genmuzical +"', '"+anlansare+"', '"+ nrbucati +"')";
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter adaug = new OleDbDataAdapter();
                adaug.InsertCommand = cmd;
                adaug.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Adaugare efectuata!");
            }
            else if(opt == "2")
            {
                string id = "";
                if (InputBox("Stergere articol", "Dati ID ul articolului dorit:", ref val, 0) == DialogResult.OK)
                    id = val;
                string query = "DELETE FROM Inventar WHERE Inventar.ID = "+ id +"";
                OleDbCommand cmd = new OleDbCommand(query,con);
                OleDbDataAdapter del = new OleDbDataAdapter();
                del.DeleteCommand = cmd;
                del.DeleteCommand.ExecuteNonQuery();
                MessageBox.Show("Stergere efectuata!");
            }

            con.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            executable = executable.Replace("\\bin\\Debug\\Test2_conexiune.exe", "\\Resurse\\BazaDeDate.accdb");
            string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + executable + ";Persist Security Info=True";

            OleDbConnection con = new OleDbConnection(connection);
            con.Open();

            string query = "SELECT * FROM Inventar";

            OleDbCommand cmd = new OleDbCommand(query,con);
            OleDbDataReader reader = cmd.ExecuteReader();

            string afis = "ID     Artist    Titlu   Gen Muzical   An Lansare    Nr Bucati \n\n";
            while (reader.Read())
            { 
                afis += " "+Convert.ToString(reader.GetInt32(0)) +"   " + reader.GetString(1) + "   " + reader.GetString(2) + "   " + reader.GetString(3) + "    " + Convert.ToString(reader.GetInt16(4)) + "    " + Convert.ToString(reader.GetInt16(5)) + "\n \n";
            }
            MessageBox.Show(afis);
        }
    }
}
