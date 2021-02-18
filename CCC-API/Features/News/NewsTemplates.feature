Feature: NewsTemplates
	In order to send standardize News Forwards
	As a C3 user
	I want to be able to use Templates

@herdOfGnus @news
Scenario: Verify permissions in Custom Template are set to False
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for all news templates
	And I get a single custom template 
	Then I should see a value of False for all permissions

@socialmedia @news
Scenario: Verify Promo for custom templates is disagled by default
	Given I login as 'socialmedia User3'
	When I perform a GET for all news templates
	Then I verify promo for custom templates is disabled by default

@socialmedia @news
Scenario Outline: Verify Promo for custom templates is available when enabled
	Given I login as 'socialmedia User2'
	When I perform a GET for all news templates
	Then I should see promo for custom templates when a company has this enabled and link should be <link>

Examples: 
	| link	|
	| https://us.vocuspr.com/Webpublish/controller.aspx?SiteName=CustomTemplates&Definition=Home   |	