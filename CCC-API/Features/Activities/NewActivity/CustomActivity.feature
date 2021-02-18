Feature: CustomActivity
	In order to organize my things
	I can create custom activity

@publishers @custom_activity @activities @smokeProd
Scenario Outline: Custom activity > Create (Post) > Activities > Delete
	Given I login as 'analytics manager'
	And custom activity combinations:
	 	| Title | Type        | Notes | TimeZoneIdentifier     | ScheduleTime  |
	 	|  4    | Callback    | 50    | GMT Standard Time      |	   -1	   | 
		|  15   | Appointment | 100   | Central Standard Time  |		0	   |  
		|  30   | Inquiry     | 10    | W. Europe Standard Time|		25	   |  
		|  20   | Other       | 1     | UTC                    |	   -24	   |  

    Given I take combination of '<type>'
	When I POST custom activity of '<type>' '<contact>' '<outlet>'
	Then can find activity of '<type>' and '<state>' activity listed among published activities
	And can GET information about Custom Activity of '<type>'
	
	Given shared session for 'system_admin' user with edition 'ESAManager'
	Then I can DELETE custom activity of type '<type>'
	And cannot find activity of '<type>' and '<state>' activity listed among published activities

	Examples:
	| type        | state     | contact | outlet |
	| Callback    | sent      |         |   ab   |
	| Appointment |           |         |   sw   |
	| Inquiry     | scheduled |         |        |
	| Other       | sent      |   test  |        |

@publishers @custom_activity @CustomFields @ignore @activities
Scenario: Custom activity > Create (Post) With DEFAULT Custom Fields
	Given shared session for 'system_admin' user with edition 'Publishers social company, custom fields'
	And custom activity combinations:
	 	| Title | Type        | Notes | TimeZoneIdentifier     | ScheduleTime  |
	 	| 100   | SendMailing | 5     | Pacific Standard Time  |        9      | 
	And custom fields for 'Activity' present: 
		| Custom Field Type |
		| String            |
		| Memo              |
		| Number            |
		| Yes / No          |
		| Date              |
		| Single-Select     |
		| Multi-Select      |
	And I take combination of 'SendMailing'
	When I POST custom activity of type 'SendMailing' with Custom Fields set to default values
	Then can find activity of 'SendMailing' and 'scheduled' activity listed among published activities
	And can GET information about Custom Activity of 'SendMailing' with custom fields values

@publishers @custom_activity @CustomFields @ignore @activities
Scenario: Custom activity > Create (Post) With Valid Custom Fields MAX values
	Given shared session for 'standard' user with edition 'Publishers social company, custom fields'
	And custom activity combinations:
	 	| Title | Type        | Notes | TimeZoneIdentifier     | ScheduleTime  |
	 	| 50    | Inquiry     | 0     | Romance Standard Time  |      -13      | 
	And custom fields for 'Activity' present: 
		| Custom Field Type |
		| String            |
		| Memo              |
		| Number            |
		| Yes / No          |
		| Date              |
		| Single-Select     |
		| Multi-Select      |
	And I take combination of 'Inquiry'
	When I POST custom activity of type 'Inquiry' with Custom Fields having each MAX allowed value
	Then can find activity of 'Inquiry' and 'sent' activity listed among published activities
	And can GET information about Custom Activity of 'Inquiry' with custom fields values

@publishers @custom_activity @ignore @activities
Scenario: Custom activity > Create Custom Activity with Type from Form Management
	Given session for edition 'ESAManager', permission: 'standard', datagroup: 'group2'
	And activity form with name 'Automation activity type', color '#FFBB00', icon 'fa-gift' exist (Settings > Form Management), edition 'ESAManager'
	And custom activity combinations:
	 	| Title | Type                     | Notes | TimeZoneIdentifier    | ScheduleTime |
	 	| 45    | Automation activity type | 10    | GMT Standard Time     | 24           |
	And I take combination of 'Automation activity type'
	When I POST custom activity with type 'Automation activity type'
	Then can find activity of 'Automation activity type' and 'Scheduled' activity listed among published activities
	And can GET information about Custom Activity of 'Automation activity type'
	
	