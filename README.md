# Virtual Pet Age Tracker

## About
Virtual pet toys are handheld games that task the player with caring for and raising a digital pet or creature. Though most iterations have a set lifespan for these pets, some will allow you to keep the game running for as long as you can keep your pet happy and healthy.

An example of the above are the Giga Pets virtual pets, which were rebooted in 2018. Unless their health score drops to zero, these pets can live forever. However, the age counter will stop increasing after 100 years (or 250 years for the more advanced models).

That means you will lose the ability to track your pet's age after approximately three months (as most pets display days passed in years).

This application is designed to help log and keep track of your pets' ages. Input your pet's name, type, and birthday to save it to your computer. The application will then display your pet's age when prompted.

Because the app follows the traditional passage of time (days = days, years = years), it can be used to keep track of non-virtual pets as well. Some sample pets have been included to demonstrate this feature and can be removed if so desired.

## Initial Setup for the Pre-Release Version

### Database
The database for this project was designed in Azure Data Studio, connecting to a Microsoft Sql Server image running in a Docker container. This works for MacOS users like myself, but if you have a Windows PC, you can initialize the included database build script in Microsoft Sql Server Management Studio natively, without creating a container. The database build script is located in the following directory: **_virtual-pet-age-tracker_WEB > Server > database_**

- Windows users should use Sql Express and the ConnectionStrings property that is, by default, commented-out in the **_virtual-pet-age-tracker_WEB > Server > Vpat > appsettings.json_** file.

- MacOS users can follow [this video tutorial](https://youtu.be/BVNWRYPv78o?si=_k999-NOUbVgILvi) for information on how to set up an MSSQL image in a Docker container and connect to it via Azure Data Studio. Make sure to use the login credentials from the active ConnectionStrings property in the **_Server > Vpat > appsettings.json_** file.

### Server
Visual Studio for Windows users can run the server application using SQL Express. MacOS users can use Visual Studio Code and install the C# Dev Kit extension. It includes a guided experience that details how to run your .NET server applications in VSCode.

Each time you run the server-side application, a new tab in your default browser will open and list the app's endpoints in Swagger. Keep this window open to keep the server running.

### Client
This project requires Node, which can be downloaded [here.](https://nodejs.org/en/) Make sure you have it installed before continuing. In your IDE, open a terminal window and navigate to the Client folder in the project directory. Run this command: `npm run dev` and `CTRL + LMB` on the Localhost URL that is provided by the terminal. This will launch the client application as a new tab in your default browser.

## Using the App:
Launching the app will take you to a home page with a navigation menu at the top of the screen. This menu is present on all pages. From here you can create an account, login or logout, contact support, and access your user page. If the user is an administrator, they also have access to the Admin Page.

### User Page
At this time, the main functionality of the user page is to manage the current user's pets. The app will display pertinent information about each pet, as well as provide options to modify or delete each entry. New entries can be created with the _Add New Pet_ button. Adding a date of pet death by clicking the _R.I.P_ button will cause the age counter to stop increasing for that pet, saving its age as what it was at its time of passing.

## Technologies Used:
- C#/.NET
- JavaScript (Vue.js 3)
- CSS (Bulma.io)
- Axios, RestSharp
- Node.js
- Microsoft SQL
- Visual Studio Code
- Azure Data Studio
- Docker

### To Do:

- Enable the deletion of individual pets by user
- Pass exceptions to front end as error messages
    - Custom message for new pet error where userID is not defined, ask user to log out and log back in again to retry 
- Create an admin page that fetches a list of hidden users and pets from the database
- Give admins the ability to make a Delete HTTP request to permanently remove hidden users and hidden pets from the database
- Create a support page that allows users to fill out a form that emails an admin account without exposing the contact address
- Host in Azure Cloud

### ~~Done:~~

- Add an R.I.P form that opens in a modal to input the dateDeath property
- Add a new pet form that opens in a modal
- Fix date and time format
- Add required fields to forms
- Add an edit pet form that opens in a modal
- Indicate that edit pet/add pet was successful
