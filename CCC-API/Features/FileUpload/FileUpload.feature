Feature: FileUpload
	As a valid C3 user
	I want to upload a file when creating a distribution


@distronauts
Scenario Outline: Upload file when creating a distribution
Given I login as 'distribution user'
When I upload a file <filepath> to create a distribution
Then  The result should be <status> on the screen

Examples: 
	| filepath					   | status |
	| FormattedText.docx		   | OK     |