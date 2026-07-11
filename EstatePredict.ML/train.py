import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import OneHotEncoder
from sklearn.compose import ColumnTransformer
from sklearn.pipeline import Pipeline
from sklearn.ensemble import RandomForestRegressor
import joblib
import os
df = pd.read_csv('data/properties.csv')


x = df.drop("CurrentPrice", axis=1)
y = df["CurrentPrice"]

numeric_features = [
    "Area",
    "NumberOfRooms",
    "YearBuilt"
]

categorical_features = [
    "PropertyType",
    "Condition",
    "Drzava",
    "Grad",
    "Opcina"
]

boolean_features = [
    "HasParking",
    "HasLift"
]

preprocessor = ColumnTransformer(
    transformers=[
        ("num", "passthrough", numeric_features),
        ("cat", OneHotEncoder(handle_unknown="ignore"), categorical_features)
    ]
)

model = RandomForestRegressor(n_estimators=100, random_state=42)

pipeline = Pipeline(steps=[
    ("preprocessor", preprocessor),
    ("model", model)
])

x_train, x_test, y_train, y_test = train_test_split(
    x, y, test_size=0.2, random_state=42
)

pipeline.fit(x_train, y_train)

score = pipeline.score(x_test, y_test)
print(f"Model score: {score}")

os.makedirs("models", exist_ok=True)

joblib.dump(pipeline, "models/price_model.pkl")
print("Model uspješno sačuvan u models/price_model.pkl")
print("Model saved to models/price_model.pkl")