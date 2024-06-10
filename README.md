# Interactive Course Flowsheet Windows App

## Get Started
1. Download the ZIP and Extract it to your own Windows machine.

<table>
  <tr>
    <td align="center"><img src="/DemoImages/GettingStarted/GS-1.png" alt="GettingStarted_Demo_1"></td>
  </tr>
</table>

2. Open the `.sln` file.

<table>
  <tr>
    <td align="center"><img src="/DemoImages/GettingStarted/GS-2.png" alt="GettingStarted_Demo_2"></td>
  </tr>
</table>

3. Open App.Config then add and name your connectionString and enter the file path for the MSAccess Database as the Data Source. <br/>
> I.e. : <br/>
> ```shell
> add name="NameThis" connectionString="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=YourFilePathHere" providerName="System.Data.OleDb"
> ``` 
> E.g. : <br/>
> ```shell
> add name="CourseFlow" connectionString="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\CourseFlowApp\CourseFlow\MSAccessDatabase\CourseFlow.accdb" providerName="System.Data.OleDb"
> ```

<table>
  <tr>
    <td align="center"><img src="/DemoImages/GettingStarted/GS-3.1.png" alt="GettingStarted_Demo_3.1"></td>
  </tr>
  <tr>
    <td align="center"><img src="/DemoImages/GettingStarted/GS-3.2.png" alt="GettingStarted_Demo_3.2"></td>
  </tr>
</table>

> _This is the .accb File where I store the Data I’ve worked on. You can use this or copy this._

<table>
  <tr>
    <td align="center"><img src="/DemoImages/GettingStarted/GS-3.3.png" alt="GettingStarted_Demo_3.3"></td>
  </tr>
</table>

4. Open RepositoryBase.cs from the Repositories Folder then replace the defined string inside ConnectionStrings[] with your own defined string of the connection string from the previous step. <br/>
> I.e. : <br/>
> ```shell
> _connectionString = ConfigurationManager.ConnectionStrings["YourDefinedName"].ConnectionString;
> ```
> E.g. : <br/>
> ```shell
> _connectionString = ConfigurationManager.ConnectionStrings["CourseFlow"].ConnectionString;
> ```

<table>
  <tr>
    <td align="center"><img src="/DemoImages/GettingStarted/GS-4.png" alt="GettingStarted_Demo_4"></td>
  </tr>
</table>

5. Build and Run the Program. <br/>

<table>
  <tr>
    <td align="center"><img src="/DemoImages/GettingStarted/GS-5.1.png" alt="GettingStarted_Demo_5.1"></td>
  </tr>
</table>

> a. You can use the existing Users: <br/>
> Username: `admin` | Password: `admin` |  Role: Admin <br/>
> Username: `user` | Password: `user` | Role: Student <br/>
> b. Or you can create your own User by Clicking `Sign-up`.

<table>
  <tr>
    <td align="center"><img src="/DemoImages/GettingStarted/GS-5.2.png" alt="GettingStarted_Demo_5.2"></td>
  </tr>
</table>

> c. Clicking Sign up will redirect you to a separate window.

<table>
  <tr>
    <td align="center"><img src="/DemoImages/GettingStarted/GS-5.3.png" alt="GettingStarted_Demo_5.3"></td>
  </tr>
</table>

6. Login using the pre-existing accounts or your created account.

<table>
  <tr>
    <td align="center"><img src="/DemoImages/GettingStarted/GS-6.png" alt="GettingStarted_Demo_6"></td>
  </tr>
</table>

7. Upon successful login, you will be redirected to the Dashboard. <br/>
a. To test the Primary Feature of the app, click the `Course Flowsheet` from the left panel.

<table>
  <tr>
    <td align="center"><img src="/DemoImages/GettingStarted/GS-7.png" alt="GettingStarted_Demo_7"></td>
  </tr>
</table>

8. Load the Data from the Selected Course and Effective Academic Year by clicking the `Load Flowsheet` Button <br/>

