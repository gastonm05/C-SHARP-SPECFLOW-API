Feature: PRWebDistribution2
	In order to use the CCC application
	As a PRWeb user
	I need to verify the endpoints works

@PRWeb
Scenario: PRWeb When a distrbution with INF subscription is saved for later CJL and CSP addons quantityUsed will not increase
	Given the API test data 'PRWebDistributionWithCJLandCSP.json'
	And I login as 'PRWebC3AutomationAddonsForAPI'
	When I get available addons for subscription 'Influencer'
	And I call save draft distribution with subscription 'Influencer'
	Then I see the quantityUsed of available Addons has not increased for subscription 'Influencer'
	Then Delete created distribution from Databases

@PRWeb
Scenario: PRWeb When a distrbution with POW subscription is saved for later CJL addons quantityUsed will not increase
	Given the API test data 'PRWebDistributionWithCJLandCSP.json'
	And I login as 'PRWebC3AutomationAddonsForAPI'
	When I get available addons for subscription 'Web Power'
	And I call save draft distribution with subscription 'Web Power'
	Then I see the quantityUsed of available Addons has not increased for subscription 'Web Power'
	Then Delete created distribution from Databases

@PRWeb
Scenario: PRWeb Verify Addons increased usedQuantity properly after submit and resubmit for INF subscription
	Given the API test data 'PRWebDistributionWithCJLandCSP.json'
	And I login as 'PRWebC3AutomationAddonsForAPI'
	When I get available addons for subscription 'Influencer'
	And I submit distribution put it back to draft and resubmit with subscription 'Influencer'
	Then I see the CJL addon QuantityUsed has incread in '2' for subscription 'Influencer'
	And I see the the CSP addon QuantityUsed has increased in one for subscription 'Influencer'
	Then Delete created distribution from Databases

@PRWeb
Scenario: PRWeb Verify Addons increased usedQuantity properly after submit and resubmit for POW subscription
	Given the API test data 'PRWebDistributionWithCJLandCSP.json'
	And I login as 'PRWebC3AutomationAddonsForAPI'
	When I get available addons for subscription 'Web Power'
	And I submit distribution put it back to draft and resubmit with subscription 'Web Power'
	Then I see the CJL addon QuantityUsed has incread in '1' for subscription 'Web Power'
	Then Delete created distribution from Databases

