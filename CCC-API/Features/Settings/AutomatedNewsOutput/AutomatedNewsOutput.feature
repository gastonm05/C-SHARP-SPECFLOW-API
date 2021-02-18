Feature: Settings - Automated News Output Config Endpoint
	To verify that Automated News Output Settings can be retrieved and modified
	As a valid CCC user from a company with parameter Elysium-CustomFields-Enabled set to true
	I want to call newsftpexport endpoint - management/newsftpexport

@acl @Settings @FTP @Export @Ignore
Scenario:  A manager user with a News-FtpExport-Enabled disabled company should NOT be allowed to view or change Automated News Output Settings (only IncludeDuplicates is Editable).
	Given I login as 'Smart Tag ON Company'
	When I perform a GET on newsftpexport endpoint to view Automated News Output Settings
	Then NewsFtpExport endpoint GET response code should be 403
	When I perform a PUT on newsftpexport endpoint to edit Automated News Output Settings setting IncludeDuplicates to 'true' 
	Then NewsFtpExport endpoint PUT response code should be 403

@acl @Settings @FTP @Export @Ignore
Scenario:  A manager user with a News-FtpExport-Enabled enabled company should be allowed to view or change Automated News Output Settings (only IncludeDuplicates is Editable).
	Given I login as 'FTP Export Enabled Company - Manager'
	When I perform a GET on newsftpexport endpoint to view Automated News Output Settings
	Then NewsFtpExport endpoint GET response code should be 200
	When I perform a PUT on newsftpexport endpoint to edit Automated News Output Settings setting IncludeDuplicates to 'true' 
	Then NewsFtpExport endpoint PUT response code should be 200

@acl @Settings @FTP @Export @Ignore
Scenario:  A SysAdmin user with a News-FtpExport-Enabled enabled company should be allowed to view or change Automated News Output Settings (only IncludeDuplicates is Editable).
	Given I login as 'FTP Export Enabled Company - SysAdmin User'
	When I perform a GET on newsftpexport endpoint to view Automated News Output Settings
	Then NewsFtpExport endpoint GET response code should be 200
	When I perform a PUT on newsftpexport endpoint to edit Automated News Output Settings setting IncludeDuplicates to 'true' 
	Then NewsFtpExport endpoint PUT response code should be 200

@acl @Settings @FTP @Export @Ignore
Scenario:  A AE user with a News-FtpExport-Enabled enabled company should be allowed to view or change Automated News Output Settings (only IncludeDuplicates is Editable).
	Given I login as 'FTP Export Enabled Company - AE'
	When I perform a GET on newsftpexport endpoint to view Automated News Output Settings
	Then NewsFtpExport endpoint GET response code should be 200
	When I perform a PUT on newsftpexport endpoint to edit Automated News Output Settings setting IncludeDuplicates to 'true' 
	Then NewsFtpExport endpoint PUT response code should be 200

@acl @Settings @FTP @Export @Ignore
Scenario:  A Read Only user with a News-FtpExport-Enabled enabled company should NOT be allowed to view or change Automated News Output Settings (only IncludeDuplicates is Editable).
	Given I login as 'FTP Export Enabled Company - Readonly User'
	When I perform a GET on newsftpexport endpoint to view Automated News Output Settings
	Then NewsFtpExport endpoint GET response code should be 403
	When I perform a PUT on newsftpexport endpoint to edit Automated News Output Settings setting IncludeDuplicates to 'true' 
	Then NewsFtpExport endpoint PUT response code should be 403

@acl @Settings @FTP @Export @Ignore
Scenario:  A Standard user with a News-FtpExport-Enabled enabled company should NOT be allowed to view or change Automated News Output Settings (only IncludeDuplicates is Editable).
	Given I login as 'FTP Export Enabled Company - Standard User'
	When I perform a GET on newsftpexport endpoint to view Automated News Output Settings
	Then NewsFtpExport endpoint GET response code should be 403
	When I perform a PUT on newsftpexport endpoint to edit Automated News Output Settings setting IncludeDuplicates to 'true' 
	Then NewsFtpExport endpoint PUT response code should be 403