Feature: ContactsEndpoint
	In order to find media contacts
	As a standard user
	I want to query contacts via Contacts Endpoint

@media @media
Scenario: Search Contacts by Contact Name
	Given  I login as 'Standard User'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper, anderson'
	Then all returned Contacts should contain 'cooper, anderson' in their sortname

@media @ignore @media
Scenario: Search Contacts by Contact Name in first name, last name order
	Given I login as 'Standard User'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'Chris Cuomo'
	Then all returned Contacts should contain 'Cuomo, Chris' in their sortname

@media @ignore @media
Scenario: Search Contacts by Outlet Name
	Given I login as 'Standard User'
	When I perform a GET for Contacts by 'Outlet_Name' criteria with a value of 'new yorker,the'
	Then all returned Contacts should contain 'The New Yorker' in their outlet name

@media @media
Scenario: Search Contacts by Talking About
	Given I login as 'Standard User'
	When I perform a GET for Contacts by 'Influencer_Keyword' criteria with a value of 'tech'
	Then the Contact response code should be '200'
	And the search should return contacts

@media @ignore @media
Scenario: Search Contacts by Subject 'Academic Certification'
	Given I login as 'Standard User'
	When I perform a GET for Contacts by 'Subject' criteria with a value of '100000'
	Then all returned contact objects should contain 'Academic Certification' in their subjects
	
@media @ignore @media
Scenario Outline: Search Contacts by Outlet Location City 'Chicago, Illinois, United States of America'
	Given I login as 'Standard User'
	When I perform a GET for Contacts by 'Outlet_Location' criteria with a value of '<id>'
	And I perform a GET for an Outlet by Id using the first contact's outlet id from the previous search
	Then the returned outlet object should have '<value>' as their city

Examples: 
| id     | value   |
| 4-5946 | Chicago |
	
@media @media
Scenario: Verify if the Twitter filter is applied
	Given I login as 'Standard User'
	When I perform a GET for Contacts by 'Subject' Education ID:'319000'
	And I perfom a GET using the Twitter filter '1247750878'
	Then the filter should be applied  for 'twitter' value

@media @ignore @media
Scenario: Verify if the Linkedin filter is applied
	Given I login as 'social filter'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'young'
	And I perform a GET using the Linkedin filter '1154326215'
	Then the filter should be applied  for 'linkedin' value 

@media @ignore @media
Scenario: Verify if the Facebook filter is applied
	Given I login as 'social filter'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'young'
	And I perform a GET using the Facebook filter '1168361333'
	Then the filter should be applied  for 'facebook' value 

@media @ignore @media
Scenario: Verify that similar contacts endpoint retrieve data
	Given I login as 'Standard User'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'Storms, Kimberly'
	And I perform a GET for Similar Contacts using the first id from the previous search
	Then I should see Similarity Score property greater than 0

@media @media
Scenario: Create new list after contact search
	Given I login as 'Standard User'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I perform a Post for creating a list with a random name using the top three ids
	Then the list created should return a non-null response

@media @ignore @media
Scenario: Verify new list cannot have a name greater than 255 characters
	Given I login as 'Standard User'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I perform a Post for creating a list with a long name
	Then the response will be an error message

@media @ignore @media
Scenario: Delete a list by name
	Given I login as 'Standard User'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I perform a Post for creating a list with a random name using the top three ids
	And I perform a DELETE for the list
	Then the response should contain the list

@media @ignore 
Scenario: Subject must be provided in Send Email
	Given I login as 'Manager Standard User'
	When I perform a POST for exporting contact '2449491' without a subject
	Then the response should return invalid because '"Subject must be provided."'

@media @ignore
Scenario: Influencer rankings appear in incremental order
	Given I login as 'Manager Standard User'
	When I perform a GET for Contact 'stephanie lee fatta'
	And I perform a GET for her influencer ranking charts
	Then the response's first item should be ranked one and the rest should be ordered incrementally

@media @ignore
Scenario: Contact search by pubic record type
	Given I login as 'manager Standard User'
	When  I perform a GET for Contacts by 'Record_Type' criteria with a value of 'public'
	Then  all returned contact objects should contain 'false' in their IsProprietaryContact property

