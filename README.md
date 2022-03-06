# myAspServer

# Init project
* Create project on github
* Create directory for repo on the computer
* Create ssh key on the computer
* Enter ssh key in the github account

# Architecture
Three layers:
* Context: Context of application. All technical stuff and interface to communicate outside of the application should be defined here. For example this part contains the database and API of REST service.
* Controller: It's orchastrator of the application. It's the link between context and the model.
* Model: This part contains the different functionnalities and the data structure of the application. This part is a model of the business.

## Model
The model is split by functionnalities and for each functionnalities is splitted in four parts:
* Entities: Data of the model
* Services: Actions and process on entities
* Repositories: Storage of entities (database, files, ...)
* Adapters (if needed): Adaptation of entities to access to context

# Run code coverage
* Start powershell console in Visual Studio
* Go to test project directory (myAspServerTest)
* Run command: ".\generateReport.ps1"
* Open coveragereport\index.html