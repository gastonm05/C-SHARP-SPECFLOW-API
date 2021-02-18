Feature: Analytics View - Views End Point
	To verify that a list of analytics views can be retrieved
	As a valid CCC user with system parameter Analytics-ViewBuilding-Enabled set to true or false
	I want to call the analytics views endpoint

@HeartsAndCharts @AnalyticsView @analytics @smokeProd
Scenario: Views - Get List of Views
	Given I login as 'analytics manager'
	When I perform a GET for all views
	Then the views endpoint has the correct response

@HeartsAndCharts @AnalyticsView @ignore @analytics
Scenario: Views - Get List of Views when Analytics-ViewBuilding-Enabled is False
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for all views
	Then the views endpoint has the correct response

@publishers @WidgetNote @AnalyticsView @bug @CCC_7727 @ignore @analytics
Scenario: View > Add note to a widget
	Given session for 'system_admin' user with edition 'Publishers social company, custom fields'
	When I use a generic automation view
	And I add (PUT) comment to a widget
	Then widget note is saved
	# add more users once CCC-7727 is fixed

@HeartsAndCharts @AnalyticsView  @analytics
Scenario: Available Widgets
	Given I login as 'analytics manager'
	When I perform a GET for 'Analytics' available widgets
	Then the available widget endpoint has the correct response

@HeartsAndCharts @AnalyticsView @CustomCategories @ignore @analytics
Scenario: Custom Categories All Groups
	Given I login as 'Impact Enabled Company'
	When I perform a GET for 'Analytics' available widgets
	Then the custom categories have all groups