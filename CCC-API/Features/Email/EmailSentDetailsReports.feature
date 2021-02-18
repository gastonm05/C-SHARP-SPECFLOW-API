Feature: EmailSentDetailsReports
	In order to share information about send email I can generate reports

Background: 	
	Given I remember expected data from 'Distributions.json' file

@publishers @email_sent_details @email_report
Scenario Outline: Generate PDF report
	 Given session for '<permission>' user with edition '<edition>'
     When I request PDF export for a distribution with <type>
	 Then I should be given the pending job url for future report of <type>	 
	 And I can download PDF report of type <type>
	 Then I can take necessary distribution details to compare with
	     And check cover page
		 And check analytics page with clicks, opens and bounces for <type>
		 And check clicks summary section for a report of type PDF
		 And check copy of email page
		 And check email details page to have times in user settings timezone for type of report PDF
		 And check recipients lists
		 And check list of recipients for the report of type PDF and recipients <type>
		 And check section of email opens
		 And check section of email clicks

Examples:  
	 | type          | permission   | edition                 |
	 | contacts      | standard     | Publishers manager user |
	 | outlets       | read_only    | Publishers manager user |
	 | individuals   | system_admin | Publishers manager user |
	 | organizations | standard     | Publishers manager user |

@publishers @email_sent_details @email_report @ignore
Scenario Outline: Generate PDF report with excluded sections
	Given shared session for 'standard' user with edition 'Publishers manager user'
	When I request export for a PDF report distribution of <type> and excluded sections <all>
	Then I should be given the pending job url for future report of <type>	 
	And I can download PDF report of type <type>
	And I can take necessary distribution details to compare with
	Then I should see '1' page with title and page number

Examples:  
	 | type          |
	 | contacts      |
	 | outlets       |

@publishers @email_sent_details @email_report
Scenario Outline: Generate DOCX report
     Given session for '<permission>' user with edition 'Publishers manager user'
     When I request DOCX export for a distribution with <type>
	 Then I should be given the pending job url for future report of <type>	 
	 And I can download DOCX report of type <type>
	 Then I can take necessary distribution details to compare with
	     And check cover page
		 And check analytics page with clicks, opens and bounces for <type>
		 And check clicks summary section for a report of type DOCX
		 And check copy of email page
		 And check email details page to have times in user settings timezone for type of report DOCX
		 And check recipients lists
		 And check list of recipients for the report of type DOCX and recipients <type>
		 And check section of email opens
		 And check section of email clicks
Examples:  
	 | type     | permission |
	 | contacts | standard   |
	 | outlets  | read_only  |

@publishers @email_sent_details @email_report @ignore
Scenario Outline: Generate DOCX report with excluded sections
    Given shared session for '<permission>' user with edition 'Publishers manager user'
	When I request export for a DOCX report distribution of <type> and excluded sections <all>
	Then I should be given the pending job url for future report of <type>	 
	And I can download DOCX report of type <type>
	And I can take necessary distribution details to compare with
	Then I should see '1' page with title and page number

Examples:  
	 | type        | permission |
	 | individuals | standard   |
	 | outlets     | read_only  |