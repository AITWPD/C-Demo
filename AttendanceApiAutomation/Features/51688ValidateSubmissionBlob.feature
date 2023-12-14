Feature: 51688ValidateSubmissionBlob
	As the department
	I want to ensure that the submission BLOB is stored correctly 
	In order that the submission data can be retrieved
@smoke @function @blob
Scenario: 1 a submission is successfully stored within the data store within an existing folder
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "ValidSubmission"
	When the Post request is processed with clientID "abbc2dc3-589b-4a5b-afb6-601e68a8eded"
	Then I write process the response json
	And I read record from blob storage
	And I write string to json
	And I confirm that the blob storage is correct
@function @blob
Scenario: 2 A submission is made and there is one more than one validation error
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "InvalidAttendanceAndInvalidYrGroup"
	When the Post request is processed with clientID "bbbc2dc3-589b-4a5b-afb6-601e68a8eded"
	Then I write string to json
	And I write process the response json
	And I read record from blob storage
	And I confirm that the blob storage is correct
@function @blob
Scenario: 3 validate YrGroup error successfully stored in Blob storage
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "OneInvalidYearGroup"
	When the Post request is processed with clientID "cccc2dc3-589b-4a5b-afb6-601e68a8eded"
	Then I write string to json
	And I write process the response json
	And I read record from blob storage
	And I confirm that the blob storage is correct
@function @blob	
Scenario: 4 validate Blob Storage with no errors
	Given a supplier has POST to the attendances API.
	And the application type is Json
	And the payload is JSON file "ValidSubmission"
	When the Post request is processed with clientID "dddd2dc3-589b-4a5b-afb6-601e68a8eded"
	Then I write string to json
	And I write process the response json
	And I read record from blob storage
	And I confirm that the blob storage is correct


	