@media @ignore
Scenario: Contact search by private record type
	Given I login as 'manager Standard User'
	When  I perform a GET for Contacts by 'Record_Type' criteria with a value of 'proprietary'
	Then  all returned contact objects should contain 'true' in their IsProprietaryContact property

@media @ignore
Scenario: Verify that new properties on Contacts lists are not null
	Given I login as 'manager user with lists'
	When I perform a POST for searching all 'MediaContact' lists
	Then all returned lists should contain not null values for the owner, modified date and creation date

@media @ignore
Scenario: Verify lists returned by outlet id
	Given I login as 'manager Standard User'
	When I perform a POST for searching all 'MediaOutlet' lists and using the 'chicago online' id sorted by name
	Then all returned lists should have the id of the outlet in the response

@media
Scenario Outline: Delete a private contact
	Given  I login as 'manager Standard User'
	When I create a new contact data associated with outlet 'chicago online' and the country '<id>'
	And I delete the contact created
	Then I should get the '200' status code

	Examples: 
	| id  | value          |
	| 292 | United Kingdom |

@media @ignore
Scenario: Not nulls id after multiple list deleted
	Given I login as 'manager user with lists' 
	When I perform a POST for searching all 'MediaContact' lists
	And I delete the first two list ids
	Then I should not get null response after multiple list deleted

@media @ignore
Scenario: Verify that a recent contact search is saved
	Given I login as 'manager Standard User'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I perform a POST to save the search using the key generated before
	And I perform a get for recent searches endpoint
	Then I should find the recent searched saved

@media @ignore
Scenario: Contact child suggestion from contact search
	Given I login as 'manager Standard User'
	When I perform a GET for Contacts suggestion using the key 'mossberg'
	Then I should see 'mossberg' in contact name as a suggestion

@media @ignore
Scenario: Verify that a saved search was created properly
	Given I login as 'Manager Standard user'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I perform a POST to keep the contact search using the key generated before
	And I perform a GET for saved searches endpoint
	Then I should find the saved searched 
	Then The saved searched is deleted

@media
Scenario: Verify that a contact saved searched was deleted properly
	Given I login as 'Manager Standard user'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I perform a POST to keep the contact search using the key generated before
	And I perform a DELETE for the saved search created
	And I perform a GET for saved searches endpoint	
	Then the saved search should be deleted properly

@media @ignore
Scenario: Verify that Datagroup id is on contact recent searches response
	Given I login as 'Standard User with default DG'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'	
	And I save the current Datagroup id
	And I perform a POST to save the search using the key generated before	
	And I perform a get for recent searches endpoint
	Then I should find the recent searched saved with the proper Datagroup id 

@media @ignore
Scenario: Verify that Datagroup id is on contact saved searches response
	Given I login as 'Standard User with default DG'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I save the current Datagroup id
	And I perform a POST to keep the contact search using the key generated before
	And I perform a GET for saved searches endpoint
	Then I should find the saved searched with the proper Datagroup id 

@media @ignore
Scenario: Verify that a contact saved seach is edited properly
	Given I login as 'Manager Standard user'
	When I perform a GET for saved searches endpoint
	And I edit the name of the first contact saved search listed
	Then I should see the name edited of the contact saved search	

@media @ignore
Scenario: Verify that the contacts are consolidated when the parameter is on true on my lists
	Given I login as 'Manager Standard user'
	When  I perform a GET for Contacts by 'Contact_Name_Consolidated'criteria with a value of 'cooper' and the consolidated profiles as 'false'
	And I add all results to a random list
	And I perform a GET for Contacts by 'Contact_List'criteria with a consolidated profiles as 'true'
	Then I should get consolidated contacts when the parameter is on true
		
@media @ignore
Scenario: Verify error when new list from contact profile has special characters
	Given I login as 'Manager Standard user'
	When  I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I add the first result to a new list with special characters using the profile endpoint
	Then I should get a response indicating the request was invalid

@media @ignore
Scenario: Verify that a company returns huge results with consolidated profiles parameter enabled
	Given I login as 'Company with huge results setted'
	When I perform a GET for Contacts by 'Record_Type' criteria with a value of 'public'  
	Then I should get all 'false' on proprietary information

@Influecers @ignore
Scenario: Verify that Has Multiple Outlets field is sortable
	Given I login as 'Manager Standard user'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I sort Contacts items '-HasMultipleOutlets' 
	Then all items returned should be sorted by 'descending'

