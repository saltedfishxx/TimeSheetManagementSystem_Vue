# TimeSheetManagementSystem_Vue


This zip file for TimeSheeManagementSystem_Vue contains 2 folders:

## AspNetCore (backend web api)
  *	Before running the code, make sure that:
      *	The SQL connection (found in ApplicationDBContext.cs) is your SQL connection string
      *	There is a Migrations folder in the solution, else open command prompt/windows PowerShell and navigate to the project directory, then run “dotnet ef migrations add setupdb”
      *	Under Startup.cs, make sure app.seedData() is uncommented on first run when setting up the database
         *	If the sql script inside this folder runs nicely, app.seedData() should be commented
  *	To run, open command prompt/windows PowerShell and navigate to the project directory, then run “dotnet run”
  ```
    D:/desktop/project > dotnet run
  ```
  *	Make sure that this web api is running first before running the client-side code

## Vue (frontend client and UI)
  *	Before running, it is best to check if all the required npm modules are installed, run “npm install” just in case
  *	To run, open command prompt/windows PowerShell and navigate to the project directory, then run “npm start”
  ```
    D:/desktop/project > npm start
  ```
  *	If build failed due to missing npm modules, check package.json to see the dependencies used
  *	It is highly recommended to not reload the webpage through web browser as it will cause routing errors, causing the navigation to not work properly


## Account and password:
#### To test for Administrator:
*	Username: admin
*	Password: admin
#### To test for Instructor:
*	Username: test
*	Password: test

## Administrator vs Instructor:
*	Only administrators can manage user accounts (to log in/sign up/update details) in the management system.
*	Administrators can also view all records for session synopses, customer accounts and account regardless of visibility 
*	Instructors can only create/update/delete session synopses, customer accounts and account rate

## Changes made for CA2:
*	Added dashboard for Admin users to have an overview on the records in the system
    * Includes recent activity, calendar that shows start/end dates of account details/rates and overview table on session synopses and customer account
*	Added CRUD for Account Detail 
    *	When creating detail, admin users can view existing detail records through the modal
    *	If there is overlapping time on the same day, will show a warning with the referenced overlapping record
    *	When user submits, if the starting/ending time and day is the same as the ones in the records, submit is prevented and an error will show
*	Added active status onto Account Rates and Account Details
    *	When current/today’s date is within the range of any account rate/detail, the row in the table will indicate that it is active (spinning gear)
*	Added notifications
    *	When user create account detail/rates or logs in, they will have a notification dropdown to view how many account details/rates are active and how many overlapping details
*	Changed layout of customer accounts table
    *	added detailed row/dropdown to show comments and added no. of rates/detail onto column for better use of estate space in the table
