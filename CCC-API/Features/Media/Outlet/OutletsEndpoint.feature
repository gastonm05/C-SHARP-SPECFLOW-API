Feature: OutletsEndpoint
	In order to find outlets
	As a standard user
	I want to query outlets via the outlets endpoint

@media @ignore
Scenario: Search Outlets by Outlet Name
	Given I login as 'Standard User'
	When I perform a GET for Outlets by 'OutletName' 'Chicago'
	Then all returned outlet objects should contain 'Chicago' in their name

@media @ignore
Scenario: Search Outlets by Outlet Location City 'Chicago, Illinois, United States of America'
	Given I login as 'Standard User'
	When I perform a GET for Outlets by 'OutletLocation' '4-5946'
	Then all returned outlet objects should have 'Chicago' as their city
	
@media @ignore
Scenario: Search Outlets by Outlet Location State 'Illinois, United States of America'
	Given I login as 'Standard User'
	When I perform a GET for Outlets by 'OutletLocation' '2-14'
	Then all returned outlet objects should have 'IL' as their state
	
@media @ignore
Scenario: Search Outlets by Outlet Location Country 'United States of America'
	Given I login as 'Standard User'
	When I perform a GET for Outlets by 'OutletLocation' '1-1'
	Then all returned outlet objects should have 'United States' as their country
	
@media @ignore
Scenario: Search Outlets by Outlet Type 'Blog, consumer'
	Given I login as 'Standard User'
	When I perform a GET for Outlets by 'OutletType' '170000'
	Then all returned outlet objects should have 'Blog, consumer' as their outlet type
	
@media @ignore
Scenario: Search Outlets by Subject 'Academic Certification'
	Given I login as 'Standard User'
	When I perform a GET for Outlets by 'Subject' '100000'
	Then all returned outlet objects should contain 'Academic Certification' in their subjects
	
@media @ignore
Scenario: Search Outlets by DMA 'Abilene-Sweetwater, TX'
	Given I login as 'Standard User'
	When I perform a GET for Outlets by 'DMA' '101'
	Then all returned outlet objects should have 'Abilene-Sweetwater, TX' as their DMA

@media @ignore
Scenario: Outlet results should contain a UVPM value equal to or greater than zero
	Given I login as 'Standard User'
	When I perform a GET for Outlets by 'OutletName' 'Chicago'
	Then all returned outlet objects should have a UVPM value equal to or greater than zero

@media @ignore
Scenario: Outlet results should contain Medium value
	Given I login as 'Standard User'
	When I perform a GET for Outlets by 'OutletName' 'Chicago'
	Then all returned outlet objects should have a Medium value

@media @ignore
Scenario: Outlet child suggestion from outlet search
	Given I login as 'Standard User'
	When I perform a GET for Outlet suggestion using the key 'chicago'
	Then I should see 'chicago' in outlet name as a suggestion

@media @ignore
Scenario: Verify that a recent outlet search is saved
	Given I login as 'manager Standard User'
	When I perform a GET for Outlets by 'OutletName' 'Chicago'
	And I perform a POST to save the outlet search using the key generated before
	And I perform a get for recent outlet searches endpoint
	Then I should find the recent searched saved
	

@media @ignore
Scenario: Regional Focus on Outlets always returns a non-null value in search results
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for Outlets by 'OutletName' 'Chicago'
	Then all returned outlets have a non-null Regional Focus

@media @ignore
Scenario: Verify that a outlet search is saved
	Given I login as 'manager Standard User'
	When I perform a GET for Outlets by 'OutletName' 'Chicago'
	And I perform a POST to keep the outlet search using the key generated before with a random name
	And I perform a get for saved outlet searches endpoint
	Then I should find the saved searched 
	Then The saved searched is deleted

@media
Scenario: Verify that an outlet saved searched was deleted properly
	Given I login as 'Manager Standard user'
	When I perform a GET for Outlets by 'OutletName' 'Chicago'
	And I perform a POST to keep the outlet search using the key generated before with a random name
	And I perform a DELETE for the outlet saved search created
	And I perform a get for saved outlet searches endpoint
	Then the outlet saved search should be deleted properly

@media @ignore
Scenario: Verify that Datagroup id is on outlet recent searches response
	Given I login as 'Standard User with default DG'
	When I perform a GET for Outlets by 'OutletName' 'Chicago'	
	And I save the current Datagroup id
	And I perform a POST to save the outlet search using the key generated before
	And I perform a get for recent outlet searches endpoint
	Then I should find the recent searched saved with the proper Datagroup id 

