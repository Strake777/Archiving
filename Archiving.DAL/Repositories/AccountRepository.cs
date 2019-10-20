using Archiving.DAL.EF;
using Archiving.DAL.Entities;
using Archiving.DAL.Interfaces;
using System.Linq;

namespace Archiving.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private EFContext _context;

        public AccountRepository(string connectionString)
        {
            _context = new EFContext(connectionString);
        }

        public bool CreateAccount(Account account)
        {
            if (!_context.Accounts.Any(a => a.Login.Equals(account.Login)))
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool CheckAccount(Account account)
        {
            return _context.Accounts.Any(a => a.Login == account.Login && a.Password == account.Password);
        }
    }
}
