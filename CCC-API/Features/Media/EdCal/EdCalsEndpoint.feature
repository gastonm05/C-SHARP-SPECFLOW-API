Feature: EdCalsEndpoint
	In order to find opportunites
	As a standard user
	I want to query edcals via the edcals endpoint

@media @ignore
Scenario: Search EdCals by Outlet Name
	Given I login as 'Standard User'
	When I perform a GET for EdCals by 'Outlet_Name' 'Chicago'
	Then all returned outlets should contain 'Chicago' in their name

@media @ignore
Scenario: Search EdCals by Contact Name
	Given I login as 'Standard User'
	When I perform a GET for EdCals by 'Contact_Name' 'Zeiger, Barbara'
	Then all returned EdCals should have 'Zeiger, Barbara' as their Contact name

@media @ignore
Scenario: Search EdCals by Outlet Country
	Given I login as 'Standard User'
	When I perform a GET for EdCals by 'Outlet_Country' '316'
	Then all returned EdCals should have 'United States' as their Outlet Country

@media @ignore
Scenario: Search EdCals by Issue Start Date only
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for EdCals by Issue Date with a 'start' date of 'Today minus 90 days'
	Then all returned EdCals should have an Issue Date 'greater' than or equal to 'Today minus 120 days'

@media @ignore
Scenario: Search EdCals by Issue End Date only
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for EdCals by Issue Date with a 'end' date of 'Today'
	Then all returned EdCals should have an Issue Date 'less' than or equal to 'Today'

@media
Scenario: Search EdCals by Issue Start and End Date
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for EdCals by Issue Date with a start date of 'Today minus 90 days' and an end date of 'Today'
	Then all returned EdCals should have an Issue Date between 'Today minus 90 days' and 'Today'