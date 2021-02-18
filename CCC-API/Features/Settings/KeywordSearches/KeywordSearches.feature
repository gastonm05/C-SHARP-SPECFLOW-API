Feature: KeywordSearches 
	To verify that KeywordSearches can be created, retrieved, modified and deleted
	As a valid CCC user from a Visible ON company with parameter c3keywordsearchesenabled set to true
	I want to call the MediaMonitoring endpoint - api/v1/management/monitoring
	
@acl @Settings @KeywordSearches @NeedsCleanup @Ignore
Scenario: An Admin User can create, list, and edit a Keyword Searches using both AND and OR Keywords
	Given I login as 'ACL Keyword Automation'
	And the API test data 'KeywordSearchesTestData.json'
 	When I perform a POST on MediaMonitoring endpoint to create a 'SAMPLE' Keyword Search with 'ANDandOR' Keywords with a search name with '200' characters and search term 'A AND B AND Z'
	And I perform a POST on MediaMonitoring endpoint to create a 'NEW' Keyword Search with 'ANDandOR' Keywords with a search name with '200' characters and search term 'A AND B AND Z'
	Then MediaMonitoring endpoint POST response code should be 201
	When I perform a GET on MediaMonitoring endpoint for '(Default)' datagroup
	Then MediaMonitoring endpoint GET response code should be 200 for '(Default)' datagroup
	And  Verify that Name matchs with just created Keyword Search for '(Default)' datagroup
	When I perform a PUT on MediaMonitoring endpoint to update just created Keyword Search. 
	Then MediaMonitoring endpoint PUT response code should be 201 
	And  Modified Name and Keyword matchs with just updated Keyword Search for a 'ANDandOR' search 	

@acl @Settings @KeywordSearches @NeedsCleanup @Ignore
Scenario: An Admin User can create, list, and edit a Keyword Searches using only OR Keywords
	Given I login as 'ACL Keyword Automation'
	And the API test data 'KeywordSearchesTestData.json'
 	When I perform a POST on MediaMonitoring endpoint to create a 'SAMPLE' Keyword Search with 'OR' Keywords with a search name with '200' characters and search term 'A AND B AND Z'
	And I perform a POST on MediaMonitoring endpoint to create a 'NEW' Keyword Search with 'OR' Keywords with a search name with '200' characters and search term 'A AND B AND Z'
	Then MediaMonitoring endpoint POST response code should be 201
	When I perform a GET on MediaMonitoring endpoint for '(Default)' datagroup
	Then MediaMonitoring endpoint GET response code should be 200 for '(Default)' datagroup
	And  Verify that Name matchs with just created Keyword Search for '(Default)' datagroup
	When I perform a PUT on MediaMonitoring endpoint to update just created Keyword Search. 
	Then MediaMonitoring endpoint PUT response code should be 201 
	And  Modified Name and Keyword matchs with just updated Keyword Search for a 'ANDandOR' search 	

@acl @Settings @KeywordSearches @NeedsCleanup @Ignore
Scenario: An Admin User can create, list, and edit a Keyword Searches using both AND and NOT Keywords
	Given I login as 'ACL Keyword Automation'
	And the API test data 'KeywordSearchesTestData.json'
 	When I perform a POST on MediaMonitoring endpoint to create a 'SAMPLE' Keyword Search with 'ANDandNOT' Keywords with a search name with '200' characters and search term 'A AND B AND Z'
	And I perform a POST on MediaMonitoring endpoint to create a 'NEW' Keyword Search with 'ANDandNOT' Keywords with a search name with '200' characters and search term 'A AND B AND Z'
	Then MediaMonitoring endpoint POST response code should be 201
	When I perform a GET on MediaMonitoring endpoint for '(Default)' datagroup
	Then MediaMonitoring endpoint GET response code should be 200 for '(Default)' datagroup
	And  Verify that Name matchs with just created Keyword Search for '(Default)' datagroup
	When I perform a PUT on MediaMonitoring endpoint to update just created Keyword Search. 
	Then MediaMonitoring endpoint PUT response code should be 201 
	And  Modified Name and Keyword matchs with just updated Keyword Search for a 'ANDandOR' search 	
	 
