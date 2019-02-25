using System.Collections.Generic;
using System.Linq;
using WeddingsPlanner.Core.Generators;
using WeddingsPlanner.Core.Reports;

using static System.Environment;
using static System.Text.Encoding;

namespace WeddingsPlanner.Business.Generators
{
    /// <summary>
    /// Generates CSV Report File.
    /// <see cref="CsvReport"/>
    /// </summary>
    public class CsvReportGenerator : ICsvReportGenerator
    {
        private const string CommaSeparator = ",";

        /// <summary>
        /// Generates report file by a collection of objects and file name.
        /// If the collection is empty will return empty file.
        /// </summary>
        /// <returns>CSV Report struct.</returns>
        public CsvReport GenerateReport<T>(IEnumerable<T> records, string reportName)
            where T : IReportModel
        {
            var properties = typeof(T).GetProperties(); // all properties of current object
            var headers = properties.Select(p => p.Name); // the first row of the report will be object's properties names

            var bodyRows = records.Select(record => new { record, props = record.GetType().GetProperties() })
                .Select(metaRec =>
                    metaRec.props.Select(prop => metaRec.record.GetType().GetProperty(prop.Name).GetValue(metaRec.record).ToString() ?? string.Empty))
                .Select(columns => string.Join(CommaSeparator, columns))
                .ToList();

            return PrepareReportFile(headers, bodyRows, reportName);
        }

        private static IEnumerable<string> GetAllRows(IEnumerable<string> titles, IEnumerable<string> values)
        {
            var headerRow = string.Join(CommaSeparator, titles);
            var allRows = new List<string>(values);
            allRows.Insert(0, headerRow);
            return allRows;
        }

        private static CsvReport PrepareReportFile(IEnumerable<string> titles, IEnumerable<string> values, string reportName)
        {
            var reportRows = GetAllRows(titles, values).ToList();
            var bytesData = UTF8.GetBytes(string.Join(NewLine, reportRows));
            return new CsvReport(reportRows, bytesData, reportName);
        }
    }
}