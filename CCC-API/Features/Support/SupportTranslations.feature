Feature: SupportTranslations

@acl 
Scenario: Get client translations
	Given I get 'client' translations for '1.1.1106' release
	Then the value 'QA Client Release test' is present in translations
	And the value 'QA Client Release test German' is present in translations
	And the value 'QA Client Release test French' is present in translations

Scenario: Get server translations
	Given I get 'server' translations for '1.1.1104' release
	Then the value 'QA Client Release test' is present in translations
	And the value 'QA Client Release test German' is present in translations
	And the value 'QA Client Release test French' is present in translations