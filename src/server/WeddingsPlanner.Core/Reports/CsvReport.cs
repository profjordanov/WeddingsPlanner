using System.Collections.Generic;
using System.Linq;

namespace WeddingsPlanner.Core.Reports
{
    public struct CsvReport
    {
        public CsvReport(IEnumerable<string> allRows, byte[] data, string fileName)
        {
            AllRows = allRows.ToList();
            Data = data;
            FileName = $"{fileName}.csv";
        }

        public IReadOnlyCollection<string> AllRows { get; }

        public byte[] Data { get; }

        public string FileName { get; }
    }
}