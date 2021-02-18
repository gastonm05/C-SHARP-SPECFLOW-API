Feature: OutletConversions
	To verify new metrics for Adobe Analytics on Outlet Conversions page
	As a valid CCC user
	I want to call the referrals endpoint in order to see the new Adobe Analytics metrics

@HeartsAndCharts
Scenario: Verify the new metrics for Adobe Analytics on Outlet Conversions
	Given I login as 'analytics manager'
	When I perform a GET for referrals using '100' as limit and 'AdobeAnalytics' as source
	Then the enpoint returns a list of referrals from 'AdobeAnalytics' with new metrics

@HeartsAndCharts
Scenario: Verify the new metrics for Google Analytics on Outlet Conversions
	Given I login as 'analytics manager'
	When I perform a GET for referrals using '100' as limit and 'GoogleAnalytics' as source
	Then the enpoint returns a list of referrals from 'GoogleAnalytics' with new metrics
