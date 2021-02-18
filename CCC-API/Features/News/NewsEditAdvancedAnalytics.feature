Feature: NewsEditAdvancedAnalytics
	User can override default analytics searches for a news article

@publishers @news
Scenario: News > Edit News Advanced Company Analytics 
	Given session for edition 'Analytics company with features enabled and dynamic news', permission: 'system_admin', datagroup: 'news'
	And analytics profile 'company' searches present: 'Reebok, Nike, Martens, Vans'
	And news article exist
	When I perform a PATCH for news item to Add company searches:
		| search  | tone     | prominence | impact |
		| Reebok  | Negative | 50         | 200    |
		| Nike    | Positive | 1          | 1      |
		| Martens | Neutral  | 49         | 5      |
		| Vans    | None     | 20         | 40     |
	Then news item has news analytics searches: 
		| search  | tone     | prominence | impact |
		| Reebok  | Negative | 50         | 200    |
		| Nike    | Positive | 1          | 1      |
		| Martens | Neutral  | 49         | 5      |
		| Vans    | None     | 20         | 40     |
	When I perform a PATCH for news item to Remove company searches:
		| search  | tone     | prominence | impact |
		| Reebok  | Negative | 1          | 1      |
	Then news item has news analytics searches: 
		| search  | tone     | prominence | impact |
		| Nike    | Positive | 1          | 1      |
		| Martens | Neutral  | 49         | 5      |
		| Vans    | None     | 20         | 40     |

@publishers @news
Scenario: News > Edit News Advanced Product Mentions Analytics > Remove
	Given session for edition 'Analytics company with features enabled and dynamic news', permission: 'system_admin', datagroup: 'news'
	And analytics profile 'product' searches present: 'Tortilla, Wasabi, Pret, Wahaca'
	And news article exist
	When I perform a PATCH for news item to Add product searches: 'Wasabi, Wahaca'
	Then news item has news analytics searches: 'Wasabi, Wahaca'
	When I perform a PATCH for news item to Remove product searches: 'Wahaca'
	Then news item has news analytics searches: 'Wasabi'

@publishers @news
Scenario: News > Edit News Advanced Message Mentions Analytics > Remove
	Given session for edition 'Analytics company with features enabled and dynamic news', permission: 'system_admin', datagroup: 'news'
	And analytics profile 'message' searches present: '1_good, 2_bad, 3_awesome'
	And news article exist
	When I perform a PATCH for news item to Add product searches: '1_good, 2_bad, 3_awesome'
	Then news item has news analytics searches: '1_good, 2_bad, 3_awesome'
	When I perform a PATCH for news item to Remove product searches: '1_good, 2_bad'
	Then news item has news analytics searches: '3_awesome'