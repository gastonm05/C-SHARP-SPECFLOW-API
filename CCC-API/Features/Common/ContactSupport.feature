Feature: ContactSupport
	
	To verify Contact Support feature it's working properly we validate all possible scenarios 
	in the creation of Contact Support requests using all C3 available languages.


@Request @Contact @Support @configuration
Scenario: A manager user verifies Response containing contact support info is recieved for the given language key.

	
	Given I login as 'API Manager User'
	When Send a GET request to Contact Support  endpoint with a separate request for each supported language key and  verify response should be correct on each case


