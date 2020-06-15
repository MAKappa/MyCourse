using System;
using System.Data;
using System.Threading.Tasks;

namespace MyCourse.Models.Services.Infrastructure
{
    public interface IDatabaseAccesser
    {
        Task<DataSet> QueryAsync(FormattableString query);
    }
}