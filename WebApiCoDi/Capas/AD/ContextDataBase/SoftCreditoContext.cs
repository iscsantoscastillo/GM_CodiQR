using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using WebApiCoDi.Capas.AD.ContextModelTables;

namespace WebApiCoDi.Capas.AD.ContextDataBase
{
    public partial class SoftCreditoContext : DbContext
    {
        public SoftCreditoContext()
        {
        }

        public SoftCreditoContext(DbContextOptions<SoftCreditoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UsuariosApiMacroPay> UsuariosApiMacroPay { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //                optionsBuilder.UseSqlServer("Server=192.168.123.44; Database=SoftCredito; user id=test; password=GrupoMacro2017");
                var configuation = GetConfiguration();
                string cn = configuation.GetSection("ConnectionStrings").GetSection("ConexionBD").Value.ToString();
                optionsBuilder.UseSqlServer(cn);
            }
        }
        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuariosApiMacroPay>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK_UsuariosApi");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Apellido)
                    .HasColumnName("apellido")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmailUsuario)
                    .IsRequired()
                    .HasColumnName("email_usuario")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta)
                    .HasColumnName("fecha_alta")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaModificado)
                    .HasColumnName("fecha_modificado")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaRegistroUsuario)
                    .HasColumnName("fecha_registro_usuario")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordUsuario)
                    .IsRequired()
                    .HasColumnName("password_usuario")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PerfilUsuario)
                    .HasColumnName("perfil_usuario")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StatusUsuario)
                    .HasColumnName("status_usuario")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
