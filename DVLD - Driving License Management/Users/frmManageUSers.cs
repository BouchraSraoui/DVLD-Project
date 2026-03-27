using DVLD___Driving_License_Management.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayerDVLD;
namespace DVLD___Driving_License_Management.Users
{
    public partial class frmManageUSers : Form
    {
        public frmManageUSers()
        {
            InitializeComponent();

            ctrlFilterBy1.Filter += _Filter;
        }

        private void _Filter(string Filter, object Field)
        {
            if (Filter == "None")
            {
                _ListUsers();
                return;
            }
            

            dataGridView1.DataSource = clsUser.Filter(Filter, Field);

        }

        private void _ListUsers()
        {
            DataTable Users = clsUser.GetAllUsers();
            dataGridView1.DataSource = Users;
        }

        private clsUser _GetSelectedUser()
        {
            int UserID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            return clsUser.GetUserByID(UserID);
        }

        private void _ShowAddUpdateform(int UserID = -1 )
        {
            frmAddUpdateUser frmAddUpdateUser = new frmAddUpdateUser(UserID);

            frmAddUpdateUser.ShowDialog();

            _ListUsers();
        }

        private bool CanDeleteUser(clsUser user)
        {
            if (user.UserID == clsSessionInfo.User.UserID)
            {
                MessageBox.Show("You cannot delete your own account.",
                               "Your Account??",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                return false;
            }
            if (user.HasData())
            {
                MessageBox.Show("This User relatd with data",
                               "User has Data??",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void _DeleteUser()
        {
            int UserID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsUser user = clsUser.GetUserByID(UserID);

            if(user == null)
            {
                MessageBox.Show("This User Not Found",
                                "User Exist??",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            if (!CanDeleteUser(user))
                return;
            

            if (MessageBox.Show("Are You sure \n you want to delete this user ???",
                "Confirm",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation) != DialogResult.OK)
                return;


            if(user.Delete())
            {
                MessageBox.Show("This User Deleted Successfully",
                              "User Deleted",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);

                _ListUsers();
                return;
            }

            MessageBox.Show("Faild",
                              "User has Data??",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);



        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageUSers_Load(object sender, EventArgs e)
        {
            _ListUsers();

            ctrlFilterBy1.FilterBy = new Dictionary<string, string>
                                                     { {"None","" },
                                                       {"Person ID","int" },
                                                       {"User ID","int" },
                                                       {"User Name","string" },
                                                       { "Is Active","bool" }
                                                     };

            ctrlFilterBy1.InitializeControl();


            lblNumberOfRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        { 
            _ShowAddUpdateform();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ShowAddUpdateform();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            _ShowAddUpdateform(UserID);

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_Password change_Password = new Change_Password(_GetSelectedUser());
            change_Password.ShowDialog();
        }

        private void showDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            frmUserDetails frmUserDetails = new frmUserDetails(UserID);
            frmUserDetails.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DeleteUser();    
        }
    }
}
