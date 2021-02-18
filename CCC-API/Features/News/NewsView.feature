Feature: News Endpoint
	To verify that news can be retrieved
	As a valid CCC user
	I want to call the news endpoint

@herdOfGnus @news
Scenario: News - Get All News
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for all news
	Then the News endpoint has the correct response

@herdOfGnus @news
Scenario: News - Get News with Multiple Facets
	Given I login as 'ESAManager'
	When I perform a GET for all news
	And I perform a GET for all available facets
	And I perform a GET for multiple faceted news
	Then the Faceted News Endpoint should have the correct response

@herdOfGnus @ignore @news
Scenario: News - Expose News External ID
	Given shared session for 'standard' user with edition 'basic'
	When I search for news by 'Keywords' with a value of 'owls'
	Then the News endpoint has the correct response
	And the News endpoint has news with value 'owls'
	And all News clips contain External ID

@herdOfGnus @ignore @news
Scenario: Validate response code 404 for non-existent sortfield
	Given session for 'standard' user with edition 'basic'
	When I perform a GET for all news
	And I perform a GET for news ordered by inexistent custom field
	Then the News endpoint has the correct response for inexistent custom field

@herdOfGnus @news
Scenario Outline: Sort News Results by valid News field and direction
	Given I login as 'Default_DG_1'
	When I search for news by start date with a value of 'Today minus 10 days'
	And I sort news results field '<sortField>' by direction '<direction>'
	Then all news results field '<sortField>' should be sorted '<direction>'
Examples: 
	| sortField		| direction  |
	| CreationDate  | Ascending  |
	| CreationDate  | Descending |

@herdOfGnus @ignore @news
Scenario Outline: Sorting News Results by invalid field and direction results in error response
	Given shared session for 'standard' user with edition 'basic'
	When I search for news by start date with a value of 'Today minus 10 days'
	And I sort news results field '<sortField>' by direction '<direction>'
	Then the news endpoint response code should be '<code>'
Examples: 
	| sortField | direction | code |
	|           | Ascending | 400  |
	| Headline  |           | 400  |
	| invalid   | Ascending | 404  |
	| Headline  | invalid   | 400  |

Scenario: PATCH News Item Headline
	Given I login as 'Manager with Default DataGroup'
	When I perform a GET for all news
	And I get a random news clip
	And I perform a PATCH to update news 'headline' to 'Automation QA Headline'
	Then the News Endpoint PATCH response should be '200' or successful operation
	And I should see the headline was updated to 'Automation QA Headline'

@herdOfGnus @news
Scenario: PATCH News Item Text
	Given I login as 'C3ShakedownAutomation Manager'
	When I perform a GET for all news
	And I get a random news clip
	And I perform a PATCH to update news 'text' to 'Automation QA text'
	Then the News Endpoint PATCH response should be '200' or successful operation
	And I should see the text was updated to 'Automation QA text'

@herdOfGnus @ignore @news
Scenario: News - Get News with two Facets of the same category
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for all news
	And I perform a GET for all available facets
	And I facet news by two facets of the 'Tone' category
	Then the Faceted News Endpoint should have the correct response

@herdOfGnus @ignore @news
Scenario: News - Get News with two Facets of different categories
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for all news
	And I perform a GET for all available facets
	And I facet news by 'Tone' category and 'Origin' category
	Then the Faceted News Endpoint should have the correct response

@herdOfGnus @ignore @news
Scenario: User can delete a news item from Coverage 
	Given shared session for 'standard' user with edition 'basic'
	When I search for news by start date with a value of 'Today minus 10 days'
	And I delete a news item 
	Then News item is deleted

@herdOfGnus @news
Scenario: Verify that all news items have a creation date
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for all news
	Then All news items have a creation date

@herdOfGnus @ignore @news
Scenario: Verify that attempt to delete non existing news item gives 404 error
	Given shared session for 'standard' user with edition 'basic'
	When I delete the news item with ID '1234567'
	Then the response code for deleting a news item should be'404'

@herdOfGnus @ignore @news
Scenario: Validate News Item response contains metrics
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for all news
	And I get a random news clip
	Then I should see that news endpoint response includes metrics

@herdOfGnus @ignore @news
Scenario: Validate that Read Only user cannot delete news item
	Given shared session for 'read_only' user with edition 'basic'
	When I search for news by start date with a value of 'Today minus 10 days'
	And I delete a news item 
	Then the response code for deleting a news item should be'403'

@herdOfGnus @ignore @news
Scenario: Validate that Social Country is present in News Response
	Given I login as 'ESAManager'
	When I perform a GET for all news
	Then I should see Social Country is included in the response

@herdOfGnus @news
Scenario: Sort News Results by Social Country 
	Given I login as 'Social Location Manager'
	When I search for news by start date with a value of 'Today minus 10 days'
	And I sort news results field 'SocialCountry' by direction 'Ascending'
	Then all news results field 'SocialCountry' should be sorted 'Ascending'
	When I sort news results field 'SocialCountry' by direction 'Ascending'
	Then all news results field 'SocialCountry' should be sorted 'Descending'

@herdOfGnus @ignore @news
Scenario: PATCH News Item Notes
	Given I login as 'ESAManager'
	When I perform a GET for all news
	And I get a random news clip
	And I perform a PATCH to update news 'notes' to 'Automation QA Note'
	Then the News Endpoint PATCH response should be '200' or successful operation
	And I should see the notes was updated to 'Automation QA Note'

@herdOfGnus @news
Scenario: PATCH News Item Article URL
	Given I login as 'ESAManager'
	When I perform a GET for all news
	And I get a random news clip
	And I perform a PATCH to update news 'articleurl' to 'http://www.google.com'
	Then the News Endpoint PATCH response should be '200' or successful operation
	And I should see the article URL was updated to 'http://www.google.com'