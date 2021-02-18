Feature: Settings - ACLS Endpoint
	To verify that a list of permissions retrieved
	As a valid CCC user
	I want to call the Settings ACLS endpoint with all possible type of users.

@acl
Scenario: Validate successful endpoint response for a Standard User
	Given I login as 'API Standard User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
	                   | property                                | subProperty |subPropertyOther| permission           | value |
					   | Access									 |	           |				| IsGranted		       | True  |
					   | AllUsers								 | Access      |				| IsGranted		       | False |
                       | AllUsers								 | Ops	       |				| CanEditExpirationDate| False |
					   | AllUsers								 | Ops	       |				| CanEditPermissions   | False |
					   | AllUsers							     | Ops	       |				| CanCreate			   | False |
					   | AllUsers                                | Ops	       |				| CanEdit			   | False |
					   | AllUsers                                | Ops	       |				| CanDelete			   | False |
                       | MyUser                                  | Access      |				| IsGranted			   | True  |
					   | MyUser	                                 | Ops	       |				| CanEditExpirationDate| True  |
					   | MyUser                                  | Ops	       |				| CanEditPermissions   | True  |
					   | MyUser								     | Ops	       |				| CanCreate			   | False |
					   | MyUser                                  | Ops	       |				| CanEdit			   | True  | 
					   | MyUser                                  | Ops	       |				| CanDelete			   | False |
					   | NewsAlertManagement                     | Access      |				| IsGranted		       | True  |
					   | MediaMonitoringSearchesManagement       | Access      |				| IsGranted		       | False |
					   | AnalyticsProfileManagement              | Access      |			    | IsGranted		       | False |
					   | AnalyticsProfileManagement              | Ops	       |				| CanCreate            | False |
					   | AnalyticsProfileManagement              | Ops	       |				| CanEdit         	   | False |
					   | AnalyticsProfileManagement              | Ops	       |				| CanDelete			   | False |
					   | OMCAccountId					         | Access      |				| IsGranted		       | False |
					   | SocialMediaManagement		             | Access      |				| IsGranted		       | False |
					   | SocialMediaManagement                   | Ops	       |				| CanCreate            | False |
					   | SocialMediaManagement                   | Ops	       |				| CanEdit         	   | False |
					   | SocialMediaManagement                   | Ops	       |				|CanDelete			   | False |
					   | Labs							         | Access      |				|IsGranted		       | False |
					   
@acl
Scenario: Validate successful endpoint response for a Read-only User
	Given I login as 'API Read-Only User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
	                   | property                                | subProperty |subPropertyOther| permission           | value |
					   | Access									 |	           |				|IsGranted		       | True  |						
                       | AllUsers								 | Access      |				|IsGranted		       | False |
                       | AllUsers								 | Ops	       |				|CanEditExpirationDate | False |
					   | AllUsers								 | Ops	       |				|CanEditPermissions	   | False |
					   | AllUsers							     | Ops	       |				| CanCreate			   | False |
					   | AllUsers                                | Ops	       |				|CanEdit			   | False |
					   | AllUsers                                | Ops	       |				| CanDelete			   | False |
                       | MyUser                                  | Access      |				| IsGranted		       | True  |
					   | MyUser	                                 | Ops	       |				|CanEditExpirationDate | True  |
					   | MyUser                                  | Ops	       |				|CanEditPermissions	   | True  |
					   | MyUser								     | Ops	       |				|CanCreate			   | False |
					   | MyUser                                  | Ops	       |				|CanEdit			   | True  | 
					   | MyUser                                  | Ops	       |				|CanDelete			   | False |
					   | NewsAlertManagement                     | Access      |				|IsGranted		       | False  |
					   | MediaMonitoringSearchesManagement       | Access      |				|IsGranted		       | False |
					   | AnalyticsProfileManagement              | Access      |				|IsGranted		       | False |
					   | AnalyticsProfileManagement              | Ops	       |				|CanCreate             | False |
					   | AnalyticsProfileManagement              | Ops	       |				|CanEdit         	   | False |
					   | AnalyticsProfileManagement              | Ops	       |				|CanDelete			   | False |
					   | OMCAccountId					         | Access      |				|IsGranted		       | False |
					   | SocialMediaManagement		             | Access      |				|IsGranted		       | False |
					   | SocialMediaManagement                   | Ops	       |				|CanCreate             | False |
					   | SocialMediaManagement                   | Ops	       |				|CanEdit         	   | False |
					   | SocialMediaManagement                   | Ops	       |				|CanDelete			   | False |
					   | Labs							         | Access      |				|IsGranted		       | False |
