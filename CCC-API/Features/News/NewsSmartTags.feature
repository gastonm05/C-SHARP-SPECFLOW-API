Feature: NewsSmartTagsFeature
	In order to categorize news items
	As a standard CCC user
	I want to be able to edit smart tags 

@herdOfGnus @ignore @news
Scenario: Edit News Type (Smart Tag) on a single News Clip
	Given I login as 'Manager with Default DataGroup'
	When I perform a GET for all news
	And I perform a GET for all available facets
	And I perform a GET for news by facet with text 'Mention'
	And I perform a GET for the first single News Item with faceted type
	And I PATCH to update single News Item to Type 'Brief'
	Then the endpoint should return the single News Item with Type 'Brief'

@herdOfGnus @ignore @news
Scenario: Validate News Type (Smart Tags) is added to News Search results
	Given I login as 'ESAManager'
	When I search for typed news by 'Keywords' with a value of 'Trump'
	Then I should see the news endpoint has the correct response and it is including typed news

@herdOfGnus @ignore @news
Scenario: Filter news items by Smart Tag
	Given I login as 'ESAManager'
	When I perform a GET for all news
	And I perform a GET for all available facets
	And I perform a GET for news by single facet with category 'newstype'
	Then all news should have selected facet option

@herdOfGnus @ignore @news
Scenario: News Search by Include Smart Tags (aka News Type)
	Given I login as 'Manager with Default DataGroup'
	When I perform a GET for all smart tags
	When I perform a GET for news by Smart Tags using the 'Include' operator
	Then the News Endpoint responds with a '200' for search by Include field
	And all items should have the included Smart Tags value
