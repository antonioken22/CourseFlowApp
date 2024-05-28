# Interactive Course Flowsheet Windows App

## Get Started
1. Download the ZIP and Extract it to your own Windows machine.
> Demo image/s are yet to be uploaded.
3. Open the `.sln` file.
> Demo image/s are yet to be uploaded.
4. Open App.Config then add and name your connectionString and enter the file path for the MSAccess Database as the Data Source. <br/>
> I.e. : <br/>
> `add name="NameThis" connectionString="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=YourFilePathHere" providerName="System.Data.OleDb"` <br/>
> E.g. : <br/>
> `add name="CourseFlow" connectionString="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\CourseFlowApp\CourseFlow\MSAccessDatabase\CourseFlow.accdb" providerName="System.Data.OleDb"`

> Demo image/s are yet to be uploaded.
4. Open RepositoryBase.cs from the Repositories Folder then replace the defined string inside ConnectionStrings[] with your own defined string of the connection string from the previous step. <br/>
> I.e. : <br/>
> `_connectionString = ConfigurationManager.ConnectionStrings["YourDefinedName"].ConnectionString;` <br/>
> E.g. : <br/>
> `_connectionString = ConfigurationManager.ConnectionStrings["CourseFlow"].ConnectionString;`

> Demo image/s are yet to be uploaded.
5. Build and Run the Program. <br/>
> a. You can use the existing Users: <br/>
> Username: `admin` | Password: `admin` |  Role: Admin <br/>
> Username: `user` | Password: `user` | Role: Student <br/>
> b. Or you can create your own User by Clicking `Sign-up`.

> Demo image/s are yet to be uploaded.
6. Login using the pre-existing accounts or your created account.
> Demo image/s are yet to be uploaded.
7. Upon successful login, you will be redirected to the Dashboard. <br/>
a. To test the Primary Feature of the app, click the `Course Flowsheet` from the left panel.
8. Load the Data from the Selected Course and Effective Academic Year by clicking the `Load Flowsheet` Button <br/>
> Demo image/s are yet to be uploaded.

> Note: <br/>
>  - Edit Mode is only available for Users with Admin roles. <br/>
>  - Only Course: BS Computer Engineering and Academic Year: 2022-2023 have pre-existing data. If you wish to create data for a separate course with its effective academic year, you may do so by using the Edit Mode which I’ll cover in the later part. <br/>
9. And there you go, you can now fully enjoy the Interactive Course Flowsheet Windows App. Whether you just want to check the subject in your Bachelor’s Program or just wanted to know in a quick mouse hover how subjects relate to each other (Pre-requisite, Co-requisite, Post-requisite) in your Bachelor’s Program, I got you covered in my app.
> Demo image/s are yet to be uploaded.

## Entity Relationship Diagram (ERD)
> Demo image/s are yet to be uploaded.

## Create, Read, Update, and Delete (CRUD) to the Database
1. Here’s the pre-existing database schema that involves a Bachelor’s Program.
> Demo image/s are yet to be uploaded.
3. The app Reads from the database after the Load Flowsheet Button is pressed.
> Demo image/s are yet to be uploaded.
4. The app only allows Create Update and Delete for the Subjects and SubjectRelationships Table. The rest will require manual modifications in the database.
> Demo image/s are yet to be uploaded.
5. To Create Subjects and their Relationships, click the Edit Mode (Admin Only) then click the Add Subjects Button.
> Demo image/s are yet to be uploaded.
6. A separate window will pop up and will require the necessary fields to be filled.
> Demo image/s are yet to be uploaded.
7. After filling out the fields, you can now save it to the database by clicking the Save Button. <br/>
> Note: <br/>
> - You may define here the related subjects of this subject from the existing data or you may edit it later using the Edit button. <br/>

> Demo image/s are yet to be uploaded.
7. After saving, you may continue to add subjects as it will still remain open or you may want to close it to check the newly added subject in the app.
> Demo image/s are yet to be uploaded.
8. Editing a subject will allow you to modify all of its data. You can also add and remove its related subjects.
> Demo image/s are yet to be uploaded.
9. The remove button will erase the Subject and its associated Related Subjects.
> Demo image/s are yet to be uploaded.
10. Congratulations, you can now use the app’s functionalities to accommodate your preferred Bachelor’s Program with its respective Effective Academic Year.

## Bugs?
- `create an issue`

Thank you.
