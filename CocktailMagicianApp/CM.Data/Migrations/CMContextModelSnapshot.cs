﻿// <auto-generated />
using System;
using CM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CM.Data.Migrations
{
    [DbContext(typeof(CMContext))]
    partial class CMContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CM.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BarId")
                        .IsUnique();

                    b.HasIndex("CityId");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("73f2c4c2-78ae-45ab-b82c-b06a48271a6d"),
                            BarId = new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
                            CityId = new Guid("320b050b-82f1-494c-9add-91ab28bf98dd"),
                            Street = "79-81 MACDOUGAL ST"
                        },
                        new
                        {
                            Id = new Guid("dfaa43d9-d6d8-4a15-bda9-48823fa4b886"),
                            BarId = new Guid("0899e918-977c-44d5-a5cb-de9559ad822c"),
                            CityId = new Guid("0eea5eb5-6151-41be-ac73-b35e056ca97e"),
                            Street = "Mayfair W1K 2AL"
                        });
                });

            modelBuilder.Entity("CM.Models.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a6dc0db8-408c-4aff-bf99-0d46efd31787"),
                            ConcurrencyStamp = "3acc5e71-bf16-40e0-a9a3-7a30b02628bd",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("acde9ca2-de8c-45a0-ad81-3c3b05c8c90e"),
                            ConcurrencyStamp = "99c5fee4-bbf3-4b3c-b5ad-1db0698c9523",
                            Name = "Member",
                            NormalizedName = "MEMBER"
                        });
                });

            modelBuilder.Entity("CM.Models.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("98190af6-ba8e-44ff-8619-4d3b90040b5b"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ba34caf3-d927-45f7-80c2-6a27de5ef690",
                            EmailConfirmed = false,
                            IsDeleted = false,
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            UserName = "testUser@test.com"
                        },
                        new
                        {
                            Id = new Guid("e355f8c4-ee01-4986-89bb-d1b56d17ae23"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f43f75d3-df00-4c5c-ba62-eef65cb445fe",
                            EmailConfirmed = false,
                            IsDeleted = false,
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            UserName = "testUser1@test.com"
                        });
                });

            modelBuilder.Entity("CM.Models.Bar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("AverageRating")
                        .HasColumnType("float");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsUnlisted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bars");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
                            AddressID = new Guid("73f2c4c2-78ae-45ab-b82c-b06a48271a6d"),
                            Details = "Weaving tradition with modernity, there’s something heart-warming about the story of Dante. When Linden Pride, Nathalie Hudson and Naren Young took over this Greenwich Village site 100 years after it first opened they could see the things that made this fading Italian café once great could be relevant again. At the heart of their mission was to renew the bar, while being authentic to its roots and appealing to the Greenwich Village community. So the classical décor was given a lift, and in came refined but wholesome Italian food, aperitivos and cocktails. There is a whole list of Negronis to make your way through, but that’s OK because Dante is an all-day restaurant-bar. The Garibaldi too is a must-order. Made with Campari and ‘fluffy’ orange juice, it has brought this once-dusty drink back to life. The measure of a bar is the experience of its customers – in hospitality, drinks and food Dante has the fundamentals down to a fine art, earning the deserved title of The World's Best Bar 2019, sponsored by Perrier.",
                            IsUnlisted = false,
                            Name = "Dante",
                            Phone = "(212) 982-5275"
                        },
                        new
                        {
                            Id = new Guid("0899e918-977c-44d5-a5cb-de9559ad822c"),
                            AddressID = new Guid("4ecac9dc-31df-4b89-93b5-5550d2608ede"),
                            Details = "No matter the workings of the cocktail world around it, the Connaught Bar stays true to its principles – artful drinks and graceful service in a stylish setting. Under the watchful gaze of Ago Perrone, the hotel’s director of mixology, the bar moves forward with an effortless glide. Last year marked 10 years since designer David Collins unveiled the bar’s elegant Cubist interior and in celebration it launched its own gin, crafted in the building by none other than Perrone himself. It’s already the most called-for spirit on the showpiece trolley that clinks between the bar’s discerning guests, serving personalised Martinis. The latest cocktail menu, Vanguard, has upped the invention – Number 11 is an embellished Vesper served in Martini glasses hand-painted every day in house, while the Gate No.1 is a luscious blend of spirits, wines and jam. But of course, the Connaught Masterpieces isn’t a chapter easily overlooked. Along with the Dry Martini, the Bloody Mary is liquid perfection and the Mulatta Daisy is Perrone’s own classic, in and out of the bar. In 2019, Connaught Bar earns the title of The Best Bar in Europe, sponsored by Michter's.",
                            IsUnlisted = false,
                            Name = "Connaught Bar",
                            Phone = "+44 (0)20 7499 7070"
                        });
                });

            modelBuilder.Entity("CM.Models.BarCocktail", b =>
                {
                    b.Property<Guid>("CocktailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BarId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CocktailId", "BarId");

                    b.HasIndex("BarId");

                    b.ToTable("BarCocktails");

                    b.HasData(
                        new
                        {
                            CocktailId = new Guid("a3fd2a00-52c4-4293-a184-6f448d008015"),
                            BarId = new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd")
                        },
                        new
                        {
                            CocktailId = new Guid("347e304b-03cd-414f-91b2-faed4fdb86e9"),
                            BarId = new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd")
                        },
                        new
                        {
                            CocktailId = new Guid("3f088822-fa2c-45f1-aa96-067f07aa04ea"),
                            BarId = new Guid("0899e918-977c-44d5-a5cb-de9559ad822c")
                        });
                });

            modelBuilder.Entity("CM.Models.BarComment", b =>
                {
                    b.Property<Guid>("BarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CommentedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("BarId", "AppUserId");

                    b.HasIndex("AppUserId");

                    b.ToTable("BarComments");
                });

            modelBuilder.Entity("CM.Models.BarRating", b =>
                {
                    b.Property<Guid>("BarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("BarId", "AppUserId");

                    b.HasIndex("AppUserId");

                    b.ToTable("BarRatings");

                    b.HasData(
                        new
                        {
                            BarId = new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
                            AppUserId = new Guid("98190af6-ba8e-44ff-8619-4d3b90040b5b"),
                            Score = 5
                        },
                        new
                        {
                            BarId = new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
                            AppUserId = new Guid("e355f8c4-ee01-4986-89bb-d1b56d17ae23"),
                            Score = 1
                        });
                });

            modelBuilder.Entity("CM.Models.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("320b050b-82f1-494c-9add-91ab28bf98dd"),
                            CountryId = new Guid("4828b9db-cd3a-487f-9782-7a23653ff99a"),
                            Name = "New York"
                        },
                        new
                        {
                            Id = new Guid("0eea5eb5-6151-41be-ac73-b35e056ca97e"),
                            CountryId = new Guid("4ecac9dc-31df-4b89-93b5-5550d2608ede"),
                            Name = "London"
                        });
                });

            modelBuilder.Entity("CM.Models.Cocktail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("AverageRating")
                        .HasColumnType("float");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsUnlisted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Recipe")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cocktails");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a3fd2a00-52c4-4293-a184-6f448d008015"),
                            IsUnlisted = false,
                            Name = "Loch Lomond"
                        },
                        new
                        {
                            Id = new Guid("347e304b-03cd-414f-91b2-faed4fdb86e9"),
                            IsUnlisted = false,
                            Name = "Strawberry Lemonade"
                        },
                        new
                        {
                            Id = new Guid("3f088822-fa2c-45f1-aa96-067f07aa04ea"),
                            IsUnlisted = false,
                            Name = "Rum Milk Punch"
                        });
                });

            modelBuilder.Entity("CM.Models.CocktailComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CocktailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CommentedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CocktailId");

                    b.ToTable("CocktailComments");
                });

            modelBuilder.Entity("CM.Models.CocktailIngredient", b =>
                {
                    b.Property<Guid>("CocktailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IngredientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Ammount")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CocktailId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("CocktailIngredients");
                });

            modelBuilder.Entity("CM.Models.CocktailRating", b =>
                {
                    b.Property<Guid>("CocktailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("CocktailId", "AppUserId");

                    b.HasIndex("AppUserId");

                    b.ToTable("CocktailRatings");
                });

            modelBuilder.Entity("CM.Models.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4828b9db-cd3a-487f-9782-7a23653ff99a"),
                            Name = "USA"
                        },
                        new
                        {
                            Id = new Guid("4ecac9dc-31df-4b89-93b5-5550d2608ede"),
                            Name = "UK"
                        });
                });

            modelBuilder.Entity("CM.Models.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CM.Models.Address", b =>
                {
                    b.HasOne("CM.Models.Bar", "Bar")
                        .WithOne("Address")
                        .HasForeignKey("CM.Models.Address", "BarId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CM.Models.City", "City")
                        .WithMany("Adresses")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CM.Models.BarCocktail", b =>
                {
                    b.HasOne("CM.Models.Bar", "Bar")
                        .WithMany("Cocktails")
                        .HasForeignKey("BarId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CM.Models.Cocktail", "Cocktail")
                        .WithMany("Bars")
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CM.Models.BarComment", b =>
                {
                    b.HasOne("CM.Models.AppUser", "AppUser")
                        .WithMany("BarComments")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CM.Models.Bar", "Bar")
                        .WithMany("Comments")
                        .HasForeignKey("BarId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CM.Models.BarRating", b =>
                {
                    b.HasOne("CM.Models.AppUser", "AppUser")
                        .WithMany("BarRatings")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CM.Models.Bar", "Bar")
                        .WithMany("Ratings")
                        .HasForeignKey("BarId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CM.Models.City", b =>
                {
                    b.HasOne("CM.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CM.Models.CocktailComment", b =>
                {
                    b.HasOne("CM.Models.AppUser", "AppUser")
                        .WithMany("CocktailComments")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CM.Models.Cocktail", "Cocktail")
                        .WithMany("Comments")
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CM.Models.CocktailIngredient", b =>
                {
                    b.HasOne("CM.Models.Cocktail", "Cocktail")
                        .WithMany("Ingredients")
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CM.Models.Ingredient", "Ingredient")
                        .WithMany("Cocktails")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CM.Models.CocktailRating", b =>
                {
                    b.HasOne("CM.Models.AppUser", "AppUser")
                        .WithMany("CocktailRatings")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CM.Models.Cocktail", "Cocktail")
                        .WithMany("Ratings")
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("CM.Models.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("CM.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("CM.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("CM.Models.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CM.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("CM.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
