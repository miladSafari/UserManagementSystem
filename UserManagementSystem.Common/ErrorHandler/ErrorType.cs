namespace UserManagementSystem.Common.Enums
{
    public enum ErrorType
    {
        Unknown = 0,
        UserIdMustBeGreaterThanZero = 1,
        UserIdIsRequired = 2,
        NameIsRequired = 3,
        NameLenghtIsInvalid = 4,
        FamilyIsRequired = 5,
        FamilyLenghtIsInvalid = 6,
        NationalCodeIsRequired = 7,
        NationalCodeLenghtIsInvalid = 8,
        BirthDateIsInvalid = 9,
        NationalCodeIsInvalid = 10
    }
}