<table>
  <tr>
    <td align="center"><img src="/DemoImages/GettingStarted/GS-8.png" alt="GettingStarted_Demo_8"></td>
  </tr>
</table>

> _Note:_ <br/>
>  - _Edit Mode is only available for Users with Admin roles._ <br/>
>  - _Only Course: BS Computer Engineering and Academic Year: 2022-2023 have pre-existing data. If you wish to create data for a separate course with its effective academic year, you may do so by using the Edit Mode which I’ll cover in the later part._ <br/>

9. And there you go, you can now fully enjoy the Interactive Course Flowsheet Windows App. Whether you just want to check the subject in your Bachelor’s Program or just wanted to know in a quick mouse hover how subjects relate to each other (Pre-requisite, Co-requisite, Post-requisite) in your Bachelor’s Program, I got you covered in my app.

<table>
  <tr>
    <td align="center"><img src="/DemoImages/GettingStarted/GS-9.png" alt="GettingStarted_Demo_9"></td>
  </tr>
</table>

## Entity Relationship Diagram (ERD)

<table>
  <tr>
    <td align="center"><img src="/DemoImages/ERD/ERD.png" alt="ERD"></td>
  </tr>
</table>

## Create, Read, Update, and Delete (CRUD) to the Database
1. Here’s the pre-existing database schema that involves a Bachelor’s Program.

<table>
  <tr>
    <td align="center"><img src="/DemoImages/CRUD/CRUD-1.png" alt="CRUD_1"></td>
  </tr>
</table>

2. The app Reads from the database after the Load Flowsheet Button is pressed.

3. The app only allows Create Update and Delete for the Subjects and SubjectRelationships Table. The rest will require manual modifications in the database.

4. To Create Subjects and their Relationships, click the Edit Mode (Admin Only) then click the Add Subjects Button.

<table>
  <tr>
    <td align="center"><img src="/DemoImages/CRUD/CRUD-4.png" alt="CRUD_4"></td>
  </tr>
</table>

5. A separate window will pop up and will require the necessary fields to be filled.

<table>
  <tr>
    <td align="center"><img src="/DemoImages/CRUD/CRUD-5.png" alt="CRUD_5"></td>
  </tr>
</table>

6. After filling out the fields, you can now save it to the database by clicking the Save Button. <br/>
> _Note:_ <br/>
> - _You may define here the related subjects of this subject from the existing data or you may edit it later using the Edit button._ <br/>

<table>
  <tr>
    <td align="center"><img src="/DemoImages/CRUD/CRUD-6.1.png" alt="CRUD_6.1"></td>
  </tr>
</table>

> _With no relationships_

<table>
  <tr>
    <td align="center"><img src="/DemoImages/CRUD/CRUD-6.2.png" alt="CRUD_6.2"></td>
  </tr>
</table>

> _With relationships_

7. After saving, you may continue to add subjects as it will still remain open or you may want to close it to check the newly added subject in the app.

<table>
  <tr>
    <td align="center"><img src="/DemoImages/CRUD/CRUD-7.1.png" alt="CRUD_7.1"></td>
  </tr>
  <tr>
    <td align="center"><img src="/DemoImages/CRUD/CRUD-7.2.png" alt="CRUD_7.2"></td>
  </tr>
</table>

8. Editing a subject will allow you to modify all of its data. You can also add and remove its related subjects.

<table>
  <tr>
    <td align="center"><img src="/DemoImages/CRUD/CRUD-8.png" alt="CRUD_8"></td>
  </tr>
</table>

9. The remove button will erase the Subject and its associated Related Subjects.

<table>
  <tr>
    <td align="center"><img src="/DemoImages/CRUD/CRUD-9.1.png" alt="CRUD_9.1"></td>
  </tr>
  <tr>
    <td align="center"><img src="/DemoImages/CRUD/CRUD-9.2.png" alt="CRUD_9.2"></td>
  </tr>
</table>

10. Congratulations, you can now use the app’s functionalities to accommodate your preferred Bachelor’s Program with its respective Effective Academic Year.

## Bugs?
- `create an issue`

Thank you.
