
Feature: AnalyticsCompanySentimentScore
	     To verify the end points for Company SentimentScore

@HeartsAndCharts @ignore @analytics
Scenario: Company sentment Score request HorizontalBar
	Given I login as 'Impact Enabled Company'
	When I perform a GET for Company sentiment score Widget 'HorizontalBar'
	Then the Sentiment score endpoint has the correct response for 'HorizontalBar'
@HeartsAndCharts @ignore @analytics
Scenario: Company sentment Score request Line
	Given I login as 'Impact Enabled Company'
	When I perform a GET for Company sentiment score Widget 'Line'
	Then the Sentiment score endpoint has the correct response for 'Line'
