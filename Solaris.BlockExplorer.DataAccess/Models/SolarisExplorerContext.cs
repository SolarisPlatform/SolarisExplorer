using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Solaris.BlockExplorer.DataAccess.Models
{
    public partial class SolarisExplorerContext : DbContext
    {
        public SolarisExplorerContext()
        {
        }

        public SolarisExplorerContext(DbContextOptions<SolarisExplorerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlockTransaction> BlockTransactions { get; set; }
        public virtual DbSet<Block> Blocks { get; set; }
        public virtual DbSet<InputScriptSignature> InputScriptSignature { get; set; }
        public virtual DbSet<Input> Inputs { get; set; }
        public virtual DbSet<OutputScriptPublicKey> OutputScriptPublicKey { get; set; }
        public virtual DbSet<OutputScriptPublicKeyAddress> OutputScriptPublicKeyAddresses { get; set; }
        public virtual DbSet<Output> Outputs { get; set; }
        public virtual DbSet<TransactionInput> TransactionInputs { get; set; }
        public virtual DbSet<TransactionOutput> TransactionOutputs { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<BlockTransaction>(entity =>
            {
                entity.HasKey(e => new { e.BlockId, e.TransactionId });

                entity.ToTable("BlockTransactions", "tables");

                entity.Property(e => e.BlockId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.BlockTransactions)
                    .HasForeignKey(d => d.BlockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlockTransactions_Blocks");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.BlockTransactions)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlockTransactions_Transactions");
            });

            modelBuilder.Entity<Block>(entity =>
            {
                entity.ToTable("Blocks", "tables");

                entity.Property(e => e.Id)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Bits)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Chainwork)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Difficulty).HasColumnType("decimal(28, 8)");

                entity.Property(e => e.Merkleroot)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousBlock)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.PreviousBlockNavigation)
                    .WithMany(p => p.InversePreviousBlockNavigation)
                    .HasForeignKey(d => d.PreviousBlock)
                    .HasConstraintName("FK_Blocks_Blocks");
            });

            modelBuilder.Entity<InputScriptSignature>(entity =>
            {
                entity.HasKey(e => e.InputId);

                entity.ToTable("InputScriptSignature", "tables");

                entity.Property(e => e.InputId).ValueGeneratedNever();

                entity.Property(e => e.Asm)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Hex)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.HasOne(d => d.Input)
                    .WithOne(p => p.InputScriptSignature)
                    .HasForeignKey<InputScriptSignature>(d => d.InputId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InputScriptSignature_Inputs");
            });

            modelBuilder.Entity<Input>(entity =>
            {
                entity.ToTable("Inputs", "tables");

                entity.HasIndex(e => new { e.TransactionId, e.OutputIndex })
                    .HasName("UQ__Inputs__DBC09B80BB19C0BA")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Coinbase)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.Inputs)
                    .HasForeignKey(d => d.TransactionId)
                    .HasConstraintName("FK_Inputs_Transactions");

                entity.HasOne(d => d.TransactionOutput)
                    .WithOne(p => p.Inputs)
                    .HasPrincipalKey<TransactionOutput>(p => new { p.TransactionId, p.OutputIndex })
                    .HasForeignKey<Input>(d => new { d.TransactionId, d.OutputIndex })
                    .HasConstraintName("FK_Inputs_TransactionOutputs");
            });

            modelBuilder.Entity<OutputScriptPublicKey>(entity =>
            {
                entity.HasKey(e => e.OutputId);

                entity.ToTable("OutputScriptPublicKey", "tables");

                entity.Property(e => e.OutputId).ValueGeneratedNever();

                entity.Property(e => e.Asm)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Hex)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Output)
                    .WithOne(p => p.OutputScriptPublicKey)
                    .HasForeignKey<OutputScriptPublicKey>(d => d.OutputId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OutputScriptPublicKey_Outputs");
            });

            modelBuilder.Entity<OutputScriptPublicKeyAddress>(entity =>
            {
                entity.ToTable("OutputScriptPublicKeyAddresses", "tables");

                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Output)
                    .WithMany(p => p.OutputScriptPublicKeyAddresses)
                    .HasForeignKey(d => d.OutputId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OutputScriptPublicKeyAddresses_OutputScriptPublicKey");
            });

            modelBuilder.Entity<Output>(entity =>
            {
                entity.ToTable("Outputs", "tables");

                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Value).HasColumnType("decimal(28, 8)");
            });

            modelBuilder.Entity<TransactionInput>(entity =>
            {
                entity.HasKey(e => new { e.TransactionId, e.InputId });

                entity.ToTable("TransactionInputs", "tables");

                entity.Property(e => e.TransactionId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.Input)
                    .WithMany(p => p.TransactionInputs)
                    .HasForeignKey(d => d.InputId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionInputs_Inputs");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.TransactionInputs)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionInputs_Transactions");
            });

            modelBuilder.Entity<TransactionOutput>(entity =>
            {
                entity.HasKey(e => new { e.TransactionId, e.OutputId });

                entity.ToTable("TransactionOutputs", "tables");

                entity.HasIndex(e => new { e.TransactionId, e.OutputIndex })
                    .HasName("UQ__Transact__DBC09B80A3078AF7")
                    .IsUnique();

                entity.Property(e => e.TransactionId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.Output)
                    .WithMany(p => p.TransactionOutputs)
                    .HasForeignKey(d => d.OutputId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionOutputs_Outputs");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.TransactionOutputs)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionOutputs_Transactions");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transactions", "tables");

                entity.Property(e => e.Id)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.BlockId)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.BlockOrder)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Vsize).HasColumnName("VSize");

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.BlockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_Blocks");
            });
        }
    }
}
