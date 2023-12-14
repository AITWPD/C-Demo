Feature: 51183ValidateAttendanceValue
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@function
Scenario: 1 A value of 0 is provided against the attendance code description
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "AttendanceValueZero"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And body returned contains submissionref and no errors
	And the request-response is logged
@function
	Scenario: 2 A value of 1 is provided against the attendance code description
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "AttendanceValueOne"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And body returned contains submissionref and no errors
	And the request-response is logged
@function
	Scenario: 3 Invalid value against attendance code provided
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "AttendanceValueNegative"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And Error "0" "id" returned equals "FAILURE_INVALID_ATTENDANCE_VALUE" 
	And Error "0" "code" returned equals "VAL105"
	And Error "0" "status" returned equals ""
	And Error "0" "title" returned equals "Invalid Attendance Value"
	And Error "0" "detail" returned equals "-1"
	And Error "0" "path" returned equals "$.submissionData[0].attendances[1].value"