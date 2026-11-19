using UserManagementSystem.Common.Enums;

public static class ErrorOldEnMessage
{
    private static readonly Dictionary<ErrorType, string> _messages = new()
    {
        [ErrorType.Unknown] = "UnKnown Error.",
        [ErrorType.UserIdMustBeGreaterThanZero] = "User Id must be greater than zero.",
        [ErrorType.NameIsRequired] = "User name is required.",
        [ErrorType.NameLenghtIsInvalid] = "User name cannot More than 50 characters.",
        [ErrorType.FamilyIsRequired] = "User family is required.",
        [ErrorType.FamilyLenghtIsInvalid] = "User family cannot more than 50 characters.",
        [ErrorType.NationalCodeIsRequired] = "User nationalCode is required.",
        [ErrorType.NationalCodeLenghtIsInvalid] = "User nationalCode must be 10 characters.",
        [ErrorType.NationalCodeIsInvalid] = "User nationalCode is Invlaid.",
        [ErrorType.BirthDateIsInvalid] = "User BirthDate is in another format.",
    };

    public static ErrorType Get(string message)
    {
        var match = _messages.FirstOrDefault(x =>
            string.Equals(x.Value, message, StringComparison.OrdinalIgnoreCase));

        var errorType = match.Equals(default(KeyValuePair<ErrorType, string>))
            ? ErrorType.Unknown
            : match.Key;

        return errorType;
    }
}