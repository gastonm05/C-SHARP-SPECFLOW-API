Feature: MediaResearchRequest
	To verify Media Research Request feature, we validate
	creating a request for a Media Contact and a Media Outlet

@media @ignore @bugCCC-15825
Scenario: A User creates a Media Research Request for a Media Contact
	Given I login as 'Manager Standard User'
	When I perform a POST to Media Research Request Endpoint for Contact 'Stephanie Lee Fatta' and Change Type 'ContactUpdate'
	Then the Media Research Request Endpoint response code should be '200'

@media
Scenario: A User creates a Media Research Request for a Media Contact using an improper Change Type
	Given I login as 'Standard User'
	When I perform a POST to Media Research Request Endpoint for Contact 'Stephanie Lee Fatta' and Change Type 'ContactAdditon'
	Then the Media Research Request Endpoint response code should be '400'

@media
Scenario: A User creates a Media Research Request for a Media Outlet
	Given I login as 'Standard User'
	When I perform a POST to Media Research Request Endpoint for Outlet 'chicago tribune' and Change Type 'ContactAddition'
	Then the Media Research Request Endpoint response code should be '200'

@media
Scenario: A User creates a Media Research Request for a invalid entity type
	Given I login as 'Standard User'
	When I perform a POST to Media Research Request Endpoint for an invalid entity type and Change Type 'ContactUpdate'
	Then the Media Research Request Endpoint response code should be '400'