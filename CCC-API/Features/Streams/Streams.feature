Feature: Streams
	In order to verify streams creation
	As a valid C3 user
	I want to create a Stream

@socialmedia
Scenario Outline: Create a Stream
Given I login as 'socialmedia User'
When I create a stream <stream_name> for <platform> social network, using contact list <list_name>
Then The response should be "Created"
And  I delete the previously created Stream


Examples: 
	|  stream_name  |	platform	| list_name			|
	|	Twitter1	|	Twitter		| Gaston's List		|
	|	Instragram1	|	Instagram	| Gaston's List		|

@socialmedia @ignore
Scenario: Streams are not viewable for companies without Stream parameter
	Given I login as 'Non Streams Manager'
	When I GET all streams
	Then The response should be "Forbidden"