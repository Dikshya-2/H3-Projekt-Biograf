namespace Biograf.API.Authorization
{
    // It allow certain endpoints in your application to be accessed by unauthenticated users,
    // even if other parts of your application require authentication.
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    {
    }
}
