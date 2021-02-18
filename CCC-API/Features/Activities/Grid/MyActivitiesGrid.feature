Feature: ActivitiesGrid
	Can be configured for user needs

@publishers  @activities @ignore
Scenario Outline: My Activities > Columns > Disable columns > Change order
	Given session for '<permission>' user with edition '<edition>'
	When I POST my activities grid order: 
	| Name   | Order | Visibility |
	| Title  | 0     | true       |
	| Type   | 2     | false      |
	| Status | 1     | false      |
	| Time   | 3     | true       |
	Then my activities grid view response contains expected settings
	When I GET my activities grid columns
	Then my activities grid view response contains expected settings
	
Examples: 
	| permission   | edition                                  |
	| read_only    | basic                                    |
	| standard     | Manager user Campaign-Enabled company    |
	| system_admin | Publishers social company, custom fields |

@publishers @activities @ignore
Scenario: Activities > Edit campaigns
	Given session for 'system_admin' user with edition 'Manager user Campaign-Enabled company'
	When I POST campaign '1' times
	Then I assign '2' activities of type 'Callback' to random campaign
	And I remove previous activities from campaign
	And I can delete campaign
	And campaign is not in campaigns list
