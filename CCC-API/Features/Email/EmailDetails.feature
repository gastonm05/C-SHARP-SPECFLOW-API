Feature: EmailDetails
	In order to see the status of my distribution
	I can use Email Sent Details page to view it

Background: 	
	And I remember expected data from 'Distributions.json' file

@publishers @email_sent_details
Scenario Outline: Filter recipients
	Given shared session for 'standard' user with edition 'Publishers manager user'
	When I perform a filter on <type> which <option> and <offset>
	Then the email distribution details endpoint should return the list of <type> who <option> skipping <offset>

Examples: 
	| type           | option            | offset |
	| contacts       | hasClickedLink    | 0      |
	| contacts       | hasOpened         | 0      |
	| contacts       | hasNotOpened      | 0      |
	| contacts       | hasNotClickedLink | 0      |
	| contacts       | hasClickedLink    | 1      |
	| outlets        | hasClickedLink    | 0      |
	| outlets        | hasOpened         | 0      |
	| outlets        | hasNotOpened      | 0      |
	| outlets	     | hasNotClickedLink | 1      |
	| outlets	     | hasNotClickedLink | 0      |	
	| individuals    | hasClickedLink    | 0      |
	| individuals	 | hasOpened         | 0      |
	| individuals	 | hasNotOpened      | 0      |
	| individuals	 | hasNotOpened      | 1	  |	
	| organizations  | hasNotClickedLink | 0      |
	| organizations  | hasClickedLink    | 1      |
	| organizations  | hasOpened         | 0      |
	| organizations  | hasNotOpened      | 0      |
	| organizations  | hasOpened		 | 1      |

@publishers @email_sent_details
Scenario: Email Sent Details Page > Analytics
	Given shared session for 'standard' user with edition 'Publishers manager user'
    And the API test data 'DistributionLinks.json'
	And I remember distribution links
	And I create sent email distribution to 'contacts,outlets' with links
	And request distribution recipients
	When I request sent email distribution details
	Then email sent details analytics request shows '0' opens, '0' clicks, '0' bounces
	And each link parsed correctly
	When open event present in the system for '3' recipients
	And I request sent email distribution details
	Then email sent details analytics request shows '3' opens, '0' clicks, '0' bounces
	When click event present in the system for '2' recipients to click '4' links
	And I request sent email distribution details
	Then email sent details analytics request shows '3' opens, '2' clicks, '0' bounces