@acl @Settings @KeywordSearches @NeedsCleanup @Ignore
Scenario: An Admin User can create, list, and edit a Keyword Searches using both OR and NOT Keywords
	Given I login as 'ACL Keyword Automation'
	And the API test data 'KeywordSearchesTestData.json'
 	When I perform a POST on MediaMonitoring endpoint to create a 'SAMPLE' Keyword Search with 'ORandNOT' Keywords with a search name with '200' characters and search term 'A AND B AND Z'
	And I perform a POST on MediaMonitoring endpoint to create a 'NEW' Keyword Search with 'ORandNOT' Keywords with a search name with '200' characters and search term 'A AND B AND Z'
	Then MediaMonitoring endpoint POST response code should be 201
	When I perform a GET on MediaMonitoring endpoint for '(Default)' datagroup
	Then MediaMonitoring endpoint GET response code should be 200 for '(Default)' datagroup
	And  Verify that Name matchs with just created Keyword Search for '(Default)' datagroup
	When I perform a PUT on MediaMonitoring endpoint to update just created Keyword Search. 
	Then MediaMonitoring endpoint PUT response code should be 201 
	And  Modified Name and Keyword matchs with just updated Keyword Search for a 'ANDandOR' search 	

@acl @Settings @KeywordSearches @NeedsCleanup @Ignore
Scenario: An Admin User can create, list, and edit a Keyword Searches using all AND, OR and NOT Keywords
	Given I login as 'ACL Keyword Automation'
	And the API test data 'KeywordSearchesTestData.json'
 	When I perform a POST on MediaMonitoring endpoint to create a 'SAMPLE' Keyword Search with 'ANDORandNOT' Keywords with a search name with '200' characters and search term 'A AND B AND Z'
	And I perform a POST on MediaMonitoring endpoint to create a 'NEW' Keyword Search with 'ANDORandNOT' Keywords with a search name with '200' characters and search term 'A AND B AND Z'
	Then MediaMonitoring endpoint POST response code should be 201
	When I perform a GET on MediaMonitoring endpoint for '(Default)' datagroup
	Then MediaMonitoring endpoint GET response code should be 200 for '(Default)' datagroup
	And  Verify that Name matchs with just created Keyword Search for '(Default)' datagroup
	When I perform a PUT on MediaMonitoring endpoint to update just created Keyword Search. 
	Then MediaMonitoring endpoint PUT response code should be 201 
	And  Modified Name and Keyword matchs with just updated Keyword Search for a 'ANDandOR' search

@acl @Settings @KeywordSearches 
Scenario: An Admin User should have different Keyword Searches results using different datagroups.
	Given I login as 'Visible ON Company'
	And the API test data 'KeywordSearchesTestData.json'
	When Change datagroup to '(Default)' datagroup	
	And I perform a GET on MediaMonitoring endpoint for '(Default)' datagroup
	Then MediaMonitoring endpoint GET response code should be 200 for '(Default)' datagroup
	And  Returned Keyword Searches results should include '(Default)' datagroup result search.
	When Change datagroup to 'otro' datagroup
	And I perform a GET on MediaMonitoring endpoint for 'otro' datagroup
	Then MediaMonitoring endpoint GET response code should be 200 for 'otro' datagroup
	And  Returned Keyword Searches results should include 'otro' datagroup result search.
	
@acl @Settings @KeywordSearches
Scenario: An Admin User should have different Earned Media Keyword Searches results using different datagroups.
	Given I login as 'Earned Media Enabled Company - Sysadmin'
	And the API test data 'KeywordSearchesTestData.json'
	When Change datagroup to '(Default)' datagroup	
	And I perform a GET on MediaMonitoring endpoint for Earned Media '(Default)' datagroup
	Then Earned Media MediaMonitoring endpoint GET response code should be 200 for '(Default)' datagroup
	And  Returned Earned Media Keyword Searches results should include '(Default)' datagroup result search.
	When Change datagroup to 'Data Group 2' datagroup
	And I perform a GET on MediaMonitoring endpoint for Earned Media 'Data Group 2' datagroup
	Then Earned Media MediaMonitoring endpoint GET response code should be 200 for 'Data Group 2' datagroup
	And  Returned Earned Media Keyword Searches results should include 'Data Group 2' datagroup result search.

