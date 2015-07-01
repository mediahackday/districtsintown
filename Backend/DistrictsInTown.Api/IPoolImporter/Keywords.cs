using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace IPoolImporter
{
    internal class Keywords
    {
        public readonly Dictionary<string, int> Scores = new Dictionary<string, int>();

        public Keywords()
        {
            var reader = File.OpenText("Data\\histogram.tsv.txt");
            var csv = new CsvReader(reader);
            csv.Configuration.Delimiter = "\t";

            var input = csv.GetRecords<Record>();
            foreach (var record in input)
            {
                Scores[record.Keyword] = record.Score;
            }
        }

        private class Record
        {
            public string Keyword { get; set; }
            public int Frequency { get; set; }
            public int Score { get; set; }
        }
    }
}