@media @ignore
Scenario Outline: Verify that outlet news focus search is returning properly results
	Given I login as 'Manager Standard user'
	When I perform a GET for Contacts by 'News_Focus' criteria with a value of '<id>'
	Then all items returned should have the '<value>' in their outlet

Examples: 
	| id | value |
	| 629| International |
	| 630| National |
	| 631| Regional |
	| 632| Local |
	| 633| Community |

@media @ignore
Scenario Outline: Verify that a contact search by outlet subjects return properly results
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for Contacts by 'Outlet_Subjects' criteria with a value of '<id_subject>'
	Then I should see the contacts that have the '<subject_value>' in their outlet information

Examples: 
	| id_subject | subject_value |
	| 101000     | Accounting |

@media @ignore
Scenario: Verify that a contact search by opted out contacts return proper results
	Given shared session for 'standard' user with edition 'basic' 
	When I perform a GET for Contacts by 'Optedout' criteria with a value of 'true'
	Then I should see the contacts that have the 'true' in their Opted out property

@media @ignore
Scenario Outline: Verify that a contact search using support accents in their searches
	Given I login as 'Manager Standard User' 
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of '<value_accents>' 
	Then I should contain any results with the '<value_accents>'

Examples:
| value_accents |
| Gonçalves     |
| Jürgen        |
| José          |
| Ñáñez         |

@media @ignore
Scenario: Verify contact search with multiple criteria returns proper results
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for Contacts by the following criteria:
		| criteriaName  | criteriaValue |
		| Contact_Name	| smith			|
		| Outlet_Name	| tribune		|
	Then all returned Contacts should have 'smith' in their sortname
	And all returned Contacts should have 'tribune' in their outlet name


@media @ignore
Scenario Outline: Export contacts to Csv
	Given shared session for 'standard' user with edition 'basic'  
	When I perform a GET for Contacts by 'Record_Type' criteria with a value of 'public'  
	And I perform a POST to export Contacts including '<field>' field 
	And I perform a GET for Contacts jobs with the id from the export
	Then the job response status code is '200'

Examples: 
  | field              |
  | /Firstname         |
  | /Lastname          |
  | /OutletName        |
  | /Outlet/CountyName |

@media @ignore
Scenario: Verify that notes are added on contacts lists properly
	Given shared session for 'standard' user with edition 'basic'  
	When I perform a GET for Contacts by 'Optedout' criteria with a value of 'true' 
	And I perform a Post for creating a list with a random name using the top three ids
	And I perform a Patch for the list created before adding some ramdom notes
	Then I should see notes on response properly

@media @ignore
Scenario: Verify that the keyword searched for is in the contacts Title
	Given I login as 'Manager Standard User'
	When I perform a GET for Contacts by 'Keyword_Title' criteria with a value of 'Director'
	Then I should see the contacts that have 'Director' in their title

@media @ignore
Scenario: Verify that the keyword searched for is in the contacts Email
	Given I login as 'Manager Standard User'
	When I perform a GET for Contacts by 'Keyword_Email' criteria with a value of 'Times'
	Then I should see the contacts that have 'Times' in their email address

@media
Scenario Outline: Verify that contact search by outlet location using radius
	Given shared session for 'standard' user with edition 'basic'  
	When I perform a GET for Contacts by the following criteria:
		| criteriaName		| criteriaValue |
		| Outlet_Location	| <location_id>	|
		| LocationRadius	| 10			|	
	Then the results should match with the '<value>' location

Examples: 
| location_id | value	|
| 4-5946      | IL |

@media @ignore
Scenario: Verify that recent tweets are displayed for contact
	Given I login as 'Standard User with default DG'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'Fallon, Jimmy'
	And I save the first ID of the response gotten from the GET request
	And I perform a GET for Contacts recent tweets endpoint
	Then I should see the contacts recent tweets in the response

@media @ignore
Scenario: Basic Account Cannot Search Contacts by Talking About
	Given I login as 'Basic Edition - Standard User'
	When I perform a GET for Contacts by 'Influencer_Keyword' criteria with a value of 'nhl'
	Then the Contact response code should be '403'

