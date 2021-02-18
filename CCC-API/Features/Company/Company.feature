Feature: Company Id
	In order to get a Company Id
	I use Company endpoint using a valid Customer Id

@Login
Scenario: get customerId
	Given the API test data 'CompanyIdCustomerId.json'
	When I get the Company Id using a Customer Id
	Then the Company ID is correct

@Ignore @Login
Scenario: get customerId with non existing customerId
	When I get the Company Id using invalid customerId 'invalidCustomerId'
	Then the Company endpoint should return a 404 error with message 'Could not find company with that CustomerID'