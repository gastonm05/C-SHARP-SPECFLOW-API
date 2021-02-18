Feature: OutletSearchResultsFiltering
	In order to find relevant outlets
	As a CCC standard user
	I want to be able to filter outlet search results

@media @ignore
Scenario: Filtering Outlet search results by media type returns only outlets with selected media type
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for Outlets by 'OutletName' 'Chicago'
	And I perform a GET for Outlets with the filter 'Blog, consumer' in the filter category 'mediaoutlettypeid'
	Then all filtered outlets returned should have 'Blog, consumer' as their Outlet Type

@media @ignore
Scenario: Filtering Outlet search results by DMA returns only outlets with selected media type
	Given  shared session for 'standard' user with edition 'basic' 
	When   I perform a GET for Outlets by 'OutletName' 'Chicago'
	And  I perform a GET for Outlets with the filter 'Chicago, IL' in the filter category 'dmaid'
	Then all filtered outlets returned should have 'Chicago, IL' as their Outlet DMA Name

@media
Scenario: Filtering Outlet search results by subject returns only outlets with selected subject
	Given shared session for 'standard' user with edition 'basic'
	When  I perform a GET for Outlets by 'OutletName' 'Washington'
	And I perform a GET for Outlets with the filter 'International News' in the filter category 'mediaoutletsubject' 
	Then all filtered outlets returned should have 'International News' as their subject

@media @ignore
Scenario: Filtering Outlet search results by state returns only outlets with selected state
	Given shared session for 'read_only' user with edition 'basic'
	When I perform a GET for Outlets by 'OutletName' 'Chicago'
	And I perform a GET for Outlets with the filter 'Illinois' in the filter category 'state' 
	Then all filtered outlets returned should have 'IL' as their state

@media @ignore
Scenario: Filtering Outlet search results by record type returns only outlets with the selected record type
	Given shared session for 'read_only' user with edition 'basic'
	When I perform a GET for Outlets by 'OutletName' 'Chicago'
	And I perform a GET for Outlets with the filter 'public' in the filter category 'publicprivate' 
	Then all filtered outlets returned should have 'false' as their proprietary data

@media @ignore
Scenario: Filtering outlet search results by outlet city returns only outlets with the selected city
	Given shared session for 'read_only' user with edition 'basic'
	When  I perform a GET for Outlets by 'OutletName' 'Washington'
	And I perform a GET for Outlets with the filter 'Washington' in the filter category 'city' 
	Then all filtered outlets returned should have 'Washington' as their Outlet city

@media @ignore
Scenario: Filtering outlet search results by outlet working language returns only outlets with the selected language	
	Given shared session for 'read_only' user with edition 'basic'
	When  I perform a GET for Outlets by 'OutletName' 'Washington'
	And I perform a GET for Outlets with the filter 'English' in the filter category 'mediaoutletworkinglanguage'  
	Then all filtered outlets returned should have 'English' as their outlet language

@media @ignore
Scenario: Filtering outlet search results by affiliated media exclusion returns only proper outlets
	Given shared session for 'read_only' user with edition 'basic'
	When  I perform a GET for Outlets by 'OutletName' 'Washington'
	And I perform a GET for Outlets with the filter 'affiliatedmediaExcluded' in the filter category 'affiliatedmedia' 
	Then I should see the outlets with non affiliated media

@media @ignore
Scenario: Filtering outlet search results by outlet county return only outlets with the selected value
	Given shared session for 'read_only' user with edition 'basic'
	When  I perform a GET for Outlets by 'OutletName' 'Washington'
	And I perform a GET for Outlets with the filter 'Denver' in the filter category 'county'
	Then I should see results with 'Denver' as their county

@media @ignore
Scenario: Filtering outlet search results by medium & type return only outlets with the selected value
	Given shared session for 'read_only' user with edition 'basic'
	When  I perform a GET for Outlets by 'OutletName' 'Chicago'
	And I perform a GET for Outlets with the filter 'Online, consumer' in the filter category 'mediaoutlettypeid'	
	Then all filtered outlets returned should have 'Online, consumer' as their Outlet Type

@media @ignore
Scenario: Filtering Outlet search results by outlets List
	Given I login as 'Standard User with lists'
	When  I perform a GET for Outlets by 'OutletName' 'Washington'
	And I perform a GET for Outlets with the group 'Outlet List' in the filter category 'entitylistitemid'
	And I perform a GET for the first outlet list listed
	Then I should get the list created in the outlet details response
