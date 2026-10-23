import joblib
import pandas as pd


model = joblib.load("models/estate_model.pkl")


def predict_price(
    area: float,
    number_of_rooms: float,
    year_built: int,
    has_parking: bool,
    has_lift: bool,
    condition: str,
    country: str,
    city: str,
    municipality: str,
    property_type: str
) -> float:

    # Priprema podataka u istom formatu kao properties.csv
    property_data = pd.DataFrame([{
        "Area": area,
        "NumberOfRooms": number_of_rooms,
        "YearBuilt": year_built,
        "HasParking": "Yes" if has_parking else "No",
        "HasLift": "Yes" if has_lift else "No",
        "Condition": condition,
        "Country": country,
        "City": city,
        "Municipality": municipality,
        "PropertyType": property_type
    }])

    predicted_price = model.predict(property_data)[0]

    return round(float(predicted_price), 2)


# Samostalni test
if __name__ == "__main__":
    price = predict_price(
        area=85,
        number_of_rooms=3.5,
        year_built=2015,
        has_parking=True,
        has_lift=True,
        condition="Good Condition",
        country="Bosnia and Herzegovina",
        city="Sarajevo",
        municipality="Centar",
        property_type="Apartment"
    )

    print(f"Predicted price: {price:,.2f}")