@acl                       
Scenario: Validate successful endpoint response for a Manager User
	Given I login as 'API Manager User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
	                   | property                                | subProperty |subPropertyOther| permission           | value |
					   | Access									 |	           |				| IsGranted		       | True  |
                       | AllUsers								 | Access      |				| IsGranted		       | True  |
                       | AllUsers								 | Ops	       |				| CanEditExpirationDate| True  |
					   | AllUsers								 | Ops	       |				|CanEditPermissions	   | True  |
					   | AllUsers							     | Ops	       |                |CanCreate			   | True  |
					   | AllUsers                                | Ops	       |				|CanEdit			   | True  |
					   | AllUsers                                | Ops	       |				|CanDelete			   | False |
                       | MyUser                                  | Access      |				| IsGranted		       | True  |
					   | MyUser	                                 | Ops	       |				| CanEditExpirationDate| True  |
					   | MyUser                                  | Ops	       |				| CanEditPermissions   | True  |
					   | MyUser								     | Ops	       |				| CanCreate			   | False |
					   | MyUser                                  | Ops	       |				| CanEdit			   | True  |
					   | MyUser                                  | Ops	       |				| CanDelete			   | False |
					   | NewsAlertManagement                     | Access      |				|IsGranted		       | True  |
					   | MediaMonitoringSearchesManagement       | Access      |			    |IsGranted		       | True  |
					   | AnalyticsProfileManagement              | Access      |				| IsGranted		       | True  |
					   | AnalyticsProfileManagement              | Ops	       |				|CanCreate             | False |
					   | AnalyticsProfileManagement              | Ops	       |				|CanEdit         	   | True  |
					   | AnalyticsProfileManagement              | Ops	       |				|CanDelete			   | False |
					   | OMCAccountId					         | Access      |				|IsGranted		       | True  |
					   | SocialMediaManagement		             | Access      |				|IsGranted		       | True  |
					   | SocialMediaManagement                   | Ops	       |				|CanCreate             | True  |
					   | SocialMediaManagement                   | Ops	       |				| CanEdit         	   | True  |
					   | SocialMediaManagement                   | Ops	       |				| CanDelete			   | True  |
					   | Labs							         | Access      |				|IsGranted		       | False |

@acl @Ignore                       
Scenario: Validate successful endpoint response for a Manager User for Distribution ACLS permissions and Settings ACLS permissions for a OMC Enabled OMC company
	Given I login as 'OMC Enabled Company'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Distribution should be:
	                   | property | subProperty | subPropertyOther | permission | value |
	                   |          |             |                  | HasOMC 	| False |
	And ACLS permissions for Settings should be:
			           | property     | subProperty | subPropertyOther | permission | value           |
					   | OMCAccountId | Access      |                  | IsGranted  | True            |
					   | OMCAccountId | Access      |                  | StatusCode | 0				  |
					   | OMCAccountId | Access      |                  | Status     | Access Granted  |
@acl @Ignore                      
Scenario: Validate successful endpoint response for a Manager User for Distribution ACLS permissions and Settings ACLS permissions for a OMC Disabled OMC company
	Given I login as 'OMC Disabled Company'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Distribution should be:
	                   | property | subProperty | subPropertyOther | permission | value  |
					   |          |             |                  | HasOMC     | False  |
					   And ACLS permissions for Settings should be:
	                   | property     | subProperty | subPropertyOther | permission | value				 |
					   | OMCAccountId | Access      |                  | IsGranted  | False				 |
					   | OMCAccountId | Access      |                  | StatusCode | 1				     |
	                   | OMCAccountId | Access      |                  |Status      | Permission Denied  |

@acl @OMC @Ignore
Scenario: Validate successful endpoint response for a Manager User for Distribution ACLS permissions  for a OMC Enabled OMC company with a disabled OMC Datagroup
	Given I login as 'Impact Enabled Company with No Company id or DataGroup id set - Manager'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Distribution should be:
	                   | property | subProperty | subPropertyOther | permission | value  |
					   |          |             |                  | HasOMC     | False  |				

@acl                       
Scenario: Validate successful endpoint response for a Manager User for News ACLS permissions
	Given I login as 'API Manager User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for News should be:
	                   | property  | subProperty | subPropertyOther | permission        | value |
					   | Analytics |             |                  | HasScoring        | True  |
	                   | Analytics |             |                  | HasToning         | True  |
	                   | Analytics |             |                  | HasToningOverride | False |
	                   | Analytics |             |                  | HasScoring 	    | True  |
