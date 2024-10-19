# Usage and Setup:

The `sheet sync tool` should run first to save the data from provided sheet ID into a provided database. 
Once the details have been saved, the `app-ui` project can then be executed along with `BudgetAnalysisDbApi`.

Note: The sheet ID and the database needs to be mentioned in the appsettings of `sheet sync tool`.


## Sheet sync tool:

* Need OAuth Client secret from google cloud with Sheet's api enabled and access provided.
* Download the client secret file after creating the OAuth Client ID.
* Once the download is complete, rename the file as `client_secrets.json` and paste it in the project's directory.
* Configure the `appsettings.json` for client secret path and other properties.
* Run the tool and details will be inserted into your local database.
* Careful while re-running this tool, since the data needs to be deleted first from the local database before inserting again.


## BudgetAnalysisDbAPI

* Configure the `appsettings.json` here for the connection string.


## Authentication Datastore

* This location stores your token received by the authorization server (this location is sensitive).
* Navigate to the location `$env:APPDATA\BudgetAnalysis.SheetSyncer` from PowerShell.
* Or, Navigate to the loation `%APPDATA%\BudgetAnalysis.SheetSyncer` from File Explorer.


## Start the application:

* Run the `SheetSyncTool` to synchronize data from Cloud to your local database.
* Run the `BudgetAnalysisDbAPI` project. (Make sure you have properly configured the `appsettings.json`).
* Run the `app-ui` (REACT APP).


## Spreadsheet Structure followed:

#### Columns:

* Mandatory Expense Type (Column: A)
* Mandatory Expense Cost (Column: B)
* Mandatory Expense Total (Column: D)
* Optional Expense Total (Column: E)
* Optional Expense Type (Column: G)
* Optional Expense Cost (Column: F)

**Note:** The column names can be different but not the positions.

#### Worksheet:

* A worksheet is named in the name of months.
* A spreadsheet (entire excel sheet) should not contain more than 12 worksheets (i.e., 12 months).
* A spreadsheet is specific to a single year.