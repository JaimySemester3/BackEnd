### Appsettings

Om toegang te krijgen tot de database, gebruikt de applicatie een appsettings.json file. Deze wordt niet gepushed naar Github, 
en moet handmatig worden toegevoegd. Dit moet in de volgende format:

{
  "TaskDatabase": {
    "ConnectionString": "{Voeg MongoDB connectionstring toe}",
    "DatabaseName": "{Voeg database naam toe}",
    "TasksCollectionName": "{Voeg collectie naam toe}"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}