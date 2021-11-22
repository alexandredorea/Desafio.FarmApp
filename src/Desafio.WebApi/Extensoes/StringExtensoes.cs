using System;
using System.Linq;

namespace Desafio.Dominio.Extensoes
{
    public static class StringExtensoes
    {
        public static string SomenteNumeros(this string valor)
        {
            if (!string.IsNullOrWhiteSpace(valor))
                return new String(valor.Where(item => Char.IsDigit(item)).ToArray());
            
            return string.Empty;
        }

        public static string SomenteLetras(this string valor)
        {
            if (!string.IsNullOrWhiteSpace(valor))
                return new String(valor.Where(c => Char.IsLetter(c)).ToArray());

            return string.Empty;
        }

        public static bool TodosCaracteresIguais(this string valor)
        {
            return valor.Distinct().Count() == 1;
        }
    }
}
