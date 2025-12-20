using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using TeamListForm;

namespace TourApp
{

    public partial class CreaTourForm : System.Windows.Forms.Form
    {
        private int? _tournamentId = null;
        public int CreatedTournamentId { get; private set; } = -1;
        public CreaTourForm(int? id = null)
        {
            InitializeComponent();
            _tournamentId = id;

            if (_tournamentId.HasValue)
            {
                this.Text = "Update Tournament";
                createBtn.Text = "Save Changes"; 
                LoadDataForEdit();
            }
        }
        private void LoadDataForEdit()
        {
            DatabaseHelper db = new DatabaseHelper();
            // Đảm bảo _tournamentId có giá trị
            DataRow row = db.GetTournamentById(_tournamentId.Value);

            if (row != null)
            {
                // Load thông tin cơ bản
                nameTextBox.Text = row["NAME"].ToString();

                // Load Sport an toàn (tránh lỗi nếu item không có trong list)
                string sportDB = row["SPORT"].ToString();
                if (sportCbox.Items.Contains(sportDB)) sportCbox.SelectedItem = sportDB;

                // Set Max trước khi set Value để tránh lỗi 123 > 100
                numPar.Minimum = 2;
                numPar.Maximum = 1000;
                numPar.Value = Convert.ToInt32(row["TEAM_COUNT"]);

                startDate.Value = Convert.ToDateTime(row["STARTDATE"]);
                prizeTextBox.Text = row["PRIZE"].ToString();

                // --- LOAD FORMAT (QUAN TRỌNG) ---
                string mode = row["FormatMode"] != DBNull.Value ? row["FormatMode"].ToString() : "Single";
                string s1 = row["Stage1Format"] != DBNull.Value ? row["Stage1Format"].ToString() : "";
                string s2 = row["Stage2Format"] != DBNull.Value ? row["Stage2Format"].ToString() : "";

                if (mode == "Single")
                {
                    singleRad.Checked = true; // Sự kiện CheckedChanged sẽ tự bật comboBox2
                    if (comboBox2.Items.Contains(s1)) comboBox2.SelectedItem = s1;
                }
                else if (mode == "Multi")
                {
                    multiRad.Checked = true; // Sự kiện CheckedChanged sẽ tự bật comboBox3, 4
                    if (comboBox3.Items.Contains(s1)) comboBox3.SelectedItem = s1;
                    if (comboBox4.Items.Contains(s2)) comboBox4.SelectedItem = s2;
                }
            }
        }
        public CreaTourForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void singleRad_CheckedChanged(object sender, EventArgs e)
        {
            if (singleRad.Checked)
            {
                comboBox2.Enabled = true;
                comboBox3.Enabled = false;
                comboBox3.SelectedIndex = -1;
                comboBox4.Enabled = false;
                comboBox4.SelectedIndex = -1;
            }
        }

        private void multiRad_CheckedChanged(object sender, EventArgs e)
        {
            if (multiRad.Checked)
            {
                comboBox2.Enabled = false;
                comboBox2.SelectedIndex = -1;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            // --- 1. VALIDATION CƠ BẢN ---
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show("Please enter a tournament name.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return;
            }
            if (sportCbox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a sport type.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (numPar.Value < 2 || numPar.Value > 256)
            {
                MessageBox.Show("Participants must be between 2 and 256.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- 2. LẤY DỮ LIỆU FORMAT (QUAN TRỌNG) ---
            string mode = "";
            string s1Format = "";
            string s2Format = null;

            if (singleRad.Checked)
            {
                // Kiểm tra chọn Single
                if (comboBox2.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a format for the Single Stage!", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBox2.Focus();
                    return;
                }
                mode = "Single";
                s1Format = comboBox2.SelectedItem.ToString();
            }
            else if (multiRad.Checked)
            {
                // Kiểm tra chọn Multi
                if (comboBox3.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a format for Stage 1!", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBox3.Focus();
                    return;
                }
                if (comboBox4.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a format for the Final Stage!", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBox4.Focus();
                    return;
                }
                mode = "Multi";
                s1Format = comboBox3.SelectedItem.ToString();
                s2Format = comboBox4.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Please select a tournament format type!", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- 3. LẤY DỮ LIỆU CHUNG ---
            string name = nameTextBox.Text.Trim();
            string sport = sportCbox.SelectedItem.ToString();
            int teamCount = (int)numPar.Value;
            DateTime date = startDate.Value;
            string prize = prizeTextBox.Text.Trim();
            string location = null;
            string posterPath = null;

            DatabaseHelper db = new DatabaseHelper();
            bool isSuccess = false;

            // --- 4. GỌI DB HELPER (ĐÃ HẾT LỖI) ---
            if (_tournamentId.HasValue)
            {
                // UPDATE (Truyền thêm mode, s1, s2)
                isSuccess = db.UpdateTournament(_tournamentId.Value, name, location, date, prize, posterPath, sport, teamCount, mode, s1Format, s2Format);
                this.CreatedTournamentId = _tournamentId.Value;
            }
            else
            {
                // ADD (Truyền thêm mode, s1, s2)
                int newId = db.AddTournament(name, location, date, prize, posterPath, sport, teamCount, mode, s1Format, s2Format);
                if (newId != -1)
                {
                    isSuccess = true;
                    this.CreatedTournamentId = newId;
                }
            }

            // --- 5. KẾT THÚC ---
            if (isSuccess)
            {
                string msg = _tournamentId.HasValue ? "Update successfully!" : "Create successfully!";
                MessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("An error occurred. Please try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numPar_ValueChanged(object sender, EventArgs e)
        {
            numPar.Minimum = 2;
            numPar.Maximum = 256;
        }

        private void LogOutBtn_Click(object sender, EventArgs e)
        {
            ContextMenuStrip myMenu = Account;
            myMenu.Show(LogOutBtn, 0, LogOutBtn.Height);
        }

        private void CreaTourForm_Load(object sender, EventArgs e)
        {
            singleRad.Checked = true;
            sportCbox.Text = "Football";
        }

        private void starTime_ValueChanged(object sender, EventArgs e)
        {
            startDate.CustomFormat = "dd/MM/yyyy";
        }

        private void Account_Opening(object sender, CancelEventArgs e)
        {

        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm homeform = new LoginForm();
            this.Hide();
            homeform.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
