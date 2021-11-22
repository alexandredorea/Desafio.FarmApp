using Desafio.Dominio.Extensoes;
using Desafio.Dominio.Modelos;
using Desafio.WebApi.ViewModel;
using FluentValidation;

namespace Desafio.Dominio.Validadores
{
    public class UsuarioValidador : AbstractValidator<UsuarioViewModel>
    {
        public UsuarioValidador()
        {
            RuleFor(u => u.Nome)
                .NotNull().WithMessage("Nome é obrigatório")
                .NotEmpty().WithMessage("Nome é obrigatório")
                .Length(3, 100).WithMessage("Nome precisa ter no mínimo 3 caracter e máximo de 100 caracter");

            RuleFor(u => u.Cpf)
                .NotNull().WithMessage("CPF é obrigatório")
                .NotEmpty().WithMessage("CPF é obrigatório")
                .Must(ValidarCpf).WithMessage("CPF inválido");

            RuleFor(u => u.Email)
                .NotNull().WithMessage("E-mail é obrigatório")
                .NotEmpty().WithMessage("E-mail é obrigatório")
                .EmailAddress().WithMessage("E-mail inválido");

            RuleFor(u => u.Telefone)
                .NotNull().WithMessage("Telefone é obrigatório")
                .NotEmpty().WithMessage("Telefone é obrigatório")
                .Telefone().WithMessage("Informe um número de telefone válido com DDD");

            RuleFor(u => u.Cep)
                .NotNull().WithMessage("CEP é obrigatório")
                .NotEmpty().WithMessage("CEP é obrigatório")
                .Must(ValidarCep).WithMessage("CEP inválido")
                .Length(8, 9).WithMessage("CEP inválido");
        }

        private readonly int[] _pesos = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        private bool ValidarCep(string valor)
        {
            var cep = valor.SomenteNumeros();
            if (cep.Length < 8)
                return false;

            return true;
        }

        private bool ValidarCpf(string valor)
        {
            var cpf = valor.SomenteNumeros();

            if (cpf.Length < 11)
                return false;

            if (cpf.TodosCaracteresIguais())
                return false;

            var digitoVerifador = new DigitoVerificador();
            var d1 = digitoVerifador.ObterDigitoMod11("0" + cpf, _pesos);
            var d2 = digitoVerifador.ObterDigitoMod11(cpf + d1.ToString(), _pesos);
            var dv = d1.ToString() + d2.ToString();

            return cpf.EndsWith(dv);
        }

        internal class DigitoVerificador
        {
            /// <summary>
            /// Obtem o mód de 11 da soma e subtrai 11 quando resto maior que 2
            /// </summary>
            /// <param name="numeroSemDigito">Número cpf sem dv</param>
            /// <param name="peso">Array de peso</param>
            /// <returns>Digito verificador esperado</returns>
            public int ObterDigitoMod11(string numeroSemDigito, int[] peso)
            {
                int digito = ObterMod(numeroSemDigito, peso);

                return digito >= 2 ? 11 - digito : 0;
            }

            /// <summary>
            /// Obtem o mód da soma;
            /// </summary>
            /// <param name="numeroSemDigito">Número cpf sem dv</param>
            /// <param name="peso">Array de peso</param>
            /// <param name="mod">mód padrão para cpf</param>
            /// <returns>resultado do resto por 11</returns>
            public int ObterMod(string numeroSemDigito, int[] peso, int mod = 11)
            {
                int soma = 0;
                for (int i = 0; i < peso.Length; ++i)
                {
                    soma += peso[i] * int.Parse(numeroSemDigito[i].ToString());
                }

                return soma % mod;
            }
        }
    }
}