Feature: NewsClipBook
	In order to customize reports
	As a C3 user
	I want to be able to CRUD News Clipbooks

@herdOfGnus @news
Scenario Outline: As a user I want to be able to create, edit and delete a News Clipbook with Grouping and Delivery Options
	Given I login as 'news archive user'
	When I perform a GET for all news
	And I perform a POST to save a News Clipbook sorted by <sort_by>
	Then the News Clipbook endpoint should have the correct response
	When I perform a PUT to update recently created News Clipbook sorted by Oldest to Newest
	Then the news Clipbook endpoint should have the correct response for editing a News Clipbook
	When I perform a DELETE for recently created news Clipbook
	Then the news Clipbook endpoint should have the correct response for deleting a single Clipbook

	Examples: 
	| sort_by				  |  
	| NewestToOldest	      | 
	| OldestToNewest		  | 
	| Medium		          | 

@herdOfGnus @news
Scenario: As a user I want to GET all news clipbooks
	Given I login as 'news archive user'
	When I perform a GET for all news Clipbooks
	Then the news Clipbook endpoint should respond with a '200'

@herdOfGnus @ignore @news
Scenario: Validate max length for Clipbook text fields is exposed
	Given I login as 'news archive user'
	When I perform a GET for news Clipbooks definitions
	Then I should see max length for Title is '255'
	And I should see max length for Summary is '500'

@herdOfGnus @ignore @news
Scenario: Clip Report - Get Customizable Fields
	Given shared session for 'standard' user with edition 'basic'
	When I search for news by 'Keywords' with a value of 'owls'
	Then the News endpoint has the correct response
	And the News endpoint has news with value 'owls'
	When I perform a GET to Clip Report endpoint
	Then I should see that the Clip Report contains Custom Fields

@herdOfGnus @ignore @news
Scenario Outline: As a user I want to be able to create and sort a News Clipbook
	Given I login as 'news archive user'
	When I perform a GET for all news
	And I perform a POST to save a News Clipbook sorted by <sort_by>
	Then the News Clipbook endpoint should have the correct response
	And I should see that news are properly sorted by <sort_by>
	When I perform a DELETE for recently created news Clipbook
	Then the news Clipbook endpoint should have the correct response for deleting a single Clipbook

Examples: 
	| sort_by				  |  
	| NewestToOldest	      | 
	| OldestToNewest		  | 
	| Medium		          |  

@herdOfGnus @ignore @news
Scenario: Validate Clipbook Group Type Definitions - Keyword Search
	Given I login as 'news archive user'
	When I perform a GET for news Clipbooks definitions
	Then I should see an attribute by the name of 'Keyword Search'

@herdOfGnus @ignore @news
Scenario Outline: As a user I want to be able to create and group News Clipbooks
	Given I login as 'news archive user'
	When I perform a GET for all news
	And I perform a POST to save a News Clipbook grouped by <group_by>
	Then the News Clipbook endpoint should have the correct response
	And I should see that news are properly grouped by <group_by>
	When I perform a DELETE for recently created news Clipbook
	Then the news Clipbook endpoint should have the correct response for deleting a single Clipbook

Examples: 
	| group_by       |  
	| Company        | 
	| Tags		     |
	| Product		 |
	| Keyword Search | 
	| Medium		 |  
	