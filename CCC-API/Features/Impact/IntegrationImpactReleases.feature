@impact
Feature: IntegrationImpactReleases
	To verify that Impact releases data is present
	As a valid CCC user
	I want to call impact release endpoint and to verify we receive content

Scenario Outline: Verify that HC releases data is present
	Given I login as 'Impact Enabled Company'
	When I call the releases endpoint with <sorting> sorting by <field> field, <limit> limit and <page> page <allAccounts> all accounts
	Then the Impact Views endpoint has the correct response for a single release

	Examples: 
	| sorting    | field    | limit | page | allAccounts |
	| Ascending  | headline | 50    | 1    | ignoring    |
	| Ascending  | date     | 50    | 1    | ignoring    |


Scenario Outline: Verify that HC engagement data is present
	Given I login as 'Impact Enabled Company'
	When I call the engagement endpoint <allAccounts> all accounts
	Then the Impact Engagement endpoint has the correct response

	Examples: 
	| allAccounts |
	| ignoring    |
	| including   |

Scenario Outline: Verify that HC webEvents data is present
	Given I login as 'Impact Enabled Company'
	When I call the webEvents endpoint <allAccounts> all accounts
	Then the Impact web events endpoint has the correct response

	Examples: 
	| allAccounts |
	| ignoring    |
	| including   |

Scenario Outline: Verify that HC views data is present
	Given I login as 'Impact Enabled Company'
	When I call the views endpoint <allAccounts> all accounts
	Then the Impact Views endpoint has the correct response

	Examples: 
	| allAccounts |
	| ignoring    |
	| including   |

Scenario Outline: Verify that HC audience data is present
	Given I login as 'Impact Enabled Company'
	When I call the audience endpoint <allAccounts> all accounts
	Then the Impact audience endpoint has the correct response

	Examples: 
	| allAccounts |
	| ignoring    |
	| including   |

