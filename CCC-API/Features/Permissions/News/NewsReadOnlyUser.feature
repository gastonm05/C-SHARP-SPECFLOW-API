Feature: NewsReadOnlyUser
	Restrict permission for the Read only User

@herdOfGnus @ignore
Scenario: Read Only User cannot delete a news item
	Given shared session for 'read_only' user with edition 'basic'
	When I search for news by start date with a value of 'Today minus 10 days'
	And I delete a news item 
	Then the response code for deleting a news item should be'403'