using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Xml.Linq;

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
        public DateTime AppointmentDate { get; set; }
        public string Problem { get; set; }
        #endregion

        #region constructors
        public BookAppointment() { }

        #endregion

        #region Book Appointment
        public void InsertBookAppointment(string record)
        {
            try
            {
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;
                projectPath = projectPath.Substring(0, projectPath.IndexOf("bin\\"));
                string filePath = Path.Combine(projectPath,fileName);

                Console.WriteLine("filePath: "+ filePath);

                if (!File.Exists(filePath)) {
                    using (writer = new StreamWriter(fileName,true)) {
                        writer.WriteLine(record);
                    }
                }
                else
                {
              
                    writer.WriteLine(record);
                }
            }
            catch (Exception ex) {
                throw new Exception($"exception trying to add a new appointment:{ex.Message}");
            }
        }
        #endregion

        #region Tostring
        public override string ToString()
        {
            string record = $"CustomerName: {CustomerName},Address: {Address},\n"
                + $"City: {City},Province: {Province},PostalCode: {PostalCode},\n"
                + $"Province: {Province},PostalCode: {PostalCode},HomePhone: {HomePhone},\n"
                + $"CellPhone: {CellPhone},Email: {Email},MakeModel: {MakeModel},\n"
                + $"Year: {Year},AppointmentDate: {AppointmentDate},Problem: {Problem}{Environment.NewLine}|"
              ;

            return record;
        }
        #endregion

    }
}
