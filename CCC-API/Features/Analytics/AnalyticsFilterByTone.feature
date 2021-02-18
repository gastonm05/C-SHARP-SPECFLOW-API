@ignore
Feature: AnalyticsFilterByTone
	In order to sort my data by tone
	I can use Filter by Tone setting

""" Please be aware that particular hook will be run to set up data for this feature
""" BE AWARE IF YOU CHANGE Company edition

@publishers @analytics
Scenario Outline: Mentions Chart > Settings > Tones Frequency Calculation Maxseries DataLabel
	Given shared session for '<permissions>' user with edition 'Analytics company with features enabled and dynamic news'
	When I GET a widget with settings: 
	| Chart   | TypeId   | Tones   | Frequency   | Datalabel   | Maxseries   | Calculation   |
	| <chart> | <typeId> | <tones> | <frequency> | <datalabel> | <maxseries> | <calculation> |

	Then response is OK with type '<expected_type>'
	And  maxseries is up to '<maxseries>'
	And  series 'Mentions' of news filtered by specified tone with '<frequency>' and '<calculation>'

	Examples: 
	| permissions  | chart              | typeId | tones    | frequency | datalabel | maxseries | calculation  | expected_type |
	| standard     | mentions_over_time | Line   |          | Daily     | Hide      | 10        | Count        | areaspline    |
	| standard     | mentions_over_time | Line   | Negative | Daily     | Hide      | 10        | Count        | areaspline    |
	| read_only    | mentions_over_time | Line   | Positive | Daily     | Show      | 2         | Count        | areaspline    |
	| system_admin | mentions_over_time | Line   | Neutral  | Daily     | Hide      | 20        | YearOverYear | areaspline    |
	| standard     | mentions_over_time | Bar    | Negative | Daily     | Show      | 5         | Count        | column        |
	| standard     | mentions_over_time | Bar    | Positive | Daily     | Hide      | 10        | YearOverYear | column        |
	| standard     | mentions_over_time | Line   | Neutral  | Weekly    | Hide      | 10        | Count        | areaspline    |
	| read_only    | mentions_over_time | Bar    | Negative | Weekly    | Show      | 2         | YearOverYear | column        |
	| standard     | mentions_over_time | Line   |          | Weekly    | Hide      | 10        | Count        | areaspline    |
	| standard     | mentions_over_time | Line   | Positive | Monthly   | Hide      | 10        | Count        | areaspline    |
	| system_admin | mentions_over_time | Line   | Neutral  | Yearly    | Hide      | 10        | Count        | areaspline    |
	| read_only    | mentions_over_time | Bar    | Positive | Yearly    | Show      | 15        | YearOverYear | column        |
	| standard     | mentions_over_time | Bar    | Negative | Yearly    | Show      | 15        | YearOverYear | column        |

@publishers @analytics
Scenario Outline: Value_Of_Coverage Chart > Tones Frequency Calculation Maxseries DataLabel
	Given shared session for '<permissions>' user with edition 'Analytics company with features enabled and dynamic news'
	When I GET a widget with settings: 
	| Chart   | TypeId   | Tones   | Frequency   | Datalabel   | Calculation   | Maxseries   | Annotations   |
	| <chart> | <typeId> | <tones> | <frequency> | <datalabel> | <calculation> | <maxseries> | <annotations> |

	Then response is OK with type '<expected_type>'
	And  maxseries is up to '<maxseries>'
	And  series 'Publicity Value' of news filtered by specified tone with '<frequency>' and '<calculation>'

	Examples: 
	| permissions | chart             | typeId | tones    | frequency | datalabel | maxseries | annotations | calculation | expected_type |
	| standard    | value_of_coverage | Line   |          | Daily     | Hide      | 10        | 1           | Count       | areaspline    |
	| standard    | value_of_coverage | Line   | Negative | Daily     | Hide      | 10        | 1           | YearOverYear| areaspline    |
	| standard    | value_of_coverage | Line   | Positive | Weekly    | Show      | 10        | 0           | Count       | areaspline    |
	| read_only   | value_of_coverage | Bar    | Neutral  | Monthly   | Hide      | 2         | 1           | Count       | column        |
	| system_admin| value_of_coverage | Bar    | Negative | Yearly    | Show      | 6         | 1           | Count       | column        |
	| standard    | value_of_coverage | Bar    |          | Yearly    | Hide      | 8         | 0           | YearOverYear| column        |
	
