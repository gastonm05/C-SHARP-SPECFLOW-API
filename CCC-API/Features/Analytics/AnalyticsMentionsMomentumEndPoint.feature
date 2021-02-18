
Feature: Analytics - Mentions Momentum
	To verify that momentum for total mentions is correct
	As a valid CCC user
	I want to call the mentions over time endpoint with different frequenies over various time periods
	@HeartsAndCharts @ignore @analytics
Scenario Outline: Momentum series data is correct for different frequencies
	Given I login as 'analytics manager'
	When I perform a GET for mentions momentum with days '<days>', type '<type>' and frequency '<frequency>'
	Then the mentions momentum endpoint <hasData> series data

	Examples:
	| description    | days | type | frequency | hasData       |
	| same day       | 0    | Line | Daily     | does not have |
	| one day        | 1    | Line | Daily     | has           |
	| over a week    | 8    | Line | Daily     | has           |
	| over a month   | 32   | Line | Daily     | has           |
	| over a year    | 367  | Line | Daily     | has           |
	| one day        | 1    | Line | Weekly    | does not have |
	| over a week    | 8    | Line | Weekly    | has           |
	| over a month   | 32   | Line | Weekly    | has           |
	| over a year    | 367  | Line | Weekly    | has           |
	| one day        | 1    | Line | Monthly   | does not have |
	| over a month   | 32   | Line | Monthly   | has           |
	| over a year    | 367  | Line | Monthly   | has           |
	| one day        | 1    | Line | Yearly    | does not have |
	| over a year    | 367  | Line | Yearly    | has           |
	| over two years | 732  | Line | Yearly    | has           |
	@HeartsAndCharts @ignore @analytics
Scenario Outline: Momentum series data for the previous year is correct for different frequencies
	Given I login as 'analytics manager'
	When I perform a GET for mentions momentum on the last day of the previous year with type '<type>' and frequency '<frequency>'
	Then the mentions momentum endpoint has series data

	Examples:
	| description | type | frequency |
	|             | Line | Daily     |
	|             | Line | Weekly    |
	| bug         | Line | Monthly   |
	|             | Line | Yearly    |