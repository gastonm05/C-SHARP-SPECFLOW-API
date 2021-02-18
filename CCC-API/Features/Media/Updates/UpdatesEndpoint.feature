Feature: UpdatesEndpoint
	In order to see updates in media
	As a standard CCC user
	I want to get media updates

@media @ignore
Scenario: Media updates endpoint returns valid results
	Given shared session for 'standard' user with edition 'basic'
	When I perform a GET for Media Updates
	Then all returned Media Updates are valid