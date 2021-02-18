Feature: NewsCedrom
	In order to manage CEDROM clips
	As a C3 user
	I want to be able to edit videos

@herdOfGnus @news
Scenario: Validate endpoint for creating segment
	Given I login as 'CEDROM Manager'
	When I perform a GET for all news
	And I perform a GET for a single CEDROM clip
	And I perform a POST to edit clip video
	Then the CEDROM segment endpoint response should be '201'
	When I perform a GET for CEDROM clip recently edited
	Then the endpoint response should include the segment information

@herdOfGnus @news
Scenario: Validate endpoint for updating segment
	Given I login as 'CEDROM Manager'
	When I perform a GET for all news
	And I perform a GET for a single CEDROM clip with video segment
	And I perform a PUT to update clip video
	Then the CEDROM segment endpoint response for updating a segment should be '200'

@herdOfGnus @news
Scenario: Validate endpoint for creating download link
	Given I login as 'CEDROM Manager'
	When I perform a GET for all news
	And I perform a GET for a single CEDROM clip
	And I perform a POST to edit clip video
	Then the CEDROM segment endpoint response should be '201'
	When I perform a GET for CEDROM clip recently edited to get dnews item links
	Then the endpoint response should include the download link

@herdOfGnus @news
Scenario: GET - CE @newsDROM Clip
	Given I login as 'CEDROM Manager'
	When I perform a GET to the CEDROM clip endpoint
	Then I should see the CEDROM clip endpoint response is '200'

@herdOfGnus @ignore @news
Scenario: GET - Validate CEDROM Clip has a value for Caption Url
	Given I login as 'CEDROM Manager'
	When I perform a GET to Cedrom News Endpoint
	Then the Cedrom News Endpoint response should be '200'
	And the value for Caption Url should not be null

@herdOfGnus @ignore @news
Scenario: Validate endpoint for creating a segment with sub-seconds
	Given I login as 'CEDROM Manager'
	When I perform a GET for all news
	And I perform a GET for a single CEDROM clip
	And I perform a POST to edit clip with sub seconds
	Then the CEDROM segment endpoint response should be '201'
	When I perform a GET for CEDROM clip recently edited
	Then the endpoint response should include the segment information

@herdOfGnus @ignore @news
Scenario: Validate CEDROM News Item with specific feed has a value for FileUrl instead of an ArticleUrl
	Given I login as 'Visible Indie Manager'
	When I perform a GET for all news
	And I perform a GET for a CEDROM clip with specific feed
	Then I should see that the FileUrl field is not null

@herdOfGnus @ignore @news
Scenario: Validate Cedrom News Item has a Clip value of null
	Given I login as 'CEDROM Manager'
	When I perform a GET for all news
	And I perform a GET for a single CEDROM clip
	Then I should see that the Clip property is null