using ITC.InfoTrack.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Interface
{
    public interface IConfigurations
    {
        Task<(string message, bool status)> SaveOrganizationConficData(ConfigureInsertDto model);
        Task<List<OrganizationConfigureDto>> GetOrganizationConficData();
        Task<(string message, bool status)> DeleteOrganizationConficData(int id);
        Task<Dictionary<long, int?>> GetHierarchyAsync();
        Task<Dictionary<string, string>> GetTypeHierarchyAsync();
        Task<List<HierarchyNodeDto>> GetHierarchyNodeAsync();
        Task<List<OrgNodeDto>> GetOrganizationTreeAsync();
        Task<(string message, bool status)> SaveLevelData(LevelDataDto model);
        Task<(string message, bool status)> TreeNodeModificationAsync(TreeNodeInsertDto model);
    }
}
