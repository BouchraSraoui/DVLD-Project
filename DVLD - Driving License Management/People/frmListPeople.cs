using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayerDVLD;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLD___Driving_License_Management.Persons
{
    public partial class ListPeople : Form
    {
        public ListPeople()
        {
            InitializeComponent();

            ctrlFilterBy2.Filter += _Filter;
        }

        //pagination, and proper indexing from the beginning. This ensures good performance, scalability, and maintainability.
        private void _ListAllPeople()
        {
            DataTable dtPeople = clsPerson.GetAllPeople();

            dataGridView1.DataSource = dtPeople;
        }

        private int GetSelectedPersonID()
        {
            int PersonID = -1;
            if (dataGridView1.CurrentRow != null)
            {
                PersonID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            }
            return PersonID;
        }

        private void _ShowAddUpdatePage(clsPerson Person = null)
        {
            frmADDUpdatePerson frmADDUpdatePerson = new frmADDUpdatePerson(Person);

            frmADDUpdatePerson.IsNewPersonSaved += _ListAllPeople;

            frmADDUpdatePerson.ShowDialog();   
        }

       // the number of records can increase over time, so it is better to use database filtering
        private void _Filter(string Filter, object Field)
        {
            if(Filter == "None")
            {
                _ListAllPeople();
                return;
            }

            if(Filter == "Nationality")
            {
                Filter = "NationalityCountryID";
                Field = Convert.ToInt32(clsCountry.GetCountryID(Field.ToString()));

                if (Convert.ToInt32(Field) == -1)
                    return;
            }
            dataGridView1.DataSource = clsPerson.Filter(Filter, Field);

        }

        
           
        private clsPerson _GetSelectedPerson()
        {
            int personId = GetSelectedPersonID();
            if (personId <= 0)
            {
                MessageBox.Show("Please select a person first.");
                return null;
            }

            clsPerson person = clsPerson.FindPersonByID(personId);
            if (person == null)
            {
                MessageBox.Show("Person Not Found!");
                
            }

            return person;

        }
        private bool _DeletePerson(int PersonID)
        {
            clsPerson Person = clsPerson.FindPersonByID(PersonID);
            if (Person == null)
                return false;

            if( MessageBox.Show("Are You Sure You Want To Delete Peroson With Id =" + PersonID + "?",
                             "Delete Person", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
            
               if (Person.HasRelatedWithData())
               {
                   MessageBox.Show("Person is Related with data ");
                   return false;
               }
                 
            
               if (Person.DeletePerson())
               {
                   MessageBox.Show("Success", "Delete Person");
                   return true;
               }
                
            }
            return false;

        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {


            _ListAllPeople();

            ctrlFilterBy2.FilterBy = new Dictionary<string, string>
                                                     {{"None","" },
                                                      {"PersonID","int" },
                                                      {"NationalNo","string" },
                                                      {"FirstName","string" },
                                                      {"LastName","string" },
                                                      {"ThirdName","string" },
                                                      {"SecondName","string" },
                                                      {"Gendor","string" },
                                                      {"Nationality","string" },
                                                      {"Phone","string" },
                                                      { "Email","string" }
                                                                  };

            ctrlFilterBy2.InitializeControl();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            _ShowAddUpdatePage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ShowAddUpdatePage();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson Person = clsPerson.FindPersonByID(GetSelectedPersonID());

            if (Person != null)
                _ShowAddUpdatePage(Person);

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = GetSelectedPersonID();
            if (_DeletePerson(PersonID))
                _ListAllPeople();
           
        }

        private void showDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {

            clsPerson person = _GetSelectedPerson();
            if (person == null)
                return;

            frmPersonDetails frmPersonDetails = new frmPersonDetails(person);

            frmPersonDetails.ShowDialog();

            _ListAllPeople();
        }

    }
}
