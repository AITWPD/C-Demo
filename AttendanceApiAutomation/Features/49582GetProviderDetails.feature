Feature: 49582 Get Provider Details
	AS the department 
	I WANT  to get school details relating to a dummy URN from a stub
	SO THAT I can test that GET method calls to the provider endpoint work

@smoke @function
Scenario: 1 Valid GET/provider/{URN} request and a valid URN is provided
	Given the MIS supplier has made a valid GET/provider/{URN} request using a dummy URN to the attendances API.
	And the urn is set to "134646"
	When the request is processed 
	Then a HTTP "200" response is returned
	And "urn" returned equals "134646" 
	And "name" returned equals "Ladybridge High School"
	And "address" returned equals "New York, Junction Road, Bolton, BL3 4NG"
	And "phaseOfEducation" returned equals "Secondary"
	And "schoolType" returned equals "Community school"
@function
Scenario: 2 Valid GET/provider/{URN} request and an invalid URN is provided
	Given the MIS supplier has made a valid GET/provider/{URN} request using a dummy URN to the attendances API.
	And the urn is set to "000000"
	When the request is processed 
	Then a HTTP "400" response is returned
	And "id" returned equals "ERROR_INVALID_URN" 
	And "code" returned equals "VAL102"
	And "status" returned equals "400"
	And "title" returned equals "URN not found"
	And "detail" returned equals "URN not found. You can search here at https://get-information-schools.service.gov.uk/ to find the appropriate URN"
@function
Scenario: 3 Valid GET/provider/{URN} request and an invalid URN is provided
	Given the MIS supplier has made a valid GET/provider/{URN} request using a dummy URN to the attendances API.
	And the urn is set to "null"
	When the request is processed 
	Then a HTTP "400" response is returned
	And "id" returned equals "ERROR_INVALID_URN" 
	And "code" returned equals "VAL101"
	And "status" returned equals "400"
	And "title" returned equals "Invalid URN"
	And "detail" returned equals "A Valid URN is required along with the submission. You can search here at https://get-information-schools.service.gov.uk/ to find the appropriate URN"