@acl                       
Scenario: Validate a Manager User from a Visible ON company has proper set of permissions to activate "Create New Searches" feature
	Given I login as 'Visible ON Company'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
	                | property                          | subProperty | subPropertyOther | permission            | value |
					| Access                            |             |                  | IsGranted             | True  |
					| AllUsers                          | Access      |                  | IsGranted             | True  |
					| AllUsers                          | Ops         |                  | CanEditExpirationDate | True  |
					| AllUsers                          | Ops         |                  | CanEditPermissions    | True  |
					| AllUsers                          | Ops         |                  | CanCreate             | True  |
					| AllUsers                          | Ops         |                  | CanEdit               | True  |
					| AllUsers                          | Ops         |                  | CanDelete             | False |
					| MyUser                            | Access      |                  | IsGranted             | True  |
					| MyUser                            | Ops         |                  | CanEditExpirationDate | True  |
					| MyUser                            | Ops         |                  | CanEditPermissions    | True  |
					| MyUser                            | Ops         |                  | CanCreate             | False |
					| MyUser                            | Ops         |                  | CanEdit               | True  |
					| MyUser                            | Ops         |                  | CanDelete             | False |
					| MediaMonitoringSearchesManagement | Access      |                  | IsGranted             | True  |
					| MediaMonitoringSearchesManagement | Ops         |                  | CanCreate		     | True  |
					   
@acl @Ignore
Scenario: a Manager User should NOT be allowed to see 'create new dashboard' button with a company has HideAnalyticsNewDashboardButton = True
	Given I login as 'SMB Package Company'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Analytics should be:
					   | property         | subProperty | subPropertyOther | permission | value               |
					   | Dashboards       | Access      |				   | IsGranted  | True                |
					   | Dashboards       | Access      |				   | Status     | Access Granted      |
					   | Dashboards       | Access      |				   | StatusCode | 0                   |
					   | Dashboards       | Ops         |                  | CanEdit    | True				  |
					   | Dashboards       | Ops         |                  | CanCreate  | False               |
					   | Dashboards       | Ops         |                  | CanDelete  | False               |

@acl @Ignore
Scenario: a Manager User should NOT be allowed to see the 'doc' option for the analytics dashboard report (only PDF is allowed) with a company has AnalysisDocumentDownloadFormatDisabled = True
	Given I login as 'SMB Package Company'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Analytics should be:
					   | property	   | subProperty | subPropertyOther | permission						      | value               |
					   |               |		     |				    | AnalysisDocumentDownloadFormatDisabled  | True                |

@acl @Ignore
Scenario: a Manager User should NOT be allowed to see 'All news search' button with a company has HideNewsArchive = True
	Given I login as 'SMB Package Company'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for News should be:
					   | property	   | subProperty | subPropertyOther | permission						      | value               |
					   | Archive       |		     |				    | CanView                                 | False               |
					   | Archive       |		     |				    | CanExportToNews                         | False               |
					   | Archive       |		     |				    | CanPreview                              | True                |

@acl	 @Ignore
Scenario: a Manager User should NOT be allowed to see 'Generate Report' button in email distributions when a company has GenerateReportEnabled = False
	Given I login as 'SMB Package Company'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Distribution should be:
					   | property	   | subProperty | subPropertyOther | permission						      | value               |
					   | Email		   |		     |				    | CanGenerateReport						  | False               |

@acl @Ignore
Scenario: a Manager User should be allowed to see Smart Tags page in Settings Area for a company has Automatic News Typing (Smart Tags) enabled.
	Given I login as 'Smart Tag ON Company'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property         | subProperty | subPropertyOther | permission | value               |
					   | SmartTags        | Access      |				   | IsGranted  | True                |
					   | SmartTags        | Access      |				   | Status     | Access Granted      |
					   | SmartTags        | Access      |				   | StatusCode | 0                   |

@acl @Ignore
Scenario: a Sysadmin User should be allowed to see Smart Tags page in Settings Area for a company has Automatic News Typing (Smart Tags) enabled.
	Given I login as 'Smart Tag ON Company - SysAdmin User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property         | subProperty | subPropertyOther | permission | value               |
					   | SmartTags        | Access      |				   | IsGranted  | True                |
					   | SmartTags        | Access      |				   | Status     | Access Granted      |
					   | SmartTags        | Access      |				   | StatusCode | 0                   |

