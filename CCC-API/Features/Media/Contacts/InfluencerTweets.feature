Feature: InfluencerTweets
	In order to see an influencers tweets
	As a CCC user
	I want an endpoint to return tweets

@media @ignore
Scenario: Get recent tweets with list name
	Given I login as 'Instagram OAuthed'
	When I perform a GET for recent tweets by list name 'baltimore radio programs'
	Then the Contact Lists Endpoint response code should be '200'
	And the response should have recent tweets

@media @ignore
Scenario: Get recent tweets - list not found
	Given I login as 'Posdemo Manager'
	When I perform a GET for recent tweets by list name 'a list not found'
	Then the Contact Lists Endpoint response code should be '200'
	And the response should not have recent tweets

@media @ignore
Scenario: Get recent tweets for UAC user should returns 404
	Given I login as 'UAC No Access 401'
	When I perform a GET for recent tweets by list name 'Automation UAC No Access - Do Not Delete'
	Then the Contact Lists Endpoint response code should be '404'
	And the response should not have recent tweets

@media @ignore
Scenario: Get recent tweets for user without OAuth returns 401
	Given I login as 'No OAuth User'
	When I perform a GET for recent tweets by list name '000 No OAuth List'
	Then the Contact Lists Endpoint response code should be '401'