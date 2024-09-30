# Usage

The `sheet sync tool` should run first to save the data from provided sheet ID into a provided database. Once the details have been saved, the `app-ui` project can then be executed.

Note: The sheet ID and the database needs to be mentioned in the appsettings of `sheet sync tool`.

# Sheet sync tool:

* Need OAuth Client secret from google cloud with Sheet's api enabled and access provided.
* Download the client secret file after creating the OAuth Client ID.
* Once the download is complete, rename the file as 'client_secrets.json' and paste it in the project's directory.
* Configure the appsettings.json for client secret path and other properties.
* Run the tool and details will be inserted into your local database.


# BudgetAnalysisDbAPI

* Configure the `appsettings.json` here for the connection string.


# Authentication Datastore

* Navigate to the location "$env:APPDATA\BudgetAnalysis.SheetSyncer".


# Start the application:

* Run the app-ui (REACT APP).
* Run the BudgetAnalysisDbAPI project. (Make sure you have properly configured the connection string in the appsettings).