@acl @Ignore
Scenario: a AE Manager User should be allowed to see Smart Tags page in Settings Area for a company has Automatic News Typing (Smart Tags) enabled.
	Given I login as 'Smart Tag ON Company - AE Manager User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property         | subProperty | subPropertyOther | permission | value               |
					   | SmartTags        | Access      |				   | IsGranted  | True                |
					   | SmartTags        | Access      |				   | Status     | Access Granted      |
					   | SmartTags        | Access      |				   | StatusCode | 0                   |

@acl
Scenario: a Manager User should NOT be allowed to see Smart Tags page in Settings Area for a company has NOT Automatic News Typing (Smart Tags) enabled.
	Given I login as 'SMB Package Company'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property         | subProperty | subPropertyOther | permission | value               |
					   | SmartTags        | Access      |				   | IsGranted  | False               |
					   | SmartTags        | Access      |				   | Status     | Permission Denied   |
					   | SmartTags        | Access      |				   | StatusCode | 1                   |					   

@acl @Ignore
Scenario: a Standard User should NOT be allowed to see Smart Tags page in Settings Area for a company that has Automatic News Typing (Smart Tags) enabled.
	Given I login as 'Smart Tag ON Company - Standard User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property         | subProperty | subPropertyOther | permission | value               |
					   | SmartTags        | Access      |				   | IsGranted  | False               |
					   | SmartTags        | Access      |				   | Status     | Permission Denied   |
					   | SmartTags        | Access      |				   | StatusCode | 1                   |

@acl @Ignore
Scenario: a Read-Only User should NOT be allowed to see Smart Tags page in Settings Area for a company that has Automatic News Typing (Smart Tags) enabled.
	Given I login as 'Smart Tag ON Company - Read-Only User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property         | subProperty | subPropertyOther | permission | value               |
					   | SmartTags        | Access      |				   | IsGranted  | False               |
					   | SmartTags        | Access      |				   | Status     | Permission Denied   |
					   | SmartTags        | Access      |				   | StatusCode | 1                   |

@acl @Ignore
Scenario: a Manager User should be allowed to see Advance Password Reset Page in Login Area for a company has Advance Password feature enabled.
	Given I login as 'Advance Password enabled Company - Sysadmin User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property            | subProperty | subPropertyOther | permission | value          |
					   | HasAdvancedSecurity | Access      |                  | IsGranted  | True           |
					   | HasAdvancedSecurity | Access      |                  | Status     | Access Granted |
					   | HasAdvancedSecurity | Access      |                  | StatusCode | 0              |
@acl @Ignore
Scenario: a Standard User should be allowed to see Advance Password Reset Page in Login Area for a company has Advance Password feature enabled.
	Given I login as 'Advance Password enabled Company - Standard User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property            | subProperty | subPropertyOther | permission | value          |
					   | HasAdvancedSecurity | Access      |                  | IsGranted  | True           |
					   | HasAdvancedSecurity | Access      |                  | Status     | Access Granted |
					   | HasAdvancedSecurity | Access      |                  | StatusCode | 0              |
@acl @Ignore
Scenario: a ReadOnly User should be allowed to see Advance Password Reset Page in Login Area for a company has Advance Password feature enabled.
	Given I login as 'Advance Password enabled Company - ReadOnly User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property            | subProperty | subPropertyOther | permission | value          |
					   | HasAdvancedSecurity | Access      |                  | IsGranted  | True           |
					   | HasAdvancedSecurity | Access      |                  | Status     | Access Granted |
					   | HasAdvancedSecurity | Access      |                  | StatusCode | 0              |
@acl @Ignore
Scenario: a Sysadmin User should NOT be allowed to see Advance Password Reset Page in Login Area for a company has Advance Password feature disabled.
	Given I login as 'Smart Tag ON Company - SysAdmin User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property            | subProperty | subPropertyOther | permission | value               |
					   | HasAdvancedSecurity | Access      |                  | IsGranted  | False               |
					   | HasAdvancedSecurity | Access      |                  | Status     | Feature Not Enabled |
					   | HasAdvancedSecurity | Access      |                  | StatusCode | 2                   |

