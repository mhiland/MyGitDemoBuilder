
# Git Demo

Project to add a specific git history to a git repo based on a pre-defined pattern.

## Getting Started
Pattern definitions are 7x10 text documents located in the PatternDefiinition folder.
These patterns are expanded to 7x50 patterns to mimic a 7day, 50 week history graph in github, over the span of 1 year from now.

Patterns use a numeric value to specify number of commits per block (to make use of githubs monochrome color gradient).

### Configuration
App.config contains configuration settings for specifying git user, email and git directory.

## Build
dotnet add package DotNetZip
dotnet add package NodaTime
dotnet build
dotnet run