using MyApiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Application.Interfaces
{
    public interface IAnalyticsService
    {
        /// <summary>
        /// Retrieves a summary of analytics data within the specified date range.
        /// </summary>
        /// <param name="from">The start date of the range.</param>
        /// <param name="to">The end date of the range.</param>
        /// <returns>A task that represents the asynchronous operation, containing the summary data.</returns>
        Task<AnalyticsSummaryDto> GetSummaryAsync(DateTime? from, DateTime? to);

    }
}
