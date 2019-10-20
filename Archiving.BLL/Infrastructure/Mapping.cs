using AutoMapper;
using Archiving.BLL.Dto;
using Archiving.DAL.Entities;

namespace Archiving.BLL.Infrastructure
{
    public class Mapping 
    {
        private static IMapper _mapper;

        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AccountDto, Account>();
            });
            _mapper = new Mapper(config);
        }
        public static IMapper EntityMapper
        {
            get { return _mapper; }
        }
    }
}
