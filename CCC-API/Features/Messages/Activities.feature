Feature: Activities
	In order to verify activities publication
	I schedule a new activity

@socialmedia
Scenario Outline: Publishing new activities
Given I login as 'socialmedia User'
When  I schedule a new <type> Activity to send on <time>, <time_zone> with a title length of <title_text_length>, a notes length of <notes_text_length>
Then The message response was "Created"

Examples: 
	| type			|   time	|	time_zone					|	title_text_length	|	notes_text_length	|
	| Appointment	|	now		|	Eastern Standard Time		| 30					|	300				    |
