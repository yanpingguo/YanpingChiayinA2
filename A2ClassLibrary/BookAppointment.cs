/**
 * Project: Book car Maintenance
 * Create by: yanping & chinayin
 * Date: 9 July 2024
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace A2ClassLibrary
{
    public class BookAppointment
    {
        StreamReader reader;
        StreamWriter writer;
        string fileName = "appointments.txt";

        #region properties
        public string CustomerName {  get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string MakeModel { get; set; }
        public int Year { get; set; }
        public string AppointmentDate { get; set; }
        public string Problem { get; set; }
        #endregion

        #region constructors
        public BookAppointment() { }

        #endregion

        #region InsertBookAppointment
        public void InsertBookAppointment(BookAppointment bookAppointment)
        {
            try
            {
                BookAppointment returnNewData = DataConversion(bookAppointment);
                string record = returnNewData.ToString();
                using (writer = new StreamWriter(fileName,true)) {
                    writer.WriteLine(record);
                }
            }
            catch (Exception ex) {
                throw new Exception($"exception trying to add a new appointment:{ex.Message}");
            }
        }
        #endregion

        #region Tostring DataConversion
        public override string ToString()
        {
            string record = $"CustomerName: {CustomerName},  Address: {Address},\n"
                + $"City: {City},  Province: {Province},  PostalCode: {PostalCode},\n"
                + $"HomePhone: {HomePhone},  CellPhone: {CellPhone},\n"
                + $"Email: {Email},  MakeModel: {MakeModel},\n"
                + $"Year: {Year},  AppointmentDate: {AppointmentDate},\n" 
                + $"Problem: {Problem}{Environment.NewLine}|" ;

            return record;
        }
        /// <summary>
        /// coversion data format
        /// </summary>
        /// <param name="bookAppointment"></param>
        /// <returns></returns>
        public BookAppointment DataConversion(BookAppointment bookAppointment)
        {
            bookAppointment.Province =  bookAppointment.Province.ToUpper();
            if (!string.IsNullOrWhiteSpace(bookAppointment.PostalCode))
            {
                bookAppointment.PostalCode = bookAppointment.PostalCode.Replace(" ", "").Insert(3, " ");
            }
        
            bookAppointment.HomePhone = getPhoneData(bookAppointment.HomePhone);
            bookAppointment.CellPhone = getPhoneData(bookAppointment.CellPhone);
            bookAppointment.Email = bookAppointment.Email.ToLower();

            DateTime dateTime = DateTime.Parse(bookAppointment.AppointmentDate);
            dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            string formatted = dateTime.ToString("dd MMM yyyy",CultureInfo.InvariantCulture);
            bookAppointment.AppointmentDate = formatted;

            return bookAppointment;
        }
        #endregion

        #region getPhoneData
        /// <summary>
        /// insert the dashes into the phone numbers
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        private string getPhoneData(string phone)
        {
            if (!string.IsNullOrWhiteSpace(phone))
            {
                string phoneNumber = Regex.Replace(phone, @"\D", "");
                return phoneNumber.Substring(0, 3) + "-" + phoneNumber.Substring(3, 3) + "-"
                + phoneNumber.Substring(6, 4);
            }

            return phone;
        }

        #endregion
    }
}
