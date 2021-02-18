Feature: NewsSavedSearches
	In order to revisit searches
	As a C3 User
	I want to be able to save my searches

@herdOfGnus @news 
Scenario: Create and Delete a News Saved Search
	Given I login as 'Manager with Default DataGroup'
	When I perform a GET for all news
	And I perform a POST to Saved Searches endpoint with name 'Automation Saved Search'
	Then the Saved Search endpoint should respond with a '201' for creating an item
	And the Saved Search endpoint should respond with a '200' for deleting an item

@herdOfGnus @news
Scenario: All saved searches are returned with name and id
	Given shared session for 'standard' user with edition 'basic'
	When I get all saved searches
	Then all saved searches returned have a name and id

@herdOfGnus @news @smokeProd
Scenario: Get single saved search
	Given I login as 'analytics manager'
	When I get all saved searches
	And I search for a single saved search
	Then the saved search is returned

@herdOfGnus @ignore @news
Scenario: User can save a news search that doesn't have any results
	Given I login as 'ESA Standard User with Default DG'
	When I search for news by 'Keywords' with a value of '3gru6hd95'
	Then No news results are returned
	When I perform a POST to Saved Searches endpoint with name 'Empty search'
	Then the Saved Search endpoint should respond with a '201' for creating an item
	And the Saved Search endpoint should respond with a '200' for deleting an item

@herdOfGnus @ignore @news
Scenario: Read Only user cannot save news search
	Given shared session for 'read_only' user with edition 'basic'
	When I search for news by 'Keywords' with a value of 'weather'
	And I perform a POST to Saved Searches endpoint with name 'Test'
	Then the Saved Search endpoint should respond with a '403' for creating an item

@herdOfGnus @news
Scenario: Validate News Details Search Criteria on Saved Search
	Given I login as 'Manager with Default DataGroup'
	When I search for news by all news details criteria
	And I save the search with name 'News Detail Saved Search'
	Then the Saved Search endpoint should respond with a '201' for creating an item
	When I perform a GET for Saved Search criteria
	Then I should see that the saved search includes the search criteria for news details
	And the Saved Search endpoint should respond with a '200' for deleting an item

@herdOfGnus @ignore @news
Scenario: Validate Saved Search Criteria Update
	Given I login as 'Manager with Default DataGroup'
	When I create a new saved search
	And I perform a GET for Saved Search criteria
	Then I should see that the saved search includes the value 'basketball' for keywords criteria
	When I perform a PATCH to update Saved Search keywords criteria with a value of 'playground'
	Then I should see that the Saved Search value for keywords criteria was updated to 'playground'
	And the Saved Search endpoint should respond with a '200' for deleting an item