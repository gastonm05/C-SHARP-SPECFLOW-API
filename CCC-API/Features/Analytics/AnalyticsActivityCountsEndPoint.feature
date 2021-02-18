
Feature: Analytics - Activity Counts End Point
	To verify activity counts can be retreived by frequency
	As a valid CCC user
	I want to call the activity counts endpoint
	@HeartsAndCharts @analytics
Scenario Outline: Activity Counts data is correct for different frequencies
	Given I login as 'analytics manager'
	When I perform a GET for activity counts for frequency '<frequency>'
	Then the activity counts endpoint returns activities by frequency '<frequency>'

	Examples:
	| frequency |
	| None      |
	| Daily     |
	| Weekly    |
	| Monthly   |
	| Yearly    |
	@HeartsAndCharts  @analytics @smokeProd
Scenario Outline: Get activity by campaign grouped by a certain field
	Given I login as 'analytics manager'
	When I create one campaign 'analytics' and I assign to it an activity '<title>' of type '<type>' with time <scheduleTime> '<timeZoneIdentifier>'
	Then I can get the activity by campaignId grouped by 'Type' with value '<type>'
		And I can get the activity by campaignId grouped by 'PublicationState' with value '<state>'
		And I can delete the activity with type '<type>' and the campaign

	Examples:
	| title				| type        | state	| timeZoneIdentifier		| scheduleTime  |
	| analytic-mail		| SendMailing | 2		| Eastern Standard Time		|     -30       | 
	| analytic-mail		| SendMailing | 0		| Central Standard Time		|	    6	    | 
	| analytic-other	| Other		  | 0		| AUS Eastern Standard Time	|	   25 	    |
	| analytic-other	| Other		  | 2		| Pacific Standard Time		|	  -40 	    |
	| analytic-callback | Callback    | 2		| Pacific Standard Time		|	  -30 	    |
	| analytic-callback | Callback    | 0		| AUS Eastern Standard Time	|	   10 	    |