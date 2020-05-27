using CM.Data;
using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CM.ServicesTests
{
	public static class Utility
	{
		public static DbContextOptions<CMContext> GetOptions(string tempDbName)
		{
			return new DbContextOptionsBuilder<CMContext>()
				.UseInMemoryDatabase(tempDbName)
				.Options;
		}

		public static async Task ArrangeContextAsync(DbContextOptions options)
		{
			var user1 = new AppUser
			{
				Id = Guid.Parse("b8a3552e-f509-42f0-bed9-7c29c3dfc6b6")
			};

			var user2 = new AppUser
			{
				Id = Guid.Parse("b54a920d-0766-4532-8dd8-d98b1df79b37")
			};

			var cocktails = new List<Cocktail>
			{
				new Cocktail
				{
					Id = Guid.Parse("9b9f85e3-51be-4fbf-918a-9fbd89546ef7"),
					Name = "Cocktail A",
					Ratings = new List<CocktailRating>
					{
						new CocktailRating
						{
							 AppUser = user1,
							 Score = 5
						},
						new CocktailRating
						{
							 AppUser = user2,
							 Score = 4
						}
					},
					AverageRating = 9 / 2d
				},
				new Cocktail
				{
					Id = Guid.Parse("e8601248-4de3-4ccb-ab20-563926dedbd5"),
					Name = "Cocktail B",
					Ratings = new List<CocktailRating>
					{
						new CocktailRating
						{
							 AppUser = user1,
							 Score = 2
						},
						new CocktailRating
						{
							 AppUser = user2,
							 Score = 1
						}
					},
					AverageRating = 3 / 2d
				},
				new Cocktail // unlisted
                {
					Id = Guid.Parse("9344e67f-f9a9-45c3-b583-7378387bf862"),
					Name = "Cocktail C", // unlisted
                    Ratings = new List<CocktailRating>
					{
						new CocktailRating
						{
							 AppUser = user1,
							 Score = 3
						},
						new CocktailRating
						{
							 AppUser = user2,
							 Score = 0
						}
					},
					AverageRating = 1.5,
					IsUnlisted = true // unlisted
                },
			
                new Cocktail
                {
                    Id = Guid.Parse("5416ceee-839d-43e3-bb85-b292976c353e"),
                    Name = "Cocktail D",
                }
            };

			var ingredients = new List<Ingredient>
			{
				new Ingredient
				{
					Id = Guid.Parse("eb5d7135-f194-4443-a5ff-cc955396648e"),
					Name = "Ingredient A"
				},
				new Ingredient
				{
					Id = Guid.Parse("966528b9-0ab8-4330-8974-b1bb9709ae74"),
					Name = "Ingredient B"
				},
				new Ingredient
				{
                    // This one intentionally not used in any cocktail
                    Id = Guid.Parse("bce99872-9407-47a3-b3fc-50cb707cb19c"),
					Name = "Ingredient C"
				},
			};

			var cocktailIngredients = new List<CocktailIngredient>
			{
				new CocktailIngredient
				{
                    // Cocktail A has ingredient A
                    CocktailId = Guid.Parse("9b9f85e3-51be-4fbf-918a-9fbd89546ef7"),
					IngredientId = Guid.Parse("eb5d7135-f194-4443-a5ff-cc955396648e")
				},
				new CocktailIngredient
				{
                    // Cocktail B has ingredients A
                    CocktailId = Guid.Parse("e8601248-4de3-4ccb-ab20-563926dedbd5"),
					IngredientId = Guid.Parse("eb5d7135-f194-4443-a5ff-cc955396648e")
				},
				new CocktailIngredient
				{
                    // Cocktail B has ingredient B
                    CocktailId = Guid.Parse("e8601248-4de3-4ccb-ab20-563926dedbd5"),
					IngredientId = Guid.Parse("966528b9-0ab8-4330-8974-b1bb9709ae74")
				},
                // Cocktail C has no ingredients
            };

			var bars = new List<Bar>
			{
				new Bar
				{
					Id = new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
					Name = "Dante",
					Phone = "(212) 982-5275",
					Details = "Weaving tradition with modernity, there’s something heart-warming about the story of Dante. When Linden Pride, Nathalie Hudson and Naren Young took over this Greenwich Village site 100 years after it first opened they could see the things that made this fading Italian café once great could be relevant again. At the heart of their mission was to renew the bar, while being authentic to its roots and appealing to the Greenwich Village community. So the classical décor was given a lift, and in came refined but wholesome Italian food, aperitivos and cocktails. There is a whole list of Negronis to make your way through, but that’s OK because Dante is an all-day restaurant-bar. The Garibaldi too is a must-order. Made with Campari and ‘fluffy’ orange juice, it has brought this once-dusty drink back to life. The measure of a bar is the experience of its customers – in hospitality, drinks and food Dante has the fundamentals down to a fine art, earning the deserved title of The World's Best Bar 2019, sponsored by Perrier.",
					AddressID = new Guid("73f2c4c2-78ae-45ab-b82c-b06a48271a6d"),
					AverageRating = 3
				},

				new Bar
				{
					Id = new Guid("0899e918-977c-44d5-a5cb-de9559ad822c"),
					Name = "Connaught Bar",
					Phone = "+44 (0)20 7499 7070",
					Details = "No matter the workings of the cocktail world around it, the Connaught Bar stays true to its principles – artful drinks and graceful service in a stylish setting. Under the watchful gaze of Ago Perrone, the hotel’s director of mixology, the bar moves forward with an effortless glide. Last year marked 10 years since designer David Collins unveiled the bar’s elegant Cubist interior and in celebration it launched its own gin, crafted in the building by none other than Perrone himself. It’s already the most called-for spirit on the showpiece trolley that clinks between the bar’s discerning guests, serving personalised Martinis. The latest cocktail menu, Vanguard, has upped the invention – Number 11 is an embellished Vesper served in Martini glasses hand-painted every day in house, while the Gate No.1 is a luscious blend of spirits, wines and jam. But of course, the Connaught Masterpieces isn’t a chapter easily overlooked. Along with the Dry Martini, the Bloody Mary is liquid perfection and the Mulatta Daisy is Perrone’s own classic, in and out of the bar. In 2019, Connaught Bar earns the title of The Best Bar in Europe, sponsored by Michter's.",
					AddressID = new Guid("4ecac9dc-31df-4b89-93b5-5550d2608ede"),
					AverageRating = 2
				},

				new Bar
				{
					Id = new Guid("0899e918-977c-44d5-a5cb-de9559ad822a"),
					Name = "Test Bar1",
					Phone = "+555 555 555",
					Details = "Test Details",
					AddressID = new Guid("dfaa43d9-d6d8-4a15-bda9-48823fa4b882"),
					AverageRating = 5
				},
				 
                new Bar
                {
                    Id = Guid.Parse("a73c9de1-2498-4b58-b545-5bc74689160e"),
                    Name = "Bar A",
                },

                new Bar
                {
                    Id = Guid.Parse("de61e799-a312-4764-950a-dd7d97713412"),
                    Name = "Bar B",
                    IsUnlisted = true,
                }
            };

			var addresses = new List<Address>
			{
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
				},

				new Address
				{
					Id = new Guid("dfaa43d9-d6d8-4a15-bda9-48823fa4b882"),
					CityId = new Guid("0eea5eb5-6151-41be-ac73-b35e056ca97e"),
					Street = "Test Street",
					BarId = new Guid("0899e918-977c-44d5-a5cb-de9559ad822a"),
				}
		};

			var cities = new List<City>
			{
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
				}
			};

			var countries = new List<Country>
			{
				new Country
				{
					Id = new Guid("4828b9db-cd3a-487f-9782-7a23653ff99a"),
					Name = "USA"
				},

				new Country
				{
					Id = new Guid("4ecac9dc-31df-4b89-93b5-5550d2608ede"),
					Name = "UK"
				},

				new Country
				{
					Id = new Guid("fb4effe9-32c1-45fd-8fce-9b45259c7ff6"),
					Name = "Bulgaria"
				}
			};

			var barCocktails = new List<BarCocktail>
			{
				new BarCocktail
				{
					BarId = new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
					CocktailId = new Guid("9b9f85e3-51be-4fbf-918a-9fbd89546ef7"),
				},

				new BarCocktail
				{
					BarId = new Guid("0899e918-977c-44d5-a5cb-de9559ad822c"),
					CocktailId = new Guid("e8601248-4de3-4ccb-ab20-563926dedbd5"),
				},

				new BarCocktail
				{
					BarId = new Guid("0899e918-977c-44d5-a5cb-de9559ad822c"),
					CocktailId = new Guid("9344e67f-f9a9-45c3-b583-7378387bf862"),
				},

				new BarCocktail
                {
                    BarId = Guid.Parse("a73c9de1-2498-4b58-b545-5bc74689160e"), // Bar A
                    CocktailId = Guid.Parse("9b9f85e3-51be-4fbf-918a-9fbd89546ef7"), // Cocktail A
                },

                new BarCocktail
                {
                    BarId = Guid.Parse("de61e799-a312-4764-950a-dd7d97713412"), // Bar B
                    CocktailId = Guid.Parse("9b9f85e3-51be-4fbf-918a-9fbd89546ef7"), // Cocktail A
                }
			};

			var comments = new List<CocktailComment>
            {
                new CocktailComment
                {
                    Id = Guid.Parse("fb9ee201-c7fb-481f-a697-92f7d1c588f9"),
                    AppUser = user1,
                    CocktailId = Guid.Parse("9b9f85e3-51be-4fbf-918a-9fbd89546ef7"), // cocktail A
                    CommentedOn = DateTimeOffset.UtcNow,
                    Text = "Lorem ipsum 1",
                },

                new CocktailComment
                {
                    Id = Guid.Parse("8bcb165b-6bca-42ae-8700-85ce663b4b03"),
                    AppUser = user1,
                    CocktailId = Guid.Parse("9b9f85e3-51be-4fbf-918a-9fbd89546ef7"), // cocktail A
                    CommentedOn = DateTimeOffset.UtcNow,
                    Text = "Lorem ipsum 2",
                },
            };


			using var arrangeContext = new CMContext(options);

			await arrangeContext.AddAsync(user1);
			await arrangeContext.AddAsync(user2);
			await arrangeContext.AddRangeAsync(cocktails);
			await arrangeContext.AddRangeAsync(ingredients);
			await arrangeContext.AddRangeAsync(cocktailIngredients);
			await arrangeContext.AddRangeAsync(bars);
			await arrangeContext.AddRangeAsync(addresses);
			await arrangeContext.AddRangeAsync(cities);
			await arrangeContext.AddRangeAsync(countries);
			await arrangeContext.AddRangeAsync(barCocktails);
			await arrangeContext.AddRangeAsync(comments);
			await arrangeContext.SaveChangesAsync();
		}
	}
 }
