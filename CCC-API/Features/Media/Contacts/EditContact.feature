Feature: EditContact
	In order to update a contact's info
	As a CCC standard User
	I want to edit a contact's profile

@media @ignore
Scenario: Edit a private contacts pitching profile
	Given shared session for 'standard' user with edition 'basic'
	When I perform a PATCH to edit the Pitching Profile of 'John McTesterson'
	Then the Pitching Profile should match the updated value

@media @ignore
Scenario: Edit a private contacts website url
	Given shared session for 'standard' user with edition 'basic'
	When I perform a PATCH to edit the Website URL of 'John McTesterson'
	Then the Website URL should match the updated value