Feature: 51192ValidatePostPayload
	A POST request can be sent to the attendance API endpoint consisting of a JSON payload/body and a HTTP code response is returned
	THIS IS A FOLLOW ON FROM STORY 49578 ALL CHANGES HAVE BEEN MARKED IN RED

@smoke @function
Scenario: 1 A JSON payload is POST to the attendances API with AllYrGroups and All Attendance Codes
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "AllYrAttendanceGroups"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And body returned contains submissionref and no errors
	And the request-response is logged
@function
Scenario: 2 A non JSON payload is POST to the attendances API

	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is not JSON
	When the Post request is processed
	Then a HTTP "400" response is returned
	And an error description is returned in the body
	And the request-response is logged
@function
Scenario: 3 The header of the request is not in JSON

	Given a supplier has POST to the attendances API.
	And the payload is JSON
	When the content header type of the request is  not application / JSON
	When the Post request is processed
	Then a HTTP "500" response is returned
	And an error description is returned in the body was "Incorrect content type"
	And the request-response is logged
@function
Scenario: 4 The body of the request is empty
	Given a supplier has POST to the attendances API.
	And the "Content-Type" of the heaader is "application/json"
	When the Post request is processed
	Then a HTTP "400" response is returned
	And an error description is returned in the body "Empty request body"
	And the request-response is logged
@function
Scenario: 5 A JSON payload is POST to the attendances API with any invalid Year Group codes
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "OneInvalidYearGroup"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And Submission Reference is returned
	And Error "0" "id" returned equals "FAILURE_INVALID_NCYEARGROUP_CODE" 
	And Error "0" "code" returned equals "VAL103"
	And Error "0" "status" returned equals ""
	And Error "0" "title" returned equals "Invalid NC Year Group Code"
	And Error "0" "detail" returned equals "T"
	And Error "0" "path" returned equals "$.submissionData[1].ncYearGroup"
@function
Scenario: 6 More than a single invalid attendance code of a type is provided against one year group
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "OneInvalidAttendanceCode"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And Submission Reference is returned
	And Error "0" "id" returned equals "FAILURE_INVALID_ATTENDANCE_CODE" 
	And Error "0" "code" returned equals "VAL104"
	And Error "0" "status" returned equals ""
	And Error "0" "title" returned equals "Invalid Attendance Code"
	And Error "0" "detail" returned equals "K"
	And Error "0" "path" returned equals "$.submissionData[0].attendances[10].attendanceCode"
@function
Scenario: 6a A JSON payload is POST to the attendances API with any invalid Attendance codes and any invalid Year Group codes
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "InvalidAttendanceAndInvalidYrGroup"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And Submission Reference is returned
	And Error "0" "id" returned equals "FAILURE_INVALID_ATTENDANCE_CODE" 
	And Error "0" "code" returned equals "VAL104"
	And Error "0" "status" returned equals ""
	And Error "0" "title" returned equals "Invalid Attendance Code"
	And Error "0" "detail" returned equals "ZZ"
	And Error "0" "path" returned equals "$.submissionData[1].attendances[2].attendanceCode"
	And Error "1" "id" returned equals "FAILURE_INVALID_NCYEARGROUP_CODE" 
	And Error "1" "code" returned equals "VAL103"
	And Error "1" "status" returned equals ""
	And Error "1" "title" returned equals "Invalid NC Year Group Code"
	And Error "1" "detail" returned equals "15"
	And Error "1" "path" returned equals "$.submissionData[2].ncYearGroup"
@function
Scenario: 7 A JSON payload is POST to the attendances API with an attendance code value that is negative
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "InvalidValue"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And Submission Reference is returned
	And Error "0" "id" returned equals "FAILURE_INVALID_ATTENDANCE_VALUE" 
	And Error "0" "code" returned equals "VAL105"
	And Error "0" "status" returned equals ""
	And Error "0" "title" returned equals "Invalid Attendance Value"
	And Error "0" "detail" returned equals "-1"
	And Error "0" "path" returned equals "$.submissionData[0].attendances[1].value"