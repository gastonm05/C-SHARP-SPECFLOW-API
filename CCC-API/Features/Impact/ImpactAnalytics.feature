@impact
Feature: ImpactAnalytics
	To verify that press releases retrieve data per chart
	As a valid CCC user
	I want to call the impact-analytics endpoint

Scenario Outline: Reach data is correct on Consolidated view 
	Given I login as 'Impact Enabled Company'
	When I call the views endpoint <allAccounts> all accounts
	Then the Impact Views endpoint has the correct response

	Examples:
	| allAccounts |
	| ignoring	  |
	| including   |

Scenario Outline: Engagement data is correct on Consolidated view 
	Given I login as 'Impact Enabled Company'
	When I call the engagement endpoint <allAccounts> all accounts
	Then the Impact Engagement endpoint has the correct response

	Examples:
	| allAccounts |
	| ignoring	  |
	| including   |

Scenario Outline: WebEvents data is correct on Consolidated view
	Given I login as 'Impact Enabled Company'
	When I call the webEvents endpoint <allAccounts> all accounts
	Then the Impact web events endpoint has the correct response

	Examples:
	| allAccounts |
	| ignoring	  |
	| including   |

Scenario Outline: Audience data is correct on Consolidated view
	Given I login as 'Impact Enabled Company'
	When I call the audience endpoint <allAccounts> all accounts
	Then the Impact audience endpoint has the correct response

	Examples:
	| allAccounts |
	| ignoring	  |
	| including   |

@smokeProd
Scenario: Reach data is correct for a single release
	Given I login as 'analytics manager'
	When I call the releases endpoint in order to get the Id and language code for a single release 
	Then the Impact Views endpoint has the correct response for a single release

Scenario: Engagement data is correct for a single release
	Given I login as 'Impact Enabled Company'
	When I call the releases endpoint in order to get the Id and language code for a single release
	Then the Impact Engagement endpoint has the correct response for a single release

Scenario: WebEvents data is correct for a single release
	Given I login as 'Impact Enabled Company'
	When I call the releases endpoint in order to get the Id and language code for a single release
	Then the Impact web events endpoint has the correct response for a single release

Scenario: Audience data is correct for a single release
	Given I login as 'Impact Enabled Company'
	When I call the releases endpoint in order to get the Id and language code for a single release
	Then the Impact audience endpoint has the correct response for a single release