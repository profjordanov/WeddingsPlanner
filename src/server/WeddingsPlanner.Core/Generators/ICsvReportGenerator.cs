using System.Collections.Generic;
using WeddingsPlanner.Core.Reports;

namespace WeddingsPlanner.Core.Generators
{
    public interface ICsvReportGenerator
    {
        CsvReport GenerateReport<T>(IEnumerable<T> records, string reportName);
    }
}