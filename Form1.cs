using BSCS3_Prelim;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BSCS3Prelim
{
    public partial class Form1 : Form
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;Database=bscs3db;User=root;Password='';");
        private const string ConnectionString = "Server=localhost;Database=bscs3db;Uid=root";
        private System.ComponentModel.BindingList<StudentData> studentList = new System.ComponentModel.BindingList<StudentData>();

        public Form1()
        {
            InitializeComponent();
            LoadDataToDataGridView();

        }
        private void LoadDataToDataGridView()
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM studentstbl";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dataGridView1.Rows.Add(
                        row["StudentID"],
                        row["Name"],
                        row["Age"],
                        row["DateRegistered"],
                        row["IsSingle"]
                    );

                    object isSingleValue = row["IsSingle"];
                    if (isSingleValue != DBNull.Value && isSingleValue != null)
                    {
                        if (isSingleValue is int intValue)
                        {
                            dataGridView1.Rows[rowIndex].Cells["IsSingle"].Value = (intValue == 1);
                        }
                        else if (isSingleValue is bool boolValue)
                        {
                            dataGridView1.Rows[rowIndex].Cells["IsSingle"].Value = boolValue;
                        }
                        else
                        {
                            // Handle the case where the data type is not int or bool
                        }
                    }
                    else
                    {
                        // Handle the case where the value is DBNull or null
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.Equals("Save"))
            {
                try
                {
                    string name = txtName.Text.Trim();
                    if (string.IsNullOrEmpty(name))
                    {
                        MessageBox.Show(" ");
                        return;
                    }

                    if (!int.TryParse(txtAge.Text, out int age))
                    {
                        MessageBox.Show(" insert correctly.");
                        return;
                    }

                    string query = "INSERT INTO studentstbl (Name, Age ,IsSingle) VALUES (@Name, @Age, @IsSingle)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Age", age);
                        cmd.Parameters.AddWithValue("@IsSingle", cbIsSingle.Checked);

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            connection.Close();
                            MessageBox.Show("insert success!");

                            LoadDataToDataGridView();

                            txtName.Text = "";
                            txtAge.Text = "";
                            cbIsSingle.Checked = false;
                        }
                        else
                        {
                            MessageBox.Show("error!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            if (button1.Text.Equals("Update"))
            {
                string NewQuery = "UPDATE studentstbl SET Name = @Name, Age = @Age, IsSingle = @IsSingle WHERE StudentID = @StudentID";
                using (MySqlCommand cmd = new MySqlCommand(NewQuery, connection))
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                        object studentid = selectedRow.Cells["Column1"].Value;
                        int newstudentid = Convert.ToInt32(studentid);
                        object name = selectedRow.Cells["Column2"].Value;
                        object age = selectedRow.Cells["Column3"].Value;
                        object issingle = selectedRow.Cells["IsSingle"].Value;

                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                        if (cbIsSingle.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@IsSingle", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@IsSingle", 0);
                        }

                        cmd.Parameters.AddWithValue("@StudentID", newstudentid);

                        try
                        {
                            connection.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                connection.Close();
                                MessageBox.Show("Update successful!");
                                LoadDataToDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("No rows updated. StudentID not found or values are the same.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error updating record: " + ex.Message);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Row header is clicked
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                // Now you can access data from the selected row if needed
                // For example, if you have a column named "ColumnName", you can get its value like this:
                object studentid = selectedRow.Cells["Column1"].Value;
                object name = selectedRow.Cells["Column2"].Value;
                object age = selectedRow.Cells["Column3"].Value;
                object dateregistered = selectedRow.Cells["Column4"].Value;
                object issingle = selectedRow.Cells["IsSingle"].Value;
                // Do something with the cellValue
                txtBoxID.Text = studentid.ToString();
                txtName.Text = name.ToString();
                txtAge.Text = age.ToString();
                if (issingle.ToString().Equals("True"))
                {
                    cbIsSingle.Checked = true;
                }
                else
                {
                    cbIsSingle.Checked = false;
                }

                button1.Text = "Update";
            }
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {

        }

        private void DeleteStudent(int studentId)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    con.Open();
                    using (MySqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "DELETE FROM studentstbl WHERE StudentID = @studentId";
                        com.Parameters.AddWithValue("@studentId", studentId);

                        com.ExecuteNonQuery();
                        LoadDataToDataGridView();


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting student: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;

                foreach (DataGridViewRow row in selectedRows)
                {
                    int studentID = Convert.ToInt32(row.Cells["Column1"].Value);
                    string name = row.Cells["Column2"].Value.ToString();
                    int age = Convert.ToInt32(row.Cells["Column3"].Value);
                    DateTime date = (DateTime)row.Cells["Column4"].Value;
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // User clicked Yes, proceed with the update
                        DeleteStudent(studentID);
                        MessageBox.Show("Deleted: Student ID: " + studentID.ToString());
                    }
                    else
                    {
                        // User clicked No or closed the dialog, do nothing or provide feedback
                    }
                }

                //DeleteStudent(selectedStudent);
                // studentList.Remove(selectedStudent);
                //MessageBox.Show("Student deleted Successfully!");
            }
            else
            {
                MessageBox.Show("Please select a student to delete.");
            }
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            txtBoxID.Text = "";
            cbIsSingle.Checked = false;
            txtAge.Text = "";
            txtName.Text = "";
            button1.Text = "Save";
            dataGridView1.ClearSelection();
        }

        private void cbIsSingle_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtBoxID_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            Zen.Barcode.CodeQrBarcodeDraw qrBarCode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            pictureBox1.Image = qrBarCode.Draw(txtBoxID.Text, 200);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtAge_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Create an instance of the Attendance form
            Attendance attendanceForm = new Attendance();

            // Show the Attendance form
            attendanceForm.Show();

            // Hide the current form (optional)
            this.Hide();

        }
    }

    public class StudentData
    {
        public int studentID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateRegistered { get; set; }
        public string IsSingle { get; set; }

        public StudentData(int StudentID, string name, int age, DateTime dateRegistered, string isSingle)
        {
            studentID = StudentID;
            Name = name;
            Age = age;
            DateRegistered = dateRegistered;
            IsSingle = isSingle;
        }
    }
}

