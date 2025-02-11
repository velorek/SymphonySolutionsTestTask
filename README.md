# Test task for AQA position with C# (SymphonySolutions)

## Overview

This solution demonstrates automated testing sample of UI functionality with C#.NET and Selenium using Page Object model design pattern.

## Preconditions

- .NET 8.0
- Allure Report (optional)

## Run locally

1. Checkout this repository
2. Build the project with `PROD` configuration

```Bash
dotnet build .\SymphonySolutionsTestTask.sln -c PROD
```

3. Run tests

```Bash
dotnet test .\SymphonySolutionsTestTask.sln -c PROD
```

4. Generate Allure report (optional)

```Bash
allure generate -c
allure open
```

## Run in Github Actions

## Author

Serhii Shatalov
