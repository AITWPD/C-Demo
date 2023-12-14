Feature: 55888ValidateEAPIMFormat
	

@eapim
Scenario: 1a.) Date valid format with 00:00:00 timestamp
	Given I am making a POST attendance code request with a valid JWT token to EAPIM
	And the application type is Json
	And the EAPIM payload is JSON file "ValidSubmission"
	And I set date [0] to "2020-10-20T00:00:00"
	And I have a valid subscription key
	When I submit POST with fileupdate
	Then a HTTP "201" response is returned
@eapim	
Scenario: 1b.) Date valid format with no timestamp
	Given I am making a POST attendance code request with a valid JWT token to EAPIM
	And the application type is Json
	And the EAPIM payload is JSON file "ValidSubmission"
	And I set date [0] to "2020-10-10"
	And I have a valid subscription key
	When I submit POST with fileupdate
	Then a HTTP "201" response is returned

@eapim	
#Scenario: 1c.) Invalid Date valid format with no timestamp
#	Given I am making a POST attendance code request with a valid JWT token to EAPIM
#	And the application type is Json
#	And the EAPIM payload is JSON file "ValidSubmission"
#	And I set date [0] to "2020-20-10"
#	And I have a valid subscription key
#	When I submit POST with fileupdate
#	Then a HTTP "400" response is returned
@eapim
Scenario: 2a.) Valid URN
	Given I am making a POST attendance code request with a valid JWT token to EAPIM
	And the application type is Json
	And the EAPIM payload is JSON file "ValidSubmission"
	And I set urn to "123456"
	And I have a valid subscription key
	When I submit POST with fileupdate
	Then a HTTP "201" response is returned
@eapim
Scenario: 2b.) Invalid URN length
	Given I am making a POST attendance code request with a valid JWT token to EAPIM
	And the application type is Json
	And the EAPIM payload is JSON file "ValidSubmission"
	And I set urn to "12345"
	And I have a valid subscription key
	When I submit POST with fileupdate
	Then a HTTP "400" response is returned
@eapim
Scenario: 2c.) Invalid URN value
	Given I am making a POST attendance code request with a valid JWT token to EAPIM
	And the application type is Json
	And the EAPIM payload is JSON file "ValidSubmission"
	And I set urn to "700014"
	And I have a valid subscription key
	When I submit POST with fileupdate
	Then a HTTP "400" response is returned
	And "id" returned equals "ERROR_INVALID_URN" 
	And "code" returned equals "VAL102"
	And "status" returned equals "400"
	And "title" returned equals "URN not found"
	And "detail" returned equals "URN not found. You can search here at https://get-information-schools.service.gov.uk/ to find the appropriate URN"
@eapim
Scenario: 3a.) Valid NcYearGroup
	Given I am making a POST attendance code request with a valid JWT token to EAPIM
	And the application type is Json
	And the EAPIM payload is JSON file "ValidSubmission"
	And I set NcYearGroup [0] to "E1"
	And I have a valid subscription key
	When I submit POST with fileupdate
	Then a HTTP "201" response is returned
@eapim
Scenario: 3b.) Invalid NcYearGroup
	Given I am making a POST attendance code request with a valid JWT token to EAPIM
	And the application type is Json
	And the EAPIM payload is JSON file "ValidSubmission"
	And I set NcYearGroup [0] to "T"
	And I have a valid subscription key
	When I submit POST with fileupdate
	Then a HTTP "201" response is returned
	And Error "0" "id" returned equals "FAILURE_INVALID_NCYEARGROUP_CODE" 
	And Error "0" "code" returned equals "VAL103"
	And Error "0" "status" returned equals ""
	And Error "0" "title" returned equals "Invalid NC Year Group Code"
	And Error "0" "detail" returned equals "T"
	And Error "0" "path" returned equals "$.submissionData[0].ncYearGroup"
@eapim
Scenario: 4a.) Validate Get Attendance Codes
	Given I am making a GET attendance code request with a valid JWT token to EAPIM
	And I have a valid subscription key
	When the "Attendance" request is submitted
	Then a HTTP "200" response is returned
	And I should see following absence codes 
	|   AttendanceCode |  AttendanceDescription| 
	|   /            |  Present (AM)      |
	|   \\            |  Present (PM)      |
	|   B           |  Approved education activity as pupil being educated off site (not dual registration)      |
	|   C           |  Authorised absence as pupil is absent due to other authorised circumstances      |
	|   D           |  Dual registered (at another establishment) - not counted in possible attendances      |
	|   E            |  Authorised absence as pupil is excluded, with no alternative provision made      |
	|   G           |  Unauthorised absence as pupil is on a family holiday, not agreed, or is taking days in excess of an agreed family holiday      |
	|   H           |  Authorised absence due to agreed family holiday      |
	|   I             |  Authorised absence due to Illness (NOT medical or dental etc. appointments)      |
	|   J             |  Approved education activity as pupil is attending interview      |
	|   L            |  Late (before registers closed) marked as present      |
	|   M            |  Authorised absence due to medical/ dental appointments |
	|   N            |  Unauthorised absence as pupil missed sessions for a reason that has not yet been provided      |
	|   O            |  Unauthorised absence as pupil missed sessions for an unauthorised absence not covered by any other code/description      |
	|   P            |  Approved education activity as pupil is attending an approved sporting activity      |
	|   R            |  Authorised absence due to religious observance      |
	|   S            |  Authorised absence due to study leave      |
	|   T           |  Authorised absence due to traveller absence      |
	|   U           |  Unauthorised absence as pupil arrived after registers closed      |
	|   V           |  Approved education activity as pupil is away on an educational visit or trip      |
	|   W           |  Approved education activity as pupil is attending work experience      |
	|   X           |  Not required to be in school (non-compulsory school age pupil) and not attending in circumstances relating to coronavirus      |
	|   Y           |  Unable to attend due to exceptional circumstances - not counted in possible attendances      |
	|   Z           |  Pupil not yet on roll - not counted in possible attendances      |
	|   #           |  Planned whole or partial school closure - not counted in possible attendances      |
