Feature: NewsTags
	In order to categorize news items
	As a standard CCC user
	I want to be able to tag items

@herdOfGnus @news
Scenario: Tags endpoint returns items
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET to the tags endpoint
	Then there should be tags returned

@herdOfGnus @ignore @news
Scenario: Validate Tag Rename Endpoint
	Given I login as 'ESAManager'
	When I perform a POST to create a new tag with name 'NewsTagTest7'
	And I perform a PATCH to update tag name
	Then the Single Tag Endpoint response should be '200'
	And I should see that the tag name was updated
	And I perform a DELETE to eliminate recently created tag

@herdOfGnus @news
Scenario: Create/Duplicate/Delete New Tag
	Given I login as 'ESAManager'
	When I perform a POST to create a new tag with name 'NewsTagTest'
	Then I should see the tags endpoint has the correct response for new tag
	When I perform a POST to create a duplicate tag with name 'NewsTagTest'
	Then I should see the tags endpoint has the correct response for creating duplicate tags
	And I perform a DELETE to eliminate recently created tag
	And I should see that the delete tag endpoint has the correct response

@herdOfGnus @news
Scenario: Validate Tags are added to News Search results
	Given I login as 'ESA Standard User with Tags'
	When I search for news by 'Tags' with a value of 'Cision'
	Then the News endpoint has the correct response for News Tags search
	And all the News Clips are tagged with 'Cision'

@herdOfGnus @ignore @news
Scenario: Attempting to add tag with existing tag name returns 400
	Given I login as 'ESAManager'
	When I create the tag 'QA Test Tag'
	When I perform a POST to create a new tag with name 'QA Test Tag'
	Then the Create News Tags endpoint response status code should be '400'

@herdOfGnus @news
Scenario: Bulk Tag News Items
	Given I login as 'ESAManager'
	When I create the tag 'Bulk Tag Test'
	And I search for news by start date with a value of 'Today minus 10 days'
	And I bulk add the tag 'Bulk Tag Test' to the first '10' news items
	Then I should see that the news endpoint has the correct response for appending tags

@herdOfGnus @ignore @news
Scenario: Validate Tag Name max length is 50 characters
	Given I login as 'ESAManager'
	When I perform GET for all available tags
	Then I should see the max length for a tag name is '50' characters

@herdOfGnus @news
Scenario: News Search by Include Tags
	Given I login as 'ESAManager'
	When I perform a GET for news by 'Cision' Tag using the 'Include' operator
	Then the News Endpoint responds with a '200' for search by Include field
	And all items should have the 'Cision' tag

@herdOfGnus @news
Scenario: News Search by Exclude Tags
	Given I login as 'ESAManager'
	When I perform a GET for news by 'Cision' Tag using the 'Exclude' operator
	Then the News Endpoint responds with a '200' for search by Exclude field
	And none of the items should have the 'Cision' tag

@herdOfGnus @news
Scenario: Validate PATCH to Bulk Replace & Remove Tags
	Given I login as 'ESAManager'
	When I create the tag 'Bulk Tag'
	And I search for news by start date with a value of 'Today minus 10 days'
	And I perform a PATCH to bulk replace with tag named 'Bulk Tag'
	Then I should see that the news endpoint has the correct response for replacing tags
	When I perform a PATCH to bulk remove tag with name 'Bulk Tag'
	Then I should see that the news endpoint has the correct response for removing tags
