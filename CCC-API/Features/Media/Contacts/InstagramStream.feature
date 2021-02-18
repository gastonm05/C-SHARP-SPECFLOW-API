Feature: InstagramStream
	In order to see a contact's Instagram stream
	as a CCC user
	I want to be able to follow, unfollow a contact's Instagram and retrieve a contact's Instagram details

@media @ignore
Scenario: Get Instagram details
	Given I login as 'Instagram OAuthed'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'anderson cooper'
	And I perform a GET for the first contact listed
	And I perform a GET for Instagram details for the selected contact
	Then I should see the username 'socialmediaautomation' in the response

@media @ignore
Scenario: Follow a contact's Instagram

	Given I login as 'Instagram OAuthed'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'anderson cooper'
	And I perform a GET for the first contact listed
	And I Follow the selected contact's Instagram
	#Then the response should be '"Follows"' - Replace last step with this one if testing against Prod
	Then The response should contains 'APINotFoundError'

@media @ignore
Scenario: Unfollow a contact's Instagram
	Given I login as 'Instagram OAuthed'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'anderson cooper'
	And I perform a GET for the first contact listed
	And I Follow the selected contact's Instagram
	And I Unfollow the selected contact's Instagram
	#Then the response should be '"None"' - Replace last step with this one if testing against Prod
	Then The response should contains 'APINotFoundError'