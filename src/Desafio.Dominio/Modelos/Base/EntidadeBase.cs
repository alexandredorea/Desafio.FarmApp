using Desafio.Dominio.Intefaces.Modelos;
using System;

namespace Desafio.Dominio.Modelos.Base
{
    public abstract class EntidadeBase<ChavePrimaria> : IEntidadeBase<ChavePrimaria>
    {
        public ChavePrimaria Id { get; protected set; }

        public override string ToString()
        {
            return $"{this.GetType().Name}#{Id}";
        }
    }

    public abstract class EntidadeBase : EntidadeBase<Guid>, IEntidadeBase
    {
        
    }
}
