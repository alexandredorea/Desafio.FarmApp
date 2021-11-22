using Desafio.Dominio.Modelos;
using Desafio.Infraestrutura.Dados.Mapeamentos.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Infraestrutura.Dados.Mapeamentos
{
    public class CertificadoMapeamento : MapeamentoBase<Certificado>
    {
        public override void Configure(EntityTypeBuilder<Certificado> builder)
        {
            builder.Property(x => x.Telefone)
                .HasMaxLength(15)
                .HasColumnType("varchar(15)");

            builder.Property(x => x.Token)
                .HasMaxLength(4)
                .HasColumnType("varchar(4)");

            builder.Property(x => x.Validado)
                .HasDefaultValue(false);

            base.Configure(builder);
        }
    }
}
