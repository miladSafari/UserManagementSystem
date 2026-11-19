using UserManagementSystem.Common.Enums;

public static class ErrorMessages
{
    private static readonly Dictionary<ErrorType, string> _messages = new()
    {
        [ErrorType.Unknown] = "خطای نامشخصی رخ داده است.",
        [ErrorType.UserIdMustBeGreaterThanZero] = "شناسه کاربر باید بزرگ‌تر از صفر باشد.",
        [ErrorType.UserIdIsRequired] = "شناسه کاربر الزامی است.",
        [ErrorType.NameIsRequired] = "نام الزامی است.",
        [ErrorType.NameLenghtIsInvalid] = "حداکثر کاراکتر برای نوشتن نام ،50 کاراکتر است.",
        [ErrorType.FamilyIsRequired] = "نام خانوادگی الزامی است.",
        [ErrorType.FamilyLenghtIsInvalid] = "حداکثر کاراکتر برای نوشتن نام خانوادگی ، 70 کاراکتر است.",
        [ErrorType.NationalCodeIsRequired] = "کد ملی الزامی است.",
        [ErrorType.NationalCodeLenghtIsInvalid] = "کدملی باید 10 کاراکتر باشد.",
        [ErrorType.NationalCodeIsInvalid] = "کد ملی معتبر نیست.",
        [ErrorType.BirthDateIsInvalid] = "تاریخ تولد معتبر نیست.",
    };

    public static string Get(ErrorType type)
        => _messages.TryGetValue(type, out var msg) ? msg : _messages[ErrorType.Unknown];
}