@media @ignore
Scenario: Verify that Datagroup id is on outlet saved searches response
	Given I login as 'Standard User with default DG'
	When I perform a GET for Outlets by 'OutletName' 'Chicago'	
	And I save the current Datagroup id
	And I perform a POST to keep the outlet search using the key generated before with a random name
	And I perform a get for saved outlet searches endpoint
	Then I should find the saved searched with the proper Datagroup id 

@media @ignore
Scenario: Verify that a outlet saved seach is edited properly
	Given I login as 'Manager Standard user'
	When I perform a get for saved outlet searches endpoint
	And I edit the name of the first outlet saved search listed
	Then I should see the name edited of the outlet saved search	

@media @ignore
Scenario: Verify that the outlet search by list name is returning proper results
	Given I login as 'Manager Standard user'
	When I perform a GET for Outlets by 'OutletName' 'Chicago'
	And I perform a Post for creating an outlet list with a random name using the top three ids
	And I perform a GET for Outlets by 'OutletListName' with the list created before
	Then I should see the outlets returned properly

@media @ignore
Scenario Outline: Verify that an outlet search using support accents in their searches
	Given shared session for 'standard' user with edition 'basic'  
	When I perform a GET for Outlets by 'OutletName' '<value_accents>'
	Then I should contain any outlets results with the '<value_accents>'

Examples:
| value_accents       |
| Süddeutsche Zeitung |
| O Diário da Região  |
| Associação          |
| Cataluña            |

@media @ignore
Scenario Outline: Verify outlet search by outlet news focus
	Given shared session for 'standard' user with edition 'basic'  
	When I perform a GET for Outlets by 'Outlet_NewsFocus' '<id>' 
	Then all items returned should have the '<value>' in their Regional Focus field

	Examples: 
	| id  | value         |
	| 629 | International |
	| 630 | National      |
	| 631 | Regional      |
	| 632 | Local         |
	| 633 | Community     |

@media @ignore
Scenario Outline: Verify that an outlet search by Outlet language is saved properly on recent search
	Given I login as 'manager Standard User' 
	When  I perform a GET for Outlets by 'Outlet_WorkingLanguage' '<id>' 
	And I perform a POST to save the outlet search using the key generated before	
	Then I should see the results and the '<id>' in the recent searches 

Examples: 
	| id    | value   |
	| 24000 | English |

@media @ignore
Scenario Outline: Verify that an outlet search by Outlet frecuency is returning properly results
	Given shared session for 'standard' user with edition 'basic'  
	When I perform a GET for Outlets by 'Outlet_Frecuency' '<id>' 
	Then I should see the '<value>' for frecuency property on outlets response 
	
	Examples: 
	| id    | value |
	| 20500 | Daily |	

@media @ignore
Scenario Outline: Verify that a new private outlet is created properly
	Given I login as 'manager Standard User'
	When I perform a POST to create a new outlet using a random name and the '<id>' 
	Then I should see the outlet created with the proper country '<country>'

	Examples: 
	| id  | country       |
	| 316 | United States |	

@media
Scenario Outline: Edit a private outlet 
	Given I login as 'manager Standard User' 
	When I perform a POST to create a new outlet using a random name and the '<id>'  
	And I perform a PATCH to edit the name of the outlet created
	Then I should see the private outlet with the data edited

Examples: 
	| id  | country       |
	| 316 | United States |	

@media @ignore
Scenario Outline: Verify that an outlet search by reach is returning proper results
	Given shared session for 'standard' user with edition 'basic'  
	When I perform a GET for Outlets '<criteria>' between '1' and '500' values
	Then I should see results returned between '1' and '500' values on the range  

Examples:
	| criteria             |
	| Outlet_AudienceReach |
	| Outlet_UVPM          |

@media
Scenario Outline: Delete a private outlet
	Given  I login as 'manager Standard User'
	When I perform a POST to create a new outlet using a random name and the '<id>' 
	And I delete the private  outlet created previously
	Then I should get the '200' status code returned

Examples: 
	| id  | country       |
	| 316 | United States |	

@media
Scenario Outline: Export outlets to Csv
	Given shared session for 'standard' user with edition 'basic'  
	When I perform a GET for Outlets by 'OutletName' 'Chicago'
	And I perform a POST to export Outlets including '<field>' field 
	And I perform a GET for Outlet jobs with the id from the export
	Then the job response status code is '200'

Examples: 
  | field                |
  | /SortName            |
 
@media @ignore
Scenario Outline: Verify that outlet search by record type return proper results
	Given shared session for 'standard' user with edition 'basic'  
	When I perform a GET for Outlets by 'Outlet_Recordtype' '<recordtype>'
	Then all results returned should have '<value>' in ther IsProprietaryOutlet value

