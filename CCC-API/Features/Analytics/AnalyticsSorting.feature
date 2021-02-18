
Feature: AnalyticsSorting
	In order to group items
	As a CCC user
	I want to be able to sort
	@HeartsAndCharts @ignore @analytics
Scenario Outline: Sort Analytics News items
	Given shared session for 'standard' user with edition 'basic'
	When I get a list of Analytics News items
	And I sort Analytics News items '<direction>' by '<field>'
	Then all Analytics News item '<field>' are sorted '<direction>'
Examples: 
	| field               | direction  |
	| OutletName          | Ascending  |
	| OutletName          | Descending |
	| OutletType          | Ascending  |
	| OutletType          | Descending |
	| PublicityValue      | Ascending  |
	| PublicityValue      | Descending |
	| CirculationAudience | Ascending  |
	| CirculationAudience | Descending |
	| UniqueVisitors      | Ascending  |
	| UniqueVisitors      | Descending |
	| NewsDate            | Ascending  |
	| NewsDate            | Descending |
	@HeartsAndCharts @ignore @analytics
Scenario Outline: Analytics default sort Descending news date when no sort field provided
	Given shared session for 'standard' user with edition 'basic'
	When I get a list of Analytics News items
	And I sort Analytics News Items '<direction>' without sort field
	Then all Analytics News item '<field>' are sorted '<sortDirection>'
Examples: 
	| direction  | field    | sortDirection |
	| Ascending  | NewsDate | Descending    |
	| Descending | NewsDate | Descending    |
	@HeartsAndCharts @ignore @analytics
Scenario Outline: Analytics default sort when no sort direction provided
Given shared session for 'standard' user with edition 'basic'
	When I get a list of Analytics News items
	And I sort Analytics News Items by '<field>' without sort direction
	Then all Analytics News item '<field>' are sorted '<sortDirection>'
Examples: 
	| field      | sortDirection |
	| NewsDate   | Ascending     |
	| OutletType | Ascending     |
	| OutletName | Ascending     | 
	@HeartsAndCharts @ignore @analytics
Scenario Outline: Sort Analytics News items by invalid field
	Given shared session for 'standard' user with edition 'basic'
	When I get a list of Analytics News items
	And I sort Analytics News items by '<direction>' with invalid field '<field>'
	Then the Analytics News items endpoint response is '<status>'
Examples: 
	| field    | direction  | status |
	| invalid  | Ascending  | 404    |
	| invalid  | Descending | 404    |
	| OutletId | Ascending  | 404    |
	| OutletId | descending | 404    |
	@HeartsAndCharts @ignore @analytics
Scenario Outline: Sort Analytics News items by invalid direction
	Given shared session for 'standard' user with edition 'basic'
	When I get a list of Analytics News items
	And I sort Analytics News items by '<field>' with invalid direction '<direction>'
	Then the Analytics News items endpoint response is '<status>'
Examples:
	| direction | field    | status |
	| invalid   | NewsDate | 400    |
	|           | NewsDate | 400    |