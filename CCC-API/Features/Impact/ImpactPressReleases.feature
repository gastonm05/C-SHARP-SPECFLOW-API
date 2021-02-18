@impact
Feature: ImpactPressReleases
	To verify that releases can be retrieve
	As a valid CCC user
	I want to call the impact endpoint

Scenario Outline: Press releases list is correct
	Given I login as 'Impact Enabled Company'
	When I call the releases endpoint with <sorting> sorting by <field> field, <limit> limit and <page> page <allAccounts> all accounts
	Then the releases endpoint has the correct response

	Examples: 
	| sorting    | field    | limit | page | allAccounts |
	| Ascending  | headline | 50    | 1    | including  |
	| Ascending  | date     | 50    | 1    | including  |
	| Descending | headline | 50    | 1    | ignoring   |
	| Descending | date     | 50    | 1    | ignoring   |

Scenario: Datebounds is correct
	Given I login as 'Impact Enabled Company'
	When I call the datebounds endpoint
	Then the datebounds endpoint has the correct response