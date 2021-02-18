Feature: Messages
	In order to verify message publication to social media networks
	I schedule a message with different input to all the available social media platforms

@socialmedia @ignore
Scenario Outline: Characters accepted boundaries testing for publishing api
Given I login as 'socialmedia User5'
When  I schedule a message to send on <time>, <time_zone> to <platform> with a text length of <text_length>, and image <image>, linked <linked>, from page <page_url>, shorten url service <shorten_urls> and <response_code> as expected response code

Examples: 
	| platform        | time | time_zone | text_length | image  | page_url | linked | shorten_urls | response_code       |
	| Twitter         | now  |           | 280         |        |          | false  | true         | OK                  |
	| Twitter         | now  |           | 281         |        |          | false  | true         | InternalServerError |
	| FacebookFanPage | now  |           | 10000       |	    |          | false  | true         | OK                  |
	| FacebookFanPage | now  |           | 10010       |	    |          | false  | true         | OK		             |


@socialmedia
Scenario Outline: Scheduling a message with empty content should return an error
Given I login as 'socialmedia User7'
When  I schedule a message to send on <time>, <time_zone> to <platform> with a text length of <text_length>, and image <image>, linked <linked>, from page <page_url>, shorten url service <shorten_urls> and <response_code> as expected response code

Examples: 
	| platform        | time | time_zone | text_length | image | page_url | linked | shorten_urls | response_code |
	| FacebookFanPage | now  |           | 0           |       |          | false  | true         | BadRequest    |

@socialmedia
Scenario Outline: Schedule a message with different type of images attached
Given I login as 'socialmedia User7'
When  I schedule a message to send on <time>, <time_zone> to <platform> with a text length of <text_length>, and image <image>, linked <linked>, from page <page_url>, shorten url service <shorten_urls> and <response_code> as expected response code

Examples: 
	| platform        | time | time_zone | text_length | image                                                                                                     | page_url | linked | shorten_urls | response_code |
	| FacebookFanPage | now  |           | 10          | http://cision-wp-files.s3.amazonaws.com/us/wp-content/uploads/2016/02/02152640/logo-60px.png              |          | false  | true         | OK         |
	| Twitter         | now  |           | 10          | https://media.giphy.com/media/T8Dhl1KPyzRqU/giphy.gif                                                    |          | false  | true         | OK         |

@socialmedia
Scenario Outline: Schedule a message with different type images linked
Given I login as 'socialmedia User3'
When  I schedule a message to send on <time>, <time_zone> to <platform> with a text length of <text_length>, and image <image>, linked <linked>, from page <page_url>, shorten url service <shorten_urls> and <response_code> as expected response code

Examples: 
	| platform        | time | time_zone | text_length | image                                                                         | page_url                                                                                              | linked | shorten_urls | response_code |
	| FacebookFanPage | now  |           | 10          | http://cdn.osxdaily.com/wp-content/uploads/2013/07/dancing-banana.gif         | http://osxdaily.com/2013/07/25/send-receive-animated-gifs-iphone/                                     | true   | true         | OK         |

@socialmedia
Scenario Outline: Schedule a message time validations	
Given I login as 'socialmedia User4'
When  I schedule a message to send on <time>, <time_zone> to <platform> with a text length of <text_length>, and image <image>, linked <linked>, from page <page_url>, shorten url service <shorten_urls> and <response_code> as expected response code

Examples: 
	| platform        | time     | time_zone              | text_length | image                                                        | page_url                                 | linked | shorten_urls | response_code       |
	| FacebookFanPage | nextYear | Eastern Standard Time  | 10          | https://processing.org/tutorials/pixels/imgs/tint1.jpg       | https://processing.org/tutorials/pixels/ | true   | true         | OK                  |
	| Twitter         | nextYear | Mountain Standard Time | 10          | https://processing.org/tutorials/pixels/imgs/tint3.jpg       |										  | false  | true         | OK                  |
