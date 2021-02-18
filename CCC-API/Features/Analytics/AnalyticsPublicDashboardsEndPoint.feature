
Feature: Analytics Public Dashboards End Point
	To verify that end points behave properly for shared public views
	As an anonymous user
	I want to make sure I cannot call dashboards end points without a public access key
	@HeartsAndCharts @analytics
Scenario: AvailableWidgets - unauthorized access
	#Given I am not logged in
	When I perform a sessionless GET for available widgets
	Then the endpoint is unauthorized
	@HeartsAndCharts @analytics
Scenario: LastViewId - unauthorized access
	#Given I am not logged in
	When I perform a sessionless GET for last view id
	Then the endpoint denies authorization
	@HeartsAndCharts @analytics
Scenario: Views - unauthorized access
	#Given I am not logged in
	When I perform a sessionless GET for all views
	Then the endpoint denies authorization
	@HeartsAndCharts @analytics
Scenario: View - unauthorized access
	#Given I am not logged in
	When I perform a sessionless GET for a view
	Then the endpoint is unauthorized