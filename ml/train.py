# ml/train.py

import os
import pandas as pd
import joblib

from sklearn.model_selection import train_test_split
from sklearn.compose import ColumnTransformer
from sklearn.preprocessing import OneHotEncoder
from sklearn.pipeline import Pipeline
from sklearn.linear_model import LinearRegression
from sklearn.metrics import mean_absolute_error, r2_score


# 1. Load dataset
df = pd.read_csv("data/dataset.csv")

print("Dataset preview:")
print(df.head())
print()


# 2. Define features and target
numeric_features = ["Area", "NumberOfRooms", "YearBuilt"]

categorical_features = [
    "HasParking",
    "HasLift",
    "Condition",
    "Country",
    "City",
    "Municipality",
    "PropertyType",
]

features = numeric_features + categorical_features
target = "CurrentPrice"

X = df[features]
y = df[target]


# 3. Train/test split
X_train, X_test, y_train, y_test = train_test_split(
    X, y, test_size=0.2, random_state=42
)


# 4. Preprocessing: one-hot encode categoricals, pass numeric columns through untouched
preprocessor = ColumnTransformer(
    transformers=[
        ("categorical", OneHotEncoder(handle_unknown="ignore"), categorical_features),
    ],
    remainder="passthrough",
)


# 5. Pipeline: preprocessing + model bundled together
model = Pipeline(steps=[
    ("preprocessor", preprocessor),
    ("regressor", LinearRegression()),
])


# 6. Train
model.fit(X_train, y_train)


# 7. Evaluate on held-out test data
predictions = model.predict(X_test)
mae = mean_absolute_error(y_test, predictions)
r2 = r2_score(y_test, predictions)

print("Model evaluation:")
print(f"MAE: {mae:.2f}")
print(f"R²: {r2:.4f}")


# 8. Save the fitted pipeline (preprocessing + model together)
os.makedirs("models", exist_ok=True)
joblib.dump(model, "models/estate_model.pkl")

print("\nModel saved to models/estate_model.pkl")