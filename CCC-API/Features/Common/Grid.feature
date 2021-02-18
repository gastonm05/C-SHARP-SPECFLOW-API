Feature: Grid - Templates
	To verify that Grid templates can be created, retrieved, modified and deleted
	using a valid CCC user I want to call the UI/Grid endpoint - api/v1/ui/grid/

@configuration @Grid 
Scenario: A Admin User can open a template and save it 
    Given I login as 'Grid Company'
	When I perform a GET on grid endpoint the GridTemplateId 'contact-datatable' 
	Then Get grid endpoint response code should be 200	
	When I perform a PUT on Grid Template endpoint to modify template id 'contact-datatable' with test data.
	Then UI/Grid endpoint 'PUT' response code should be 201 	
	And I verify all Grid Template data was changed