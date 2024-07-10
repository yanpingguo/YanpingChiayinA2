/**
 * Project: Book car Maintenance
 * Create by: yanping & chinayin
 * Date: 9 July 2024
 */

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

        /// <summary>
        /// Book Appointment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBookAppointment_Click(object sender, EventArgs e)
        {
            labMessage.Text = string.Empty;
            bool IsError = false;
            //Verify customer Name
            string customerName = ValidationHelper.Capitalize(txtCustomerName.Text);
            if (string.IsNullOrWhiteSpace(customerName))
            {
                labMessage.Text += "Please write customerName" + Environment.NewLine;
                IsError = true;
            }

            // email and postal information 
            //If email is not provided, the postal information is required.
            //All four are fine, but not mandatory, when an email is provided.

            bool isPostalcode = ValidationHelper.IsValidPostalCode(txtPostalCode.Text);
            bool isProvinceCode = ValidationHelper.IsValidProvinceCode(txtProvince.Text);
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!ValidationHelper.IsValidEmail(txtEmail.Text))
                {
                    labMessage.Text += "Please write correct email address" + Environment.NewLine;
                    IsError = true;
                }
                if (!string.IsNullOrWhiteSpace(txtPostalCode.Text) && !isPostalcode)
                {
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
                    labMessage.Text += "Please write  Canadian postal pattern A2A 2A2 if email not provided" + Environment.NewLine;
                    IsError = true;
                }

                if (!isProvinceCode)
                {
                    labMessage.Text += "Please write Canadian province or territory code if email not provided" + Environment.NewLine;
                    IsError = true;
                }
                if (string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    labMessage.Text += "Please write Address if email not provided" + Environment.NewLine;
                    IsError = true;
                }

                if (string.IsNullOrWhiteSpace(txtCity.Text))
                {
                    labMessage.Text += "Please write City if emial not provided" + Environment.NewLine;
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

            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                labMessage.Text += "Please enter Make & model" + Environment.NewLine;
                IsError = true;
            }

            if (!string.IsNullOrWhiteSpace(txtYear.Text) && !ValidationHelper.IsValidYear(txtYear.Text))
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

            //save data to appointment.txt file
            try
            {
                BookAppointment bookAppointment = getAppointmentObject(customerName);
                new BookAppointment().InsertBookAppointment(bookAppointment);
                labMessage.Text = "Book Appointment successful";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "Book Appointment Fail";
            }

        }

        /// <summary>
        /// get appoinment object
        /// </summary>
        /// <returns></returns>
        public BookAppointment getAppointmentObject(string customerName)
        {
            BookAppointment bookAppointment = new BookAppointment();
            bookAppointment.Address = txtAddress.Text;
            bookAppointment.CustomerName = customerName;
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
            bookAppointment.AppointmentDate = dateAppointment.Value.ToString();
            bookAppointment.Problem = richBoxProblem.Text;
            return bookAppointment;
        }

        /// <summary>
        /// Reset button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtCustomerName.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtProvince.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            txtHomePhone.Text = string.Empty;
            txtCellPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtYear.Text = string.Empty;
            txtAddress.Text = string.Empty;
            richBoxProblem.Text = string.Empty;

            labMessage.ForeColor = Color.Black;
            labMessage.Text = "Reset successful";
        }

        /// <summary>
        /// Close button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Prefill button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreFill_Click(object sender, EventArgs e)
        {
            txtCustomerName.Text = "Emma ";
            txtCity.Text = "Waterloo";
            txtProvince.Text = "ON";
            txtPostalCode.Text = "A2A 2A2";
            txtHomePhone.Text = "123-123-1234";
            txtCellPhone.Text = "123-123-1234";
            txtEmail.Text = "emma@gmail.com";
            txtModel.Text = "Honda Civic";
            txtYear.Text = "1990";
            txtAddress.Text = "123 King Street North";

            labMessage.ForeColor = Color.Black;
            labMessage.Text = "Pre-fill successful";
        }
    }
}
