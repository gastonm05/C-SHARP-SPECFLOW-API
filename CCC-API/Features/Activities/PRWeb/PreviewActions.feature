Feature: PreviewActions
	In order to test the preview actions
	As a user
	I want to see the response of the endopoint ok

@PRWeb
Scenario: Send preview to an email
	Given the API test data 'PRWebDistribution.json'
	And I login as 'prweb'
	When I call send email preview to 'testemail@cision.com'
	Then the job response status code is '200'

@PRWeb
Scenario: Download preview PDF
	Given the API test data 'PRWebDistribution.json'
	And I login as 'prweb'
	When I call download preview to use the PDF link
	Then the job response status code is '200'
