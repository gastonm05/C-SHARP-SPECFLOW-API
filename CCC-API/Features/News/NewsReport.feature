Feature: NewsReports
	In order to revisit searches
	As a C3 User
	I want to be able to create a news clip report

@socialmedia @news
Scenario: Create a News Clip Report with the required fields selected
	Given I login as 'socialmedia User'
	When I perform a GET for all news
	And I perform a POST to Saved Searches endpoint with name 'Automation'
	And I add 'NewsDate' field to report request
	And I add 'SourceLink' field to report request
	And I create a clip report
	Then Report creation was successful with a '200' response code
	And the Saved Search endpoint should respond with a '200' for deleting an item

@socialmedia @news
Scenario: Create a News Clip Report with all available fields included
	Given I login as 'socialmedia User'
	When I perform a GET for all news
	And I perform a POST to Saved Searches endpoint with name 'Automation'
	And I add 'NewsDate' field to report request
	And I add 'Headline' field to report request
	And I add 'SourceLink' field to report request
	And I add 'MediaContact' field to report request
	And I add 'MediaOutlet' field to report request
	And I add 'DigitalReach' field to report request		
	And I add 'NewsDate' field to report request
	And I add 'UVPM' field to report request
	And I add 'AudienceReach' field to report request
	And I add 'Tone' field to report request
	And I add 'Text' field to report request
	And I add 'Tags' field to report request
	And I add 'AdvancedAnalytics' field to report request
	And I create a clip report
	Then Report creation was successful with a '200' response code
	And the Saved Search endpoint should respond with a '200' for deleting an item
