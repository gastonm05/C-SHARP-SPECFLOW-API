Feature: DistributionPrivilege
	In order to access activities distribution
	I want to see the privilege of the company

@PRWeb
Scenario: The Company does not have PRWeb Access based on subscription expiration date
	Given I login as 'TheParrotHasPRWebAccess'
	When I Update prweb distribution expiration date to '-1' days from today
	Then The Company access to prweb distribution is 'false'

@PRWeb
Scenario: The Company has PRWeb Access based on subscription expiration date
	Given I login as 'TheParrotHasPRWebAccess'
	When I Update prweb distribution expiration date to '300' days from today
	Then The Company access to prweb distribution is 'true'