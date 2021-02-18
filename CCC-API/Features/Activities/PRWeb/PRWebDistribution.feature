@PRWeb
Feature: PRWebDistribution
	In order to use the CCC application
	As a PRWeb user
	I need to verify the endpoints works

Scenario: Save Draft Distribution Test
	Given the API test data 'PRWebDistribution.json'
	And I login as 'prweb'
	When I call save draft distribution
	Then The distribution is in the listing data
	Then Delete created distribution from Databases

Scenario: Is Limited Distribution Approval Test
	Given the API test data 'PRWebDistribution.json'
	And I login as 'prweb'
	When I call send distribution and set status to 'PendingDistributionUponUserApproval'
	Then The distribution status is Pending Distribution Upon User Approval
	Then Delete created distribution from Databases

Scenario: On Hold Reasons Test
	Given the API test data 'OnHoldReason.json'
	And I login as 'PRWebOnHoldReasonsTest'
	When I call the On hold Reason Endpoint
	Then The on hold reason match as expected
	
Scenario Outline: PRWeb Create Distribution Test With Video
	Given the API test data 'PRWebDistributionWithVideo.json'
	And I login as 'prweb'
	When I call send new distribution with video '<videoUrl>'
	Then The distribution is in the listing data
	Then Delete created distribution from Databases

	Examples: Videos urls
	| videoUrl                     |
	| https://youtu.be/EzKImzjwGyM |
	| https://vimeo.com/6370469    |

Scenario: Verify endpoint prweb distribution preview returns the proper info
	Given the API test data 'PRWebDistribution.json'
	And I login as 'prweb'
	When I call get distribution preview
	Then The preview info match the provided data

Scenario: PRWeb Verify the IDL get added in to the distribution and storage in the DB
	Given the API test data 'PRWebDistributionWithIdl.json'
	And I login as 'getDistributionIdl'
	When I call send distribution
	Then The IDL is in the response and added to the DB
	Then Delete created distribution from Databases

Scenario: PRWeb Get Chart Impression Test
	Given the API test data 'AnalyticSentDistribution.json'
	And I login as 'PRWebElysiumCompany1'
	When I clean and update count for release
	And I call to API headlines impressions
	And I call to API full release reads
	Then The news Aggregator and prweb impressions match to headlines impressions
	And The count read match the full release reads

Scenario: PRWeb Back To Draft Test
	Given the API test data 'PRWebDistribution.json'
	And I login as 'prweb'
	When I call send distribution and set status to 'InEditorialReview' 
	Then I call distribution back to draft
	And Delete created distribution from Databases

Scenario: PRWeb Get Analytics Online Pickup Totals
	Given the API test data 'AnalyticsSentDistributionOnlinepickup.json'
	And I login as 'PRWebElysiumCompany1'
	When I call online pickup analitycs
	Then I get all the online pickup and potencial audience in a descending order

Scenario: PRWeb Verify the attachments get added in to the distribution
	Given the API test data 'PRWebDistributionImageAttach.json'
	And I login as 'prweb'
	When I call send distribution
	Then The values of the attachments are in the reponse
	And Delete created distribution from Databases

Scenario: PRWeb Create Distribution Test
	Given the API test data 'PRWebDistribution.json'
	And I login as 'prweb'
	When I call send distribution
	Then I see all the values in the response
	And Delete created distribution from Databases

Scenario: PRWeb Create Distributions And Check Sort Order For Activities Grid
	Given the API test data 'PRWebDistribution.json'
	And I login as 'prweb10'
	When I send multiple distribution and set publish activity status
	Then I see the distribution are listed in order
	And Delete multiple distributions from database

Scenario: Save Distribution With OAuths Test
	Given the API test data 'PRWebDistribution.json'
	And I login as 'prweb'
	When I call save draft distribution
	Then The OAuth values are in the response
	Then Delete created distribution from Databases

Scenario: Save Draft Distribution and persist data
	Given the API test data 'PRWebDistribution.json'
	And I login as 'prweb'
	When I call save draft distribution
	Then I see all the values in the response
	And All data is saved in the release
	And Delete created distribution from Databases

Scenario: PRWeb Create Distribution With RetireEcomOptions2017 param Test
	Given the API test data 'PRWebDistribution.json'
	And I login as 'prweb with RetireEcomOptions2017 enabled'
	When I call send distribution
	Then I see all the values in the response
	And Delete created distribution from Databases

Scenario: PRWeb Create Distribution With PullOutQuote
	Given the API test data 'PRWebDistributionWithPullOutQuote.json'
	And I login as 'prweb'
	When I call send distribution
	Then Then Pull Out Quote Will Have Been Saved
	Then Delete created distribution from Databases

Scenario: PRWeb Verify SendToIris returns true value for Distribution
	Given the API test data 'PRWebDistributionIdWithSendToIris.json'
	And I login as 'prweb21'
	When I call is send to Iris
	Then I see a true response

Scenario: PRWeb Verify SendToIris returns false value for Distribution
	Given the API test data 'PRWebDistributionIdWithoutSendToIris.json'
	And I login as 'prweb21'
	When I call is send to Iris
	Then I see a false response

Scenario: PRWeb Verify Subscription is SendToIris
	Given I login as 'PRWebSendToIrisAccount'
	When I call get valid subscription endpoint
	Then I see a subscription where SendToIris is true

Scenario: PRWeb Create Distribution Test set back to draft and resubmmit with different subscription id
	Given the API test data 'PRWebDistribution.json'
	And I login as 'prweb'
	When I call send distribution
	Then I call distribution back to draft
	And I resubmit distribution with a different subscription to error validation
	And Delete created distribution from Databases

Scenario: PRWeb 30 day re-submit rule enforced
	Given the API test data 'PRWebDistributionPublished30plusDaysAgo.json'
	And I login as 'PRWebElysiumCompany4'
	When I call Resubmit distribution
	Then I get the validation issue about the 30 day re-edit rule

Scenario: PRWeb Get selected addons for a Distribution
	Given the API test data 'PRWebDistributionWithAddons.json'
	When I call get selected addons
	Then I get the selected addons for the Distribution

Scenario: PRWeb Update an addon subscription quantity used
	Given the API test data 'PRWebAddonSubscriptionWithSession.json'
	When I call update addons
	Then the addon quantity is updated
	And I reset the addon quantity in the DB

Scenario: PRWeb The cpre phone extension default value should not be used.
	Given the API test data 'PRWebDistribution.json'
	And I login as 'PRWebElysiumCompany4'
	And the press contact phone extension userparameter has a value
	When I call save draft distribution
	Then the distribution press contact phone extension is blank

Scenario: CAP Account and Tweet OauthXRef and OAuthJSON data confirmation
	Given the API test data 'PRWebDistributionWithCAP.json'
	And I login as 'PRWebElysiumCompany4'
	When I call send distribution
	Then I see TwitterID and Message values in the response
	And Delete created distribution from Databases
