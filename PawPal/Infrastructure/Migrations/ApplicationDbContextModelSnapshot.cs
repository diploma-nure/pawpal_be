﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("pet_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer")
                        .HasColumnName("age");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("Gender")
                        .HasColumnType("integer")
                        .HasColumnName("gender");

                    b.Property<bool>("HasSpecialNeeds")
                        .HasColumnType("boolean")
                        .HasColumnName("has_special_needs");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<List<string>>("PicturesUrls")
                        .HasColumnType("jsonb")
                        .HasColumnName("pictures_urls");

                    b.Property<int>("Size")
                        .HasColumnType("integer")
                        .HasColumnName("size");

                    b.Property<int>("Species")
                        .HasColumnType("integer")
                        .HasColumnName("species");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("PK_pets");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.PetFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("pet_feature_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Feature")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("feature");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_pet_features");

                    b.ToTable("pet_features", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.PetLike", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("PetId")
                        .HasColumnType("integer")
                        .HasColumnName("pet_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("UserId", "PetId")
                        .HasName("PK_pet_likes");

                    b.HasIndex("PetId");

                    b.ToTable("pet_likes", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("survey_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("OwnerDetailsId")
                        .HasColumnType("integer")
                        .HasColumnName("owner_details_id");

                    b.Property<int>("PetPreferencesId")
                        .HasColumnType("integer")
                        .HasColumnName("pet_preferences_id");

                    b.Property<int>("ResidenceDetailsId")
                        .HasColumnType("integer")
                        .HasColumnName("residence_details_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<string>("VacationPetCarePlan")
                        .HasColumnType("text")
                        .HasColumnName("vacation_pet_care_plan");

                    b.HasKey("Id")
                        .HasName("PK_surveys");

                    b.HasIndex("OwnerDetailsId");

                    b.HasIndex("PetPreferencesId")
                        .IsUnique();

                    b.HasIndex("ResidenceDetailsId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("surveys", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.SurveyOwnerDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("survey_owner_details_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<bool>("HasOwnnedPetsBefore")
                        .HasColumnType("boolean")
                        .HasColumnName("has_owned_pets_before");

                    b.Property<bool>("HasSufficientFinancialResources")
                        .HasColumnType("boolean")
                        .HasColumnName("has_sufficient_financial_resources");

                    b.Property<bool>("UnderstandsResponsibility")
                        .HasColumnType("boolean")
                        .HasColumnName("understands_responsibility");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("PK_surveys_owner_details");

                    b.ToTable("surveys_owner_details", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.SurveyPetPreferences", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("survey_pet_preferences_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("DesiredActivityLevel")
                        .HasColumnType("integer")
                        .HasColumnName("desired_activity_level");

                    b.Property<List<PetAge>>("PreferredAges")
                        .HasColumnType("jsonb")
                        .HasColumnName("preferred_ages");

                    b.Property<List<PetGender>>("PreferredGenders")
                        .HasColumnType("jsonb")
                        .HasColumnName("preferred_genders");

                    b.Property<List<PetSize>>("PreferredSizes")
                        .HasColumnType("jsonb")
                        .HasColumnName("preferred_sizes");

                    b.Property<List<PetSpecies>>("PreferredSpecies")
                        .HasColumnType("jsonb")
                        .HasColumnName("preferred_species");

                    b.Property<bool>("ReadyForSpecialNeedsPet")
                        .HasColumnType("boolean")
                        .HasColumnName("ready_for_special_needs_pet");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("PK_surveys_pet_preferences");

                    b.ToTable("surveys_pet_preferences", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.SurveyResidenceDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("survey_residence_details_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<bool>("HasOtherPets")
                        .HasColumnType("boolean")
                        .HasColumnName("has_other_pets");

                    b.Property<bool>("HasSafeWalkingArea")
                        .HasColumnType("boolean")
                        .HasColumnName("has_safe_walking_area");

                    b.Property<bool>("HasSmallChildren")
                        .HasColumnType("boolean")
                        .HasColumnName("has_small_children");

                    b.Property<int>("PetsAllowedAtResidence")
                        .HasColumnType("integer")
                        .HasColumnName("pets_allowed_at_residence");

                    b.Property<int>("PlaceOfResidence")
                        .HasColumnType("integer")
                        .HasColumnName("place_of_residence");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("PK_surveys_residence_details");

                    b.ToTable("surveys_residence_details", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.TestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("test_entity_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("PK_test_entities");

                    b.ToTable("test_entities", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .HasColumnType("text")
                        .HasColumnName("full_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("ProfilePictureUrl")
                        .HasColumnType("text")
                        .HasColumnName("profile_picture_url");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("PK_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("pets_pet_features", b =>
                {
                    b.Property<int>("pet_id")
                        .HasColumnType("integer");

                    b.Property<int>("pet_feature_id")
                        .HasColumnType("integer");

                    b.HasKey("pet_id", "pet_feature_id");

                    b.HasIndex("pet_feature_id");

                    b.ToTable("pets_pet_features");
                });

            modelBuilder.Entity("surveys_pet_preferences_pet_features", b =>
                {
                    b.Property<int>("survey_pet_preferences_id")
                        .HasColumnType("integer");

                    b.Property<int>("pet_feature_id")
                        .HasColumnType("integer");

                    b.HasKey("survey_pet_preferences_id", "pet_feature_id");

                    b.HasIndex("pet_feature_id");

                    b.ToTable("surveys_pet_preferences_pet_features");
                });

            modelBuilder.Entity("Domain.Entities.PetLike", b =>
                {
                    b.HasOne("Domain.Entities.Pet", "Pet")
                        .WithMany("PetLikes")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("PetLikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Survey", b =>
                {
                    b.HasOne("Domain.Entities.SurveyOwnerDetails", "OwnerDetails")
                        .WithMany("Surveys")
                        .HasForeignKey("OwnerDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.SurveyPetPreferences", "PetPreferences")
                        .WithOne("Survey")
                        .HasForeignKey("Domain.Entities.Survey", "PetPreferencesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.SurveyResidenceDetails", "ResidenceDetails")
                        .WithMany("Surveys")
                        .HasForeignKey("ResidenceDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithOne("Survey")
                        .HasForeignKey("Domain.Entities.Survey", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OwnerDetails");

                    b.Navigation("PetPreferences");

                    b.Navigation("ResidenceDetails");

                    b.Navigation("User");
                });

            modelBuilder.Entity("pets_pet_features", b =>
                {
                    b.HasOne("Domain.Entities.PetFeature", null)
                        .WithMany()
                        .HasForeignKey("pet_feature_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_pets_pet_features_pet_feature_id");

                    b.HasOne("Domain.Entities.Pet", null)
                        .WithMany()
                        .HasForeignKey("pet_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_pets_pet_features_pet_id");
                });

            modelBuilder.Entity("surveys_pet_preferences_pet_features", b =>
                {
                    b.HasOne("Domain.Entities.PetFeature", null)
                        .WithMany()
                        .HasForeignKey("pet_feature_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_surveys_pet_preferences_pet_features_pet_feature_id");

                    b.HasOne("Domain.Entities.SurveyPetPreferences", null)
                        .WithMany()
                        .HasForeignKey("survey_pet_preferences_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_surveys_pet_preferences_pet_features_survey_pet_preferences_id");
                });

            modelBuilder.Entity("Domain.Entities.Pet", b =>
                {
                    b.Navigation("PetLikes");
                });

            modelBuilder.Entity("Domain.Entities.SurveyOwnerDetails", b =>
                {
                    b.Navigation("Surveys");
                });

            modelBuilder.Entity("Domain.Entities.SurveyPetPreferences", b =>
                {
                    b.Navigation("Survey")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.SurveyResidenceDetails", b =>
                {
                    b.Navigation("Surveys");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("PetLikes");

                    b.Navigation("Survey")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
