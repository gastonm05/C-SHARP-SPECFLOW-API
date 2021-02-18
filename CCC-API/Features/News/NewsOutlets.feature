Feature: NewsOutlets
	In order to manage News
	As a C3 User
	I want to be able to sort/filter/search by Outlets

@herdOfGnus @news
Scenario: Exclude Outlet List in Search My Coverage
	Given I login as 'ESAManager'
	When I perform a GET for news by 'Outlet_List' using the 'Exclude' operator
	Then the News Endpoint responds with a '200' for search by Exclude field
	And none of the items should be from the excluded outlet

@herdOfGnus @news
Scenario: Include Outlet List in Search My Coverage
	Given I login as 'ESAManager'
	When I perform a GET for news by 'Outlet_List' using the 'Include' operator
	Then the News Endpoint responds with a '200' for search by Include field
	And all items should be from the included outlet

@herdOfGnus @ignore @news
Scenario: Outlet Medium on News clips always returns a non-null value in search results 
	Given session for 'standard' user with edition 'basic'
	When I perform a GET for all news
	Then all returned news results have a non-null Outlet Medium

@herdOfGnus @news
Scenario: News Search by including particular Outlet
	Given session for 'standard' user with edition 'basic'
	When I perform a GET for news by 'Outlet_Name' using the 'Include' operator
	Then the News Endpoint responds with a '200' for search by Include field
	And all items should be from the included outlet

@herdOfGnus @news
Scenario: News Search by excluding particular Outlet
	Given session for 'standard' user with edition 'basic'
	When I perform a GET for news by 'Outlet_Name' using the 'Exclude' operator
	Then the News Endpoint responds with a '200' for search by Exclude field
	And none of the items should be from the excluded outlet

@herdOfGnus @ignore @news
Scenario: News Search by Invalid Outlet ID
	Given I login as 'C3ShakedownAutomation Manager'
	When I perform a GET for news by 'Invalid_Outlet_Name' using the 'Include' operator
	Then the News Endpoint responds with a '400' for invalid Outlet Id

@herdOfGnus @ignore @news
Scenario Outline: Sort News Results by valid Outlet field and direction
	Given shared session for 'standard' user with edition 'basic'
	When I search for news by start date with a value of 'Today minus 10 days'
	And I sort news results field '<sortField>' by direction '<direction>'
	Then all news results Outlet field '<field>' should be sorted '<direction>'
Examples: 
	| sortField    | direction  | field  |
	| OutletName   | Ascending  | Name   |
	| OutletName   | Descending | Name   |
	| OutletMedium | Ascending  | OutletMedium |
	| OutletMedium | Descending | OutletMedium |

@herdOfGnus @ignore @news
Scenario Outline: Search News by Outlet Location
	Given I login as 'C3ShakedownAutomation Manager'
	When I search for news by location criteria '<criteria>' with a value of '<location>'
	Then all returned news items have an Outlet '<field>' equal to '<text>'
Examples:
	| criteria         | location      | text          | field       |
	| Outlet_Locations | Chicago       | Chicago       | City        |
	| Outlet_Locations | Illinois      | IL            | State       |
	| Outlet_Locations | United States | United States | CountryName |
	| Outlet_Location  | Chicago       | Chicago       | City        |
	| Outlet_Location  | Illinois      | IL            | State       |
	| Outlet_Location  | United States | United States | CountryName |
