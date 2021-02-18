Feature: DataGroup
	In order to separate data in my account
	As a standard CCC User
	I want to manage datagroups

@configuration
Scenario: Change to valid data group
	Given I login as 'basic'
	When I perform a PUT to change the datagroup
	Then the active datagroup should be changed

@configuration
Scenario: Change to invalid data group returns 404
	Given shared session for 'standard' user with edition 'basic'
	When I perform a PUT to change to datagroup '-1'
	Then the Accounts endpoint response should be '404'
	And the Accounts endpoint content should be '{"IncidentId":null,"Message":"The requested resource (-1) was not found.","ErrorCode":null,"ErrorData":null}'

@configuration
Scenario: Change to existing invalid data group returns 404
	Given shared session for 'standard' user with edition 'basic'
	When I perform a PUT to change to datagroup '13979293'
	Then the Accounts endpoint response should be '404'
	And the Accounts endpoint content should be '{"IncidentId":null,"Message":"The requested resource (13979293) was not found.","ErrorCode":null,"ErrorData":null}'

@configuration
Scenario: Data groups are returned in alphabetical order
	Given I login as 'basic'
	When I get all datagroups for the user
	Then the datagroups should be in alphabetical order

@configuration
Scenario: Get data groups with invalid company id returns 403
	Given shared session for 'standard' user with edition 'basic'
	When I get all datagroups for the user with company id '99999999'
	Then the Accounts endpoint response should be '403'

@configuration
Scenario: ALL datagroup is not returned as a valid datagroup
	Given I login as 'ESAManager'
	Then the '(ALL)' datagroup is not returned in list of datagroups