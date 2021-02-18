Feature: Settings - CustomFields Endpoint
	To verify that CustomFields can be created, retrieved, modified and deleted
	As a valid CCC user from a company with parameter Elysium-CustomFields-Enabled set to true
	I want to call the Customfields endpoint - api/v1/customfields

@acl @Settings @CustomFields 
Scenario: An Admin User can create a new News CustomFields 
	Given I login as 'Custom Fields - Manager User'
	When I perform a POST on customfields endpoint to create a 'String' 'News' customfield
	Then CustomField endpoint 'POST' response code should be 201  
	Then Delete just created CustomFields and response code should be 200   

@acl @Settings @CustomFields @Ignore
Scenario: An Admin User with already created News customFields can view them
	Given I login as 'Custom Fields - Manager User'
	When I perform a GET on customfields 'news' endpoint
	Then Get customfields endpoint response code should be 200

@acl @Settings @CustomFields @Ignore
Scenario: An Admin User can create/edit/delete Text News customFields 
	Given I login as 'Custom Fields - Manager User'
	When I perform a POST on customfields endpoint to create a 'String' 'News' customfield
	Then CustomField endpoint 'POST' response code should be 201
	When I perform a PUT on CustomField endpoint to modify the one just created with this data :
				| id| name			| type		|defaultValue	|entityType		|maxlength  |
				|	| ADUSERMEMO    | String	|tests			|News			|255		|  
	Then CustomField endpoint 'PUT' response code should be 201
	Then I verify all CustomField data was changed
	Then Delete just created CustomFields and response code should be 200

@acl @Settings @CustomFields @Ignore
Scenario: An Admin User can create/edit/delete Memo News customFields 
	Given I login as 'Custom Fields - Manager User'
	When I perform a POST on customfields endpoint to create a 'Memo' 'News' customfield
	Then CustomField endpoint 'POST' response code should be 201
	When I perform a PUT on CustomField endpoint to modify the one just created with this data :
				| id| name			| type		|defaultValue	|entityType		|maxlength  |
				|	| ADUSERMEMO    | Memo	    |tests			|News			|			| 
	Then CustomField endpoint 'PUT' response code should be 201
	Then I verify all CustomField data was changed
	Then Delete just created CustomFields and response code should be 200

@acl @Settings @CustomFields @Ignore
Scenario: An Admin User can create/edit/delete  Number News customFields
	Given I login as 'Custom Fields - Manager User'
	When I perform a POST on customfields endpoint to create a 'Number' 'News' customfield
	Then CustomField endpoint 'POST' response code should be 201
	When I perform a PUT on CustomField endpoint to modify the one just created with this data :
				| id| name			| type		|defaultValue	|entityType		|maxlength  |
				|	| ADUSERNUMBER  | Number	|3000			|News			|255		|
	Then CustomField endpoint 'PUT' response code should be 201
	Then I verify all CustomField data was changed
	Then Delete just created CustomFields and response code should be 200

@acl @Settings @CustomFields @Ignore
Scenario: An Admin User can create/edit/delete  Date News customFields
	Given I login as 'Custom Fields - Manager User'
	When I perform a POST on customfields endpoint to create a 'Date' 'News' customfield
	Then CustomField endpoint 'POST' response code should be 201
	When I perform a PUT on CustomField endpoint to modify the one just created with this data :
				| id| name			| type		|defaultValue	|entityType		|maxlength  |
				|	| ADUSERDate    | Date		|				|News			|255		|
	Then CustomField endpoint 'PUT' response code should be 201
	Then I verify all CustomField data was changed
	Then Delete just created CustomFields and response code should be 200

@acl @Settings @CustomFields @Ignore
Scenario: An Admin User can create/edit/delete Single-select News customFields
	Given I login as 'Custom Fields - Manager User'
	When I perform a POST on customfields endpoint to create a 'Single-Select' 'News' customfield
	Then CustomField endpoint 'POST' response code should be 201
	When I perform a PUT on CustomField endpoint to modify the one just created with this data :
				| id| name					| type				|defaultValue	|entityType		|maxlength  |
				|	| NewSingleSelectName   | Single-Select		|				|News			|255		|
	Then CustomField endpoint 'PUT' response code should be 201
	Then I verify all CustomField data was changed
	Then Delete just created CustomFields and response code should be 200

@acl @Settings @CustomFields @Ignore
Scenario: An Admin User can create/edit/delete  Multi-select News custom Fields
	Given I login as 'Custom Fields - Manager User'
	When I perform a POST on customfields endpoint to create a 'Multi-Select' 'News' customfield
	Then CustomField endpoint 'POST' response code should be 201
	When I perform a PUT on CustomField endpoint to modify the one just created with this data :
				| id| name					| type				|defaultValue	|entityType		|maxlength  |
				|	| NewSingleSelectName   | Multi-Select		|				|News			|255		|
	Then CustomField endpoint 'PUT' response code should be 201
	Then I verify all CustomField data was changed
	Then Delete just created CustomFields and response code should be 200

@acl @Settings @CustomFields @Ignore
Scenario: An Admin User can create bad put request the systems should responde with 400 code
	Given I login as 'Custom Fields - Manager User'
	When I perform a POST on customfields endpoint to create a 'String' 'News' customfield
	Then CustomField endpoint 'POST' response code should be 201
	When I perform a PUT on CustomField endpoint to modify the one just created with this data :
				| id| name			| type		|defaultValue	|entityType		|maxlength  |
				|	| ADUSERNUMBER  | Nothing	|				|News			|255		|
	Then CustomField endpoint 'PUT' response code should be 400
	Then Delete just created CustomFields and response code should be 200


