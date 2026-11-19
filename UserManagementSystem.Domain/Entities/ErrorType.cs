namespace UserManagementSystem.Common.Enums
{
    public enum ErrorType
    {
        Unknown = 0,
        UserIdMustBeGreaterThanZero = 1,
        UserIdIsRequired = 2,
        NameIsRequired = 3,
        FamilyIsRequired = 4,
        NationalCodeIsRequired = 5,
        BirthDateIsInvalid = 6,
        UserNotFound = 7,
        UserAlreadyExists = 8,
    }
}