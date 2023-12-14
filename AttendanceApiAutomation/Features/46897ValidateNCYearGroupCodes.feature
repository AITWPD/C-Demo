Feature: 46897ValidateNcYearGroupCodes
	AS A department 
	I WANT to carry out validation on national curriculum year group codes provided by MIS suppliers
	SO THAT I can ensure that the codes are valid

@smoke @function
Scenario: 1 Wake application
Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "AllYearGroups"
	When the Post request is processed 
	Then I wait "60" seconds

@smoke @function
Scenario: Valid national curriculum year group codes are provided
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "AllYearGroups"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And body returned contains submissionref and no errors
	And the request-response is logged

@smoke @function
Scenario: One valid national curriculum year group codes is not provided
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "OneInvalidYearGroup"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And Error "0" "id" returned equals "FAILURE_INVALID_NCYEARGROUP_CODE" 
	And Error "0" "code" returned equals "VAL103"
	And Error "0" "status" returned equals ""
	And Error "0" "title" returned equals "Invalid NC Year Group Code"
	And Error "0" "detail" returned equals "T"
	And Error "0" "path" returned equals "$.submissionData[1].ncYearGroup"
@function
Scenario: More than one invalid national curriculum year group codes are not provided
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "TwoInvalidYearGroups"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And Error "0" "id" returned equals "FAILURE_INVALID_NCYEARGROUP_CODE" 
	And Error "0" "code" returned equals "VAL103"
	And Error "0" "status" returned equals "" 
	And Error "0" "title" returned equals "Invalid NC Year Group Code"
	And Error "0" "detail" returned equals "T"
	And Error "0" "path" returned equals "$.submissionData[1].ncYearGroup"
	And Error "1" "id" returned equals "FAILURE_INVALID_NCYEARGROUP_CODE" 
	And Error "1" "code" returned equals "VAL103"
	And Error "1" "status" returned equals ""
	And Error "1" "title" returned equals "Invalid NC Year Group Code"
	And Error "1" "detail" returned equals "15"
	And Error "1" "path" returned equals "$.submissionData[3].ncYearGroup"