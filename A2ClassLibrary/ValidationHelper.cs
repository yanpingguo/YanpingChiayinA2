using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace A2ClassLibrary
{
    public class ValidationHelper
    {
        /// <summary>
        /// capitalize each word in the string
        /// Beware of 1-letter words: if it’s the first word, capitalize it, otherwise keep it lower case.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string Capitalize(string parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter))
            {
                return string.Empty;
            }

            parameter = parameter.Trim().ToLower();

            string [] words = parameter.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 1)
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                }
            }
            return string.Join(" ", words);
        }

        /// <summary>
        /// validation postal code accept upper or lowercase, with or without the single space.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Boolean IsValidPostalCode(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            string pattern = @"^[A-Za-z]\d[A-Za-z] ?\d[A-Za-z]\d$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(input);
        }

        /// <summary>
        ///  return true only if the string matches one of the valid 2 letter province or territory codes.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Boolean IsValidProvinceCode(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            
            string[] provinceCode = { "AB", "BC", "MB", "NB", "NL", "NS", "NT", "NU", "ON", "PE", "QC", "SK", "YT" };
            for(int i = 0; i < provinceCode.Length; i++)
            {
                if (provinceCode[i].Equals(input.ToUpper()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// return true only if the entire string fits the phone pattern “123-123-1234”, with or without the dashes.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Boolean IsValidPhoneNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) { 
                return false;
            }
            string pattern = @"^\d{3}-?\d{3}-?\d{4}$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// validation email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// validation year
        /// </summary>
        /// <param name="yearStr"></param>
        /// <returns></returns>
        public static bool IsValidYear(string yearStr)
        {
            string pattern = @"^\d{4}$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(yearStr))
            {
                return false;
            }

            if (!int.TryParse(yearStr, out int year))
            {
                return false;
            }

            //get current year
            int currentYear = DateTime.Now.Year;

            if (year >= 1900 && year <= currentYear + 1)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// validation appointment date cannot be in the past
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Boolean IsAppointmentDate(DateTime date)
        {
            if(string.IsNullOrWhiteSpace(date.ToString()))
            {
                return false;
            }
            if(date < DateTime.Today)
            {
                return false;
            }
            return true;
        }
    }
}
