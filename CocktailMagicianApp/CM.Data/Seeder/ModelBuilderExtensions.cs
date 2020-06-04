using System;
using System.Collections.Generic;
using System.Text;
using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CM.Data.Seeder
{
	public static class ModelBuilderExtensions
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Country>().HasData(
				new Country
				{
					Id = new Guid("4828b9db-cd3a-487f-9782-7a23653ff99a"),
					Name = "USA"
				},

				new Country
				{
					Id = new Guid("4ecac9dc-31df-4b89-93b5-5550d2608ede"),
					Name = "UK"
				});

			modelBuilder.Entity<City>().HasData(
				new City
				{
					Id = new Guid("320b050b-82f1-494c-9add-91ab28bf98dd"),
					Name = "New York",
					CountryId = new Guid("4828b9db-cd3a-487f-9782-7a23653ff99a")
				},

				new City
				{
					Id = new Guid("0eea5eb5-6151-41be-ac73-b35e056ca97e"),
					Name = "London",
					CountryId = new Guid("4ecac9dc-31df-4b89-93b5-5550d2608ede")
				});


			modelBuilder.Entity<Address>().HasData(
				new Address
				{
					Id = new Guid("73f2c4c2-78ae-45ab-b82c-b06a48271a6d"),
					CityId = new Guid("320b050b-82f1-494c-9add-91ab28bf98dd"),
					Street = "79-81 MACDOUGAL ST",
					BarId = new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
				},

				new Address
				{
					Id = new Guid("dfaa43d9-d6d8-4a15-bda9-48823fa4b886"),
					CityId = new Guid("0eea5eb5-6151-41be-ac73-b35e056ca97e"),
					Street = "Mayfair W1K 2AL",
					BarId = new Guid("0899e918-977c-44d5-a5cb-de9559ad822c"),
				});

			modelBuilder.Entity<AppUser>().HasData(
				new AppUser
				{
					Id = new Guid("98190af6-ba8e-44ff-8619-4d3b90040b5b"),
					UserName = "testUser@test.com",
				},
				new AppUser
				{
					Id = new Guid("e355f8c4-ee01-4986-89bb-d1b56d17ae23"),
					UserName = "testUser1@test.com",
				});

			modelBuilder.Entity<BarRating>().HasData(
				new BarRating
				{
					BarId = Guid.Parse("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
					AppUserId =Guid.Parse("98190af6-ba8e-44ff-8619-4d3b90040b5b"),
					Score = 5
				},
				new BarRating
				{
					BarId = Guid.Parse("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
					AppUserId = Guid.Parse("e355f8c4-ee01-4986-89bb-d1b56d17ae23"),
					Score = 1
				});

			modelBuilder.Entity<Bar>().HasData(
				new Bar
				{
					Id = new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
					Name = "Dante",
					Phone = "(212) 982-5275",
					Details = "Weaving tradition with modernity, there’s something heart-warming about the story of Dante. When Linden Pride, Nathalie Hudson and Naren Young took over this Greenwich Village site 100 years after it first opened they could see the things that made this fading Italian café once great could be relevant again. At the heart of their mission was to renew the bar, while being authentic to its roots and appealing to the Greenwich Village community. So the classical décor was given a lift, and in came refined but wholesome Italian food, aperitivos and cocktails. There is a whole list of Negronis to make your way through, but that’s OK because Dante is an all-day restaurant-bar. The Garibaldi too is a must-order. Made with Campari and ‘fluffy’ orange juice, it has brought this once-dusty drink back to life. The measure of a bar is the experience of its customers – in hospitality, drinks and food Dante has the fundamentals down to a fine art, earning the deserved title of The World's Best Bar 2019, sponsored by Perrier.",
					AddressID = new Guid("73f2c4c2-78ae-45ab-b82c-b06a48271a6d"),
					ImagePath = "/images/9BDBF5E7-AD83-415C-B359-9FF5E2F0DEDD.jpg"
				},

				new Bar
				{
					Id = new Guid("0899e918-977c-44d5-a5cb-de9559ad822c"),
					Name = "Connaught Bar",
					Phone = "+44 (0)20 7499 7070",
					Details = "No matter the workings of the cocktail world around it, the Connaught Bar stays true to its principles – artful drinks and graceful service in a stylish setting. Under the watchful gaze of Ago Perrone, the hotel’s director of mixology, the bar moves forward with an effortless glide. Last year marked 10 years since designer David Collins unveiled the bar’s elegant Cubist interior and in celebration it launched its own gin, crafted in the building by none other than Perrone himself. It’s already the most called-for spirit on the showpiece trolley that clinks between the bar’s discerning guests, serving personalised Martinis. The latest cocktail menu, Vanguard, has upped the invention – Number 11 is an embellished Vesper served in Martini glasses hand-painted every day in house, while the Gate No.1 is a luscious blend of spirits, wines and jam. But of course, the Connaught Masterpieces isn’t a chapter easily overlooked. Along with the Dry Martini, the Bloody Mary is liquid perfection and the Mulatta Daisy is Perrone’s own classic, in and out of the bar. In 2019, Connaught Bar earns the title of The Best Bar in Europe, sponsored by Michter's.",
					AddressID = new Guid("4ecac9dc-31df-4b89-93b5-5550d2608ede"),
					ImagePath = "/images/0899E918-977C-44D5-A5CB-DE9559AD822C-logo.png"
				});

			modelBuilder.Entity<Cocktail>().HasData(
				new Cocktail
				{
					Id = new Guid("a3fd2a00-52c4-4293-a184-6f448d008015"),
					Name = "Loch Lomond",
				},

				new Cocktail
				{
					Id = new Guid("347e304b-03cd-414f-91b2-faed4fdb86e9"),
					Name = "Strawberry Lemonade",
				},

				new Cocktail
				{
					Id = new Guid("3f088822-fa2c-45f1-aa96-067f07aa04ea"),
					Name = "Rum Milk Punch",
				});


			modelBuilder.Entity<BarCocktail>().HasData(
				new BarCocktail
				{
					BarId = new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
					CocktailId = new Guid("a3fd2a00-52c4-4293-a184-6f448d008015"),
				},

				new BarCocktail
				{
					BarId = new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
					CocktailId = new Guid("347e304b-03cd-414f-91b2-faed4fdb86e9"),
				},

				new BarCocktail
				{
					BarId = new Guid("0899e918-977c-44d5-a5cb-de9559ad822c"),
					CocktailId = new Guid("3f088822-fa2c-45f1-aa96-067f07aa04ea"),
				});

			modelBuilder.Entity<AppRole>().HasData(
				new AppRole { Id = new Guid("a6dc0db8-408c-4aff-bf99-0d46efd31787"), Name = "Admin", NormalizedName = "ADMIN" },
				new AppRole { Id = new Guid ("acde9ca2-de8c-45a0-ad81-3c3b05c8c90e"), Name = "Member", NormalizedName = "MEMBER" }
			);
		}
	}
}