@media @ignore
Scenario Outline: Verify that a user with read_only permission cannot create a private contact
Given shared session for 'read_only' user with edition 'basic'
When I create a new private contact with country '<id>'
Then I should get an error message

Examples:
| id  | value         |
| 316 | United States |


@media @ignore
Scenario: Basic Account Cannot Search Contacts by News Talking About
	Given I login as 'Basic Edition - Standard User'
	When I perform a GET for Contacts by 'NewsInfluencer_Keyword' criteria with a value of 'nhl'
	Then the Contact response code should be '403'

@media @ignore
Scenario Outline: Sort Media Contacts 
	Given shared session for 'standard' user with edition 'basic'  
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'smith'
	And I sort Contacts '<direction>' by '<field>'
	Then all Contacts item '<field>' are sorted '<direction>'

Examples: 
	| field				     | direction  |
	| SortName			     | Ascending  |
	| SortName			     | Descending |
	| OutletName		     | Ascending  |
	#| OutletName		     | Descending |
	| CirculationAudience    | Ascending  |
	| CirculationAudience    | Descending |
	| UniqueVisitorsPerMonth | Ascending  |
	| UniqueVisitorsPerMonth | Descending |

@media @ignore
Scenario Outline: Verify that optin request return a valid response
	Given I login as 'Standard User with default DG'
	When I create a new contact without list data associated with outlet 'chicago online' and the country '<id>'
	And I perform a PATCH for opted out the private contact created setting as 'true'
	And I perform a POST for opt in request using the same private contact created before
	Then I should see the contact name in the opt in request response
	Then the contact is deleted

Examples: 
	| id  | value          |
	| 316 | United States  |

@media
Scenario Outline: Verify that Opt In request for PATCH operation is properly performed
	Given I login as 'Standard User with default DG' 
	When I create a new contact without list data associated with outlet 'chicago online' and the country '<id>'
	And I perform a PATCH for opted out the private contact created setting as 'true'
	And I perform a POST for opt in request using the same private contact created before
	And I perform a PATCH to edit the Has Opted in value
	Then I should see the property already updated
	Then the contact is deleted

Examples: 
	| id  | value          |
	| 316 | United States  |

@media @ignore
Scenario Outline: Verify the contact location search by county
	Given I login as 'manager Standard User' 
	When I perform a GET for Contacts by 'Outlet_Location' criteria with a value of '<location_id>'
	Then I should see the proper '<value>' on the contacts response

Examples: 
| location_id | value	|
| 3-1873      | New York |

@media @ignore
Scenario: Verify that contact name search can be performed using multiples values
	Given I login as 'manager Standard User'
	When I perform a GET for contacts ids for the following contacts 'Stephanie Lee Fatta,Dave Boyer'
	Then I should see results with the given contacts names


@media @ignore
Scenario: Delete a private contact with read only user should return 403 status code
	Given  I login as 'Read Only PositiveTest'
	When I perform a GET for Contacts by 'Record_Type' criteria with a value of 'proprietary'
	And I delete the first item returned
	Then I should get the '403' status code for the item selected


@media @ignore
Scenario Outline: Search contacts by outlet target area 
	Given I login as 'manager Standard User'
	When I perform a GET for Contacts by 'OutletTarget_Area' criteria with a value of '<id>'	
	Then I should see '<value>' for contact response on target area location

Examples: 
| id      | value   |
| 2119290 | Chicago |

@media @ignore
Scenario: Duplicate a contact list 
	Given I login as 'manager Standard User'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I perform a Post for creating a list with a random name using the top three ids
	And I duplicate the list for the same datagroup changing the name
	And I retrieve the list information recently duplicated
	Then I should see the same results that the original one


@media @ignore
Scenario Outline: Upload bulk import when creating a bulk import process
	Given I login as 'Posdemo Manager'
	When I upload a file <filepath> to create a import process for contacts
	Then  The result should be <status> for the import process

Examples: 
	| filepath				| status |
	| ImportSample.csv		| OK     |

@media
Scenario Outline: Upload bulk import with list info for private contacts
	Given I login as 'Posdemo Manager'
	When I upload a file <filepath> to create a import process for contacts
	Then  The result should be <status> for the import process

Examples: 
	| filepath				| status |
	| ImportSampleList.csv		| OK     |
	

	