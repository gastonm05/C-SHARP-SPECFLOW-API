Feature: UserParameters
	In order to assign parameters to a user
	As a CCC user
	I want a user parameter endpoint

@acl @Ignore
Scenario: Create user parameter and verify its existence
	Given shared session for 'standard' user with edition 'ESAManager'
	When I create a user parameter with the name 'CCC-TestParam' and a value of 'ParamValue'
	And I perform a GET for user parameters with the name 'CCC-TestParam'
	Then the returned user parameter name is 'CCC-TestParam' and the value is 'ParamValue'

@acl @Ignore
Scenario: UnAuthenticated user cannot create user parameter
	Given I create a user parameter with the name 'CCC-ParamFail' and a value of 'ParamFail'
	Then the user parameter response code should be '401'

@acl @Ignore
Scenario: 404 error returned for non-existing parameters
	Given shared session for 'standard' user with edition 'ESAManager'
	When I perform a GET for user parameters with the name 'InvalidParam' to get value only
	Then the user parameter response code should be '404'