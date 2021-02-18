Feature: ExportEdCals
	In order to export a list of EdCals
	As a standard CCC user
	I want an endpoint to export EdCals

@media
Scenario: Export EdCals response returns a valid id, status, and file value
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for EdCals by Issue Date with a 'start' date of 'Today minus 90 days'
	And I perform a POST to export EdCals
	Then the EdCals Export response has a valid id, status, and file

@media @ignore
Scenario: Exporting EdCals creates a task
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for EdCals by Issue Date with a 'start' date of 'Today minus 150 days'
	And I perform a POST to export EdCals
	And I perform a GET for EdCal jobs with the id from the export
	Then the job response status code is '200'