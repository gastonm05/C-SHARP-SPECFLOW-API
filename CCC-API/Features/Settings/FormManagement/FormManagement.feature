Feature: FormManagement
	
@publishers @ignore
Scenario: Settings > Form Management > New Form > Activities > Create New Form Type
	Given shared session for 'system_admin' user with edition 'Publishers social company, custom fields'
	When I GET activities fields
	And I PUT New Form with color '#00B5FF', icon 'fa-folder-open-o'
	Then new form activity is created in Activities Forms (GET) with color and icon
	When I DELETE activity form
	Then activity form is not among Activities Forms (GET)

@publishers @ignore
Scenario: Settings > Form Management > Edit Activities Form
	Given shared session for 'system_admin' user with edition 'Publishers social company, custom fields'
	When I GET activities fields
	And I PUT New Form with color '#01090D', icon 'fa-bell'
	And I edit (PUT) activity form name, color '#2A2B2B', icon 'fa-exclamation'
	Then new form activity is created in Activities Forms (GET) with color and icon
	And I DELETE activity form

@publishers
Scenario: Settings > Form Management > New Form > Activities > Create New Form Type with fields
	Given shared session for 'system_admin' user with edition 'Publishers social company, custom fields'
	When I GET activities fields
	And I PUT New Form with color '#01090D', icon 'fa-exclamation', disabling:
	|Fields |
	|Link Influencer  |
	|Notes  |
	Then new form activity is created in Activities Forms (GET) with color, icon and fields
	When I DELETE activity form
	Then activity form is not among Activities Forms (GET)
