Feature: NewsAnalytics
	In order to assess News Search Results
	As a C3 User
	I want to be able to use Analytics

@HeartsAndCharts @news
Scenario: Social Analytics data on News clips
	Given I login as 'analytics manager'
	When I search for news by start date with a value of 'Today minus 30 days'
	Then all returned news results have analytics social data

@herdOfGnus	 @news
Scenario: PATCH - Bulk Edit Advanced Analytics
	Given I login as 'Manager with Default DataGroup'
	When I search for news by 'Keywords' with a value of 'owls'
	And I perform a PATCH to all the results to update Advanced Analytics field
	Then the News Advanced Analytics Bulk Edit Endpoint response should be '204'

@herdOfGnus @news
Scenario: News Search by Include or Exclude Analytics Fields
	Given I login as 'ESAManager'
	When I perform a GET for news by News Analytics field 'Product' and value 'iphone' using the 'Include' operator
	Then the News Endpoint responds with a '200' for search by Include field
	And all items should have the included Analytics Field value
	When I perform a GET for news by News Analytics field 'Product' and value 'iphone' using the 'Exclude' operator
	Then the News Endpoint responds with a '200' for search by Exclude field
	And none of the items should have the excluded Analytics Field value

@herdOfGnus @ignore @news
Scenario: Validate Read-Only user cannot create analytics searches
	Given I login as 'Analytics Read Only'
	When I perform a POST to create a new analytics search 'read only test'
	Then the news analytics endpoint should respond with unauthorized access

@herdOfGnus @ignore @news
Scenario: Validate Read-Only user cannot create news analytics dashboards
	Given I login as 'Analytics Read Only'
	When I perform a POST to create a new analytics dashboard 'read only dashboard'
	Then the news analytics dashboard endpoint should respond with unauthorized access
