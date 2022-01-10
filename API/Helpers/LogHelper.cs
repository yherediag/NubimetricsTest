using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class LogHelper : ILogHelper
    {
        private const string PATH = "Logfiles";

        public async Task Guardar(string texto)
        {
            using StreamWriter writer = File.AppendText(@$"{PATH}\CurrenciesJson_{DateTime.Today:yyyyMMdd}.txt");
            await writer.WriteLineAsync($"{DateTime.Now:hh:mm dd-MM-yyyy} - {texto}");
        }

        public async Task GuardarCSV(IEnumerable lista)
        {
            using StreamWriter writer = File.AppendText(@$"{PATH}\CurrenciesRatio_{DateTime.Today:yyyyMMdd}.csv");
            await writer.WriteLineAsync(string.Join(",", (List<double>)lista));
        }

        public async Task Error(string texto)
        {
            using StreamWriter writer = File.AppendText(@$"{PATH}\Error_{DateTime.Today:yyyyMMdd}.txt");
            await writer.WriteLineAsync($"{DateTime.Now:hh:mm dd-MM-yyyy} - Mensaje: {texto}");
        }
    }
}
