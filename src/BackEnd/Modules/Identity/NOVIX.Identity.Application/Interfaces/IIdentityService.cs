using System.Threading.Tasks;

namespace NOVIX.Identity.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<(bool IsSuccess, string Token, string ErrorMessage)> LoginAsync(string email, string password);
        Task<(bool IsSuccess, string ErrorMessage)> RegisterAsync(string email, string password, string firstName, string lastName);
        Task<(bool IsSuccess, string Token, string ErrorMessage)> GoogleLoginAsync(string idToken);
    }
}
