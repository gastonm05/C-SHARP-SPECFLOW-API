Feature: ContactProfile
	In order to view details about a contact
	as a standard CCC User
	I want to see the contact's profile

@media @ignore @media
Scenario: IsProprietaryContact is set to false for a public contact
	Given I login as 'Posdemo Manager'
	When I perform a GET for Contact 'walter mossberg'
	Then the IsProprietaryContact field should be FALSE

@media @ignore @media
Scenario: IsProprietaryContact is set to true for a private contact
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for Contact 'john mctesterson'
	Then the IsProprietaryContact field should be TRUE

@media @media
Scenario: Consolidated profile for contacts related
	Given I login as 'Posdemo Manager'
	When I perform a GET for Contact 'Walter Mossberg'
	And I perform a GET for related contacts using the id
	And  I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'Mossberg, Walter'
	Then I should see the related Contacts for that profile matches with the all coopers data

@media @ignore @media
Scenario: Verify that the properly oulets returned on activities response
	Given I login as 'Posdemo Manager'
	When  I perform a GET for Contact 'Chris Cuomo'
	And I perform a GET for history contact information using the id
	And  I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'Cuomo, Chris'
	And I perform a GET for Contacts with the filter 'National' in the filter category 'dmaid'	
	Then all outlets information should match with the data from previous search

@media @ignore @media
Scenario: Verify that notes are added across all consolidated contacts
	Given I login as 'Posdemo Manager'
	When  I perform a GET for Contact 'Walter Mossberg'
	And I perform a PATCH to add some random notes	
	And  I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'Mossberg, Walter'
	Then I should get same notes for all consolidated profile

@media @ignore @media
Scenario: Edit the Opted Out property for a contact
	Given shared session for 'standard' user with edition 'basic'
	When  I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I perform a GET to the detail using the first id from the previous search
	And I perform a PATCH for the opted out property 'true'
	Then I should see the Opted Out property updated

@media @ignore @media
Scenario: Verify that EntityId and custom activities values on profile page are not null
	Given shared session for 'standard' user with edition 'basic'
	When  I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'Cuomo, Chris'
	And I perform a GET to for history using the first id from the previous search	
	Then I should see non null values on Customactivities and EntityId

@media @ignore @media
Scenario: Working Languages array is set properly for a public contact
	Given I login as 'Posdemo Manager'
	When I perform a GET for Contact 'anderson cooper'
	Then the WorkingLanguages array is set properly