# EstatePredict

EstatePredict is a real estate price prediction application that estimates property prices based on property characteristics and location.

## Features

- Predict property prices based on area, number of rooms, year built, condition, and other features.
- Select locations and property types from the database.
- View the estimated property price and price per square meter.

## Tech Stack

- **Frontend:** React, TypeScript
- **Backend:** ASP.NET Core Web API, Entity Framework Core
- **Database:** SQL Server
- **Machine Learning:** Python, scikit-learn, FastAPI

## How It Works

The React frontend sends property details to the .NET API, which communicates with a Python ML service to generate a price prediction using a trained machine learning model.

## Project Status

**In Development** — EstatePredict is currently under construction. New features, improvements, and model accuracy enhancements are still in progress.

## Future Improvements

- Improve prediction accuracy with a larger dataset.
- Add user authentication and prediction history.
- Enhance the user interface and validation.
