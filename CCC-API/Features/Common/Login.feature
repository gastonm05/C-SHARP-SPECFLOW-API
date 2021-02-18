Feature: Login
	In order to verify custom and non-custom logins
	the session key and user stored (or not stored) in the PropertyBucket
	should contain the expected values

@Login @smokeProd @smokeProd
Scenario: Login with custom user
	Given I login as 'analytics manager'
	Then the session key exists in the PropertyBucket
	Then the user exists in the PropertyBucket

@Login @ignore 
Scenario Outline: Attempt to login with invalid credentials
	Given I login to Company <company> with <username> and <password>
	Then I should see the message <message>
	And the session key should be empty
	And the connect status code should be <status>

Examples: 
	| company             | username             | password    | message                                             | status |
	| ShakeDownAutomation | apitest              | p           | ShakeDownAutomation is not an Elysium company       | 2      |
	| ShakeDownAutomation | a                    | 1           | Error creating session (Code: InvalidCompanyLogin). | 1      |
	| ShakeDown           | apitest              | 1           | Error creating session (Code: InvalidCompanyLogin). | 1      |
	| AdvancePwdCompany   | sysadminNew          | 33          | User requires a more secure password.               | 6      |
	| AdvancePwdCompany   | AutomationLockedUser | Verano2018$ | Account is Locked                                   | 8      |

@MSA @Login
Scenario Outline: Complete MSA login flow for a user without/with MSA token
	Given the API test data 'MSAloginInfo.json'
	When I login 'without' existing MSA TOKEN to Company <company> with <username> and <password> and <languageKey>
	Then the connect status code should be 5
	And I verify Activation code it's stored in userparameter table for <index>
	And Login content response has a valid value
	When I send a POST to msa endpoint with loginInfo and activation code just obtained 
	Then Verify response code is '0' and response has both valid session and save msa token to verify I can login with an existing MSA TOKEN
	When I login 'with' existing MSA TOKEN to Company <company> with <username> and <password> and <languageKey>
	Then Verify MSA existing response code is '0' and response has a valid session

Examples:
	| index  | company           | username              | password | languagekey |
	|   0    | Msaenabledcompany | FirstTimeUserSysadmin | 33       | en-us       |
	|   1    | Msaenabledcompany | FirstTimeUserStandard | 33       | en-us       |
	|	2    | Msaenabledcompany | FirstTimeUserReadOnly | 33       | en-us       |	
	
@MSA @Login
Scenario Outline: Verify invalid activation code validation for MSA login 
	Given the API test data 'MSAloginInfo.json'
	When I login 'without' existing MSA TOKEN to Company Msaenabledcompany with FirstTimeUserSysadmin and 33 and en-us
	Then Login content response has a valid value
	When I send a POST to msa endpoint with login info and <activationCode> 
	Then I verify response code it's <status> OK and response has <message>

Examples:
	| activationCode | message                   | status |
	| 1111111        | InvalidCode               | 1      |
	| 0              | Invalid MSA Login Request | 1      |

@Login @ChurnZero  
Scenario Outline: Verify autologin endpoint is working fine to send login event to ChurnZero
Given I login to Company <company> with <username> and <password>
When I sent a POST to AutoLogin endpoint using user credentials
Then I should see the proper AutoLogin response

Examples: 
	| company             | username               | password |
	| gnus3				  |AutoLoginAdmin	       | 1        |
	| gnus3		          |AutoLoginStandard       | 1        |
	| gnus3		          |AutoLoginReadOnly       | 1        |

@Login @SSOAutoLogin @SSO @Ignore	
Scenario Outline: Verify Successful response for new SSO autologin endpoint using a valid SSO company
When I sent a POST to SSO AutoLogin endpoint using this <company>
Then I should see the proper SSO AutoLogin <statusCode> - <identityProvider> - <message> response

Examples: 
	| company        | statusCode | identityProvider | message                                       |
	| OnpointCompany | 4          | okta-jt          |                                               |
	| NoExistent     | 1          |                  |                                               |
	| ACL   | 9          |                  | Single Sign On is not set up for this company |


		

@configuration @MSA @NeedsCleanupSuccess @Login @Ignore
Scenario: Verify Resend Code flow for MSA login are working as expected for Success scenario
	Given the API test data 'MSAloginInfo.json'
	When I login 'without' existing MSA TOKEN to Company Msaenabledcompany with MSACodeSuccessUser and 33 and en-us
	Then Login content response has a valid value
	When I send a POST to msa code endpoint using this login info for this scenario
	Then I verify Msa code endpoint status code is '0' OK and message is 'Success'

@configuration @MSA @NeedsCleanupMaxCodeResendsReached @Login @Ignore
Scenario: Verify Resend Code flow for MSA login are working as expected for Maximum Code Resends resend limit is Reached
	Given the API test data 'MSAloginInfo.json'
	When I login 'without' existing MSA TOKEN to Company Msaenabledcompany with MSACodeMaxCodeResendsReachedUser and 33 and en-us
	Then Login content response has a valid value
	When I send a POST to msa code endpoint using this login info for this scenario
	Then I verify Msa code endpoint status code is '2' OK and message is 'MaxCodeResendsReached'
	
 @Login @Ignore
Scenario: Verify Resend Code flow for MSA login are working as expected for Maximum Code Resends resend limit is Reached 10 times
	Given the API test data 'MSAloginInfo.json'
	When I login 'without' existing MSA TOKEN to Company Msaenabledcompany with MSACodeAccountLockedUser and 33 and en-us
	Then Login content response has a valid value
	When I send a POST to msa code endpoint using this login info for this scenario
	Then I verify Msa code endpoint status code is '3' OK and message is 'AccountLocked'
	
 @Login
Scenario Outline: Verify LCID user parameter gets created once user is logged in. 
	Given the API test data 'LCIDLoginInfo.json'
	When I login to Company <company> with <username> and <password> and <LCID>
	Then Verify response code is '0' and response has a valid session
	And User parameter <LCID> is created for this user with correct value.

Examples:
	| company          | username | password | LCID |
	| Churnzerocompany | manager  | 33       | 1031 |
	| Churnzerocompany | manager  | 33       | 1033 |
	| Churnzerocompany | manager  | 33       | 1036 |
	| Churnzerocompany | manager  | 33       | 1043 |
	| Churnzerocompany | manager  | 33       | 2057 |
	| Churnzerocompany | manager  | 33       | 4105 |