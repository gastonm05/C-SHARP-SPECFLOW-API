Feature: NewsTVEyes
	In order to see edited TVEyes clips
	As a C3 User
	I want to be able to query endpoints

@herdOfGnus @news
Scenario: GET TVEyes Edited Clips
	Given I login as 'news archive manager'
	When I perform a GET for TVEyes Clip with edited videos for News ID '610913866'
	Then I should see the TVEyes endppoint has the correct response
	And I should see that a primary clip exists
	And I should see that all clips have a Start Time
	And I should see that all clips have an End Time
	And I should see that all clips have a Type
	And I should see that all clips hve a Download link	