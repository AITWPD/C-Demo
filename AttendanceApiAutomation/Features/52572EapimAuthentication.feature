Feature: 52572EapimAuthentication
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@eapim
Scenario: 1 MIS provider calls with valid subscription key and JWT
	Given I am making a GET attendance code request with a valid JWT token to EAPIM
	And I have a valid subscription key
	When the "Attendance" request is submitted
	Then a HTTP "200" response is returned
@eapim
Scenario: 2 MIS provider calls with valid invalid subscription key and JWT
	Given I am making a GET attendance code request with an invalid JWT token to EAPIM
	And I have a valid subscription key
	When the "Attendance" request is submitted
	Then a HTTP "401" response is returned