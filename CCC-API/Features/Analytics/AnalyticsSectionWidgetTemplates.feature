@HeartsAndCharts @analytics
Feature: Analytics Section - Widget Templates
	To verify data on section and widget templates 
	As a valid CCC user
	I want to call the section and widget endpoint

@ignore @analytics
Scenario: Section template
	Given I login as 'analytics manager'
	When I perform a GET for 'Analytics' section templates endpoint
	Then the section templates endpoint has the correct response

@ignore @analytics
Scenario: Widget template
	Given I login as 'analytics manager'
	When I perform a GET for 'Analytics' widget templates endpoint
	Then the widget templates endpoint has the correct response

	@analytics
Scenario: Create section with widgets
	Given I login as 'analytics manager'
	When I use a generic automation view
	And I perform a POST for section endpoint with the following widgets
	| widgetId | widgetName  |
	| 27       | Top Outlets |
	| 21       | All Groups  |
	| 81       | Facebook    |
	Then the section endpoint has the correct response
	And the following widgets have been created
	| widgetId | widgetName  |
	| 27       | Top Outlets |
	| 21       | All Groups  |
	| 81       | Facebook    |