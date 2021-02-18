Feature: ContactSearchResultsFiltering
	In order to find relevant contacts
	As a CCC standard user
	I want to be able to filter contact search results

@media @media
Scenario: Filtering Contact search results by single subject returns only contacts with selected subject
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for Contacts by 'record_type' criteria with a value of 'public'
	And I perform a GET for Contacts with the filter 'Education' in the filter category 'mediacontactsubject'
	Then all contacts should contain 'Education' as their Subject

@media @ignore @media
Scenario: Filtering Contact search results by Contacts List
	Given I login as 'Standard User with lists'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I perform a GET for Contacts with the group 'Contact List' in the filter category 'entitylistitemid'
	And I perform a GET for the first contact listed
	Then I should get the list created in the contact details response

@media @ignore @media
Scenario: Filtering Contact search results by Country returns only contacts with selected Country
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I perform a GET for Contacts with the filter 'United States' in the filter category 'country'
	Then all filtered contacts returned should have 'United States' as their Outlet Country

@media @ignore @media
Scenario: Filtering Contact search results by City returns only contacts with selected City
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for Contacts by 'Keyword_Profile' criteria with a value of 'illinois'
	And I perform a GET for Contacts with the filter 'Chicago' in the filter category 'city'
	Then all filtered contacts returned should have 'Chicago' as their Outlet City

@media @ignore @media
Scenario: Filtering Contact search results by Outlet Type returns only contacts with selected Outlet Type
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for Contacts by 'Keyword_Profile' criteria with a value of 'illinois'
	And I perform a GET for Contacts with the filter 'Newspaper' in the filter category 'mediaoutlettypeid'
	Then all filtered contacts returned should have 'Newspaper' as their Outlet Type

@media @ignore @media
Scenario: Filtering Contact search results by DMA returns only contacts with selected DMA
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for Contacts by 'Keyword_Profile' criteria with a value of 'illinois'
	And I perform a GET for Contacts with the filter 'Chicago, IL' in the filter category 'dmaid'
	Then all filtered contacts returned should have 'Chicago, IL' as their DMA

@media @ignore @media
Scenario: Filtering Contact search results applying two filters
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I perform a GET for Contacts with the filter 'United States' in the filter category 'country'
	And I perform a GET for Contacts applying a second filter 'Newspaper' in the filter category 'mediaoutlettypeid'
	Then all returned contacts should have 'United States' as ther country and 'Newspaper' as their Outlet Type

@media @ignore @media
Scenario: Filtering Contact search results by Record type returns only contact with selected record type
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for Contacts by 'Outlet_Name' criteria with a value of 'chicago'
	And I perform a GET for Contacts with the filter 'public' in the filter category 'publicprivate'
	Then all filtered contacts returned should have FALSE as their IsProprietary value	

@media @ignore @media
Scenario:  Filtering Contact search results by Contact Language returns only contacts with selected Language
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I perform a GET for Contacts with the filter 'Spanish' in the filter category 'workinglanguage'
	Then all filtered contacts returned should have 'Spanish' as their language

@media @ignore @media
Scenario: Filtering contact search results by outlet county return only contacts with the selected value
	Given shared session for 'read_only' user with edition 'basic'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I perform a GET for Contacts with the filter 'Denver' in the filter category 'county'
	Then I should see results with 'Denver' as their county for contacts results

@media @ignore @media
Scenario: Filtering Contact search results by by medium & type returns only contacts with selected medium
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for Contacts by 'Contact_Name' criteria with a value of 'cooper'
	And I perform a GET for Contacts with the filter 'Newspaper' in the filter category 'mediaoutlettypeid'
	Then all filtered contacts returned should have 'Newspaper' as their Outlet Type
