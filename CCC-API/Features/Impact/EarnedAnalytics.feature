@impact
Feature: EarnedAnalytics
	To verify that earned analytics retrieve data per chart
	As a valid CCC user
	I want to call the earned-analytics endpoints


Scenario Outline: Earned Views data is correct on analytics view
	Given I login as 'Impact Enabled Company'
		And I call the search endpoint in order to get the search id
	When I call the earned views endpoint on the last <days> days
	Then the earned views endpoint has the correct response

	Examples:
	| days |
	|  90  |
	|  30  |
	|   7  |

Scenario Outline: Earned WebEvents data is correct on analytics view
	Given I login as 'Impact Enabled Company'
		And I call the search endpoint in order to get the search id
	When I call the earned WebEvents endpoint on the last <days> days
	Then the earned WebEvents endpoint has the correct response

	Examples:
	| days |
	|  90  |
	|  30  |
	|   7  |

Scenario Outline: Earned Audience data is correct on analytics view
	Given I login as 'Impact Enabled Company'
		And I call the search endpoint in order to get the search id
	When I call the earned Audience endpoint on the last <days> days
	Then the earned Audience endpoint has the correct response

	Examples:
	| days |
	|  90  |
	|  30  |
	|   7  |

Scenario Outline: Earned TopOutlet data is correct on analytics view
	Given I login as 'Impact Enabled Company'
		And I call the search endpoint in order to get the search id
	When I call the earned TopOutlet endpoint on the last <days> days
	Then the earned TopOutlet endpoint has the correct response

	Examples:
	| days |
	|  90  |
	|  30  |
	|   7  |