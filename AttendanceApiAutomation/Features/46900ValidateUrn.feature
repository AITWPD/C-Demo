Feature: 46900ValidateUrnInPostAttendances
	AS A department 
	I WANT to carry out validation on the URN supplied against a stub
	SO THAT I can ensure that it is valid

@smoke @function
Scenario: Valid URN are provided
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON
	And the "urn" is set to "123456"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And body returned contains submissionref and no errors

@smoke @function	
Scenario: Valid URN are not provided
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "InvalidUrn"
	When the Post request is processed 
	Then a HTTP "400" response is returned
	And "id" returned equals "ERROR_INVALID_URN" 
	And "code" returned equals "VAL102"
	And "status" returned equals "400"
	And "title" returned equals "URN not found"
	And "detail" returned equals "URN not found. You can search here at https://get-information-schools.service.gov.uk/ to find the appropriate URN"
@function
Scenario: URN is not provided
	Given a supplier has POST to the attendances API.
	And the payload is JSON is set with no URN
	When the Post request is processed 
	Then a HTTP "400" response is returned
	And "id" returned equals "ERROR_INVALID_URN" 
	And "code" returned equals "VAL100"
	And "status" returned equals "400"
	And "title" returned equals "URN is required"
	And "detail" returned equals "No URN was provided in the submission"


#Scenario: URN is not 6 digits
#	Given a supplier has POST to the attendances API.
#	And the application type is Json
#	And the payload is JSON file "UrnFiveDigit"
#	When the Post request is processed  
#	Then a HTTP "400" response is returned
#	And "id" returned equals "ERROR_INVALID_URN" 
#	And "code" returned equals "VAL101"
#	And "status" returned equals "400"
#	And "title" returned equals "Invalid URN"
#	And "detail" returned equals "A Valid URN is required along with the submission. You can search here at https://get-information-schools.service.gov.uk/ to find the appropriate URN"