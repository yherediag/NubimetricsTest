using Microsoft.Extensions.Configuration;
using System;

namespace API.Helpers
{
    public class ValidacionesHelper
    {
        internal static void ValidarIdPais(string idPais, IConfiguration config)
        {
            // Validaciones
            if (idPais.Length != 2)
                throw new Exception("Código de país invalido") { HResult = 400 };

            var paisesDesautorizados = config.GetValue<string>("UnauthorizedCountries");
            if (paisesDesautorizados.IndexOf(idPais, StringComparison.InvariantCultureIgnoreCase) > -1)
                throw new Exception("Código de país no autorizado") { HResult = 401 };
        }
    }
}