@publishers @analytics
Scenario Outline: Reach Chart > Settings > Tones Frequency Calculation Maxseries DataLabel
	Given shared session for '<permissions>' user with edition 'Analytics company with features enabled and dynamic news'
	When I GET a widget with settings: 
	| Chart   | TypeId   | Tones   | Frequency   | Datalabel   | Maxseries   | Calculation   | Annotations   |
	| <chart> | <typeId> | <tones> | <frequency> | <datalabel> | <maxseries> | <calculation> |<annotations>  |

	Then response is OK with type '<expected_type>'
	And  maxseries is up to '<maxseries>'
	And  news filtered by tone with '<frequency>' and '<calculation>' for series: 'Reach, UVPM'

	Examples: 
	| permissions | chart | typeId | tones    | frequency | datalabel | maxseries | annotations | calculation  | expected_type |
	| standard    | reach | Line   |          | Daily     | Hide      | 2         | 0           | Count        | spline        |
	| read_only   | reach | Line   | Negative | Weekly    | Show      | 2         | 1           | YearOverYear | spline        |
	| system_admin| reach | Line   | Positive | Monthly   | Show      | 2         | 1           | YearOverYear | spline        |
	| read_only   | reach | Line   | Neutral  | Yearly    | Show      | 10        | 0           | Count        | spline        | 

@publishers @analytics
Scenario Outline: Sentiment > Settings > Spline > Tones Frequency Calculation Maxseries DataLabel
	Given shared session for '<permissions>' user with edition 'Analytics company with features enabled and dynamic news'
	When I GET a widget with settings: 
	| Chart   | TypeId   | Tones   | Frequency   | Datalabel   | Maxseries   | Calculation   | Annotations   |
	| <chart> | <typeId> | <tones> | <frequency> | <datalabel> | <maxseries> | <calculation> |<annotations>  |

	Then response is OK with type '<expected_type>'
	And  maxseries is up to '<maxseries>'
	And  news filtered by tone with '<frequency>' and '<calculation>' for series: '<tones>'

	Examples: 
	| permissions  | chart     | typeId      | tones    | frequency | datalabel | maxseries | annotations | calculation | expected_type |
	| standard     | sentiment | Line        | Negative | Daily     | Show      | 10        | 0           | YearOverYear| areaspline    |
	| read_only    | sentiment | StackedBar  | Positive | Daily     | Show      | 3         | 1           | Count       | column        |
	| system_admin | sentiment | StackedArea | Neutral  | Daily     | Hide      | 10        | 0           | Count       | areaspline    |
	| standard     | sentiment | StackedBar  | Negative | Monthly   | Show      | 3         | 1           | YearOverYear| column        |
	| read_only    | sentiment | StackedArea | Positive | Monthly   | Show      | 5         | 0           | Count       | areaspline    |
	| standard     | sentiment | StackedBar  | Neutral  | Yearly    | Show      | 2         | 1           | YearOverYear| column        |
	| read_only    | sentiment | StackedArea | Negative | Yearly    | Hide      | 2         | 0           | Count       | areaspline    |
	
@publishers @analytics
Scenario Outline: Sentiment > Settings > Donut > Tones Frequency Calculation Maxseries DataLabel
	Given shared session for '<permissions>' user with edition 'Analytics company with features enabled and dynamic news'
	When I GET a widget with settings: 
	| Chart   | TypeId   | Tones   | Datalabel   | Maxseries   |
	| <chart> | <typeId> | <tones> | <datalabel> | <maxseries> |

	Then response is OK with type ''
	And  maxseries is up to '<maxseries>'
	And  news filtered by tone grouped for period for series: '<included>'
	And  series not present: '<excluded>'
	
	Examples: 
	| permissions | chart     | typeId | tones    | datalabel | maxseries | included                 | excluded          |
	| standard    | sentiment | Donut  | Negative | Hide      | 3         | Negative                 | Positive,Neutral  |
	| read_only   | sentiment | Donut  | Positive | Show      | 2         | Positive                 | Negative,Neutral  |
	| system_admin| sentiment | Donut  | Neutral  | Hide      | 3         | Neutral                  | Positive,Negative |