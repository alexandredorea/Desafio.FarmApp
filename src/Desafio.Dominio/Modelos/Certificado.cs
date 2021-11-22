using Desafio.Dominio.Modelos.Base;

namespace Desafio.Dominio.Modelos
{
    public class Certificado : EntidadeBase
    {
        public string Telefone { get; private set; }
        public string Token { get; private set; }
        public bool Validado { get; private set; } = false;

        public Certificado(
            string telefone, 
            string token)
        {
            Telefone = telefone;
            Token = token;
        }

        public void ValidarToken(bool validado)
        {
            Validado = validado;
        }

        public void AtualizarToken(string token)
        {
            Token = token;
        }
    }
}
