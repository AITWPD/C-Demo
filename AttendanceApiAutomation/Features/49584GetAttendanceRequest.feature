Feature: 49584 - Get Attendance Codes
	AS the department 
	I WANT  to get attendance codes from a stub
	SO THAT I can test that GET method calls to the codes / attendance request endpoint work


Background: 

Given that the API request is valid
@smoke @function
Scenario: GET/codes/attendance request

Given a supplier has submitted a GET/codes/attendance request to the attendances API.
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