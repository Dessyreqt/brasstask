# brasstask
A task-scheduling and notification system designed to explore engineering ideas.

## Code License

Note that there is no license for this project. This code isn't meant to be forked, cloned, etc. It is purely a demonstration of what I can build and an exploration for me to learn new things.

## Setting up the project

Once the code is cloned down, run install-tools.ps1. This will install the NUKE tool globally if you don't have it and update it if you do. Then it will run the nuke target to install the remaining tools, which are installed in the `./bin` path.

Development of this project is only tested on Windows and may not work on other operating systems.

### C# API specific setup

The C# API Project requires the use of user secrets. To set this up, run `nuke setup-user-secrets`. This will determine which secrets need to be set and provide default values for them. Once this is run, in order to have full functionality of the application, you will need to set the following secrets:

- `Serilog:WriteTo:2:Args:apiKey` - This is the API key for the BrassTask API in Seq. This is used to log to Seq.

## Setting up the database

The database can be setup by running `nuke restore-mssql`. This will run RoundhousE, a command-line-based database migration software. For testing purposes, it may make sense to quickly drop and recreate the database. This can be done by running `nuke drop-and-restore-mssql`.