@acl @Settings @KeywordSearches
Scenario: An Admin User shouldn't be able to delete a Non-user created search (both Support and EarnedM Media)
	Given I login as 'Visible ON Company' 
	And the API test data 'KeywordSearchesTestData.json'
	When Change datagroup to '(Default)' datagroup	
	Then Delete a non user created (both Support and EarnedM Media) Keyword Search and response code should be 403 and message 'You can not edit or delete a non user created search.'

@acl @Settings @KeywordSearches
Scenario: An Admin User shouldn be able to get all Visible Keyword Search information for Countries, Languages and Sources combo boxes
	Given I login as 'Visible ON Company'
	When I perform a GET on NewsArchiveCountries endpoint
	Then NewsArchiveCountries endpoint GET response code should be 200 and returned data
	When I perform a GET on NewsArchiveLanguages endpoint
	Then NewsArchiveLanguages endpoint GET response code should be 200 and returned data
	When I perform a GET on NewsArchiveSources endpoint
	Then NewsArchiveSources endpoint GET response code should be 200 and returned data

@acl @Settings @KeywordSearches @Ignore
Scenario: An Admin User can create a Sample Keyword Search using all AND, OR and NOT Keywords
	Given I login as 'ACL Keyword Automation'
	And the API test data 'KeywordSearchesTestData.json'
	When I perform a POST on MediaMonitoring endpoint to create a 'SAMPLE' Keyword Search with 'AND' Keywords with a search name with '200' characters and search term 'A AND B AND Z'
	Then Sample MediaMonitoring Search endpoint POST response code should be 200
	And  Verify return results are correct for 'SAMPLE'
		
@acl @Settings @KeywordSearches @Ignore  
Scenario: An Admin User can verify 200 Character Limit for Name Keyword Search field using a over limit value.
	Given I login as 'ACL Keyword Automation'
	And the API test data 'KeywordSearchesTestData.json'
	When I perform a POST on MediaMonitoring endpoint to create a 'SAMPLE' Keyword Search with 'NAMELIMIT' Keywords with a search name with '201' characters and search term 'A AND B AND Z'
	And I perform a POST on MediaMonitoring endpoint to create a 'NEWNAMELIMIT' Keyword Search with 'AND' Keywords with a search name with '201' characters and search term 'A AND B AND Z'
	Then MediaMonitoring endpoint POST response code should be 500
	And  returned message is 'Search name cannot be more than 200 characters long'

@acl @Settings @KeywordSearches @NeedsCleanup
Scenario: An Admin User can verify 200 Character Limit for Name Keyword Search field using a under limit value.
	Given I login as 'ACL Keyword Automation'
	And the API test data 'KeywordSearchesTestData.json'
	When I perform a POST on MediaMonitoring endpoint to create a 'SAMPLE' Keyword Search with 'NAMELIMIT' Keywords with a search name with '200' characters and search term 'A AND B AND Z'
	And I perform a POST on MediaMonitoring endpoint to create a 'NEWNAMELIMIT' Keyword Search with 'AND' Keywords with a search name with '200' characters and search term 'A AND B AND Z'
	Then MediaMonitoring endpoint POST response code should be 201	

@acl @Settings @KeywordSearches @NeedsCleanup
Scenario: An Admin User can create, list, and edit a Keyword Searches using Advanced Boolean Keywords.
	Given I login as 'ACL Keyword Automation'
	And the API test data 'KeywordSearchesTestData.json'
 	When I perform a POST on MediaMonitoring endpoint to create a 'SAMPLE' Keyword Search with 'ADVANCED' Keywords with a search name with '200' characters and search term A AND B AND Z
	And I perform a POST on MediaMonitoring endpoint to create a 'NEW' Keyword Search with 'ANDandOR' Keywords with a search name with '200' characters and search term A AND B AND Z
	Then MediaMonitoring endpoint POST response code should be 201
	When I perform a GET on MediaMonitoring endpoint for '(Default)' datagroup
	Then MediaMonitoring endpoint GET response code should be 200 for '(Default)' datagroup
	And  Verify that Name matchs with just created Keyword Search for '(Default)' datagroup
	When I perform a PUT on MediaMonitoring endpoint to update just created Keyword Search. 	
    Then MediaMonitoring endpoint PUT response code should be 201 
	And  Modified Name and Keyword matchs with just updated Keyword Search for a 'ADVANCED' search

