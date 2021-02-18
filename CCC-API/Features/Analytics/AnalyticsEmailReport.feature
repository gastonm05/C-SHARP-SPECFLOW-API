Feature: AnalyticsEmailReport
	In order to share my dashboards
	I can generate analytics reports

@publishers @ignore @analytics
Scenario Outline: Analytics > Email Report > Schedule
    Given I remember file 'AnalyticsShareSections.json'
	And   shared session for '<permissions>' user with edition 'Analytics company with features enabled and dynamic news'
	When  I schedule (POST) '<end_time>' email alert with settings 'AnalyticsShareSections.json'
	Then  periodic email exists in admin alert management list with proper time, delivery days, status '1', recipients
	When  I unschedule (DELETE) periodic email alert
	Then  periodic email exists in admin alert management list with proper time, delivery days, status '3', recipients

Examples: 
	| permissions  | end_time          |
	| standard     | No time           |
	| system_admin | Today plus 2 days |

@publishers @ignore @analytics
Scenario: Analytics > Email Report > Schedule > Update periodic email
    Given I remember file 'AnalyticsShareSections.json'
	And   session for 'standard' user with edition 'Analytics company with features enabled and dynamic news'
	When  I schedule (POST) 'Today plus 30 days' email alert with settings 'AnalyticsShareSections.json' and days: 'Mon,Wed,Fri'
	Then  job in scheduled in DB for days 'Monday,Wednesday,Friday' excluding days 'Tuesday,Thursday,Saturday,Sunday'
	And   periodic email exists in admin alert management list with proper time, delivery days, status '1', recipients
	When  I edit (PUT) analytics alert with new subject, recipients, delivery days 'Tue' and time
	Then  periodic email exists in admin alert management list with proper time, delivery days, status '1', recipients
	And   I unschedule (DELETE) periodic email alert