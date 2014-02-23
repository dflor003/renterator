using AutoMapper;
using Renterator.DataAccess.Model;
using Renterator.Services.Dto;

namespace Renterator.Services
{
    public static class MappingHelper
    {
        private static bool isInitialized;

        public static void InitializeMappings()
        {
            if (isInitialized)
            {
                return;
            }

            Mapper.CreateMap<User, UserAccountCreationInfo>()
                .ForMember(dto => dto.Password, prop => prop.Ignore());
            Mapper.CreateMap<UserAccountCreationInfo, User>()
                .ForMember(ent => ent.PasswordHash, prop => prop.Ignore());

            isInitialized = true;
        }
    }
}
