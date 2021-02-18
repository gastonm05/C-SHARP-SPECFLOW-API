Feature: Insights
	In order to verify insights working
	As a valid C3 user
	I want to create table with insights stats

@socialmedia @smokeProd
Scenario Outline: Retrieve social networks insights for the last days
Given I login as 'analytics manager'
When I retrieve <social_network> insights for the last <days> days
Then The insights response should be "OK"

Examples: 
	|  social_network  		|	days	| 
	|	facebook			|	28		|
	|	twitter				|	28		| 
