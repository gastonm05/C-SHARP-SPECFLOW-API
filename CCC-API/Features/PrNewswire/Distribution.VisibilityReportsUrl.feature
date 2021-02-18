Feature: Distribution - VisibilityReportsUrl
	To verify that a valid url is returned
	As a valid CCC user
	I want to call the GET /api/v1/prnewswire/distribution/VisibilityReportsUrl endpoint

@Ignore @acl
Scenario: Validate valid URL is returned for a C3 User with valid PrNewsWire email address
	Given I login as 'C3 with OMC email user'
	When I perform a GET for prnewswire/distribution/VisibilityReportsUrl endpoint
	Then The Endpoint response code should be OK
	And the response URL should be valid	