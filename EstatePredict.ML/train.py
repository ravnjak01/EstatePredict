import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import OneHotEncoder
from sklearn.compose import ColumnTransformer
from sklearn.pipeline import Pipeline
from sklearn.ensemble import RandomForestRegressor
from sklearn.metrics import mean_absolute_error, mean_squared_error, r2_score
import joblib
import os

df = pd.read_csv("data/properties.csv")

#
x = df.drop(columns=["Id", "CurrentPrice", "PricePerSquareMeter"])
y = df["CurrentPrice"]

numeric_features = [
    "Area",
    "NumberOfRooms",
    "YearBuilt"
]

categorical_features = [
    "PropertyType",
    "Condition",
    "Country",
    "City",
    "Municipality"
]

boolean_features = [
    "HasParking",
    "HasLift"
]

preprocessor = ColumnTransformer(
    transformers=[
        ("num", "passthrough", numeric_features + boolean_features),
        ("cat", OneHotEncoder(handle_unknown="ignore"), categorical_features)
    ]
)

model = RandomForestRegressor(n_estimators=200, random_state=42)

pipeline = Pipeline(steps=[
    ("preprocessor", preprocessor),
    ("model", model)
])

x_train, x_test, y_train, y_test = train_test_split(
    x, y, test_size=0.2, random_state=42
)

pipeline.fit(x_train, y_train)

y_pred = pipeline.predict(x_test)

r2 = r2_score(y_test, y_pred)
mae = mean_absolute_error(y_test, y_pred)
rmse = mean_squared_error(y_test, y_pred) ** 0.5

print(f"R2 score: {r2:.4f}")
print(f"MAE: {mae:,.2f} KM")
print(f"RMSE: {rmse:,.2f} KM")

os.makedirs("models", exist_ok=True)
joblib.dump(pipeline, "models/price_model.pkl")
print("Model uspješno sačuvan u models/price_model.pkl")