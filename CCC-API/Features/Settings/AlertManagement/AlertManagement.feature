Feature: AlertManagement
	In order to receive alerts from CCC
	As a CCC User
	I want to manage alerts

@acl @Ignore
Scenario: Alerts are returned for Manager user
	Given I login as 'Alert Manager'
	When I perform a GET for Management Alerts
	Then the Alerts response code should be '200'
	And there should be a management alert named 'Alert1'
	And there should be a management alert named 'Alert2'
	And there should be a management alert named 'Alert3'

@acl @Ignore
Scenario: Alerts are not returned for unauthorized user
	Given I login to Company 'onpointcompanyalert' with 'alertusertest' and '1'
	When I perform a GET for Management Alerts
	Then the Alerts response code should be '401'
	And there should be '0' alerts returned in the response

@acl @Ignore
Scenario: Alerts are returned for Standard user
	Given I login as 'Alert Standard User'
	When I perform a GET for Management Alerts
	Then the Alerts response code should be '200'
	And there should be a management alert named 'Alert1'
	And there should be a management alert named 'Alert2'
	And there should be a management alert named 'Alert3'