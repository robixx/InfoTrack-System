using ITC.InfoTrack.Model.DAO;
using ITC.InfoTrack.Model.Interface;
using Microsoft.AspNetCore.Rewrite;

namespace ITC.InfoTrack.Utility
{
    public static class ServiceInjection
    {
        public static void InjectService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IMenuSet, MenuDAO>();
            services.AddScoped<ICorporate, CorporateDAO>();
            services.AddScoped<IDropDown, DropDownDAO>();
            services.AddScoped<IMetaData, MetaDataDAO>();
            services.AddScoped<IRole, RoleDAO>();
            services.AddScoped<IOrgLocation, CorpLocationDAO>();
            services.AddScoped<IConfigurations, ConfigurationDAO>();
            
        }
    }
}
