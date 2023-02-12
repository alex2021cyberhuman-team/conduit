﻿// <auto-generated />
using System;
using Conduit.Articles.DataAccessLayer.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Conduit.Articles.DataAccessLayer.Migrations
{
    [DbContext(typeof(ArticlesDbContext))]
    partial class ArticlesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ArticleDbModelAuthorDbModel", b =>
                {
                    b.Property<Guid>("FavoritersId")
                        .HasColumnType("uuid")
                        .HasColumnName("favoriters_id");

                    b.Property<Guid>("FavoritesId")
                        .HasColumnType("uuid")
                        .HasColumnName("favorites_id");

                    b.HasKey("FavoritersId", "FavoritesId")
                        .HasName("pk_author_favorite");

                    b.HasIndex("FavoritesId")
                        .HasDatabaseName("ix_author_favorite_favorites_id");

                    b.ToTable("author_favorite", (string)null);
                });

            modelBuilder.Entity("ArticleDbModelTagDbModel", b =>
                {
                    b.Property<Guid>("ArticlesId")
                        .HasColumnType("uuid")
                        .HasColumnName("articles_id");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uuid")
                        .HasColumnName("tags_id");

                    b.HasKey("ArticlesId", "TagsId")
                        .HasName("pk_tag_article");

                    b.HasIndex("TagsId")
                        .HasDatabaseName("ix_tag_article_tags_id");

                    b.ToTable("tag_article", (string)null);
                });

            modelBuilder.Entity("AuthorDbModelAuthorDbModel", b =>
                {
                    b.Property<Guid>("FollowedsId")
                        .HasColumnType("uuid")
                        .HasColumnName("followeds_id");

                    b.Property<Guid>("FollowersId")
                        .HasColumnType("uuid")
                        .HasColumnName("followers_id");

                    b.HasKey("FollowedsId", "FollowersId")
                        .HasName("pk_author_follower");

                    b.HasIndex("FollowersId")
                        .HasDatabaseName("ix_author_follower_followers_id");

                    b.ToTable("author_follower", (string)null);
                });

            modelBuilder.Entity("Conduit.Articles.DataAccessLayer.Models.ArticleDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("article_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("body");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("FavoritesCount")
                        .HasColumnType("integer")
                        .HasColumnName("favorites_count");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("slug");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_article");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_article_author_id");

                    b.HasIndex("CreatedAt")
                        .HasDatabaseName("ix_article_created_at");

                    b.HasIndex("Slug")
                        .IsUnique()
                        .HasDatabaseName("ix_article_slug");

                    b.ToTable("article", (string)null);
                });

            modelBuilder.Entity("Conduit.Articles.DataAccessLayer.Models.AuthorDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<string>("Bio")
                        .HasColumnType("text")
                        .HasColumnName("bio");

                    b.Property<string>("Image")
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_author");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasDatabaseName("ix_author_username");

                    b.ToTable("author", (string)null);
                });

            modelBuilder.Entity("Conduit.Articles.DataAccessLayer.Models.TagDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("tag_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_tag");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_tag_name");

                    b.ToTable("tag", (string)null);
                });

            modelBuilder.Entity("ArticleDbModelAuthorDbModel", b =>
                {
                    b.HasOne("Conduit.Articles.DataAccessLayer.Models.AuthorDbModel", null)
                        .WithMany()
                        .HasForeignKey("FavoritersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_author_favorite_author_favoriters_id");

                    b.HasOne("Conduit.Articles.DataAccessLayer.Models.ArticleDbModel", null)
                        .WithMany()
                        .HasForeignKey("FavoritesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_author_favorite_article_favorites_id");
                });

            modelBuilder.Entity("ArticleDbModelTagDbModel", b =>
                {
                    b.HasOne("Conduit.Articles.DataAccessLayer.Models.ArticleDbModel", null)
                        .WithMany()
                        .HasForeignKey("ArticlesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tag_article_article_articles_id");

                    b.HasOne("Conduit.Articles.DataAccessLayer.Models.TagDbModel", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tag_article_tag_tags_id");
                });

            modelBuilder.Entity("AuthorDbModelAuthorDbModel", b =>
                {
                    b.HasOne("Conduit.Articles.DataAccessLayer.Models.AuthorDbModel", null)
                        .WithMany()
                        .HasForeignKey("FollowedsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_author_follower_author_followeds_id");

                    b.HasOne("Conduit.Articles.DataAccessLayer.Models.AuthorDbModel", null)
                        .WithMany()
                        .HasForeignKey("FollowersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_author_follower_author_followers_id");
                });

            modelBuilder.Entity("Conduit.Articles.DataAccessLayer.Models.ArticleDbModel", b =>
                {
                    b.HasOne("Conduit.Articles.DataAccessLayer.Models.AuthorDbModel", "Author")
                        .WithMany("Articles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_article_author_author_id");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Conduit.Articles.DataAccessLayer.Models.AuthorDbModel", b =>
                {
                    b.Navigation("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}
