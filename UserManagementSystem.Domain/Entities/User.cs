using System.Globalization;
using System.Xml.Linq;
using UserManagementSystem.Common.Enums;

namespace UserManagementSystem.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string NationalCode { get; private set; }
        public DateTime BirthDate { get; private set; }
        public bool IsDeleted { get; private set; }
        private User()
        {
            Name = null!;
            Family = null!;
            NationalCode = null!;
        }
        public User(Guid id, string name, string family, string nationalCode, DateTime birthDate)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("User Id must be greater than zero.");



            Id = id;
            Name = ValidateUserName(name);
            Family = ValidateUserFamily(family);
            NationalCode = ValidateUserNationalCode(nationalCode);
            BirthDate = ValidateUserBirthDate(birthDate);
        }
        public void ChangeName(string newName)
        {
            Name = ValidateUserName(newName);
        }
        public void ChangeFamily(string newFamily)
        {
            Family = ValidateUserFamily(newFamily);
        }
        public void ChangeNationalCode(string newNationalCode)
        {
            NationalCode = ValidateUserNationalCode(newNationalCode);
        }
        public void ChangeBirthDate(DateTime newBirthDate)
        {
            BirthDate = ValidateUserBirthDate(newBirthDate);
        }
        public static string ValidateUserName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("User name is required.");

            name = name.Trim();

            if (name.Length > 50)
                throw new ArgumentOutOfRangeException(
                    "User name cannot More than 50 characters.");

            return name;

        }
        public static string ValidateUserFamily(string family)
        {
            if (string.IsNullOrWhiteSpace(family))
                throw new ArgumentException("User family is required.");

            family = family.Trim();

            if (family.Length > 70)
                throw new ArgumentOutOfRangeException(
                    "User family cannot more than 50 characters.");

            return family;

        }
        public static string ValidateUserNationalCode(string nationalCode)
        {
            if (string.IsNullOrWhiteSpace(nationalCode))
                throw new ArgumentException("User nationalCode is required.");

            if(!NationalCodeValidator.IsValid(nationalCode))
                throw new ArgumentException("User nationalCode is Invlaid.");


            nationalCode = nationalCode.Trim();

            if (nationalCode.Length != 10)
                throw new ArgumentOutOfRangeException(
                    "User nationalCode must be 10 characters.");

            return nationalCode;

        }
        public static DateTime ValidateUserBirthDate(DateTime birthDate)
        {
            string[] formats = { "MM/dd/yyyy", "M/d/yyyy", "M/dd/yyyy", "MM/d/yyyy" };
            if (!DateTime.TryParseExact(DateTime.Now.ToString("MM/dd/yyyy"), formats, new CultureInfo("en-US"),
                                        DateTimeStyles.None, out birthDate))
                throw new ArgumentException("User BirthDate is in another format.");

            return birthDate;
        }
        public void DeleteUser()
        {
            IsDeleted = true;
        }
        public static class NationalCodeValidator
        {
            public static bool IsValid(string? nationalCode)
            {
                if (string.IsNullOrWhiteSpace(nationalCode))
                    return false;

                if (nationalCode.Length != 10 || !nationalCode.All(char.IsDigit))
                    return false;

                if (nationalCode.Distinct().Count() == 1)
                    return false;

                var sum = 0;
                for (var i = 0; i < 9; i++)
                {
                    sum += (nationalCode[i] - '0') * (10 - i);
                }

                var remainder = sum % 11;
                var checkDigit = nationalCode[9] - '0';

                return (remainder < 2 && checkDigit == remainder)
                    || (remainder >= 2 && checkDigit == 11 - remainder);
            }
        }
    }
}
