Feature: ExportActivities
	In order to save my activities to Excel
	I can export them

@publishers @activities @ignore
Scenario Outline: Activities > Export as XLSX
	Given session for edition 'Publishers manager user', permission: '<permission>', datagroup: 'dbg1'
	When I export activities with default sections
	Then the job created with pending status and link to the report
	And I can download xlxs report by link with correct exported activities

	Examples: 
	| permission  |
	| standard    |
	| read_only   |
	| system_admin|