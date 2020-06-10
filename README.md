# Team6_CocktailMagician

Web app for browsing bars and the cocktails they offer.

 [Find Trello board here!](https://trello.com/b/zMhsHErO/cocktail-magician)
 
## ASP.NET Core MVC Project

*Project Description*

Cocktail Magician allows creation of recipes for innovative, exotic, awesome cocktails and follows their distribution and success in amazing bars.

* The website visitors (unregistered users) can see the bars and the cocktails they offer
*	Bar Crawlers (registered users) can rate cocktails and bars (using stars) and leave comments for them
*	Cocktail Magicians (Cocktail Magician employees) can create cocktails (defining their ingredients) and define the bars which can offer them

## Public Part

Visible for all website visitors - no authentication required.
The public part consists of a home page displaying top rated bars and cocktails in separate sections on the page. 

It also includes searching possibility for: 
*	Bars: by Name, Address and Rating
*	Cocktails: by Name, Ingredient/s and Rating

The result of the search should be displayed in a grid structure with paging (if needed).
The information shown in the grid contains 

*	For the Bars: image, name, address and rating 
*	For the Cocktails: image, name, ingredients (comma separated) and rating

Upon clicking a bar, the visitor can see details for the bar (image, name, rating, address, phone, and comments) and the cocktails it offers (with links to the cocktail).
Upon clicking a cocktail, the visitor can see details for the cocktail (image, name, rating, ingredients, and comments) and the bars this cocktail is offered in (with links to the bar).

The public part includes login page and register page as well.

## Private Part (Bar Crawlers)

After login, Bar Crawlers see everything visible to website visitors and additionally they can:
*	Rate bars (from 1 to 5 scale/stars)
*	Rate cocktails (regardless of which bar offers them) (from 1 to 5 scale/stars)
*	Leave a comment for a bar (maximum 500 characters)
* Leave a comment for a cocktail (maximum 500 characters)

## Administration Part (Cocktail Magicians)

Cocktail Magicians can:
*	Manage ingredients – CRUD operations for ingredients for cocktails (delete ingredient only if not used in any cocktail)
*	Manage cocktails – CRUD operations for cocktails (never delete a cocktail, just hide it from the users)
*	Manage bars – CRUD operations for bars (never delete a bar, just hide it from the users)
*	Set cocktails as available in particular bars 




###### Used technologies, frameworks and development techniques:

*	ASP.NET Core 3.1 MVC and Visual Studio 2019.
*	Razor template engine for generating the UI.
*	MS SQL Server as database back-end.
*	Entity Framework Core 3.1 to access the database
*	DataTables with server-side paging and sorting for a model entity.
*	Bootstrap 4
*	ASP.NET Identity System for managing users and roles.
*	AJAX in some parts of the application.
*	Unit test framework: MSTest.
*	Mocking framework: Moq.
*	DB Provider: EF InMemory.



