@impact
Feature: EarnedMedia
	To verify that earned media retrieve data
	As a valid CCC user
	I want to call the eaned media endpoints

Scenario: Searches data is correct 
	Given I login as 'Impact Enabled Company'
	When I call the HC search endpoint 
	Then the search endpoint has the correct response