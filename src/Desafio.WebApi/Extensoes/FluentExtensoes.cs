using FluentValidation;
using System.Text.RegularExpressions;

namespace Desafio.Dominio.Extensoes
{
    public static class FluentExtensions
    {
        public static IRuleBuilderOptions<T, TElement> Telefone<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)
        { 
            return ruleBuilder.Must((_, valor) => Regex.IsMatch(valor.ToString(), @"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$"));
        }
    }
}
