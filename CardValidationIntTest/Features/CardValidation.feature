Feature: Card Validation

As a client I want to be abble to send the Card data and get the type of card 
that is being sent if the data is valid.

Background:
	Given That I have the API adress

@integration
Scenario Outline: Sending valid card
	When I send the right data for <typeOfCard>
	Then the return should be <typeOfCard>
	Examples: 
		| typeOfCard       |
		| Visa             |
		| Mastercard       |
		| American Express |


Scenario Outline: Sending valid card but with a field blank
	When I send the card data missing the <blankField>
	Then the return error message should be <blankField> is required
	Examples: 
		| blankField |
		| owner      |
		| date       |
		| cvv        |
		| number     |


Scenario Outline: Sending invalid data
	When I send the card data with wrong <wrongField>
	Then the return error message should be wrong <wrongField>
	Examples: 
		| wrongField             |
		| ownerWithFourNames     |
		| dateSmallerThanCurrent |
		| dateWrongFormat	 |
		| cvvWithTwoDigit        |
		| cvvWithFiveDigit       |
		| numberWrong            |
