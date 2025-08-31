using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.DAO
{
    public class DashboardSummaryDAO : IDashboard
    {
        private readonly DatabaseConnection _connection;

        public DashboardSummaryDAO(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<DashboardCardSummaryDto> getDashboardSummary(int dataValue)
        {
            try
            {
                var parameters = new[]
                    {
                            new NpgsqlParameter("p_param", dataValue),

                    };

                var summarylist = await _connection.DashboardCardSummaryDto
                    .FromSqlRaw("SELECT * FROM get_dashboard_summary(@p_param)", parameters)
                    .ToListAsync();

                return summarylist.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
