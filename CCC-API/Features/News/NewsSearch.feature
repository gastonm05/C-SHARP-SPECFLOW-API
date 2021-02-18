Feature: NewsSearch
	In order to get News
	As a C3 User
	I want to be able to search for News

@herdOfGnus @ignore @news
Scenario: News - Search News by Keyword - 1 Phrase
	Given I login as 'Default_DG_1'
	When I search for news by 'Keywords' with a value of '"donald trump"'
	Then the News endpoint has the correct response
	And the News endpoint has news with value 'trump'

@herdOfGnus @ignore @news
Scenario: News - Search News by Keyword - 2 Terms
	Given I login as 'Default_DG_1'
	When I search for news by 'Keywords' with a value of 'sports forecast'
	Then the News endpoint has the correct response
	And the News endpoint has news with value 'sports'
	And the News endpoint has news with value 'forecast'

@herdOfGnus @ignore @news
Scenario: News - Search News by Keyword - 2 Terms - PLUS
	Given I login as 'Default_DG_1'
	When I search for news by 'Keywords' with a value of 'sports+forecast'
	Then the News endpoint has the correct response
	And the News endpoint has news with value 'sports'
	And the News endpoint has news with value 'forecast'

@herdOfGnus @ignore @news
Scenario: News - Search News by Keyword - 1 Term
	Given I login as 'Default_DG_2'
	When I search for news by 'Keywords' with a value of 'owl'
	Then the News endpoint has the correct response
	And the News endpoint has news with value 'owl'

@herdOfGnus @news 
Scenario: News - Search News by Keyword with no parameters
	Given shared session for 'standard' user with edition 'basic'
	When I search for news by 'Keywords' with a value of ''
	Then the News endpoint has the correct response

@herdOfGnus @ignore @news
Scenario: News - Search News returns no results
	Given shared session for 'standard' user with edition 'basic'
	When I search for news by 'Keywords' with a value of 'ThisSearchHasNoResults'
	Then the News endpoint has no results

@herdOfGnus @ignore @news
Scenario: News - Search News by Keyword - AND - OR - PARENS - Phrases - Asterisk
	Given I login as 'Default_DG_4'
	When I search for '1000' news by 'Keywords' with a value of 'pence AND (mike and "donald trump") OR (united and states) OR president OR Kore*'
	Then the News endpoint has the correct response
	And the News endpoint has news with values 'pence' and 'mike'
	And the News endpoint has news with values 'pence' and 'donald trump'
	And the News endpoint has news with values 'mike' and 'donald trump'
	And the News endpoint has news with values 'united' and 'states'
	And the News endpoint has news with value 'president'
	And the News endpoint has news with value 'Korea'
	#Kore* should match Korea(n) and little else

@herdOfGnus @news
Scenario: News - Search News by Keyword - Headline Logic
	Given I login as 'Default_DG_5'
	When I search for news by 'Keywords' with a value of '[Headline] nhl'
	Then the News endpoint has the correct response
	And the News endpoint has news with value 'nhl'
	And all returned results contain 'nhl' in the headline

@herdOfGnus @news @smokeProd
Scenario: News Search by Keywords, Start Date & End Date
	Given I login as 'analytics manager'
	When I perform a GET for news by Keywords 'argentina' and Start Date '3/1/2020' and End Date '6/15/2020'
	Then the News Endpoint response should be '200' for keywords, start date and end date news search
	And all the news clips are within the expected date range

@herdOfGnus @ignore @news
Scenario: Search News by Company Tone Negative
	Given I login as 'ESAManager'
	When I search for news by Company Tone with value '-1'
	Then the News endpoint has the correct response
	And all news clips Company Tone include the value '-1'

@herdOfGnus @ignore @news
Scenario: Search News by Company Tone Neutral
	Given I login as 'ESAManager'
	When I search for news by Company Tone with value '-2'
	Then the News endpoint has the correct response
	And all news clips Company Tone include the value '-2'

@herdOfGnus @ignore @news
Scenario: Search News by Company Tone Positive
	Given I login as 'ESAManager'
	When I search for news by Company Tone with value '-3'
	Then the News endpoint has the correct response
	And all news clips Company Tone include the value '-3'

@herdOfGnus @ignore @news
Scenario: Search for news with start date only returns news items with news date on or after start date
	Given session for 'standard' user with edition 'basic'
	When I search for news by start date with a value of 'Today minus 30 days'
	Then all returned news results have a date greater than or equal to 'Today minus 30 days'

@herdOfGnus @ignore @news
Scenario: Search for news with end date only returns news items with news date on or before end date
	Given session for 'standard' user with edition 'basic'
	When I search for news by end date with a value of 'Today'
	Then all returned news results have a date less than or equal to 'Today'

@herdOfGnus @ignore @news
Scenario: Search for news by date range returns news items with news date within date range
	Given session for 'standard' user with edition 'basic'
	When I search for news with a start date or 'Today minus 30 days' and an end date of 'Today'
	Then all returned news results have a news date that is greater than or equal to 'Today minus 30 days' and less than or equal to 'Today plus 1 day'

@herdOfGnus @news
Scenario: Search for News by Social Location
	Given I login as 'Social Location Manager'
	When I perform a GET for social locations by name 'united'
	And I perform a GET for news searching by Social Locations
	Then the News endpoint has the correct response
	When I perform a GET for all available facets
	Then I should see a facet with name 'Outlet Country'

@herdOfGnus @news
Scenario: Search for News by Media Type
	Given I login as 'analytics manager'
	When I perform a GET for news searching by Media Type
	Then the News endpoint has the correct response
	And all news have media type 'Blog, consumer'

@herdOfGnus @ignore @news
Scenario: Validate News Item includes VTKey
	Given I login as 'analytics manager'
	When I perform a GET for all news
	Then the News endpoint has the correct response
	When I perform GET to a single news item from search
	Then I should see the VTKey attribute is present