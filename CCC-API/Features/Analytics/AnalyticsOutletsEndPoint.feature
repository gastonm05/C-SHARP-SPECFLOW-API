@HeartsAndCharts 
Feature: Analytics - Outlets End Point
	To verify that a list of outlets can be retrieved
	As a valid CCC user with system parameter Analytics-ViewBuilding-Enabled set to true
	I want to call the analytics outlets endpoint and verify the outlets that are returned

Scenario: Outlets - NOD Outlets are not removed
	Given I login as 'analytics manager'
	When I perform a GET for outlets

@ignore @analytics
Scenario Outline: Top Article gets correct count
	Given I login as 'analytics manager'
	When I perform a GET for '<outletCount>' outlets with '<offset>' pagination offset that are sorted by '<sortField>'
	Then there are '<outletCount>' outlets

	Examples: 
	| outletCount | offset | sortField      |
	| 10          | 0      | NumberOfClips  |
	| 10          | 0      | Reach          |
	| 10          | 0      | PublicityValue |
	| 10          | 11     | NumberOfClips  |
	| 10          | 11     | Reach          |
	| 10          | 11     | PublicityValue |

@ignore @analytics
Scenario Outline: Top Outlet gets correct count
	Given I login as 'analytics manager'
	When I perform a GET for '<articleCount>' articles with '<offset>' pagination offset that are sorted by '<sortField>'
	Then there are '<articleCount>' articles

	Examples: 
	| articleCount | offset | sortField |
	| 10           | 0      | Reach     |
	| 10           | 11     | Reach     |
	