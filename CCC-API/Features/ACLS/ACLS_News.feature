Feature: News - ACLS Endpoint
	To verify that a list of permissions retrieved
	As a valid CCC user
	I want to call the ACLS endpoint

@acl
Scenario: Validate News Tags Permissions Values for Read Only User
	Given I login as 'readOnly1418'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for News should be:
						| property | subProperty | subPropertyOther | permission | value |
						| Tags     |             |                  | CanView    | True  |
						| Tags     |             |                  | CanCreate  | False |
						| Tags     |             |                  | CanEdit    | False |
						| Tags     |             |                  | CanDelete  | False |