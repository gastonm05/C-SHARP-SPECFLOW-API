Feature: EditorialSupport
	
	To verify Editorial Contact feature it's working properly we validate all possible scenarios 
	in the creation of Editorial Contact requests

@configuration @UserManagement @Request @Editorial @Contact @Ignore
Scenario: A User creates a Editorial Contact requests for a supported country (with Editorial support link) 
	Given the API test data 'EditorialSupportData.json'
	And I login as 'Global Portal On Canadian Language Company - Manager User'
	When I perform a POST for contact/editorial endpoint to send a Editorial Contact Request using a 'VALID' EditorialContactDetailsId 
	Then Editorial Contact endpoint response should be '200'

@configuration @UserManagement @Request @Editorial @Contact @Ignore
Scenario: A User creates a Editorial Contact requests for a unsupported country (without Editorial support link)  
	Given the API test data 'EditorialSupportData.json'
	And I login as 'Global Portal On Company - Manager User'
	When I perform a POST for contact/editorial endpoint to send a Editorial Contact Request using a 'VALID' EditorialContactDetailsId 
	Then Editorial Contact endpoint response should be '400'
	And Editorial Contact Endpoint response message should be 'Invalid EditorialContactDetailsId was specified'

@configuration @UserManagement @Request @Editorial @Contact @Ignore
Scenario: A User creates a Editorial Contact requests using an invalid LanguageKey
	Given the API test data 'EditorialSupportData.json'
	And I login as 'Global Portal On Company - Manager User'
	When I perform a POST for contact/editorial endpoint to send a Editorial Contact Request using a 'INVALID' EditorialContactDetailsId 
	Then Editorial Contact endpoint response should be '400'
	And Editorial Contact Endpoint response message should be 'Invalid EditorialContactDetailsId was specified'