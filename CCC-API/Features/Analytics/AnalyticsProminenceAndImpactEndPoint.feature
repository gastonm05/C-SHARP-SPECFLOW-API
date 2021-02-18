
Feature: Analytics - Prominence and Impact End Point
	To verify that prominence and impact scores are correct
	As a valid CCC user
	I want to call the analytics prominence and impact endpoint
	@HeartsAndCharts @ignore @analytics
Scenario Outline: Prominence series data is correct for different frequencies
	Given I login as 'analytics manager'
	When I perform a GET for company prominence with type '<type>', y axis metric '<yAxisMetric>' and frequency '<frequency>'
	Then the company prominence endpoint has series data with total and average

	Examples:
	| type			 | yAxisMetric | frequency |
	| Line			 | Average     | None      |
	| Donut			 | Average     | None      |
	| HorizontalBar  | Average     | None      |
	| Line			 | Total       | None      |
	| Donut			 | Total       | None      |
	| HorizontalBar  | Total	   | None      |
	| Line			 | Average     | Daily     |
	| Donut			 | Average     | Daily     |
	| HorizontalBar  | Average	   | Daily     |
	| Line			 | Total       | Daily     |
	| Donut			 | Total       | Daily     |
	| HorizontalBar  | Total	   | Daily     |
	| Line			 | Average     | Weekly    |
	| Donut			 | Average     | Weekly    |
	| HorizontalBar	 | Average     | Weekly    |
	| Line			 | Total       | Weekly    |
	| Donut			 | Total       | Weekly    |
	| HorizontalBar	 | Total       | Weekly    |
	| Line			 | Average     | Monthly   |
	| Donut			 | Average     | Monthly   |
	| HorizontalBar	 | Average     | Monthly   |
	| Line			 | Total       | Monthly   |
	| Donut			 | Total       | Monthly   |
	| HorizontalBar	 | Total       | Monthly   |
	| Line			 | Average     | Yearly    |
	| Donut			 | Average     | Yearly    |
	| HorizontalBar	 | Average     | Yearly    |
	| Line			 | Total       | Yearly    |
	| Donut			 | Total       | Yearly    |
	| HorizontalBar	 | Total       | Yearly    |
	

	@HeartsAndCharts @ignore @analytics
Scenario Outline: Impact series data is correct for different frequencies
	Given I login as 'analytics manager'
	When I perform a GET for company impact with type '<type>', y axis metric '<yAxisMetric>' and frequency '<frequency>'
	Then the company impact endpoint has series data with total and average

	Examples:
	| type			 | yAxisMetric | frequency |
	| Line			 | Average     | None      |
	| Donut			 | Average     | None      |
	| HorizontalBar  | Average     | None      |
	| Line			 | Total       | None      |
	| Donut			 | Total       | None      |
	| HorizontalBar  | Total	   | None      |
	| Line			 | Average     | Daily     |
	| Donut			 | Average     | Daily     |
	| HorizontalBar  | Average	   | Daily     |
	| Line			 | Total       | Daily     |
	| Donut			 | Total       | Daily     |
	| HorizontalBar  | Total	   | Daily     |
	| Line			 | Average     | Weekly    |
	| HorizontalBar	 | Average     | Weekly    |
	| Donut			 | Average     | Weekly    |
	| Line			 | Total       | Weekly    |
	| Donut			 | Total       | Weekly    |
	| HorizontalBar	 | Total	   | Weekly    |
	| Line			 | Average     | Monthly   |
	| Donut			 | Average     | Monthly   |
	| HorizontalBar	 | Average	   | Weekly    |
	| Line			 | Total       | Monthly   |
	| Donut			 | Total       | Monthly   |
	| HorizontalBar	 | Total	   | Weekly    |
	| Line			 | Average     | Yearly    |
	| Donut			 | Average     | Yearly    |
	| HorizontalBar	 | Average	   | Weekly    |
	| Line			 | Total       | Yearly    |
	| Donut			 | Total       | Yearly    |
	| HorizontalBar	 | Total	   | Weekly    |