@acl @Impact @Ignore
Scenario: a Manager User should NOT be allowed to see Wire Distribution Page in Settings Area for a company has PressReleaseImpactEnabled feature disabled.
	Given I login as 'Smart Tag ON Company'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property           | subProperty | subPropertyOther | permission | value               |
					   | PressReleaseImpact | Access      |                  | IsGranted  | False               |
					   | PressReleaseImpact | Access      |                  | Status     | Feature Not Enabled |
					   | PressReleaseImpact | Access      |                  | StatusCode | 2                   |
					   | PressReleaseImpact | Ops         |                  | CanView    | False               |
					   | PressReleaseImpact | Ops         |                  | CanCreate  | False               |
					   | PressReleaseImpact | Ops         |                  | CanEdit    | False               |
					   | PressReleaseImpact | Ops         |                  | CanDelete  | False               |
@acl @Impact @Ignore
Scenario: a SysAdmin User should NOT be allowed to see Wire Distribution Page in Settings Area for a company has PressReleaseImpactEnabled feature enabled.
	Given I login as 'Press Release Impact Enabled Company - Sysadmin'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property           | subProperty | subPropertyOther | permission | value               |
					   | PressReleaseImpact | Access      |                  | IsGranted  | False               |
					   | PressReleaseImpact | Access      |                  | Status     | Feature Not Enabled |
					   | PressReleaseImpact | Access      |                  | StatusCode | 2                   |
					   | PressReleaseImpact | Ops         |                  | CanView    | False               |
					   | PressReleaseImpact | Ops         |                  | CanCreate  | False               |
					   | PressReleaseImpact | Ops         |                  | CanEdit    | False               |
					   | PressReleaseImpact | Ops         |                  | CanDelete  | False               |
@acl @Impact @Ignore
Scenario: a Standard User should NOT be allowed to see Wire Distribution Page in Settings Area for a company has PressReleaseImpactEnabled feature enabled.
	Given I login as 'Press Release Impact Enabled Company - Standard'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property           | subProperty | subPropertyOther | permission | value               |
					   | PressReleaseImpact | Access      |                  | IsGranted  | False               |
					   | PressReleaseImpact | Access      |                  | Status     | Feature Not Enabled |
					   | PressReleaseImpact | Access      |                  | StatusCode | 2                   |
					   | PressReleaseImpact | Ops         |                  | CanView    | False               |
					   | PressReleaseImpact | Ops         |                  | CanCreate  | False               |
					   | PressReleaseImpact | Ops         |                  | CanEdit    | False               |
					   | PressReleaseImpact | Ops         |                  | CanDelete  | False               |

@acl @Impact @Ignore
Scenario: a ReadOnly User should NOT be allowed to see Wire Distribution Page in Settings Area for a company has PressReleaseImpactEnabled feature enabled.
	Given I login as 'Press Release Impact Enabled Company - ReadOnly'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property           | subProperty | subPropertyOther | permission | value               |
					   | PressReleaseImpact | Access      |                  | IsGranted  | False               |
					   | PressReleaseImpact | Access      |                  | Status     | Feature Not Enabled |
					   | PressReleaseImpact | Access      |                  | StatusCode | 2                   |
					   | PressReleaseImpact | Ops         |                  | CanView    | False               |
					   | PressReleaseImpact | Ops         |                  | CanCreate  | False               |
					   | PressReleaseImpact | Ops         |                  | CanEdit    | False               |
					   | PressReleaseImpact | Ops         |                  | CanDelete  | False               |
@acl @Impact @Ignore
Scenario: a Manager User should be allowed to see Wire Distribution Page in Settings Area for a company has PressReleaseImpactEnabled feature enabled.
	Given I login as 'Press Release Impact Enabled Company - Manager'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property           | subProperty | subPropertyOther | permission | value          |
					   | PressReleaseImpact | Access      |                  | IsGranted  | True           |
					   | PressReleaseImpact | Access      |                  | Status     | Access Granted |
					   | PressReleaseImpact | Access      |                  | StatusCode | 0              |
					   | PressReleaseImpact | Ops         |                  | CanView    | True           |
					   | PressReleaseImpact | Ops         |                  | CanCreate  | True           |
					   | PressReleaseImpact | Ops         |                  | CanEdit    | True           |
					   | PressReleaseImpact | Ops         |                  | CanDelete  | True           |
