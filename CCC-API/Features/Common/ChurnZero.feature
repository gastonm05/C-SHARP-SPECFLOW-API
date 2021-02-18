Feature: ChurnZero

@Ignore @configuration
Scenario: ChurnZero endpoint returns correct company name
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for ChurnZero Integrations
	Then the returned ChurnZero Company should be '"ElyShakedownAutomation"'