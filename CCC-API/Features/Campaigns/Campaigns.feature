Feature: Campaigns
	In order to group my activities
	As a valid CCC user with system parameter Campaign-Enabled set to true
	I can assign assign activities to a campaign

@publishers @campaigns @ignore
Scenario Outline: Campaign > Create > Code
	Given I login as shared user 'Manager user Campaign-Enabled company'
	When I POST campaign with '<name>' and '<description>'
	Then I have to be given '<result>'

	Examples: 
	| name     | description | result |
	| some     |             | 200    |
	| 50 chars | 250 chars   | 200    |
	|          |             | 400    |
	|          | some        | 400    |
	| 51 chars | some        | 400    |
	| some     | 251 chars   | 400    |

@publishers @campaigns @ignore
Scenario: Campaign > Get Id
	Given I login as shared user 'Manager user Campaign-Enabled company'
	When I POST campaign '1' times
	Then I have to be given id
	And I can GET campaign by id

@publishers @campaigns @ignore
Scenario: Campaign > List of campaigns
	Given I login as shared user 'Manager user Campaign-Enabled company'
	When I POST campaign '2' times
	Then I have to be given ids
	And I can find '2' created campaigns in campaigns list

@publishers @campaigns @ignore
Scenario: Campaign > Edit
	Given I login as shared user 'Manager user Campaign-Enabled company'
	When I POST campaign '1' times
	Then I can edit campaign
	And I can find '1' created campaigns in campaigns list

@publishers @campaigns
Scenario: Campaign > Delete
	Given I login as shared user 'Manager user Campaign-Enabled company'
	When I POST campaign '1' times
	Then I can delete campaign
	And campaign is not in campaigns list

@publishers @campaigns
Scenario: Campaign > Assign Email Draft > Remove from Campaign
	Given I login as shared user 'Manager user Campaign-Enabled company'
	Given I create email 'draft'
	When I POST campaign '1' times
	Then I can assign 'draft' to campaign
	And can find 'draft' in publish activities by campaign
	When I delete 'draft' from campaign
	Then cannot find 'draft' in publish activities by campaign
	
@publishers @campaigns 
Scenario Outline: Campaign > Assign Custom Activity > Remove from Campaign
	Given I login as shared user 'Manager user Campaign-Enabled company'
	Given custom activity combinations:
	 	| Title | Type        | Notes | TimeZoneIdentifier          | ScheduleTime  |
	 	| 30    | SendMailing | 0     | Central Standard Time       |       6       | 
	 	|  15   | Other       | 50    | AUS Eastern Standard Time   |	   -8	    | 
		|  5    | Callback    | 32    | Pacific Standard Time       |	   -2 	    |
	Given I take combination of '<type>'
	Given I create custom activity of '<type>' '<state>' '<contact>' '<outlet>'
	When I POST campaign '1' times
	Then I can assign '<type>' to campaign
	And can find '<type>' in publish activities by campaign
	When I delete '<type>' from campaign
	Then cannot find '<type>' in publish activities by campaign

	Examples:
	| type        | state     | contact | outlet  |
	| SendMailing | scheduled | kevin   |         |
	| Other       | sent      |         | twitter |
	| Callback    | sent      |         |		  |

@publishers @campaigns
Scenario: Campaign > Assign News Article > Remove from Campaign
	Given shared session for 'standard' user with edition 'Manager user Campaign-Enabled company'
	And news article exist
	And campaign 'Assign' present (to create: 'system_admin' for 'Manager user Campaign-Enabled company')
    And campaign 'Assign 2' present (to create: 'system_admin' for 'Manager user Campaign-Enabled company')
    And campaign 'Assign 3' present (to create: 'system_admin' for 'Manager user Campaign-Enabled company')
	When I assign news article to campaign
	Then I see campaign 'Assign' in news article
    When I add an additional campaign
    Then news article campaigns are 'Assign' and 'Assign 2'
    When I add and remove campaigns on news article
    Then news article campaigns are 'Assign' and 'Assign 3'
	When I remove all campaigns from news article
	Then I cannot see any campaigns in news article

@publishers @campaigns
Scenario Outline: Campaign > Assign Social Post > Remove from Campaign
	
	Given session for '<permissions>' user with edition 'Manager user Campaign-Enabled company'
	And campaign 'Assign' present (to create: 'system_admin' for 'Manager user Campaign-Enabled company')
	And a social post activity to '<Platform>' with time '<time>', timezone 'W. Europe Standard Time'
	Then I can assign '<Platform>' to campaign
	And can find '<Platform>' in publish activities by campaign
	When I delete '<Platform>' from campaign
	Then cannot find '<Platform>' in publish activities by campaign

	Examples:
	| Platform | time     | permissions  |
	| Twitter  | tomorrow | standard     |
	| Twitter  | past     | system_admin |