@eapim
Scenario: 4b.) Validate Get NcYearGroup Codes
	Given I am making a GET ncYear Group Code request with a valid JWT token to EAPIM
	And I have a valid subscription key
	When the "Attendance" request is submitted
	Then a HTTP "200" response is returned
	And I should see the following year group values 
	|   YearGroupCode |  YearGroup  | 
	|   E1                     |  Early first year      |
	|   E2                     |  Early second year      |
	|   N1                    |  Nursery first year      |
	|   N2                   |  Nursery second year      |
	|   R                      |  Reception      |
	|   1                      |  Year 1       |
	|   2                      |  Year 2      |
	|   3                      |  Year 3      |
	|   4                     |  Year 4      |
	|   5                      |  Year 5      |
	|   6                      |  Year 6      |
	|   7                      |  Year 7      |
	|   8                      |  Year 8      |
	|   9                      |  Year 9      |
	|   10                   |  Year 10    |
	|   11                    |  Year 11    |
	|   12                    |  Year 12     |
	|   13                    |  Year 13      |
	|   14                    |  Year 14      |
	|   M                    |  Mixed year class - Only to be used for the 'class year group (N00269)' |  
	|   X                    |  National curriculum not followed - available for special schools where pupils are not following a particular NC year. | 
@eapim
Scenario: 4c.) Validate Get Provider details 
	Given I am making a GET Provider request with a valid JWT token to EAPIM
	And I have a valid subscription key
	And the urn is set to "134646"
	When the request is processed 
	Then a HTTP "200" response is returned
	And "urn" returned equals "134646" 
	And "name" returned equals "Ladybridge High School"
	And "address" returned equals "New York, Junction Road, Bolton, BL3 4NG"
	And "phaseOfEducation" returned equals "Secondary"
	And "schoolType" returned equals "Community school"
@eapim
Scenario: 4d.) Valid GET/provider/{URN} request and an invalid URN is provided
	Given I am making a GET Provider request with a valid JWT token to EAPIM
	And I have a valid subscription key
	And the urn is set to "710101"
	When the request is processed 
	Then a HTTP "400" response is returned
	And "id" returned equals "ERROR_INVALID_URN" 
	And "code" returned equals "VAL102"
	And "status" returned equals "400"
	And "title" returned equals "URN not found"
	And "detail" returned equals "URN not found. You can search here at https://get-information-schools.service.gov.uk/ to find the appropriate URN"
@eapim
Scenario: 4e.) Valid GET/provider/{URN} request and an invalid URN is provided
	Given I am making a GET Provider request with a valid JWT token to EAPIM
	And I have a valid subscription key
	And the urn is set to "null"
	When the request is processed 
	Then a HTTP "400" response is returned
	And "id" returned equals "ERROR_INVALID_URN" 
	And "code" returned equals "VAL101"
	And "status" returned equals "400"
	And "title" returned equals "Invalid URN"
	And "detail" returned equals "A Valid URN is required along with the submission. You can search here at https://get-information-schools.service.gov.uk/ to find the appropriate URN"

Scenario: 5a.) Negative Attendance Value
	Given I am making a POST attendance code request with a valid JWT token to EAPIM
	And I have a valid subscription key
	And the application type is Json
	And the EAPIM payload is JSON file "ValidSubmission"
	And I set attendance value [0] for SubmissionData [0] to value [-1]
	And I have a valid subscription key
	When I submit POST with fileupdate
	Then a HTTP "201" response is returned
	And Error "0" "id" returned equals "FAILURE_INVALID_ATTENDANCE_VALUE" 
	And Error "0" "code" returned equals "VAL105"
	And Error "0" "status" returned equals ""
	And Error "0" "title" returned equals "Invalid Attendance Value"
	And Error "0" "detail" returned equals "-1"
	And Error "0" "path" returned equals "$.submissionData[0].attendances[0].value"

Scenario: 5b.) Zero Attendance Value
	Given I am making a POST attendance code request with a valid JWT token to EAPIM
	And the application type is Json
	And the EAPIM payload is JSON file "ValidSubmission"
	And I set attendance value [0] for SubmissionData [0] to value [0]
	And I have a valid subscription key
	When I submit POST with fileupdate
	Then a HTTP "201" response is returned
	And body returned contains submissionref and no errors

