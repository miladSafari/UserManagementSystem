using System.Globalization;
using System.Xml.Linq;

namespace UserManagementSystem.Domain
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string NationalCode { get; private set; }
        public DateTime BirthDate { get; private set; }
        private User()
        {
            Name = null!;
            Family = null!;
            NationalCode = null!;
        }
        public User(Guid id, string name, string family, string nationalCode, DateTime birthDate)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("User Id must be greater than zero", nameof(id));

            string[] formats = { "MM/dd/yyyy", "M/d/yyyy", "M/dd/yyyy", "MM/d/yyyy" };
            if (!DateTime.TryParseExact(DateTime.Now.ToString("MM/dd/yyyy"), formats, new CultureInfo("en-US"),
                                        DateTimeStyles.None, out birthDate))
                throw new ArgumentException("User BirthDate is in another format.");

            Id = id;
            Name = ValidateUserName(name);
            Family = ValidateUserFamily(family);
            NationalCode = ValidateUserNationalCode(nationalCode);
            BirthDate = birthDate;
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
        public static string ValidateUserName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("User name is required.", nameof(name));

            name = name.Trim();

            if (name.Length > 50)
                throw new ArgumentOutOfRangeException(
                    nameof(name),
                    name.Length,
                    "User name cannot More than 50 characters.");

            return name;

        }
        public static string ValidateUserFamily(string family)
        {
            if (string.IsNullOrWhiteSpace(family))
                throw new ArgumentException("User family is required.", nameof(family));

            family = family.Trim();

            if (family.Length > 70)
                throw new ArgumentOutOfRangeException(
                    nameof(family),
                    family.Length,
                    "User family cannot more than 50 characters.");

            return family;

        }
        public static string ValidateUserNationalCode(string nationalCode)
        {
            if (string.IsNullOrWhiteSpace(nationalCode))
                throw new ArgumentException("User nationalCode is required.", nameof(nationalCode));

            nationalCode = nationalCode.Trim();

            if (nationalCode.Length != 10)
                throw new ArgumentOutOfRangeException(
                    nameof(nationalCode),
                    nationalCode.Length,
                    "User nationalCode must be 10 characters.");

            return nationalCode;

        }
    }
}
