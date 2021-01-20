using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FullStackAppSample.Models
{
    public partial class EntityFrameworkExampleContext : DbContext
    {
        public EntityFrameworkExampleContext()
        {
        }

        public EntityFrameworkExampleContext(DbContextOptions<EntityFrameworkExampleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-4T33R9SV\\MSSQLSERVER01;Initial Catalog=EntityFrameworkExample;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.PedId);

                entity.ToTable("Pedido");

                entity.Property(e => e.PedId).HasColumnName("PedID");

                entity.Property(e => e.PedIva).HasColumnName("PedIVA");

                entity.Property(e => e.PedProd).HasColumnName("pedProd");

                entity.Property(e => e.PedSubTot).HasColumnType("money");

                entity.Property(e => e.PedTotal).HasColumnType("money");

                entity.Property(e => e.PedVrUnit).HasColumnType("money");

                entity.HasOne(d => d.PedProdNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.PedProd)
                    .HasConstraintName("FK_Pedido_Pedido");

                entity.HasOne(d => d.PedUsuNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.PedUsu)
                    .HasConstraintName("FK_Pedido_Usuarios");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.ProId)
                    .HasName("PK_ProValor");

                entity.ToTable("Producto");

                entity.Property(e => e.ProId).HasColumnName("ProID");

                entity.Property(e => e.ProDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProValor).HasColumnType("money");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuId);

                entity.Property(e => e.UsuId).HasColumnName("UsuID");

                entity.Property(e => e.UsuNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsuPass)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
