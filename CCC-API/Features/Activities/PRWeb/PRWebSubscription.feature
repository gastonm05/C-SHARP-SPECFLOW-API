Feature: PRWebSubscription
	In order to use the CCC application
	As a PRWeb user
	I need to verify the endpoints works
	
@PRWeb
Scenario: Get valid subscription with PRWeb company
	Given I login as 'prweb'
	When I call get valid subscription endpoint
	Then The response should have all valid subscription

@PRWeb
Scenario: For a published distribution single subscription endpoint should be call
	Given I login as 'prweb21'
	Given the API test data 'DistributionIDForSingleSubscription.json'
	And I get subscription id of the published release 'DistributionID'
	When I call the single subscription endpoint
	Then I see only one subscription and it is the same as the release contain