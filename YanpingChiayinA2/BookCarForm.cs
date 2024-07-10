using A2ClassLibrary;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net;

namespace YanpingChiayinA2
{
    public partial class BookCarForm : Form
    {
        public BookCarForm()
        {
            InitializeComponent();
        }

        private void btnBookAppointment_Click(object sender, EventArgs e)
        {
            labMessage.Text = string.Empty;
            bool IsError = false;
            //Verify customer Name
            string customerName = ValidationHelper.Capitalize(txtCustomerName.Text);
            if (string.IsNullOrWhiteSpace(customerName)) {
                labMessage.Text += "Please write customerName"+ Environment.NewLine;
                IsError=true;
            }

            // email and postal information 
            //If email is not provided, the postal information is required.
            //All four are fine, but not mandatory, when an email is provided.

            bool isPostalcode = ValidationHelper.IsValidPostalCode(txtPostalCode.Text);
            bool isProvinceCode =  ValidationHelper.IsValidProvinceCode(txtProvince.Text);
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!ValidationHelper.IsValidEmail(txtEmail.Text))
                {
                    labMessage.Text += "Please write correct email address" + Environment.NewLine;
                    IsError = true;
                }
                if (!string.IsNullOrWhiteSpace(txtPostalCode.Text) && !isPostalcode) {
                    labMessage.Text += "Please write correct Canadian postal pattern A2A 2A2" + Environment.NewLine;
                    IsError = true;
                }

                if (!string.IsNullOrWhiteSpace(txtProvince.Text) && !isProvinceCode)
                {
                    labMessage.Text += "Please write correct Canadian province or territory code" + Environment.NewLine;
                    IsError = true;
                }
            }
            else
            {
                if (!isPostalcode)
                {
                    labMessage.Text += "Please write correct Canadian postal pattern A2A 2A2" + Environment.NewLine;
                    IsError = true;
                }

                if (!isProvinceCode)
                {
                    labMessage.Text += "Please write correct Canadian province or territory code" + Environment.NewLine;
                    IsError = true;
                }
                if(string.IsNullOrWhiteSpace(txtAddress.Text)){
                    labMessage.Text += "Please write Address" + Environment.NewLine;
                    IsError = true;
                }

                if (string.IsNullOrWhiteSpace(txtCity.Text))
                {
                    labMessage.Text += "Please write City" + Environment.NewLine;
                    IsError = true;
                }
            }

            //validation Home Phone and cell phone
            bool isHomePhone = ValidationHelper.IsValidPhoneNumber(txtHomePhone.Text);
            bool isCellPhone = ValidationHelper.IsValidPhoneNumber(txtCellPhone.Text);
            if (!string.IsNullOrWhiteSpace(txtHomePhone.Text))
            {
                if (!isHomePhone)
                {
                    labMessage.Text += "Please enter correct home phone" + Environment.NewLine;
                    IsError = true;
                }
                if (!string.IsNullOrWhiteSpace(txtCellPhone.Text) && !isCellPhone)
                {
                    labMessage.Text += "Please enter correct cell phone" + Environment.NewLine;
                    IsError = true;
                }
            }
            else
            {
                if (!isCellPhone)
                {
                    labMessage.Text += "Please enter correct cell phone" + Environment.NewLine;
                    IsError = true;
                }

                if (!string.IsNullOrWhiteSpace(txtHomePhone.Text) && !isHomePhone)
                {
                    labMessage.Text += "Please enter correct home phone" + Environment.NewLine;
                    IsError = true;
                }
            }

            if (string.IsNullOrWhiteSpace(txtModel.Text)) {
                labMessage.Text += "Please enter Make & model" + Environment.NewLine;
                IsError = true;
            }

            if(!string.IsNullOrWhiteSpace(txtYear.Text) && !ValidationHelper.IsValidYear(txtYear.Text))
            {
                labMessage.Text += "Please enter this year between 1900 and the current year plus one" + Environment.NewLine;
                IsError = true;
            }
            if (!ValidationHelper.IsAppointmentDate(dateAppointment.Value))
            {
                labMessage.Text += "The appointment date is required and cannot be in the past" + Environment.NewLine;
                IsError = true;
            }


            //Verify that there are no errors, and if there are errors, the red font will be displayed
            if (IsError)
            {
                labMessage.ForeColor = Color.Red;
                return;
            }
            else
            {
                labMessage.ForeColor = Color.Black;
            }

            string record = $"CustomerName:{txtCustomerName.Text},Address:{txtAddress.Text},"
                + $"City:{txtCity.Text},Province:{txtProvince.Text},PostalCode:{txtPostalCode.Text},"
                + $"HomePhone: {txtHomePhone.Text},CellPhone:{txtCellPhone.Text},Email:{txtEmail.Text}," +
                $"MakeModel:{txtModel.Text},"
                + $"Year:{txtYear.Text} ,AppointmentDate: {dateAppointment.Text} ,Problem: {richBoxProblem.Text},"
              ;

            new BookAppointment().InsertBookAppointment(record);
            labMessage.Text = "Book Appointment successful";
            }

            public void getAppointmentObject()
        {
            BookAppointment bookAppointment = new BookAppointment();
            bookAppointment.Address = txtAddress.Text;
            bookAppointment.CustomerName = txtCustomerName.Text;
            bookAppointment.City = txtCity.Text;
            bookAppointment.Province = txtProvince.Text;
            bookAppointment.PostalCode = txtPostalCode.Text;
            bookAppointment.HomePhone = txtHomePhone.Text;
            bookAppointment.CellPhone = txtCellPhone.Text;
            bookAppointment.Email = txtEmail.Text;
            bookAppointment.MakeModel = txtModel.Text;
            if (!string.IsNullOrWhiteSpace(txtYear.Text))
            {
                bookAppointment.Year = int.Parse(txtYear.Text);
            }
            bookAppointment.AppointmentDate = dateAppointment.Value;
            bookAppointment.Problem = richBoxProblem.Text;
        }
        }
}
