Feature: NewsForwards
	In order to forward news
	As a valid C3 User
	I want to be able to use themed templates

@herdOfGnus @ignore @news
Scenario: Should not be able to forward another company's template
	Given I login as 'CCC1481'
	When I perform a GET for all news
	And I POST to News Forwards endpoint with another Company template
	Then the News Forward endpoint should respond with a '400'
	And the response message should be 'does not exist'

@herdOfGnus @news @smokeProd
Scenario: User can forward news
	Given I login as 'CCC1481'
	When I perform a GET for all news
	And I POST to News Forwards endpoint with all available fields
	Then the News Forward endpoint should respond with a '202'
