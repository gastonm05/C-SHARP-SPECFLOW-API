
Feature: AnalyticsDMAEndPoint
	To verify DMA data can be retreived 
	As a valid CCC user
	I want to call the DMA endpoint
@HeartsAndCharts @ignore @analytics
Scenario Outline: DMA gets correct data
	Given I login as 'analytics manager'
	When I perform a GET for DMA widget with '<type>'
	Then the data is the correct 

	Examples: 
	| type      |
	| MapBubble |
	