Feature: 49585 - Get NC Year Group
	AS the department 
	I WANT  to get ncyeargroup codes from a stub
	SO THAT I can test that GET method calls to the codes / ncyeargroup request endpoint work


Background: 

Given that the API request is valid

@smoke @function
Scenario: GET/codes/ncyeargroup request

Given the supplier has submitted a GET/codes/ncyeargroup request to the attendances API.
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