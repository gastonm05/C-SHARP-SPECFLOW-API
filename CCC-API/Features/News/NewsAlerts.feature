Feature: NewsAlerts
	In order to receive updated news items
	As a CCC standard user
	I want to use news alerts

@herdOfGnus @ignore @news
Scenario: Get single news alert
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for All News Alerts
	And I perform a GET for a Single News Alert
	Then returned news alert id is the same as the requested alert id

@herdOfGnus @ignore @news
Scenario: 400 error returned for invalid news alert id
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for a Single News Alert with id '999999999'
	Then the news alert endpoint response should return a '400' status