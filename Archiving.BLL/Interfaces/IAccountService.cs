using Archiving.BLL.Dto;

namespace Archiving.BLL.Interfaces
{
    public interface IAccountService
    {
        bool CreateAccount(AccountDto accountDto);
        bool Authorize(AccountDto accountDto);
    }
}
