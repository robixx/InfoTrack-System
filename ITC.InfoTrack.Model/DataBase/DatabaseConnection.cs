using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.DataBase
{
    public class DatabaseConnection : DbContext
    {
        public DatabaseConnection(DbContextOptions<DatabaseConnection> options) : base(options)
        {

        }

        public DbSet<CorporateOffice> CorporateOffice { get; set; }
        public DbSet<OfficeBranch> OfficeBranch { get; set; }
        public DbSet<OfficeBooth> OfficeBooth { get; set; }
        public DbSet<BoothAsset> BoothAsset { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<OrgResource> OrgResource { get; set; }
        public DbSet<VisitSchedule> VisitSchedule { get; set; }
        public DbSet<VisitScheduleDetails> VisitScheduleDetails { get; set; }
        public DbSet<ResourceList> ResourceList { get; set; }
        public DbSet<OrgLocation> OrgLocation { get; set; }
        public DbSet<OrgAsset> OrgAsset { get; set; }
        public DbSet<MetaDataType> MetaDataType { get; set; }
        public DbSet<MetaDataElements> MetaDataElements { get; set; }
        public DbSet<CheckListBank> CheckListBank { get; set; }
        public DbSet<CheckListAnsBank> CheckListAnsBank { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<RoleBasePagePermission> RoleBasePagePermission { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MenuSetUp> MenuSetUp { get; set; }
        public DbSet<OrganizationConfigure> OrganizationConfigure { get; set; }
        public DbSet<OrganizationConfigureDto> OrganizationConfigureDto { get; set; }
        public DbSet<OrgRootConfiguration> OrgRootConfiguration { get; set; }
        public DbSet<ScheduleDataDto> ScheduleDataDto { get; set; }
        public DbSet<UserWiseRoleShowDto> UserWiseRoleShowDto { get; set; }
        public DbSet<RoleBaseSubMenuDto> RoleBaseSubMenuDto { get; set; }
        public DbSet<CustomTypeWiseLoadedDto> CustomTypeWiseLoadedDto { get; set; }
        public DbSet<BranchInfo> BranchInfo { get; set; }
        public DbSet<LevelSetting> LevelSetting { get; set; }
        public DbSet<ProfileWiseOrganization> ProfileWiseOrganization { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CorporateOffice>().HasKey(x => x.CorpId);
            modelBuilder.Entity<OfficeBranch>().HasKey(x => x.BranchId);
            modelBuilder.Entity<OfficeBooth>().HasKey(x => x.BoothId);
            modelBuilder.Entity<BoothAsset>().HasKey(x => x.AssetId);
            modelBuilder.Entity<Organization>().HasKey(x => x.OrgId);
            modelBuilder.Entity<OrgResource>().HasKey(x => x.ResourceProfileId);
            modelBuilder.Entity<VisitSchedule>().HasKey(x => x.ScheduleId);
            modelBuilder.Entity<ResourceList>().HasKey(x => x.ResListId);
            modelBuilder.Entity<OrgLocation>().HasKey(x => x.LocationId);
            modelBuilder.Entity<OrgAsset>().HasKey(x => x.AssetId);
            modelBuilder.Entity<MetaDataType>().HasKey(x => x.PropertyId);
            modelBuilder.Entity<MetaDataElements>().HasKey(x => x.DataElementId);
            modelBuilder.Entity<CheckListBank>().HasKey(x => x.CheckListId);
            modelBuilder.Entity<CheckListAnsBank>().HasKey(x => x.AnsBankId);
            modelBuilder.Entity<Role>().HasKey(x => x.RoleId);
            modelBuilder.Entity<User>().HasKey(x => x.UserId);
            modelBuilder.Entity<RolePermission>().HasKey(x => x.Id);
            modelBuilder.Entity<RoleBasePagePermission>().HasKey(x => x.PermissionId);
            modelBuilder.Entity<VisitScheduleDetails>().HasKey(x => x.VisitScheduleDetailsId);
            modelBuilder.Entity<OrgRootConfiguration>().HasKey(x => x.Id);
            modelBuilder.Entity<OrganizationConfigure>().HasKey(x => x.Id);
            modelBuilder.Entity<BranchInfo>().HasKey(x => x.Id);
            modelBuilder.Entity<LevelSetting>().HasKey(x => x.LevelId);
            modelBuilder.Entity<ProfileWiseOrganization>().HasKey(x => x.Id);
            modelBuilder.Entity<MenuSetUp>().HasNoKey();
            modelBuilder.Entity<ScheduleDataDto>().HasNoKey(); 
            modelBuilder.Entity<UserWiseRoleShowDto>().HasNoKey();
            modelBuilder.Entity<RoleBaseSubMenuDto>().HasNoKey();
            modelBuilder.Entity<CustomTypeWiseLoadedDto>().HasNoKey();
            modelBuilder.Entity<OrganizationConfigureDto>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }

    }
}
