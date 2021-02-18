Feature: DmaEndpoint
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@media @ignore
Scenario: Verify DMA are properly sorted by rank
	Given shared session for 'standard' user with edition 'basic' 
	When I perform a GET for DMA endpoint sorted by 'Rank'
	Then I should see the DMA ranks in the correct order

@media @ignore
Scenario: Verify DMA are properly sorted by Name
	Given shared session for 'standard' user with edition 'basic' 
	When I perform a GET for DMA endpoint sorted by 'Name'
	Then I should see the DMA Name in the correct order