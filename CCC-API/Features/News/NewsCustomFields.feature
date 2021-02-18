Feature: NewsCustomFields
	In order to manage News
	As a C3 User
	I want to be able to CRUD Custom Fields

@herdOfGnus @news
Scenario: News - Expose News Custom Fields
	Given shared session for 'standard' user with edition 'basic'
	When I search for news by 'Keywords' with a value of 'owls'
	Then the News endpoint has the correct response
	And the News endpoint has news with value 'owls'
	When I perform GET to a single news item from search
	Then I should see that the News Clip contains Custom Fields

@herdOfGnus @ignore @news
Scenario: PATCH - News Custom Fields
	Given I login as 'analytics manager'
	When I perform a GET for all news
	And I perform GET to a single news item from search
	And I perform a GET for all available custom fields
	And I perform a PATCH to News Custom Field with type 'String' from Single News
	Then the News Custom Field Endpoint response should be '200'

@herdOfGnus @ignore @news
Scenario: PATCH - News Custom Fields Null out Date values
	Given I login as 'analytics manager'
	When I perform a GET for all news
	And I perform GET to a single news item from search
	And I perform a GET for all available custom fields
	And I perform a PATCH to null out News Custom Field with type 'Date' from Single News
	Then the News Custom Field Endpoint response should be '200'

@herdOfGnus @ignore @news
Scenario: PATCH - Bulk Edit Text Custom Field
	Given I login as 'ESAManager - Custom Fields'
	When I search for news by 'Keywords' with a value of 'automation testing'
	And I perform a PATCH to all the results to update 'Text UDF' custom field
	Then the News Custom Field Bulk Edit Endpoint response should be '204'

@herdOfGnus @ignore @news
Scenario: PATCH - Bulk Edit Decimal Custom Field
	Given I login as 'ESAManager - Custom Fields'
	When I search for news by 'Keywords' with a value of 'automation testing'
	And I perform a PATCH to all the results to update 'Decimal UDF' custom field
	Then the News Custom Field Bulk Edit Endpoint response should be '204'

@herdOfGnus @ignore @news
Scenario: PATCH - Bulk Edit Date Custom Field
	Given I login as 'ESAManager - Custom Fields'
	When I search for news by 'Keywords' with a value of 'automation testing'
	And I perform a PATCH to all the results to update 'Date UDF' custom field
	Then the News Custom Field Bulk Edit Endpoint response should be '204'

@herdOfGnus @ignore @news
Scenario: PATCH - Bulk Edit Boolean Custom Field
	Given I login as 'ESAManager - Custom Fields'
	When I search for news by 'Keywords' with a value of 'automation testing'
	And I perform a PATCH to all the results to update 'Bool UDF' custom field
	Then the News Custom Field Bulk Edit Endpoint response should be '204'
	
@herdOfGnus @ignore @news
Scenario: PATCH - Bulk Edit Memo Custom Field
	Given I login as 'ESAManager - Custom Fields'
	When I search for news by 'Keywords' with a value of 'automation testing'
	And I perform a PATCH to all the results to update 'Memo UDF' custom field
	Then the News Custom Field Bulk Edit Endpoint response should be '204'

@herdOfGnus @news
Scenario: Order by Custom Fields
	Given I login as 'ESAManager'
	When I perform a GET for all news
	And I perform a GET for all available custom fields
	And I perform a GET for news ordered by allowed custom field
	Then the News endpoint has the correct response for ordered news by custom field

@herdOfGnus @news
Scenario: News Search by Include or Exclude Custom Field 
	Given I login as 'ESAManager'
	When I perform a GET for all available custom fields
	And I select the first with Type 'string' and a MultiSelect value of 'true'
	And I perform a GET for news by selected Custom Field using the 'Include' operator
	Then the News Endpoint responds with a '200' for search by Include field
	And all items should be from the included custom field
	When I perform a GET for news by selected Custom Field using the 'Exclude' operator
	Then the News Endpoint responds with a '200' for search by Exclude field
	And none of the items should be from the excluded custom field
