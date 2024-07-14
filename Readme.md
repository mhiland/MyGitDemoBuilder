
# Git Demo

Project to add a specific git history to a git repo based on a pre-defined pattern.

## Getting Started
Pattern definitions are 7x10 text documents located in the PatternDefiinition folder.
These patterns represents 7 days for 10 weeks. The pattern is repeated for n number of weeks starting with a defined start date in appsettings.

Patterns use a numeric value to specify number of commits per day/block (to make use of githubs monochrome color gradient).

### Configuration
appsettings.json contains configuration settings for specifying git user, email and git directory.
StartDate must be a Monday.

## Dependencies
- dotnet add package NUnit
- dotnet add package NUnit3TestAdapter
- dotnet add package Microsoft.NET.Test.Sdk
- dotnet add package DotNetZip
- dotnet add package NodaTime

## Build
- dotnet build
- dotnet run

## GitDemo Visualizer
A python project for visualizing the contribution activity locally before pushing to github.