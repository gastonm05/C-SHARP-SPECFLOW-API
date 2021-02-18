Feature: Feeds
	To verify that news feeds have been ingested
	As companies with news feeds configured
	I want to verify that I have ingested news from the feeds

@news
Scenario: News Feeds Ingested within the past week
	Given the API test data 'Feeds.json'
	When I have the news feeds for each company under test
	Then news was ingested for each feed
