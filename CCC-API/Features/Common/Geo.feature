Feature: Geo
	In order to test functionality 
	I want to see if countries are returned

@Login
Scenario: Get Valid Countries
	#TODO: Improve this scenario to compare against list of valid countries
	Given I login as 'prweb'
	When I call get valid Countries endpoint
	Then the number of countries returned should equal '237'
	And each country name should not be null

@Login
Scenario: Geo endpoint returns correct auto suggest results
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for places with a param of 'chi'
	Then all result descriptions should contain 'chi'
