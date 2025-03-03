﻿using LeQuangTrungMVC.BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LeQuangTrungMVC.DataAccessObjects
{
    public partial class FunewsManagementContext : DbContext
    {
        public FunewsManagementContext()
        {
        }

        public FunewsManagementContext(DbContextOptions<FunewsManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<NewsArticle> NewsArticles { get; set; }

        public virtual DbSet<SystemAccount> SystemAccounts { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        private string? GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();

            var str = configuration.GetConnectionString("FUNewsManagementSystem");

            return str;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>(entity =>
            //{
            //    entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B8AFE0FEB");

            //    entity.ToTable("Category");

            //    entity.HasIndex(e => e.ParentCategoryId, "IX_Category_ParentCategoryID");

            //    entity.Property(e => e.CategoryId)
            //        .HasDefaultValueSql("(newid())")
            //        .HasColumnName("CategoryID");
            //    entity.Property(e => e.CategoryDescription).HasColumnType("text");
            //    entity.Property(e => e.CategoryName).HasMaxLength(150);
            //    entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");

            //    entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
            //        .HasForeignKey(d => d.ParentCategoryId)
            //        .HasConstraintName("FK_Category_ParentCategoryID");
            //});

            //modelBuilder.Entity<NewsArticle>(entity =>
            //{
            //    entity.HasKey(e => e.NewsArticleId).HasName("PK__NewsArti__4CD0926C20CC2030");

            //    entity.ToTable("NewsArticle");

            //    entity.HasIndex(e => e.CategoryId, "IX_NewsArticle_CategoryID");

            //    entity.HasIndex(e => e.CreatedById, "IX_NewsArticle_CreatedByID");

            //    entity.HasIndex(e => e.UpdatedById, "IX_NewsArticle_UpdatedByID");

            //    entity.Property(e => e.NewsArticleId)
            //        .HasDefaultValueSql("(newid())")
            //        .HasColumnName("NewsArticleID");
            //    entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            //    entity.Property(e => e.CreatedById).HasColumnName("CreatedByID");
            //    entity.Property(e => e.CreatedDate)
            //        .HasDefaultValueSql("(getdate())")
            //        .HasColumnType("datetime");
            //    entity.Property(e => e.Headline).HasMaxLength(150);
            //    entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            //    entity.Property(e => e.NewsContent).HasColumnType("text");
            //    entity.Property(e => e.NewsSource).HasMaxLength(250);
            //    entity.Property(e => e.NewsTile).HasMaxLength(150);
            //    entity.Property(e => e.UpdatedById).HasColumnName("UpdatedByID");

            //    entity.HasOne(d => d.Category).WithMany(p => p.NewsArticles)
            //        .HasForeignKey(d => d.CategoryId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_NewsArticle_CategoryID");

            //    entity.HasOne(d => d.CreatedBy).WithMany(p => p.NewsArticleCreatedBies)
            //        .HasForeignKey(d => d.CreatedById)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_NewsArticle_CreatedByID");

            //    entity.HasOne(d => d.UpdatedBy).WithMany(p => p.NewsArticleUpdatedBies)
            //        .HasForeignKey(d => d.UpdatedById)
            //        .HasConstraintName("FK_NewsArticle_UpdatedByID");

            //    entity.HasMany(d => d.Tags).WithMany(p => p.NewsArticles)
            //        .UsingEntity<Dictionary<string, object>>(
            //            "NewsTag",
            //            r => r.HasOne<Tag>().WithMany()
            //                .HasForeignKey("TagId")
            //                .OnDelete(DeleteBehavior.ClientSetNull)
            //                .HasConstraintName("FK_NewsTag_TagID"),
            //            l => l.HasOne<NewsArticle>().WithMany()
            //                .HasForeignKey("NewsArticleId")
            //                .OnDelete(DeleteBehavior.Cascade)
            //                .HasConstraintName("FK_NewsTag_NewsArticleID"),
            //            j =>
            //            {
            //                j.HasKey("NewsArticleId", "TagId");
            //                j.ToTable("NewsTag");
            //                j.HasIndex(new[] { "TagId" }, "IX_NewsTag_TagID");
            //                j.IndexerProperty<Guid>("NewsArticleId").HasColumnName("NewsArticleID");
            //                j.IndexerProperty<Guid>("TagId").HasColumnName("TagID");
            //            });
            //});



            //modelBuilder.Entity<SystemAccount>(entity =>
            //{
            //    entity.HasKey(e => e.AccountId).HasName("PK__SystemAc__349DA586CF6622FA");

            //    entity.ToTable("SystemAccount");

            //    entity.HasIndex(e => e.AccountEmail, "UQ__SystemAc__FC770D33B3276155").IsUnique();

            //    entity.Property(e => e.AccountId)
            //        .HasDefaultValueSql("(newid())")
            //        .HasColumnName("AccountID");
            //    entity.Property(e => e.AccountEmail).HasMaxLength(150);
            //    entity.Property(e => e.AccountName).HasMaxLength(150);
            //    entity.Property(e => e.AccountPassword).HasMaxLength(255);
            //});

            //modelBuilder.Entity<Tag>(entity =>
            //{
            //    entity.HasKey(e => e.TagId).HasName("PK__Tag__657CFA4CFB003B55");

            //    entity.ToTable("Tag");

            //    entity.Property(e => e.TagId)
            //        .HasDefaultValueSql("(newid())")
            //        .HasColumnName("TagID");
            //    entity.Property(e => e.Note).HasColumnType("text");
            //    entity.Property(e => e.TagName).HasMaxLength(150);
            //});

            //OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B8AFE0FEB");

                entity.ToTable("Category");

                entity.HasIndex(e => e.ParentCategoryId, "IX_Category_ParentCategoryID");

                entity.Property(e => e.CategoryId)
                    .HasDefaultValueSql("(newid())")
                    .HasColumnName("CategoryID");
                entity.Property(e => e.CategoryDescription).HasColumnType("text");
                entity.Property(e => e.CategoryName).HasMaxLength(150);
                entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");

                entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                    .HasForeignKey(d => d.ParentCategoryId)
                    .HasConstraintName("FK_Category_ParentCategoryID");
            });

            modelBuilder.Entity<NewsArticle>(entity =>
            {
                entity.HasKey(e => e.NewsArticleId).HasName("PK__NewsArti__4CD0926C20CC2030");

                entity.ToTable("NewsArticle");

                entity.HasIndex(e => e.CategoryId, "IX_NewsArticle_CategoryID");

                entity.HasIndex(e => e.CreatedById, "IX_NewsArticle_CreatedByID");

                entity.HasIndex(e => e.UpdatedById, "IX_NewsArticle_UpdatedByID");

                entity.Property(e => e.NewsArticleId)
                    .HasDefaultValueSql("(newid())")
                    .HasColumnName("NewsArticleID");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
                entity.Property(e => e.CreatedById).HasColumnName("CreatedByID");
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Headline).HasMaxLength(150);
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
                entity.Property(e => e.NewsContent).HasColumnType("text");
                entity.Property(e => e.NewsSource).HasMaxLength(250);
                entity.Property(e => e.NewsTile).HasMaxLength(150);
                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedByID");

                entity.HasOne(d => d.Category).WithMany(p => p.NewsArticles)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NewsArticle_CategoryID");

                entity.HasOne(d => d.CreatedBy).WithMany(p => p.NewsArticleCreatedBies)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NewsArticle_CreatedByID");

                entity.HasOne(d => d.UpdatedBy).WithMany(p => p.NewsArticleUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById)
                    .HasConstraintName("FK_NewsArticle_UpdatedByID");

                entity.HasMany(d => d.Tags).WithMany(p => p.NewsArticles)
                    .UsingEntity<Dictionary<string, object>>(
                        "NewsTag",
                        r => r.HasOne<Tag>().WithMany()
                            .HasForeignKey("TagId")
                            .OnDelete(DeleteBehavior.Cascade) // Ensure cascade delete
                            .HasConstraintName("FK_NewsTag_TagID"),
                        l => l.HasOne<NewsArticle>().WithMany()
                            .HasForeignKey("NewsArticleId")
                            .OnDelete(DeleteBehavior.Cascade) // Ensure cascade delete
                            .HasConstraintName("FK_NewsTag_NewsArticleID"),
                        j =>
                        {
                            j.HasKey("NewsArticleId", "TagId");
                            j.ToTable("NewsTag");
                            j.HasIndex(new[] { "TagId" }, "IX_NewsTag_TagID");
                            j.IndexerProperty<Guid>("NewsArticleId").HasColumnName("NewsArticleID");
                            j.IndexerProperty<Guid>("TagId").HasColumnName("TagID");
                        });
            });

            modelBuilder.Entity<SystemAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId).HasName("PK__SystemAc__349DA586CF6622FA");

                entity.ToTable("SystemAccount");

                entity.HasIndex(e => e.AccountEmail, "UQ__SystemAc__FC770D33B3276155").IsUnique();

                entity.Property(e => e.AccountId)
                    .HasDefaultValueSql("(newid())")
                    .HasColumnName("AccountID");
                entity.Property(e => e.AccountEmail).HasMaxLength(150);
                entity.Property(e => e.AccountName).HasMaxLength(150);
                entity.Property(e => e.AccountPassword).HasMaxLength(255);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.TagId).HasName("PK__Tag__657CFA4CFB003B55");

                entity.ToTable("Tag");

                entity.Property(e => e.TagId)
                    .HasDefaultValueSql("(newid())")
                    .HasColumnName("TagID");
                entity.Property(e => e.Note).HasColumnType("text");
                entity.Property(e => e.TagName).HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
