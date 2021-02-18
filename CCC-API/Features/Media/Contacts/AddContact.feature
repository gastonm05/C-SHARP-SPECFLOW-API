Feature: AddContact
	To verify that I can add a private contact in the correct circumstances

@media @media
Scenario Outline: Add a private contact with list data
	Given I login as 'Standard User'
	When I create a new contact with list data associated with outlet 'chicago online' and the country '<id>'
	Then the list data should match when I GET the new contact
	Then the contact is deleted

	Examples: 
	| id  | value          |
	| 316 | United States  |

@media @ignore @media
Scenario Outline: Add a private contact without list data
	Given I login as 'manager Standard User'
	When I create a new contact without list data associated with outlet 'chicago online' and the country '<id>'
	Then the data should match when I GET the new contact
	Then the contact is deleted

	Examples: 
	| id  | value          |
	| 316 | United States  |

@media @ignore @media
Scenario Outline: Verify that a private contact is added properly when the user select a country outside its segmentation
	Given I login as 'company with US data segmentation'
	When I create a new contact data associated with outlet 'chicago online' and the country '<id>'
	Then the data should match when I GET the new contact
	Then the contact is deleted

Examples: 
	| id  | value          |
	| 292 | United Kingdom |

@media @ignore @media
Scenario Outline: Add a private contact using a country from CCC-5595
	Given I login as 'Posdemo Manager'
	When I create a new contact without list data associated with outlet 'chicago online' and the country '<id>'
	Then the country for the new contact created should contain '<value>'
	Then the contact is deleted

	Examples: 
	| id  | value          |
	| 389 | South Sudan    |