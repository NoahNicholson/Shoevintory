using System.Threading.Tasks;
using Shoevintory.Auth.Models;


namespace Shoevintory.Auth
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}
