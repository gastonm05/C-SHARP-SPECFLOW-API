Feature: Export - Templates
	To verify that export templates can be created, retrieved, modified and deleted
	using a valid CCC user I want to call the UI/Grid endpoint - api/v1/exporttemplates

@configuration @ExportTemplates @Ignore
Scenario: A Admin User can modify a 'Outlet' template and save it 
    Given the API test data 'ExportTemplateData.json'
	And I login as 'API Manager User'
	When I perform a GET on export template endpoint using a 'Outlet' exporttemplateid
	Then Get ExportTemplate endpoint response code should be 200	
	When I perform a PUT on Export Template endpoint using a 'Outlet' exporttemplateid
	Then Export Template endpoint 'PUT' response code should be 201
	And I verify all Export Template data was changed

@configuration @ExportTemplates @Ignore
Scenario: A Admin User can modify a 'News' template and save it 
    Given the API test data 'ExportTemplateData.json'
	And I login as 'API Manager User'
	When I perform a GET on export template endpoint using a 'News' exporttemplateid
	Then Get ExportTemplate endpoint response code should be 200	
	When I perform a PUT on Export Template endpoint using a 'News' exporttemplateid
	Then Export Template endpoint 'PUT' response code should be 201
	And I verify all Export Template data was changed

@configuration @ExportTemplates @Ignore
Scenario: A Admin User can modify a 'Contact' template and save it 
	Given the API test data 'ExportTemplateData.json'
    And I login as 'API Manager User'
	When I perform a GET on export template endpoint using a 'Contact' exporttemplateid
	Then Get ExportTemplate endpoint response code should be 200	
	When I perform a PUT on Export Template endpoint using a 'Contact' exporttemplateid
	Then Export Template endpoint 'PUT' response code should be 201
	And I verify all Export Template data was changed

@configuration @ExportTemplates @Ignore
Scenario: A Admin User can create a 'Contact' template and delete it 
    Given the API test data 'ExportTemplateData.json'
	And I login as 'API Manager User'
	When I perform a POST on export template endpoint with export type '0'
	Then Export Template endpoint 'POST' response code should be 201
	When I perform a DELETE on export template endpoint to delete just created export template
	Then Delete ExportTemplate endpoint response code should be 200

@configuration @ExportTemplates @Ignore
Scenario: A Admin User can create a 'Outlet' template and delete it 
    Given the API test data 'ExportTemplateData.json'
	And I login as 'API Manager User'
	When I perform a POST on export template endpoint with export type '1'
	Then Export Template endpoint 'POST' response code should be 201
	When I perform a DELETE on export template endpoint to delete just created export template
	Then Delete ExportTemplate endpoint response code should be 200

@configuration @ExportTemplates  @Ignore
Scenario: A Admin User can create a 'News' template and delete it 
    Given the API test data 'ExportTemplateData.json'
	And I login as 'API Manager User'
	When I perform a POST on export template endpoint with export type '2'
	Then Export Template endpoint 'POST' response code should be 201
	When I perform a DELETE on export template endpoint to delete just created export template
	Then Delete ExportTemplate endpoint response code should be 200