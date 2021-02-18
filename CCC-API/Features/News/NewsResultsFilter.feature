Feature: NewsResultsFilter
	In order to narrow down news results
	As a standard CCC user
	I want to filter results

@herdOfGnus @ignore @news
Scenario: News - Get News with Multiple Outlet Facets
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for all news
	And I perform a GET for news with 'facet categories Outlet Type, Outlet Medium' facets
	Then all returned news items have an Outlet 'MediaType' equal to the facet category 'Outlet Type'
	And all returned news items have an Outlet 'OutletMedium' equal to the facet category 'Outlet Medium'

@herdOfGnus @ignore @news
Scenario: Validate Filter by Outlet Name
	Given I login as 'ESAManager'
	When I perform a GET for all news
	And I perform a GET for news with 'facet categories Outlet Name' facets
	Then all returned news items have an Outlet 'Name' equal to the facet category 'Outlet Name'