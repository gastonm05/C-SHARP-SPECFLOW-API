Feature: Language
	In order to see all available languages
	I want an endpoint to return available languages

@configuration
Scenario Outline: Languages endpoint returns correct languages
	When I perform a GET for all languages
	Then the languages endpoint returns 16 languages
	And the languages response contains the language '<lang>' with an id of '<id>' a code of '<code>' and a status of '<status>'
Examples: 
	| lang               | id   | code  | status |
	| German             | 1031 | de-de | True   |
	| English            | 1033 | en-us | True   |
	| Spanish            | 1034 | es-es | False  |
	| Finnish            | 1035 | fi-fi | False  |
	| French             | 1036 | fr-fr | True   |
	| Italian            | 1040 | it-it | False  |
	| Dutch              | 1043 | nl-nl | True   |
	| Norwegian (Bokmål) | 1044 | no-no | False  |
	| Portuguese         | 1046 | pt-br | False  |
	| Swedish            | 1053 | sv-se | False  |
	| Chinese            | 2052 | zh-cn | False  |
	| English            | 2057 | en-gb | True   |
	| Spanish            | 2058 | es-mx | False  |
	| French             | 3084 | fr-ca | True   |
	| English            | 4105 | en-ca | True   |