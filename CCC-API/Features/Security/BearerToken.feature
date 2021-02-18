Feature: BearerToken
	In order to verify a valid user
	As a CCC User
	I want to check the validity of a Bearer token

@acl
Scenario: Authenticated bearer token returns user information
	Given I login as 'OnpointCompany Manager'
	When I perform a GET to verify the token
	Then the token should be valid and return correct user information

@acl
Scenario: Invalid bearer tokens are not validated
	Given I perform a GET to verify the token
	Then the token endpoint response status should be '401'