@acl @Settings @CustomFields 
Scenario: An Standard User  can see already created news custom fields 
	Given I login as 'ACL Standard User'
	When I perform a GET on customfields 'news' endpoint
	Then Get customfields endpoint response code should be 200

@acl @Settings @CustomFields @Ignore
Scenario: An Standard User can NOT create text  News customFields
	Given I login as 'ACL Standard User'
	When I perform a POST on customfields endpoint to create a 'String' 'News' customfield
	Then CustomField endpoint 'POST' response code should be 403

@acl @Settings @CustomFields @Ignore
Scenario: An ReadOnly User  can see already created news custom fields 
	Given I login as 'ACL ReadOnly User'
	When I perform a GET on customfields 'news' endpoint
	Then Get customfields endpoint response code should be 200

@acl @Settings @CustomFields @Ignore
Scenario: An ReadOnly User can NOT create text  News customFields
	Given I login as 'ACL ReadOnly User'
	When I perform a POST on customfields endpoint to create a 'String' 'News' customfield
	Then CustomField endpoint 'POST' response code should be 403

@acl @Settings @CustomFields @Ignore
Scenario: An Admin User with already created Activities customFields can view them
	Given I login as 'ACL Manager User'
	When I perform a GET on customfields 'activity' endpoint
	Then Get customfields endpoint response code should be 200

@acl @Settings @CustomFields @Ignore
Scenario: An Admin User can create/edit/delete Text Activity customFields 
	Given I login as 'Custom Fields - Manager User'
	When I perform a POST on customfields endpoint to create a 'String' 'Activity' customfield
	Then CustomField endpoint 'POST' response code should be 201
	When I perform a PUT on CustomField endpoint to modify the one just created with this data :
				| id| name			| type		|defaultValue	|entityType		|maxlength  |
				|	| ADUSERMEMO    | String	|tests			|Activity			|255		|  
	Then CustomField endpoint 'PUT' response code should be 201
	Then I verify all CustomField data was changed
	Then Delete just created CustomFields and response code should be 200

@acl @Settings @CustomFields @Ignore
Scenario: An Admin User can create/edit/delete Memo Activity customFields 
	Given I login as 'Custom Fields - Manager User'
	When I perform a POST on customfields endpoint to create a 'Memo' 'Activity' customfield
	Then CustomField endpoint 'POST' response code should be 201
	When I perform a PUT on CustomField endpoint to modify the one just created with this data :
				| id| name			| type		|defaultValue	|entityType		|maxlength  |
				|	| ADUSERMEMO    | Memo	    |tests			|Activity		|			| 
	Then CustomField endpoint 'PUT' response code should be 201
	Then I verify all CustomField data was changed
	Then Delete just created CustomFields and response code should be 200

@acl @Settings @CustomFields @Ignore
Scenario: An Admin User can create/edit/delete  Number Activity customFields
	Given I login as 'Custom Fields - Manager User'
	When I perform a POST on customfields endpoint to create a 'Number' 'Activity' customfield
	Then CustomField endpoint 'POST' response code should be 201
	When I perform a PUT on CustomField endpoint to modify the one just created with this data :
				| id| name			| type		|defaultValue	|entityType		|maxlength  |
				|	| ADUSERNUMBER  | Number	|3000			|Activity			|255		|
	Then CustomField endpoint 'PUT' response code should be 201
	Then I verify all CustomField data was changed
	Then Delete just created CustomFields and response code should be 200

@acl @Settings @CustomFields @Ignore
Scenario: An Admin User can create/edit/delete  Date Activity customFields 
	Given I login as 'Custom Fields - Manager User'
	When I perform a POST on customfields endpoint to create a 'Date' 'Activity' customfield
	Then CustomField endpoint 'POST' response code should be 201
	When I perform a PUT on CustomField endpoint to modify the one just created with this data :
				| id| name			| type		|defaultValue	|entityType		|maxlength  |
				|	| ADUSERDate    | Date		|				|Activity			|255		|
	Then CustomField endpoint 'PUT' response code should be 201
	Then I verify all CustomField data was changed
	Then Delete just created CustomFields and response code should be 200

@acl @Settings @CustomFields @Ignore
Scenario: An Admin User can create/edit/delete Single-select Activity customFields
	Given I login as 'Custom Fields - Manager User'
	When I perform a POST on customfields endpoint to create a 'Single-Select' 'Activity' customfield
	Then CustomField endpoint 'POST' response code should be 201
	When I perform a PUT on CustomField endpoint to modify the one just created with this data :
				| id| name					| type				|defaultValue	|entityType		|maxlength  |
				|	| NewSingleSelectName   | Single-Select		|				|Activity			|255		|
	Then CustomField endpoint 'PUT' response code should be 201
	Then I verify all CustomField data was changed
	Then Delete just created CustomFields and response code should be 200

@acl @Settings @CustomFields @Ignore
Scenario: An Admin User can create/edit/delete  Multi-select Activity custom Fields
	Given I login as 'Custom Fields - Manager User'
	When I perform a POST on customfields endpoint to create a 'Multi-Select' 'Activity' customfield
	Then CustomField endpoint 'POST' response code should be 201
	When I perform a PUT on CustomField endpoint to modify the one just created with this data :
				| id| name					| type				|defaultValue	|entityType		|maxlength  |
				|	| NewSingleSelectName   | Multi-Select		|				|Activity			|255		|
	Then CustomField endpoint 'PUT' response code should be 201
	Then I verify all CustomField data was changed
	Then Delete just created CustomFields and response code should be 200
