Feature: EmailDistributionWizard
	In order to send email distributions
	As a user
	I should be able to create my emails

@publishers @email_distribution @ignore
Scenario Outline: Email > Convert word document to html
	Given shared session for 'standard' user with edition 'Publishers manager user'
	When I post a word file '<typeFile>' to the worddoctohtml endpoint
	Then the response should be html code that can be compared to expected '<expFile>'

Examples: 
	| typeFile		    |       expFile   	     |
	| FormattedText.docx|FormattedDocxTextExp.txt|
	| FormattedText.doc |FormattedDocTextExp.txt |

@publishers @email_distribution @ignore
Scenario: Email > Save Html template
	Given I remember file 'IlluminaHtmlTemplate.txt'
	Given shared session for 'standard' user with edition 'Publishers manager user'
	When I post html code 'IlluminaHtmlTemplate.txt' to htmltemplates/user endpoint with 'some' name
	Then html template is saved
	And I can find html template among custom templates 'IlluminaHtmlTemplate.txt'
	And I can see html template thumbnail
	And I delete html template Publishers admin user
	
@publishers @email_distribution @ignore
Scenario: Email > Edit Html template
	Given I remember file 'IlluminaHtmlTemplate.txt'
	Given session for 'system_admin' user with edition 'Manager user Campaign-Enabled company'
	Given I create email html template 'IlluminaHtmlTemplate.txt' with name '50' chars
	When I edit (put) html template
	Then the operation 'htmltemplates/user response' is '200'
	Then I can find html template among custom templates 'IlluminaHtmlTemplate.txt'
	And I delete html template

@publishers @email_distribution
Scenario: Email > Delete Html template
	Given I remember file 'IlluminaHtmlTemplate.txt'
	Given shared session for 'system_admin' user with edition 'ESAManager'
	Given I create email html template 'IlluminaHtmlTemplate.txt' with name '10' chars
	When I delete html template
	Then the operation 'htmltemplates/user response' is '204'
	Then I cannot find html template among custom templates

@publishers @email_distribution 
Scenario Outline: Email > Submit Email With Additional Recipients
	Given shared session for 'standard' user with edition 'Manager user Campaign-Enabled company'
	Given a few common email addresses:
	| email                                     |
	| oleh.123.ilnytskyi@cision.com             |
	| test-9876543210@gmail.com                 |
	| tst.tst=tst@o2.co.uk                      |
	| i_want@to.travel                          |
	| so+me@localhost                           |
	| !#$%&*+-/=?^_`very@very-wierd.dot.dot.com |
	| t-t@yahoo.co.jp                           |
	Given I remember avaliable email credits for the company
	When I create email distribution to '<contacts>' '<outlets>' with additional recipients with schedule type '<schedule_type>'
	And company credits are charged for unique emails

Examples: 
	| schedule_type | contacts | outlets |
	| now           |          |         |
	| in future     |          |         |
	| now           |   yes    |   yes   |

@publishers @email_distribution @ignore
Scenario Outline: Email > Unique email count
	Given shared session for 'standard' user with edition 'Manager user Campaign-Enabled company'
	Given a few common email addresses:
	| email                         |
	| oleh.123.ilnytskyi@cision.com |
	| test-9876543210@gmail.com     |
	| som$other@o2.co.uk            |
	| duplicate@dot.dot.dot         |
	| duplicate@dot.dot.dot         |
	When I perform POST to email/distribution/uniqueemailcount with '<recipients>'
	Then I see a number of unique emails
Examples: 
	| recipients                  |
	| contacts,outlets,additional |
	| additional                  |
	| contacts                    |
	| outlets                     |

@publishers @email_distribution @email_analytics @ignore
Scenario Outline: Email > Email with SendCopy, OverrideAddress, Analytics Tracking
	Given shared session for '<permission>' user with edition '<edition>'
	When I POST email distribution with '<schedule_type>', '<send_copy>', '<override_address>', tracking type: '<tracking_type>', '<parameters>'
	And I retrieve distribution infromation from tables Distribution, DistributionEmail
	Then distribution has tracking type: '<tracking_type>' and parameters: '<parameters>' in DB	
	And distribution has other parameters saved in DB to tables Distribution, DistributionEmail

Examples: 
	| edition                                  | permission   | override_address | schedule_type | send_copy | tracking_type       | parameters                                                                                                                             |
	| Publishers manager user                  | standard     | true             | in future     | true      | GoogleAnalytics     | {"utm_source":"cision","utm_medium":"200x100banner","utm_campaign":"jobs","utm_term":"seo_services","utm_content":"content"}           |
	| Manager user Campaign-Enabled company    | standard	  | true             | now           | false     | Eloqua              | {"campaign_source":"eloqua","campaign_name":"eloqua"}                                                                                  |
	| Publishers social company, custom fields | standard     | false            | in future     | false     | AdobeMarketingCloud | {"keyword":"email","categoryID":"email"}                                                                                               |
	| Publishers manager user                  | system_admin | true             | in future     | true      | Hubspot             | {"utm_source":"purchased_last-30-days","utm_content":"20-off-offer","utm_medium":"email","utm_campaign":"BMX15","utm_term":"bicycles"} |
	| Publishers manager user                  | standard     | false            | in future     | false     | Other               | {"utm_medium":"display","utm_source":"cision.com","utm_content":"SearchLandArticle"}                                                   |

@publishers @email_distribution @ownership
Scenario Outline: Email > Schedule email distribution > Unschedule
	Given session for '<permission>' user with edition '<edition>'
	When I schedule (POST) email distribution with: <timezone>, <recipients>
	Then scheduled 'Email' activity contains correct publication state, content, date time, owner	
	When I unschedule (POST) email distribution
	Then can find activity of 'Email' and 'Draft' activity listed among published activities

	Examples: 
	| edition                               | permission   | timezone                | recipients       |
	| Publishers manager user               | standard     | GMT Standard Time       | contacts         |
	| Publishers manager user               | system_admin | W. Europe Standard Time | contacts,outlets |

@publishers @email_distribution
Scenario Outline: Email > Design your Email > Merge Fields
	Given I remember file '<file>'
	And   shared session for '<permission>' user with edition '<edition>'
	When  I GET email merge fields for '<recipients_type>'
	Then  response contains merge fields from file '<file>'

	Examples: 
	| edition                               | permission   | recipients_type           | file                                 |
	| Publishers manager user               | standard     | MediaContact              | EmailMergeFieldsContacts.json        |
	| Publishers manager user               | system_admin | MediaOutlet               | EmailMergeFieldsOutlets.json         |
	| Manager user Campaign-Enabled company | standard     | MediaContact,MediaOutlet  | EmailMergeFieldsContactsOutlets.json |