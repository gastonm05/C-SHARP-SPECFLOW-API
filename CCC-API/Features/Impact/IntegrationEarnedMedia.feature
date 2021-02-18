@impact
Feature: IntegrationEarnedMedia
	To verify that earned media data is present
	As a valid CCC user
	I want to call impact earned media endpoint and to verify we receive content

Scenario Outline: Verify that HC TopOutlet earnedMedia data is received
	Given I login as 'Impact Enabled Company'
		And searches are available for earned media
	When I call the earned TopOutlet endpoint on the last <days> days
	Then the earned TopOutlet endpoint has the correct response

	Examples:
	| days |
	|  90  |
	|  30  |
	|   7  |

Scenario Outline: Verify that HC WebEvents earnedMedia data is received
	Given I login as 'Impact Enabled Company'
		And searches are available for earned media
	When I call the earned WebEvents endpoint on the last <days> days
	Then the earned WebEvents endpoint has the correct response

	Examples:
	| days |
	|  90  |
	|  30  |
	|   7  |

Scenario Outline: Verify that HC Views earnedMedia data is received
	Given I login as 'Impact Enabled Company'
		And searches are available for earned media
	When I call the earned views endpoint on the last <days> days
	Then the earned views endpoint has the correct response

	Examples:
	| days |
	|  90  |
	|  30  |
	|   7  |


Scenario Outline: Verify that HC audience earnedMedia data match with CisionId audience earnedMedia data
	Given I login as 'Impact Enabled Company'
		And searches are available for earned media
	When I call the earned Audience endpoint on the last <days> days
	Then the earned Audience endpoint has the correct response

	Examples:
	| days |
	|  90  |
	|  30  |
	|   7  |