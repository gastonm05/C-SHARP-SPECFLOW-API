Feature: NewsArchiveSearch
	In order to retrieve news from Archive
	As a C3 user
	I want to be able to use the API

@herdOfGnus @news
Scenario: Automate Add Sources to Archive Search Criteria
	Given I login as 'news archive manager'
	When I search for news archive by keywords with the value 'marvel' and by sources with a value of '1'
	Then the News Archive Endpoint has the correct response

@herdOfGnus @ignore @news
Scenario: User can pull in 1000 clips
	Given I login as 'news archive user'
	When I search for news archive by keywords with a value of 'marvel'
	Then the News Archive Endpoint has the correct response
	And I should see a value of '1000' for AddNewsToLimit

@herdOfGnus @ignore @news
Scenario: Remove Connect Website Analytics Button from All News
	Given I login as 'news archive manager'
	When I search for news archive by keywords with a value of 'marvel'
	Then the News Archive Endpoint has the correct response

@herdOFGnus @news
Scenario: Validate NLA Sources in Archive News Search
	Given I login as 'news archive manager'
	When I search for news archive by keywords with the value 'eclipse' and by source name with a value of 'NLA Web'
	Then the News Archive Endpoint has the correct response
	And I should see all the archive clips are coming from NLA Web source

@herdOFGnus @news
Scenario: Validate CLA Sources in Archive News Search
	Given I login as 'news archive manager'
	When I search for news archive by keywords with the value 'eclipse' and by source name with a value of 'CLA Web'
	Then the News Archive Endpoint has the correct response
	And I should see all the archive clips are coming from CLA Web source

@herdOfGnus @ignore @news
Scenario: Validate News Archive Search with Orion Syntax by Case Sensitive
	Given I login as 'Orion Syntax User'
	When I search for news archive by keywords with a value of 'cs(FOX)'
	Then the News Archive Endpoint has the correct response

@herdOfGnus @ignore @news
Scenario: Validate News Archive Search with Orion Syntax by Proximity
	Given I login as 'Orion Syntax User'
	When I search for news archive by keywords with a value of '(samsung) w/9(galaxy)'
	Then the News Archive Endpoint has the correct response

@herdOfGnus @ignore @news
Scenario: Validate News Archive Search with Orion Syntax by Truncation
	Given I login as 'Orion Syntax User'
	When I search for news archive by keywords with a value of 'snack*'
	Then the News Archive Endpoint has the correct response

@herdOfGnus @ignore @news
Scenario: Validate News Archive Search with Orion Syntax by Minimum Mentions
	Given I login as 'Orion Syntax User'
	When I search for news archive by keywords with a value of 'atleast2(trump)'
	Then the News Archive Endpoint has the correct response
	
@herdOFGnus @news
Scenario: Validate Archive News Search Results can be added to My Coverage
	Given I login as 'news archive manager'
	When I search for news archive by keywords with a value of 'marvel'
	Then the News Archive Endpoint has the correct response
	When I add the first '100' results to My Coverage
	Then the News Archive Import Endpoint has the correct response

@herdOfGnus @ignore @news
Scenario: Validate News Archive Parameter Max Archive Date Range
	Given I login as 'C3ShakedownAutomation Manager'
	When I perform a GET for archve search preferences
	Then I should see the value for Archive Search Days Limit

@herdOfGnus @ignore @news
Scenario: Validate News Archive Search with Orion Syntax using boolean
	Given I login as 'Orion Syntax User'
	When I search for news archive by keywords with a value of 'samsung OR galaxy'
	Then the News Archive Endpoint has the correct response

@herdOfGnus @ignore @news
Scenario: Validate News Archive Search with Orion Syntax using special characters (#wine)
	Given I login as 'Orion Syntax User'
	When I search for news archive by keywords with a value of '%23wine'
	Then the News Archive Endpoint has the correct response

@herdOfGnus @ignore @news
Scenario: Validate News Archive Search with wrong Orion Syntax returns proper response
	Given I login as 'Orion Syntax User'
	When I search for news archive by keywords with a value of 'Music%20AND%20(jazz%20OR%20metal'
	Then the news archive endpoint response is correct

@herdOfGnus @ignore @news
Scenario: Validate TVEyes Content in Search All News
	Given I login as 'news archive manager'
	When I search for news archive by keywords with a value of 'weather'
	Then the News Archive Endpoint has the correct response
	And all the news clips are from TVEyes