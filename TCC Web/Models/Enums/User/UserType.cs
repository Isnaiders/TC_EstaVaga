namespace TCC_Web.Models.Enums.User
{
    [Flags]
    public enum UserType : int
	{
        Unknown = 0,
        Client = 1,
        Administrator = 2,
        Provider = 4
    }
}
