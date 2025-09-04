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
        public DbSet<OrganizationHierarchyDto> OrganizationHierarchyDto { get; set; }
        public DbSet<CalenderVisitScheduleDto> CalenderVisitScheduleDto { get; set; }


        // Database Entity
        public DbSet<LoginResponse> LoginResponse { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryWiseDetails> CategoryWiseDetails { get; set; }
        public DbSet<TokenMaster> TokenMaster { get; set; }
        public DbSet<TokenDetails> TokenDetails { get; set; }
        public DbSet<TokenDetailsImage> TokenDetailsImage { get; set; }
        public DbSet<TokenMasterDto> TokenMasterDto { get; set; }
        public DbSet<LocationMapping> LocationMapping { get; set; }
        public DbSet<TokenDetailsShowDto> TokenDetailsShowDto { get; set; }
        public DbSet<MappingCustodian> MappingCustodian { get; set; }
        public DbSet<Custodian> Custodian { get; set; }
        public DbSet<GetVisitLogScheduleDto> GetVisitLogScheduleDto { get; set; }
        public DbSet<DataCollection> DataCollection { get; set; }
        public DbSet<VisitLog> VisitLog { get; set; }
        public DbSet<VisitLogDetails> VisitLogDetails { get; set; }
        public DbSet<DataMapping> DataMapping { get; set; }


        // Model view Dto

       public DbSet<DashboardCardSummaryDto> DashboardCardSummaryDto { get; set; }
       public DbSet<DataCollectionResultDto> DataCollectionResultDto { get; set; }
       public DbSet<DataMappingDto> DataMappingDto { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /// database entity 
            
            modelBuilder.Entity<CorporateOffice>().HasKey(x => x.CorpId);
            modelBuilder.Entity<OfficeBranch>().HasKey(x => x.BranchId);
            modelBuilder.Entity<OfficeBooth>().HasKey(x => x.BoothId);
            modelBuilder.Entity<BoothAsset>().HasKey(x => x.AssetId);
            modelBuilder.Entity<Organization>().HasKey(x => x.OrgId);
            modelBuilder.Entity<OrgResource>().HasKey(x => x.ResourceProfileId);
            modelBuilder.Entity<Category>().HasKey(x => x.CategoryId);
            modelBuilder.Entity<CategoryWiseDetails>().HasKey(x => x.Id);
            modelBuilder.Entity<TokenMaster>().HasKey(x => x.TokenId);
            modelBuilder.Entity<TokenDetails>().HasKey(x => x.Id);
            modelBuilder.Entity<TokenDetailsImage>().HasKey(x => x.ImageId);
            modelBuilder.Entity<VisitSchedule>(entity =>
            {
                // Primary Key
                entity.HasKey(x => x.ScheduleId);

                // Make ScheduleId auto-increment
                entity.Property(x => x.ScheduleId)
                      .ValueGeneratedOnAdd();

                // Map TimeOfVisit to PostgreSQL "time"
                entity.Property(x => x.TimeOfVisit)
                      .HasColumnType("time");
            });
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
            modelBuilder.Entity<MappingCustodian>().HasKey(x => x.Id);
            modelBuilder.Entity<LocationMapping>().HasKey(x => x.Id);
            modelBuilder.Entity<Custodian>().HasKey(x => x.CustodianId);
            modelBuilder.Entity<DataCollection>().HasKey(x => x.DataId);
            modelBuilder.Entity<VisitLog>().HasKey(x => x.VisitLogId);
            modelBuilder.Entity<VisitLogDetails>().HasKey(x => x.VisitLogDetailsId);
            modelBuilder.Entity<DataMapping>().HasKey(x => x.Id);


            // Get data from  Procrdure 

            modelBuilder.Entity<MenuSetUp>().HasNoKey();
            modelBuilder.Entity<ScheduleDataDto>().HasNoKey(); 
            modelBuilder.Entity<UserWiseRoleShowDto>().HasNoKey();
            modelBuilder.Entity<RoleBaseSubMenuDto>().HasNoKey();
            modelBuilder.Entity<CustomTypeWiseLoadedDto>().HasNoKey();
            modelBuilder.Entity<OrganizationConfigureDto>().HasNoKey();
            modelBuilder.Entity<OrganizationHierarchyDto>().HasNoKey();
            modelBuilder.Entity<CalenderVisitScheduleDto>().HasNoKey();
            modelBuilder.Entity<LoginResponse>().HasNoKey();
            modelBuilder.Entity<TokenMasterDto>().HasNoKey();
            modelBuilder.Entity<TokenDetailsShowDto>().HasNoKey();
            modelBuilder.Entity<GetVisitLogScheduleDto>().HasNoKey();
            modelBuilder.Entity<DashboardCardSummaryDto>().HasNoKey();
            modelBuilder.Entity<DataCollectionResultDto>().HasNoKey();
            modelBuilder.Entity<DataMappingDto>().HasNoKey();


            base.OnModelCreating(modelBuilder);
        }

    }
}
