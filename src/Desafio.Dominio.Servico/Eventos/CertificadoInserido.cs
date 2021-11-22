using Desafio.Dominio.Modelos;
using MediatR;

namespace Desafio.Dominio.Mediador.Eventos
{
    public class CertificadoInserido : INotification
    {
        public Certificado Entidade { get; set; }

        public bool Completo { get; internal set; }
    }
}
