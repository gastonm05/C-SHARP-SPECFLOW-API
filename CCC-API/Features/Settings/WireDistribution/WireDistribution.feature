Feature: WireDistribution
	To verify that WireDistribution Settings can be retrieved and modified
	As a valid C3 user from a company with parameter PressReleaseImpactEnabled set to true
	I want to call the WireDisribution management endpoint - api/v1/management/wiredistributionaccount

@acl @WireDistribution @Ignore
Scenario: a Manager C3 user get current WireDistribution configuration for a Impact enabled company 
	Given I login as 'Press Release Impact Enabled Company - Manager'
	When I get current WireDistribution configuration 
	Then the response returns a valid WireDistribution configuration

@acl @WireDistribution @Ignore
Scenario: a AE C3 user get current WireDistribution configuration for a Impact enabled company 
	Given I login as 'Press Release Impact Enabled Company - AE'
	When I get current WireDistribution configuration 
	Then the response returns a valid WireDistribution configuration

@acl @WireDistribution @Ignore
Scenario: a Manager C3 user get current WireDistribution configuration for a Impact disabled company 
	Given I login as 'Smart Tag ON Company'
	When I get current WireDistribution configuration 
	Then the response returns a 'Access Denied' message

@acl @WireDistribution @Ignore
Scenario: a Standard C3 user get current WireDistribution configuration for a Impact enabled company 
	Given I login as 'Press Release Impact Enabled Company - Standard'
	When I get current WireDistribution configuration 
	Then the response returns a 'Access Denied' message

@acl @WireDistribution @Ignore
Scenario: a ReadOnly C3 user get current WireDistribution configuration for a Impact enabled company 
	Given I login as 'Press Release Impact Enabled Company - ReadOnly'
	When I get current WireDistribution configuration 
	Then the response returns a 'Access Denied' message
@acl @WireDistribution @Ignore
Scenario: a SysAdmin C3 user get current WireDistribution configuration for a Impact enabled company 
	Given I login as 'Press Release Impact Enabled Company - SysAdmin'
	When I get current WireDistribution configuration 
	Then the response returns a 'Access Denied' message

@acl @WireDistribution @NeedsCleanupWireDistribution @Ignore
Scenario: a Manager C3 user set multiple Oracle IDs, one for each DataGroup for his WireDistribution configuration for a Impact enabled company 
	Given I login as 'Press Release Impact Enabled Company - Manager'
	When I get current WireDistribution configuration 
	And  I set WireDistribution configuration with multiple Oracle IDs, one for each DataGroup
	Then I should see the proper WireDistribution response

@acl @WireDistribution @Ignore
Scenario: a Manager C3 user set only one Oracle ID for all DataGroup for his WireDistribution configuration for a Impact enabled company 
	Given I login as 'Press Release Impact Enabled Company - Manager'
	When I get current WireDistribution configuration
	And  I set WireDistribution configuration with multiple Oracle IDs, one for each DataGroup	
	And  I set WireDistribution configuration with only one Oracle ID for all DataGroup
	Then I should see the proper WireDistribution response

@acl @WireDistribution @Ignore
Scenario: Manager C3 user set only one Oracle ID for all DataGroup for his WireDistribution configuration for a Impact enabled company with View all for all Datagroup
	Given I login as 'Press Release Impact Enabled Company - Manager'
	When I get current WireDistribution configuration	
	And  I set WireDistribution configuration with only one Oracle ID for all DataGroup with View All for all data groups
	Then I should see the proper WireDistribution response

@acl @WireDistribution @NeedsCleanupWireDistribution @Ignore
Scenario: a Manager C3 user set Default(today) date as Impact Start Date for his WireDistribution configuration for a Impact enabled company 
	Given I login as 'Press Release Impact Enabled Company - Manager'
	When I get current WireDistribution configuration 
	And  I set WireDistribution configuration with only one Oracle ID for all DataGroup with View All for all data groups and 'DEFAULT' date as Impact Start Date	
	Then I should see the proper WireDistribution response for ImpactStartDateDefaultEnabled property which should be 'true'

@acl @WireDistribution @NeedsCleanupWireDistribution @Ignore 
Scenario: a Manager C3 user set 90 Days prior date as Impact Start Date for his WireDistribution configuration for a Impact enabled company 
	Given I login as 'Press Release Impact Enabled Company - Manager'
	When I get current WireDistribution configuration 
	And  I set WireDistribution configuration with only one Oracle ID for all DataGroup with View All for all data groups and 'NINETYDAYS' date as Impact Start Date	
	Then I should see the proper WireDistribution response for ImpactStartDateDefaultEnabled property which should be 'false'

@acl @WireDistribution @Ignore
Scenario: a Manager C3 user with WireDistribution configuration set to no Company Wire Account ID and has a Impact enabled DG set to use Data Group ID should have OMC SSO if that DG is selected.
	Given I login as 'Impact Enabled Company with No Company id or DataGroup id set - Manager - DGLevelAccountID DG Selected'
	When I perform a GET to verify the token
	Then the token should be valid and it shouldn't return an empty AccountID

@acl @WireDistribution @Ignore
Scenario: a Manager C3 user with WireDistribution configuration set to no Company Wire Account ID and has a Impact enabled DG set to use Data Group ID should NOT have OMC SSO if that DG is not selected.
	Given I login as 'Impact Enabled Company with No Company id or DataGroup id set - Manager'
	When I perform a GET to verify the token
	Then the token should be valid and return an empty AccountID
