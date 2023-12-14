Feature: 49575VaildateAttendanceCodes
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@smoke @function
Scenario: Valid attendance codes are provided
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "AllAttendanceCodes"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And body returned contains submissionref and no errors
	And the request-response is logged
@smoke @function
Scenario: One invalid attendance code is provided against one year group
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "OneInvalidAttendanceCode"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And Error "0" "id" returned equals "FAILURE_INVALID_ATTENDANCE_CODE" 
	And Error "0" "code" returned equals "VAL104"
	And Error "0" "status" returned equals ""
	And Error "0" "title" returned equals "Invalid Attendance Code"
	And Error "0" "detail" returned equals "K"
	And Error "0" "path" returned equals "$.submissionData[0].attendances[10].attendanceCode"
@function
Scenario: More than a single invalid attendance code of a type is provided against one year group
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "TwoInvaidAttendanceCode"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And Error "0" "id" returned equals "FAILURE_INVALID_ATTENDANCE_CODE" 
	And Error "0" "code" returned equals "VAL104"
	And Error "0" "status" returned equals ""
	And Error "0" "title" returned equals "Invalid Attendance Code"
	And Error "0" "detail" returned equals "ZZ"
	And Error "0" "path" returned equals "$.submissionData[0].attendances[0].attendanceCode"
	And Error "1" "id" returned equals "FAILURE_INVALID_ATTENDANCE_CODE" 
	And Error "1" "code" returned equals "VAL104"
	And Error "1" "status" returned equals ""
	And Error "1" "title" returned equals "Invalid Attendance Code"
	And Error "1" "detail" returned equals "K"
	And Error "1" "path" returned equals "$.submissionData[0].attendances[2].attendanceCode"
@function
Scenario: One invalid attendance code is provided in a submission against more than one year group
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "TwoInvaidAttendanceCodeMuliYr"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And Error "0" "id" returned equals "FAILURE_INVALID_ATTENDANCE_CODE" 
	And Error "0" "code" returned equals "VAL104"
	And Error "0" "status" returned equals ""
	And Error "0" "title" returned equals "Invalid Attendance Code"
	And Error "0" "detail" returned equals "K"
	And Error "0" "path" returned equals "$.submissionData[0].attendances[2].attendanceCode"
	And Error "1" "id" returned equals "FAILURE_INVALID_ATTENDANCE_CODE" 
	And Error "1" "code" returned equals "VAL104"
	And Error "1" "status" returned equals ""
	And Error "1" "title" returned equals "Invalid Attendance Code"
	And Error "1" "detail" returned equals "ZZ"
	And Error "1" "path" returned equals "$.submissionData[1].attendances[2].attendanceCode"
@function
Scenario: More than one invalid attendance code is provided across more than one year group
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "SameInvAttendanceCodeMultiTimes"
	When the Post request is processed 
	Then a HTTP "201" response is returned
	And Error "0" "id" returned equals "FAILURE_INVALID_ATTENDANCE_CODE" 
	And Error "0" "code" returned equals "VAL104"
	And Error "0" "status" returned equals ""
	And Error "0" "title" returned equals "Invalid Attendance Code"
	And Error "0" "detail" returned equals "K"
	And Error "0" "path" returned equals "$.submissionData[0].attendances[2].attendanceCode"
	And Error "1" "id" returned equals "FAILURE_INVALID_ATTENDANCE_CODE" 
	And Error "1" "code" returned equals "VAL104"
	And Error "1" "status" returned equals ""
	And Error "1" "title" returned equals "Invalid Attendance Code"
	And Error "1" "detail" returned equals "K"
	And Error "1" "path" returned equals "$.submissionData[1].attendances[3].attendanceCode"