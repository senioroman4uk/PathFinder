namespace PathFinder.Security.UserManagement.Constants
{
    public static class ValidationConstants
    {
        public const string PasswordsAreNotEqualValidationMessage = "Passwords are not equals";
        public const string PhoneNumberRegex = @"\+38\([0-9]{3}\)\s[0-9]{3}-[0-9]{2}-[0-9]{2}";
        public const string UserNameRegex = @"^[A-Za-z0-9@_]+$";
    }
}
