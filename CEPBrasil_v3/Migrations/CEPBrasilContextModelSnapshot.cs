using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CEPBrasil_v3;

namespace CEPBrasil_v3.Migrations
{
    [DbContext(typeof(CEPBrasilContext))]
    partial class CEPBrasilContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CEPBrasil_v3.Bairro", b =>
                {
                    b.Property<int>("Id_Estado");

                    b.Property<int>("Id_Cidade");

                    b.Property<int>("Id_Bairro")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("Id_Estado", "Id_Cidade", "Id_Bairro");

                    b.ToTable("Bairros");
                });

            modelBuilder.Entity("CEPBrasil_v3.Cidade", b =>
                {
                    b.Property<int>("Id_Estado");

                    b.Property<int>("Id_Cidade")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("Id_Estado", "Id_Cidade");

                    b.ToTable("Cidades");
                });

            modelBuilder.Entity("CEPBrasil_v3.Estado", b =>
                {
                    b.Property<int>("Id_Estado")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UF");

                    b.HasKey("Id_Estado");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("CEPBrasil_v3.Logradouro", b =>
                {
                    b.Property<int>("Id_Estado");

                    b.Property<int>("Id_Cidade");

                    b.Property<int>("Id_Bairro");

                    b.Property<int>("Id_Logradouro")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CEP");

                    b.Property<string>("Nome");

                    b.HasKey("Id_Estado", "Id_Cidade", "Id_Bairro", "Id_Logradouro");

                    b.ToTable("Logradouros");
                });

            modelBuilder.Entity("CEPBrasil_v3.Bairro", b =>
                {
                    b.HasOne("CEPBrasil_v3.Cidade", "cidade")
                        .WithMany("Bairros")
                        .HasForeignKey("Id_Estado", "Id_Cidade")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CEPBrasil_v3.Cidade", b =>
                {
                    b.HasOne("CEPBrasil_v3.Estado", "Estado")
                        .WithMany("Cidades")
                        .HasForeignKey("Id_Estado")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CEPBrasil_v3.Logradouro", b =>
                {
                    b.HasOne("CEPBrasil_v3.Bairro", "bairro")
                        .WithMany("Logradouros")
                        .HasForeignKey("Id_Estado", "Id_Cidade", "Id_Bairro")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
