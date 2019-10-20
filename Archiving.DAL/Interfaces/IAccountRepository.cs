using Archiving.DAL.Entities;

namespace Archiving.DAL.Interfaces
{
    public interface IAccountRepository
    {
        bool CreateAccount(Account account);
        bool CheckAccount(Account account);
    }
}
