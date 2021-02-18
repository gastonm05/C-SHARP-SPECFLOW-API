Feature: AnalyticsShareUrl
	In order to share data to external stakeholders
	I can use share my views by url

@publishers @HeartsAndCharts @ShareUrl @ignore @analytics
Scenario Outline: Share url > Correct data
	Given shared session for '<permission>' user with edition 'analytics manager'
	And I take 'COMP_ANALYTICS_SYS_VIEW_NAME_DEFAULT' analytics view information
	When I share (POST) report as url with view id and a password
	Then valid url is generated
	
Examples: 
	| permission |
	| read_only  |
	| standard   |
	| system_admin|

@publishers @HeartsAndCharts @ShareUrl @ignore @analytics
Scenario: Share url > No password
	Given shared session for 'standard' user with edition 'analytics manager'
	And I take 'COMP_ANALYTICS_SYS_VIEW_NAME_DEFAULT' analytics view information
	When I share (POST) report as url with view id and no password
	Then the response: "Password is required"