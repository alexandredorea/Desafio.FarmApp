using System;

namespace Desafio.Dominio.Intefaces.Modelos
{
    public interface IEntidadeBase<TChavePrimaria>
    {
        TChavePrimaria Id { get; }
    }

    public interface IEntidadeBase : IEntidadeBase<Guid>
    {
    }
}
