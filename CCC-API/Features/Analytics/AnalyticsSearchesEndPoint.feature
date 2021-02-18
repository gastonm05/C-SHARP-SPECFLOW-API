@HeartsAndCharts @analytics
Feature: Analytics - Searches End Point
	To verify that analytics searches and groups can be created, modified and retrieved
	As a valid CCC user with system parameter Analytics-SearchGroups-Enabled set to true
	I want to call the analytics searches endpoint
	@HeartsAndCharts @analytics @smokeProd
Scenario: Get Searches
	Given I login as 'analytics user'
	When I perform a GET for analytics searches
	Then there are analytics searches
	@HeartsAndCharts @analytics
Scenario: Create Search
	Given I login as 'analytics manager'
	When I create a new analytics search 'test'
	Then the new analytics search 'test' exists
		And I can GET the analytics search 'test' by id
		And I delete the analytics search 'test'

@ignore	@analytics
Scenario: Get Search Groups
	Given I login as 'analytics manager'
	When I perform a GET for analytics search groups
	Then there are analytics group searches

@ignore @analytics
Scenario: Create Search Group
	Given I login as 'analytics manager'
	When I create a new analytics search group
	Then the new analytics search group exists
		And I delete the analytics search group

@ignore @analytics
Scenario: Cannot Create duplicate Search Group
	Given I login as 'analytics manager'
	When I create a new analytics search group
	Then the new analytics search group exists
		And I cannot create a duplicate analytics search group
		And the new analytics search group exists
		And I delete the analytics search group

@ignore @analytics
# create search first and delete search first
Scenario: Add Search to Search Group
	Given I login as 'analytics manager'
	When I create a new analytics search 'test'
		And I create a new analytics search group
		And I add the analytics search 'test' to the search group
	Then the new analytics search 'test' exists
		And the new analytics search group exists
		And I delete the analytics search 'test'
		And I delete the analytics search group

@ignore @analytics
Scenario: Add duplicate Search to Search Group
	Given I login as 'analytics manager'
	When I create a new analytics search 'test'
		And I create a new analytics search group
		And I add the analytics search 'test' to the search group
		And I add the analytics search 'test' to the search group
	Then the new analytics search 'test' exists
		And the new analytics search group exists
		And I delete the analytics search 'test'
		And I delete the analytics search group

@ignore @analytics
Scenario: Remove single Search from Search Group
	Given I login as 'analytics manager'
	When I create a new analytics search group
		And I create a new analytics search 'test'
		And I add the analytics search 'test' to the search group
		And I remove the analytics search 'test' from the search group
	Then the new analytics search group exists
		And the new analytics search 'test' exists
		And I delete the analytics search group
		And I delete the analytics search 'test'
		@analytics
# create search second and delete search first
Scenario: Modify Search Group multiple times
	Given I login as 'analytics manager'
	When I create a new analytics search group
		And I create a new analytics search 'test'
		And I add the analytics search 'test' to the search group
		And I remove the analytics search 'test' from the search group
		And I add the analytics search 'test' to the search group
		And I remove the analytics search 'test' from the search group
	Then the new analytics search group exists
		And the new analytics search 'test' exists
		And I delete the analytics search 'test'
		And I delete the analytics search group
		@analytics
Scenario: Add multiple Searches to Search Group
	Given I login as 'analytics manager'
	When I create a new analytics search group
		And I create a new analytics search '#1'
		And I add the analytics search '#1' to the search group
		And I create a new analytics search '#2'
		And I add the analytics search '#2' to the search group
	Then the new analytics search group exists
		And the new analytics search '#1' exists
		And the new analytics search '#2' exists
		And I delete the analytics search group
		And I delete the analytics search '#1'
		And I delete the analytics search '#2'
		@analytics
Scenario: Remove multiple Searches from Search Group
	Given I login as 'analytics manager'
	When I create a new analytics search group
		And I create a new analytics search '#1'
		And I add the analytics search '#1' to the search group
		And I create a new analytics search '#2'
		And I add the analytics search '#2' to the search group
		And I remove the analytics search '#1' from the search group
		And I remove the analytics search '#2' from the search group
	Then the new analytics search group exists
		And the new analytics search '#1' exists
		And the new analytics search '#2' exists
		And I delete the analytics search group
		And I delete the analytics search '#1'
		And I delete the analytics search '#2'