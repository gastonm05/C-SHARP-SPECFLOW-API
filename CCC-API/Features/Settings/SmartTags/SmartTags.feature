Feature: Settings - SmartTags Config Endpoint
	To verify that SmartTags Settings can be retrieved and modified
	As a valid CCC user from a company with parameter Elysium-CustomFields-Enabled set to true
	I want to call the SmartTags Config endpoint - news/smartTags/config

@acl @Settings @SmartTagsConfig @Ignore
Scenario: A Manager User should be allow to view Smart Tags config values in Settings Area for a company that has Automatic News Typing (Smart Tags) enabled
	Given I login as 'Smart Tag ON Company'
	When I perform a GET on SmartTags Config endpoint endpoint
	Then SmartTags Config GET endpoint response code should be 200

@acl @Settings @SmartTagsConfig @Ignore
Scenario: A Manager User should be allow to edit Smart Tags with valid config values in Settings Area for a company that has Automatic News Typing (Smart Tags) enabled
	Given I login as 'Smart Tag ON Company'
	When I perform a PUT on SmartTags Config endpoint with these config values 1 and 1 and 1 and 1 and test 
	Then SmartTags Config PUT endpoint response code should be 200 and message should be OK

@acl @Settings @SmartTagsConfig @Ignore
Scenario Outline: A Manager User should NOT be allow to edit Smart Tags with invalid config values in Settings Area for a company that has Automatic News Typing (Smart Tags) enabled
	Given I login as 'Smart Tag ON Company'
	When I perform a PUT on SmartTags Config endpoint with these config values <FeatureMentions> and <FeatureWords> and <BriefMentions> and <BriefWords> and <SearchTerm>
	Then SmartTags Config PUT endpoint response code should be 400 and message should be <Message>
Examples: 
	| FeatureMentions | FeatureWords | BriefMentions | BriefWords | SearchTerm                            | Message                                                                           |
	| 1               | 1            | 1             | 1          |                                       | Search clause cannot be blank.                                                    |
	| 0               | 0            | 0             | 0          | test                                  | All number inputs must be greater than 0.                                         |
	| 1               | 1            | 1             | 1          | (test                                 | Search is missing an opening or closing parenthesis                               |
	| 1               | 1            | 1             | 1          | \"disney\"{                           | Search contains one or more invalid characters.                                   |
	| 1               | 1            | 1             | 1          | \"disney\" AND                        | Search cannot end with boolean AND/OR operators                                   |
	| 1               | 1            | 1             | 1          | \"disney\" OR                         | Search cannot end with boolean AND/OR operators                                   |
	| 1               | 1            | 1             | 1          | AND \"disney\"                        | Search cannot start with boolean AND/OR operators.                                |
	| 1               | 1            | 1             | 1          | OR \"disney\"                         | Search cannot start with boolean AND/OR operators.                                |
	| 1               | 1            | 1             | 1          | \"disney                              | Search is missing an opening or closing quotation mark                            |
	| 1               | 1            | 1             | 1          | \"disney world\" [headline]           | Search cannot end with a query modifier.                                          |
	| 1               | 1            | 1             | 1          | \"disney world\" (OR \"mouse\")       | Search contains a syntax error.                                                   |
	| 1               | 1            | 1             | 1          | \"disney world\" (\"mouse\" AND)      | Search contains a boolean AND/OR operator that is not followed by a search term.  |
	| 1               | 1            | 1             | 1          | \"disney world\" OR AND NOT \"mouse\" | Search contains a boolean AND/OR operator that is not followed by a search term   |
	| 1               | 1            | 1             | 1          | \"disney world\" ( AND \"mouse\")     | Search contains a boolean AND/OR operator that is not preceded by a search term." |
	| 1               | 1            | 1             | 1          | \"disney world\" AND NOT OR \"mouse\" | Search contains a boolean AND/OR operator that is not preceded by a search term.  |