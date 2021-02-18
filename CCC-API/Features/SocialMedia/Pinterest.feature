Feature: Pinterest

@acl
Scenario: Get Pinterest Boards
	Given I login as 'API Manager User'
	When I get all pinterest boards
	Then the response returns pinterest boards