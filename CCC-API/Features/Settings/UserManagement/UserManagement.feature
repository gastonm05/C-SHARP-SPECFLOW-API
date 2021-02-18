Feature: UserManagement  
	To verify UserManagement section it's working properly we validate all possible scenarios 
	in the creation and modification of users using all availables User Types.

@acl @UserManagement 
Scenario: Validate that a User can save his selected default page 
	Given the API test data 'UserManagerData.json'
	And I login as 'User Management Company'
	When I perform a GET for management/user/management  endpoint to get existing OMC user 
	And  I perform a PUT for management/user/management endpoint to set 'news.since' as default page
	Then I verify PUT transaction was successfully completed 

@acl @UserManagement @Password @Forgot @Settings @Reset
Scenario: Validate that a Sysadmin User can reset his password using Forgot Password flow for a company with Advance Security Password enabled.
	Given the API test data 'ForgotPasswordData.json'
	And I perform a POST to api/v1/management/user/password/forgot endpoint to start reset password flow
	Then Forgot Password Endpoint response should be '200'
	And I get from DB recently created UserAccountResetPasswordid for this user
	When I perform a POST on Reset Password Endpoint using 'Advanced' password security
	Then Reset Password Endpoint response should be '200' and status code be '0'

@acl @UserManagement @Password @Forgot @Settings @Reset
Scenario: Validate that a Sysadmin User can reset his password using Forgot Password flow for a company with Advance Security Password disabled.
	Given the API test data 'ForgotPasswordRegularData.json'
	And I perform a POST to api/v1/management/user/password/forgot endpoint to start reset password flow
	Then Forgot Password Endpoint response should be '200'
	And I get from DB recently created UserAccountResetPasswordid for this user
	When I perform a POST on Reset Password Endpoint using 'Regular' password security
	Then Reset Password Endpoint response should be '200' and status code be '0'

@acl @UserManagement @Password @Forgot @Settings @Reset @Ignore
Scenario: Validate that a Sysadmin User cannot reset his password using a password without haveing at least 8 charsfor a company with Advance Security Password enabled.
	Given the API test data 'ForgotPasswordData.json'
	And I perform a POST to api/v1/management/user/password/forgot endpoint to start reset password flow
	Then Forgot Password Endpoint response should be '200'
	And I get from DB recently created UserAccountResetPasswordid for this user
	When I perform a POST on Reset Password Endpoint using 'Regular' password security
	Then Reset Password Endpoint response should be '200' and status code be '1'

@acl @UserManagement @Password @Forgot @Settings @Reset @Ignore
Scenario: Validate that a Sysadmin User cannot reset his password using a password without any number char for a company with Advance Security Password enabled.
	Given the API test data 'ForgotPasswordData.json'
	And I perform a POST to api/v1/management/user/password/forgot endpoint to start reset password flow
	Then Forgot Password Endpoint response should be '200'
	And I get from DB recently created UserAccountResetPasswordid for this user
	When I perform a POST on Reset Password Endpoint using 'AnyNumberChar' password security
	Then Reset Password Endpoint response should be '200' and status code be '1'

@acl @UserManagement @Password @Forgot @Settings @Reset @Ignore
Scenario: Validate that a Sysadmin User cannot reset his password using a password without any letter char (A or a) for a company with Advance Security Password enabled.
	Given the API test data 'ForgotPasswordData.json'
	And I perform a POST to api/v1/management/user/password/forgot endpoint to start reset password flow
	Then Forgot Password Endpoint response should be '200'
	And I get from DB recently created UserAccountResetPasswordid for this user
	When I perform a POST on Reset Password Endpoint using 'AnyLetterChar' password security
	Then Reset Password Endpoint response should be '200' and status code be '1'

@acl @UserManagement @Password @Forgot @Settings @Reset @Ignore
Scenario: Validate that a Sysadmin User cannot reset his password using a same as current password for a company with Advance Security Password enabled.
	Given the API test data 'ForgotPasswordSameData.json'
	And I perform a POST to api/v1/management/user/password/forgot endpoint to start reset password flow
	Then Forgot Password Endpoint response should be '200'
	And I get from DB recently created UserAccountResetPasswordid for this user
	When I perform a POST on Reset Password Endpoint using 'SamePassword' password security
	Then Reset Password Endpoint response should be '200' and status code be '2'
	