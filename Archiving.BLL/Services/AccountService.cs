using AutoMapper;
using Archiving.BLL.Dto;
using Archiving.BLL.Infrastructure;
using Archiving.BLL.Interfaces;
using Archiving.DAL.Entities;
using Archiving.DAL.Interfaces;

namespace Archiving.BLL.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;
        private IMapper _mapper;
        public AccountService()
        {
            _accountRepository = IoCRepository.AccountRepository;
            _mapper = Mapping.EntityMapper;
        }
        public bool CreateAccount(AccountDto accountDto)
        {
            return _accountRepository.CreateAccount(_mapper.Map<AccountDto, Account>(accountDto));
        }
        public bool Authorize(AccountDto accountDto)
        {
            return _accountRepository.CheckAccount(_mapper.Map<AccountDto, Account>(accountDto));
        }
    }
}
