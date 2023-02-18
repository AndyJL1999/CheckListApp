<h1>CheckListApp</h1>

<p>
A WPF app for creating and keeping track of tasks. Create Canvases, create TaskBoards, and then list out your tasks.
</p>

<h2>Didn't want to call it a ToDo List app</h2>

<p>
I took inspiration from Trello and decided to make a checklist app. A user will be able to sign up and log-in, then they can create canvases that will contain multiple taskboards, that can hold multiple tasks. The tasks can be marked as Done, In-Progress, or Not-Started. 
</p>

<h3>Start-Up View</h3>
<img src="https://user-images.githubusercontent.com/88408654/219816714-badc75b7-e6b2-491f-89bf-e4199185c0b0.png"/>
<img src="https://user-images.githubusercontent.com/88408654/219816856-a54c75af-01bf-4436-93c2-48d61a0dafdb.png"/>

<h2>Working with Entity Framework Core</h2>

<p>
Unlike my last <a href="https://github.com/AndyJL1999/FictionHoarder">project</a>, I decided to use EF Core as my ORM. I've worked with it before and am familiar with it. I took a code-first approach when it came to the database and entity creation. One-to-many realtionships are prevelant in this project with one entity holding a list of other entities.
</p>

<h2>Too packed?</h2>

<p>
This project is made up of two separate projects templates; a WPF app and a WebAPI app. The API holds the entities and EF Core methods while the WPF app holds all UI elements. I could separate some of their resources into separate class libraries but decided to leave it be because of some previous projects I worked on that held a similiar structure.
</p>

<h2>API</h2>

<p>
The API has 3 main controllers; AuthController, UserController, and CheckListController. Each has access to their own repository that holds the methods that use EF Core to GET, PUT, POST, and DELETE data from the database. I am looking to move from the repository pattern into the Unit-of-Work pattern. 
</p>

<h2>WPF</h2>

<p>
The WPF app has 3 main views; StartUpView, AccountView, and CanvasView. Each has their own view model. There are also ActionViewModels that support ActionViews that take care of creating and editing entities. The entities used in the UI are DisplayModels that mirror the actual entities that are within the API.

There are also event aggregators that help ActionViews communicate changes to MainViews. These are held within a 'Resources' folder that contain Events, Interfaces, and Helpers. The API is accessed through the 'ApiHelper' and 'CheckListEndpoint'.

Through dependency injection one instance of 'ApiHelper' is maintained and injected into the MainViewModels and ActionViewModels to manipulate the entities.
</p>

<h3>Account View</g3>
<img src="https://user-images.githubusercontent.com/88408654/219822654-6f654c93-3c91-44bc-b37e-6265edfbfda5.png"/>

<h3>Canvas View</h3>
<img src="https://user-images.githubusercontent.com/88408654/219822751-b6b34f03-a6bf-4861-b105-a63d0b0efcec.png"/>

<h3>Edit Account ActionView</h3>
<img src="https://user-images.githubusercontent.com/88408654/219822944-10e7aac7-6fc3-4955-b4ab-9cddb9168f4c.png"/>

<h2>The End.. for now</h2>
As of right now I can say that this project meets all the requirements I set out for it when I began. There is another feature I've been thinking of adding but I decided that it can wait. For now, I can set this project down and start thinking about the next one.
<p>

</p>
