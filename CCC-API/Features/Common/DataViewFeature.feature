Feature: DataView
	To verify that DataViews can be created, retrieved, and deleted
	using a valid CCC user I want to call the UI/DataView endpoint

@configuration
Scenario: A Admin user updates sort column to 'Phone' and sort direction to 'desc'
    
	Given I login as 'API Manager User'
	When I run a PUT on DataView endpoint to modify 'contact-datatable' DataView with sort column to 'Phone' and sort direction to 'desc'
	Then PUT DataView endpoint response code should be 201
	When I perform a GET on DataView endpoint for GridView 'contact-datatable' 
	Then I verify response code should be 200 and sort column was properly set to 'Phone' and sort direction to 'desc'	