@acl @Settings @KeywordSearches
Scenario Outline: A Manager User should NOT be allow to Save a Advanced Boolean Keywords search with invalid values 
	Given I login as 'ACL Keyword Automation'
	And the API test data 'KeywordSearchesTestData.json'
	When I perform a POST on MediaMonitoring endpoint to create a 'SAMPLEINVALID' Keyword Search with 'ADVANCED' Keywords with a search name with '200' characters and search term <SearchTerm>
	Then Monitoring Sample POST endpoint response code should be 400 and validation message should be <Message> and Error code <ErrorCode>
Examples: 
	| SearchTerm              | Message                                                                                                                                                                                                            | ErrorCode |
	| a w/2(b and c)          | Proximity matches must be used with either single terms or OR lists of terms on both sides (left: OrionTerm, right: MultiTermAndFilter)                                                                            | 110       |
	| atleast2(a and b)       | Atleast2() can only be used with a single term                                                                                                                                                                     | 110       |
	| a and not atleast2(b)   | Multiple mentions cannot be used with NOT                                                                                                                                                                          | 110       |
	| a w/2 b*                | Cannot have a wildcard term with proximity search                                                                                                                                                                  | 110       |
	| A (AND B)               | Search term is not preceded/followed by a search term (current token: AND, current position: 3, current search: A (AND B))                                                                                         | 110       |
	| a or (not b)            | OR (NOT is not valid syntax                                                                                                                                                                                        | 111       |
	| (a                      | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 1, current search: (a)                                                                                         | 104       |
	| A headline() AND B      | Search clause contains a query modifier that does not precede a search term (current token: (, current position: 10, current search: A headline() AND B)                                                           | 108       |
	| AND a                   | AND cannot be at the beginning of the query (current token: AND, current position: 0, current search: AND a)                                                                                                       | 103       |
	| OR a                    | OR cannot be at the beginning of the query (current token: OR, current position: 0, current search: OR a)                                                                                                          | 103       |
	| a AND                   | AND/OR cannot be at the end of the query (current token: , current position: 4, current search: a AND)                                                                                                             | 102       |
	| a OR                    | AND/OR cannot be at the end of the query (current token: , current position: 3, current search: a OR)                                                                                                              | 102       |
	|                         | Search cannot be blank                                                                                                                                                                                             | 100       |
	| "unending               | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 8, current search: "unending)                                                                                  | 104       |
	| "unending quotes        | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 15, current search: "unending quotes)                                                                          | 104       |
	| ""                      | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 1, current search: "")                                                                                         | 104       |
	| "                       | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 0, current search: ")                                                                                          | 104       |
	| AND "foo"               | AND cannot be at the beginning of the query (current token: AND, current position: 0, current search: AND "foo")                                                                                                   | 103       |
	| "foo" AND               | AND/OR cannot be at the end of the query (current token: , current position: 8, current search: "foo" AND)                                                                                                         | 102       |
	| OR "foo"                | OR cannot be at the beginning of the query (current token: OR, current position: 0, current search: OR "foo")                                                                                                      | 103       |
	| "foo" OR                | AND/OR cannot be at the end of the query (current token: , current position: 7, current search: "foo" OR)                                                                                                          | 102       |
	| foo AND NOT             | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 10, current search: foo AND NOT)                                                                               | 104       |
	| AND NOT foo             | AND cannot be at the beginning of the query (current token: AND, current position: 0, current search: AND NOT foo)                                                                                                 | 103       |
	| "foo" AND NOT           | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 12, current search: "foo" AND NOT)                                                                             | 104       |
	| AND NOT "foo"           | AND cannot be at the beginning of the query (current token: AND, current position: 0, current search: AND NOT "foo")                                                                                               | 103       |
	| ()                      | Search clause cannot end with a query modifier or  Search clause contains a boolean AND/OR operator that is not followed by a search term (current token: ), current position: 1, current search: ())              | 105       |
	| (())                    | Search clause cannot end with a query modifier or  Search clause contains a boolean AND/OR operator that is not followed by a search term (current token: ), current position: 2, current search: (()))            | 105       |
	| (foo                    | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 3, current search: (foo)                                                                                       | 104       |
	| ("foo"                  | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 5, current search: ("foo")                                                                                     | 104       |
	| foo)                    | Search clause cannot end with a query modifier or  Search clause contains a boolean AND/OR operator that is not followed by a search term (current token: ), current position: 3, current search: foo))            | 105       |
	| "foo")                  | Search clause cannot end with a query modifier or  Search clause contains a boolean AND/OR operator that is not followed by a search term (current token: ), current position: 5, current search: "foo"))          | 105       |
	| cs()                    | Search clause cannot end with a query modifier or  Search clause contains a boolean AND/OR operator that is not followed by a search term (current token: ), current position: 3, current search: cs())            | 105       |
	| cs("foo"                | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 7, current search: cs("foo")                                                                                   | 104       |
	| cs(foo                  | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 5, current search: cs(foo)                                                                                     | 104       |
	| CS("foo")               | The given key was not present in the dictionary.                                                                                                                                                                   | 110       |
	| HEADLINE("FOO")         | The given key was not present in the dictionary.                                                                                                                                                                   | 110       |
	| headline()              | Search clause cannot end with a query modifier or  Search clause contains a boolean AND/OR operator that is not followed by a search term (current token: ), current position: 9, current search: headline())      | 105       |
	| headline("foo"          | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 13, current search: headline("foo")                                                                            | 104       |
	| headline(foo            | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 11, current search: headline(foo)                                                                              | 104       |
	| publication()           | Search clause cannot end with a query modifier or  Search clause contains a boolean AND/OR operator that is not followed by a search term (current token: ), current position: 12, current search: publication())  | 105       |
	| PUBLICATION("foo")      | The given key was not present in the dictionary.                                                                                                                                                                   | 110       |
	| publication("foo"       | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 16, current search: publication("foo")                                                                         | 104       |
	| publication(foo         | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 14, current search: publication(foo)                                                                           | 104       |
	| atleast2()              | Search clause cannot end with a query modifier or  Search clause contains a boolean AND/OR operator that is not followed by a search term (current token: ), current position: 9, current search: atleast2())      | 105       |
	| ATLEAST2("foo")         | The given key was not present in the dictionary.                                                                                                                                                                   | 110       |
	| atleast2("foo"          | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 13, current search: atleast2("foo")                                                                            | 104       |
	| atleast2(foo            | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 11, current search: atleast2(foo)                                                                              | 104       |
	| author()                | Search clause cannot end with a query modifier or  Search clause contains a boolean AND/OR operator that is not followed by a search term (current token: ), current position: 7, current search: author())        | 105       |
	| author("foo"            | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 11, current search: author("foo")                                                                              | 104       |
	| author(foo              | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 9, current search: author(foo)                                                                                 | 104       |
	| pageid()                | Search clause cannot end with a query modifier or  Search clause contains a boolean AND/OR operator that is not followed by a search term (current token: ), current position: 7, current search: pageid())        | 105       |
	| pageid(                 | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 6, current search: pageid()                                                                                    | 104       |
	| pageid(12345            | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 11, current search: pageid(12345)                                                                              | 104       |
	| sitedomain()            | Search clause cannot end with a query modifier or  Search clause contains a boolean AND/OR operator that is not followed by a search term (current token: ), current position: 11, current search: sitedomain())   | 105       |
	| sitedomain("")          | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 13, current search: sitedomain(""))                                                                            | 104       |
	| sitedomain(             | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 10, current search: sitedomain()                                                                               | 104       |
	| sitedomain(domain.com   | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 20, current search: sitedomain(domain.com)                                                                     | 104       |
	| sitedomain("domain.com" | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 22, current search: sitedomain("domain.com")                                                                   | 104       |
	| w/5 "bar"               | Snytax error and should contact support for help (current token: w/5, current position: 0, current search: w/5 "bar")                                                                                              | 110       |
	| "foo" w/5               | Search is missing qoute/paran or contains unbalanced paratheses (current token: , current position: 8, current search: "foo" w/5)                                                                                  | 104       |
	| "foo" w/1.5 "bar"       | Proximity matches must be used with either single terms or OR lists of terms on both sides (left: OrionTerm, right: MultiTermAndFilter)                                                                            | 110       |
	| *foo                    | Snytax error and should contact support for help (current token: *, current position: 0, current search: *foo)                                                                                                     | 110       |
	| fo*o                    | Snytax error and should contact support for help (current token: o, current position: 3, current search: fo*o)                                                                                                     | 110       |
	| (foo)*                  | Snytax error and should contact support for help (current token: *, current position: 5, current search: (foo)*)                                                                                                   | 110       |
	| *                       | Snytax error and should contact support for help (current token: *, current position: 0, current search: *)                                                                                                        | 110       |
	| (*)                     | Snytax error and should contact support for help (current token: *, current position: 1, current search: (*))                                                                                                      | 110       |
	| foo AND OR bar          | Search clause contains a syntax error or Search clause contains a boolean AND/OR operator that is not preceded by a search term (current token: OR, current position: 8, current search: foo AND OR bar)           | 110       |
	| foo AND AND NOT bar     | Search term is not preceded/followed by a search term (current token: AND, current position: 8, current search: foo AND AND NOT bar)                                                                               | 110       |
	| (foo AND bar) OR OR baz | Search clause contains a syntax error or Search clause contains a boolean AND/OR operator that is not preceded by a search term (current token: OR, current position: 17, current search: (foo AND bar) OR OR baz) | 110       |
		
@acl @Settings @KeywordSearches @Rename
Scenario: An Admin User should be able to rename his Boolean Advanced Keyword Search.
	Given I login as 'ACL Keyword Automation'
	And the API test data 'KeywordSearchesTestData.json'
	When I perform a PUT on MediaMonitoring name endpoint to rename ADVANCED Keyword Search
	Then MediaMonitoring name endpoint PUT response code for Advanced Boolean Search should be 200

@acl @Settings @KeywordSearches @Rename
Scenario: An Admin User should be able to rename his Regular Keyword Search.
	Given I login as 'ACL Keyword Automation'
	And the API test data 'KeywordSearchesTestData.json'
	When I perform a PUT on MediaMonitoring name endpoint to rename REGULAR Keyword Search
	Then MediaMonitoring name endpoint PUT response code for Regular Search should be 200 	
	
@acl @Settings @KeywordSearches @security 
Scenario: A Read Only User should NOT be allow to run a Sample Advanced Boolean Keywords search
	Given I login as 'ACL ReadOnly User'
	And the API test data 'KeywordSearchesTestData.json'
	When I perform a POST on MediaMonitoring endpoint to create a 'SAMPLEINVALID' Keyword Search with 'ADVANCED' Keywords with a search name with '200' characters and search term <SearchTerm>
	Then Monitoring Sample POST endpoint response code should be 403 and validation message should be Security error. and Error code 0

@acl @Settings @KeywordSearches @security 
Scenario: A Standard User should NOT be allow to run a Sample Advanced Boolean Keywords search
	Given I login as 'ACL Standard User'
	And the API test data 'KeywordSearchesTestData.json'
	When I perform a POST on MediaMonitoring endpoint to create a 'SAMPLEINVALID' Keyword Search with 'ADVANCED' Keywords with a search name with '200' characters and search term <SearchTerm>
	Then Monitoring Sample POST endpoint response code should be 403 and validation message should be Security error. and Error code 0

@acl @Settings @KeywordSearches @Rename
Scenario: An Readonly User should NOT be able to rename his Regular Keyword Search.
	Given I login as 'ACL ReadOnly User'
	And the API test data 'KeywordSearchesTestData.json'
	When I perform a PUT on MediaMonitoring name endpoint to rename REGULAR Keyword Search
	Then MediaMonitoring name endpoint PUT response code for Regular Search should be 403 

@acl @Settings @KeywordSearches @Rename
Scenario: An Standard User should NOT be able to rename his Boolean Advanced Keyword Search.
	Given I login as 'ACL Standard User'
	And the API test data 'KeywordSearchesTestData.json'
	When I perform a PUT on MediaMonitoring name endpoint to rename ADVANCED Keyword Search
	Then MediaMonitoring name endpoint PUT response code for Advanced Boolean Search should be 403

@acl @Settings @KeywordSearches @Security
Scenario: An Readonly User can NOT create a Keyword Searches
	Given I login as 'ACL ReadOnly User'
	And the API test data 'KeywordSearchesTestData.json'
 	When I perform a POST on MediaMonitoring endpoint to create a 'SAMPLE' Keyword Search with 'ADVANCED' Keywords with a search name with '200' characters and search term A AND B AND Z
	And I perform a POST on MediaMonitoring endpoint to create a 'NEW' Keyword Search with 'ANDandOR' Keywords with a search name with '200' characters and search term A AND B AND Z
	Then MediaMonitoring endpoint POST response code should be 403