@acl @Impact @Ignore
Scenario: a AE User should be allowed to see Wire Distribution Page in Settings Area for a company has PressReleaseImpactEnabled feature enabled.
	Given I login as 'Press Release Impact Enabled Company - AE'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property           | subProperty | subPropertyOther | permission | value          |
					   | PressReleaseImpact | Access      |                  | IsGranted  | True           |
					   | PressReleaseImpact | Access      |                  | Status     | Access Granted |
					   | PressReleaseImpact | Access      |                  | StatusCode | 0              |
					   | PressReleaseImpact | Ops         |                  | CanView    | True           |
					   | PressReleaseImpact | Ops         |                  | CanCreate  | True           |
					   | PressReleaseImpact | Ops         |                  | CanEdit    | True           |
					   | PressReleaseImpact | Ops         |                  | CanDelete  | True           |

@acl @Impact @Ignore
Scenario: a Manager User should be allowed to see Impact Tab in nav bar Area for a company has PressReleaseImpactEnabled feature enabled and DataGroup set to use Company Wire Distribution Account ID
	Given I login as 'Impact Tab Enabled per DataGroup Company - (Default)'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Impact should be:
					   | property | subProperty | subPropertyOther | permission | value          |
					   | Access   |             |                  | IsGranted  | True           |
					   | Access   |             |                  | Status     | Access Granted |
					   | Access   |             |                  | StatusCode | 0              |
@acl @Impact @Ignore
Scenario: a Manager User should be allowed to see Impact Tab in nav bar Area for a company has PressReleaseImpactEnabled feature enabled and DataGroup set to use it's own Wire Distribution Account ID
	Given I login as 'Impact Tab Enabled per DataGroup Company - ImpactEnabledDataGroup'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Impact should be:
					   | property | subProperty | subPropertyOther | permission | value          |
					   | Access   |             |                  | IsGranted  | True           |
					   | Access   |             |                  | Status     | Access Granted |
					   | Access   |             |                  | StatusCode | 0              |
@acl @Impact @Ignore
Scenario: a Manager User should NOT be allowed to see Impact Tab in nav bar Area for a company has PressReleaseImpactEnabled feature enabled but DataGroup set as disabled for Wire Distribution.
	Given I login as 'Impact Tab Enabled per DataGroup Company - DGDisabledforImpact'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Impact should be:
					   | property | subProperty | subPropertyOther | permission | value             |
					   | Access   |             |                  | IsGranted  | False             |
					   | Access   |             |                  | Status     | Permission Denied |
					   | Access   |             |                  | StatusCode | 1                 |

@acl @FTP @Ignore
Scenario: a Manager User should be allowed to see Automated News Output Page in Settings Area for a company has News-FtpExport-Enabled feature enabled.
	Given I login as 'FTP Export Enabled Company - Manager'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property  | subProperty | subPropertyOther | permission | value          |
					   | FtpExport | Access      |                  | IsGranted  | True           |
					   | FtpExport | Access      |                  | Status     | Access Granted |
					   | FtpExport | Access      |                  | StatusCode | 0              |
					   | FtpExport | Ops         |                  | CanView    | True           |
					   | FtpExport | Ops         |                  | CanCreate  | False          |
					   | FtpExport | Ops         |                  | CanEdit    | True           |
					   | FtpExport | Ops         |                  | CanDelete  | False          |

@acl @FTP @Ignore
Scenario: a System Admin User should be allowed to see Automated News Output Page in Settings Area for a company has News-FtpExport-Enabled feature enabled.
	Given I login as 'FTP Export Enabled Company - SysAdmin User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property  | subProperty | subPropertyOther | permission | value          |
					   | FtpExport | Access      |                  | IsGranted  | True           |
					   | FtpExport | Access      |                  | Status     | Access Granted |
					   | FtpExport | Access      |                  | StatusCode | 0              |
					   | FtpExport | Ops         |                  | CanView    | True           |
					   | FtpExport | Ops         |                  | CanCreate  | False          |
					   | FtpExport | Ops         |                  | CanEdit    | True           |
					   | FtpExport | Ops         |                  | CanDelete  | False          |

@acl @FTP @Ignore
Scenario: a AE User should be allowed to see Automated News Output Page in Settings Area for a company has News-FtpExport-Enabled feature enabled.
	Given I login as 'FTP Export Enabled Company - AE'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property  | subProperty | subPropertyOther | permission | value          |
					   | FtpExport | Access      |                  | IsGranted  | True           |
					   | FtpExport | Access      |                  | Status     | Access Granted |
					   | FtpExport | Access      |                  | StatusCode | 0              |
					   | FtpExport | Ops         |                  | CanView    | True           |
					   | FtpExport | Ops         |                  | CanCreate  | False          |
					   | FtpExport | Ops         |                  | CanEdit    | True           |
					   | FtpExport | Ops         |                  | CanDelete  | False          |
					    
