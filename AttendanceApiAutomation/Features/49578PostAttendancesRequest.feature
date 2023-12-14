Feature: 49578 - POST Attendance Data
	As an MIS supplier
	I want to be able to submit school attendance data to the DfE 
	So that I can allow my schools to meet their obligations to provide attendance data to the DfE


@smoke @function
Scenario: A JSON payload is POST to the attendances API

Given a supplier has POST to the attendances API.
And the application type is Json
And the payload is JSON
When the Post request is processed 
Then a HTTP "201" response is returned
And body returned contains submissionref and no errors
And the request-response is logged
@function
Scenario: A non JSON payload is POST to the attendances API

Given a supplier has POST to the attendances API.
And the application type is Json
And the payload is not JSON
When the Post request is processed
Then a HTTP "400" response is returned
And an error description is returned in the body
And the request-response is logged
@function
Scenario: The header of the request is not in JSON

Given a supplier has POST to the attendances API.
And the payload is JSON
When the content header type of the request is  not application / JSON
When the Post request is processed
Then a HTTP "500" response is returned
And an error description is returned in the body was "Incorrect content type"
And the request-response is logged
@function
Scenario: The body of the request is empty
Given a supplier has POST to the attendances API.
And the "Content-Type" of the heaader is "application/json"
When the Post request is processed
Then a HTTP "400" response is returned
And an error description is returned in the body "Empty request body"
And the request-response is logged