Examples: 
| recordtype  | value |
| public      | false |
| proprietary | true  |

@media @ignore
Scenario Outline: Verify that list are patched properly on private outlets
	Given I login as 'manager Standard User'
	When I perform a POST to create a new outlet using a random name and the '<id>' 	
	And I perform a post to get all 'MediaOutlet' lists
	And I perform a PATCH to add a list for that outlet taking the first id and name for the list
	Then I should see the list added properly on response 
	 
Examples: 
	| id  | country       |
	| 316 | United States |	

@media @ignore
Scenario: Verify that recent tweets are displayed for outlets
	Given I login as 'Standard User with default DG'
	When I perform a GET for Outlets by 'OutletName' 'chicago tribune'
	And I save the fist id of the response getted before
	And I perform a GET for the recent twitter endpoint
	Then I should see proper data on response

@media @ignore
Scenario: Verify that notes are added on outlets lists properly
	Given shared session for 'standard' user with edition 'basic'  
	When I perform a GET for Outlets by 'OutletName' 'Chicago'
	And I perform a Post for creating an outlet list with a random name using the top three ids
	And I perform a Patch for the outlet list created before adding some ramdom notes  
	Then I should see notes for outlet list on response properly

@media @ignore
Scenario: Verify that using the word "the" outlet name should return same numbers of results than without it
	Given shared session for 'standard' user with edition 'basic' 
	When I perform a GET for Outlets by 'OutletName' 'new york times' 	
	And I perform a second GET for Outlets by 'OutletName' 'the new york times' 
	Then I should get the same numbers of results 

@media @ignore
Scenario: Verify that outlet search by news only outlet return proper results
	Given shared session for 'standard' user with edition 'basic' 
	When I perform a GET for Outlets by 'Outlet_NewsOnlyOutlets' 'true'
	Then I should see any NOD only outlet returned on response 	

@media @ignore
Scenario Outline: Verify that outlet search by outlet location using radius
	Given shared session for 'standard' user with edition 'basic'  
	When I perform a GET for Outlets by the following criteria:
		| criteriaName		| criteriaValue |
		| OutletLocation	| <location_id>	|
		| Outlet_LocationRadius	| 10		|	
	Then the results should match with the '<value>' location on outlets response

Examples: 
| location_id | value	|
| 4-5946      | IL |

@media @ignore
Scenario: Verify that multiple search can be perform for media outlets
	Given I login as 'manager Standard User'
	When I perform a GET for Outlets by the following criteria:
		| criteriaName      | criteriaValue |
		| OutletName        | Chicago       |
		| Outlet_Recordtype | public        |
	Then all returned Outlets should have 'chicago' in their fullname and the false in the proprietary

@media @ignore
Scenario Outline: Verify the outlets location search by county
	Given I login as 'manager Standard User' 
	When I perform a GET for Outlets by 'OutletLocation' '<location_id>'
	Then I should see the proper '<value>' on the response

Examples: 
| location_id | value	|
| 3-1873      | New York |

@media @ignore
Scenario: Verify that outlet name can be performed using multiples values
	Given I login as 'manager Standard User'
	When I perform a GET for outlets ids for the following outlets 'chicago online,chicago tribune'
	Then I should see results with the given names

@media @ignore
Scenario Outline: Search outlets by target
	Given I login as 'manager Standard User'
	When I perform a GET for Outlets by 'OutletTarget_Ids' '<id>' 
	Then I should see '<value>' as target area location

Examples: 
| id      | value   |
| 2119290 | Chicago |


@media @ignore
Scenario: Duplicate an outlet list 
	Given I login as 'manager Standard User'
	When I perform a GET for Outlets by 'OutletName' 'new york times' 		
	And I perform a Post for creating an outlet list with a random name using the top three ids
	And I duplicate the oulet list for the same datagroup changing the name
	And I retrieve the outlet list information recently duplicated
	Then I should see the same results that the original one on outlets lists

@media @ignore
Scenario: Verify that outlet search by opted out from emails return relevant results
	Given I login as 'Posdemo Manager'
	When I perform a GET for Outlets by 'OutletOptedOut' 'true'
	Then I should see outlets results with the 'True' value

@media @ignore
Scenario Outline: Verify that a outlet created is opted out properly
	Given I login as 'Posdemo Manager'
	When I perform a POST to create a new outlet using a random name and the '<id>' 
	And I perform a PATCH for opted out the private outlet created setting as 'true'
	Then I should see the 'true' value on outlet edited
	And the outlet is deleted

	Examples: 
	| id  | country       |
	| 316 | United States |	