@acl @FTP @Ignore
Scenario: a Manager User should NOT be allowed to see Automated News Output Page in Settings Area for a company has News-FtpExport-Enabled feature disabled.
	Given I login as 'Press Release Impact Enabled Company - Manager'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property  | subProperty | subPropertyOther | permission | value               |
					   | FtpExport | Access      |                  | IsGranted  | False               |
					   | FtpExport | Access      |                  | Status     | Feature Not Enabled |
					   | FtpExport | Access      |                  | StatusCode | 2                   |
					   | FtpExport | Ops         |                  | CanView    | False               |
					   | FtpExport | Ops         |                  | CanCreate  | False               |
					   | FtpExport | Ops         |                  | CanEdit    | False               |
					   | FtpExport | Ops         |                  | CanDelete  | False               |

@acl @FTP @Ignore
Scenario: a Standard User should NOT be allowed to see Automated News Output Page in Settings Area for a company has News-FtpExport-Enabled feature disabled.
	Given I login as 'FTP Export Enabled Company - Standard User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property  | subProperty | subPropertyOther | permission | value               |
					   | FtpExport | Access      |                  | IsGranted  | False               |
					   | FtpExport | Access      |                  | Status     | Feature Not Enabled |
					   | FtpExport | Access      |                  | StatusCode | 2                   |
					   | FtpExport | Ops         |                  | CanView    | False               |
					   | FtpExport | Ops         |                  | CanCreate  | False               |
					   | FtpExport | Ops         |                  | CanEdit    | False               |
					   | FtpExport | Ops         |                  | CanDelete  | False               |

@acl @FTP
Scenario: a Read Only User should NOT be allowed to see Automated News Output Page in Settings Area for a company has News-FtpExport-Enabled feature disabled.
	Given I login as 'FTP Export Enabled Company - Readonly User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property  | subProperty | subPropertyOther | permission | value               |
					   | FtpExport | Access      |                  | IsGranted  | False               |
					   | FtpExport | Access      |                  | Status     | Feature Not Enabled |
					   | FtpExport | Access      |                  | StatusCode | 2                   |
					   | FtpExport | Ops         |                  | CanView    | False               |
					   | FtpExport | Ops         |                  | CanCreate  | False               |
					   | FtpExport | Ops         |                  | CanEdit    | False               |
					   | FtpExport | Ops         |                  | CanDelete  | False               |

@acl 
Scenario: a Manager can edit any user PRNewswire User ID when the company does not have Wire Distribution Account ID.
	Given I login as 'PR News Wire Enabled - Without Wire Distribution Account ID - Manager'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property     | subProperty | subPropertyOther | permission | value          |
					   | OMCAccountId | Access      |                  | IsGranted  | True           |
					   | OMCAccountId | Access      |                  | Status     | Access Granted |
					   | OMCAccountId | Access      |                  | StatusCode | 0              |

@acl
Scenario: a Standard User cannot edit PRNewswire User ID when the company does not have Wire Distribution Account ID.
	Given I login as 'PR News Wire Enabled - Without Wire Distribution Account ID - Standard User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property     | subProperty | subPropertyOther | permission | value             |
					   | OMCAccountId | Access      |                  | IsGranted  | False             |
					   | OMCAccountId | Access      |                  | Status     | Permission Denied |
					   | OMCAccountId | Access      |                  | StatusCode | 1                 |

@acl
Scenario: a Read Only user cannot edit PRNewswire User ID when the company does not have Wire Distribution Account ID.
	Given I login as 'PR News Wire Enabled - Without Wire Distribution Account ID - Read Only'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property     | subProperty | subPropertyOther | permission | value             |
					   | OMCAccountId | Access      |                  | IsGranted  | False             |
					   | OMCAccountId | Access      |                  | Status     | Permission Denied |
					   | OMCAccountId | Access      |                  | StatusCode | 1                 |

@acl
Scenario: a AE user cannot edit PRNewswire User ID when the company does not have Wire Distribution Account ID.
	Given I login as 'PR News Wire Enabled - Without Wire Distribution Account ID - AE'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property     | subProperty | subPropertyOther | permission | value             |
					   | OMCAccountId | Access      |                  | IsGranted  | False             |
					   | OMCAccountId | Access      |                  | Status     | Permission Denied |
					   | OMCAccountId | Access      |                  | StatusCode | 1                 |

