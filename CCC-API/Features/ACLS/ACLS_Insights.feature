Feature: Insights - ACLS Endpoint
	To verify that a list of permissions retrieved for Insights
	As a valid CCC user
	I want to call the ACLS endpoint

@acl @Ignore
Scenario: A company with ImageIQNavEnabled param false cannot access Insights - ImageIQ
	Given I login as 'PRWebElysiumCompany4'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Insights should be:
						| property | subProperty | subPropertyOther | permission | value               |
						| ImageIQ  | Access      |                  | IsGranted  | False               |
						| ImageIQ  | Access      |                  | Status     | Feature Not Enabled |
						| ImageIQ  | Access      |                  | StatusCode | 2                   |

@acl @Ignore
Scenario: A company with ImageIQNavEnabled param true can access Insights - ImageIQ
	Given I login as 'ImageIQNavEnabled param True - Manager'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Insights should be:
						| property | subProperty | subPropertyOther | permission | value          |
						| ImageIQ  | Access      |                  | IsGranted  | True           |
						| ImageIQ  | Access      |                  | Status     | Access Granted |
						| ImageIQ  | Access      |                  | StatusCode | 0              |