Feature: Create Balances For A Newly Created Budget 

Scenario: Create a balance item for a budget
	Given there is a budget which has id 1 
	Given there is an existing wallet named "Wallet 1"
	When receive a request to create a balance for that budget 
	Then a balance item should be created 