@acl @Impact @EarnedMedia
Scenario: a Manager User should be allow to see Earned Media Searches in Keyword Searches page from Settings Area
	Given I login as 'Earned Media Enabled Company - Manager'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property          | subProperty | subPropertyOther | permission | value          |
					   | EarnedMediaImpact | Access      |                  | IsGranted  | True           |
					   | EarnedMediaImpact | Access      |                  | Status     | Access Granted |
					   | EarnedMediaImpact | Access      |                  | StatusCode | 0              |

@acl @Impact @EarnedMedia
Scenario: a sysadmin User should be allow to see Earned Media Searches in Keyword Searches page from Settings Area
	Given I login as 'Earned Media Enabled Company - Sysadmin'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property          | subProperty | subPropertyOther | permission | value          |
					   | EarnedMediaImpact | Access      |                  | IsGranted  | True           |
					   | EarnedMediaImpact | Access      |                  | Status     | Access Granted |
					   | EarnedMediaImpact | Access      |                  | StatusCode | 0              |

@acl @Impact @EarnedMedia
Scenario: a standard User should NOT be allow to see Earned Media Searches in Keyword Searches page from Settings Area
	Given I login as 'Earned Media Enabled Company - Standard'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property          | subProperty | subPropertyOther | permission | value             |
					   | EarnedMediaImpact | Access      |                  | IsGranted  | False             |
					   | EarnedMediaImpact | Access      |                  | Status     | Permission Denied |
					   | EarnedMediaImpact | Access      |                  | StatusCode | 1                 |

@acl @Impact @EarnedMedia
Scenario: a readonly User should NOT be allow to see Earned Media Searches in Keyword Searches page from Settings Area
	Given I login as 'Earned Media Enabled Company - ReadOnly'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property          | subProperty | subPropertyOther | permission | value             |
					   | EarnedMediaImpact | Access      |                  | IsGranted  | False             |
					   | EarnedMediaImpact | Access      |                  | Status     | Permission Denied |
					   | EarnedMediaImpact | Access      |                  | StatusCode | 1                 |

@acl @Impact @EarnedMedia
Scenario: a AE User should NOT be allow to see Earned Media Searches in Keyword Searches page from Settings Area
	Given I login as 'Earned Media Enabled Company - AE'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property          | subProperty | subPropertyOther | permission | value             |
					   | EarnedMediaImpact | Access      |                  | IsGranted  | False             |
					   | EarnedMediaImpact | Access      |                  | Status     | Permission Denied |
					   | EarnedMediaImpact | Access      |                  | StatusCode | 1                 |

@acl @Ignore
Scenario: A read only user should not have access to Settings Alert Management
	# When SettingsAlertsManagement is True, the company has news but the user is read only
	# The user should not have access to Settings Alert Management
	Given I login as 'ACL ReadOnly User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property            | subProperty | subPropertyOther | permission | value             |
					   | NewsAlertManagement | Access      |                  | IsGranted  | False             |
					   | NewsAlertManagement | Access      |                  | Status     | Permission Denied |
					   | NewsAlertManagement | Access      |                  | StatusCode | 1                 |

@acl @Ignore
Scenario: A User will not have access to Settings Alert Management when SettingsAlertsManagement is False
	# When the company has news and the user is not read only, but SettingsAlertsManagement is False
	# The user should not have access to Settings Alert Management
	Given I login as 'Manager Standard User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Settings should be:
					   | property            | subProperty | subPropertyOther | permission | value             |
					   | NewsAlertManagement | Access      |                  | IsGranted  | False             |
					   | NewsAlertManagement | Access      |                  | Status     | Permission Denied |
					   | NewsAlertManagement | Access      |                  | StatusCode | 1                 |


@acl 
Scenario: A User with Reconnect Configured Should be able to see this settings enabled
	Given I login as 'Reconnect User'
	When I perform a GET ACLS permissions
	Then ACLS Endpoint response should be '200'
	And ACLS permissions for Distribution should be:
					   | property  | subProperty | subPropertyOther | permission            | value |
					   | ReConnect |             |                  | CanCreate             | False |
					   | ReConnect |             |                  | CanDelete             | False |
					   | ReConnect |             |                  | CanEdit               | False |
					   | ReConnect |             |                  | CanView               | True  |
					   | ReConnect |             |                  | IsDemo                | False |
					   | ReConnect |             |                  | ShowFeaturedImage     | True  |
					   | ReConnect | Services    |                  | HasAccessToCisionNews | True  |