import argparse
import json
import sys
import pandas as pd
import joblib

MODEL_PATH = "models/price_model.pkl"

REQUIRED_FIELDS = [
    "Area", 
    "NumberOfRooms", 
    "YearBuilt", 
    "PropertyType", 
    "Condition",      
    "LocationId", 
    "HasParking",     
    "HasLift",        
    "TargetYear"
]
_pipeline = None


def load_model():
    global _pipeline
    if _pipeline is None:
        _pipeline = joblib.load(MODEL_PATH)
    return _pipeline


def predict_price(property_data: dict) -> dict:
    # Validate incoming data
    missing = [f for f in REQUIRED_FIELDS if f not in property_data]
    if missing:
        raise ValueError(f"Nedostaju obavezna polja: {', '.join(missing)}")

    pipeline = load_model()
    
    # Extract values to compute required dynamic features
    target_year = int(property_data["TargetYear"])
    year_built = int(property_data["YearBuilt"])
    area = float(property_data["Area"])
    rooms = int(property_data["NumberOfRooms"])
    location_id = int(property_data["LocationId"])
    property_type_id = int(property_data["PropertyTypeId"])
    property_age = target_year - year_built

    feature_data = {
        "Area": area,
        "NumberOfRooms": rooms,
        "YearBuilt": year_built,
        "PropertyAge": property_age, 
        "PropertyTypeId": property_type_id,
        "LocationId": location_id,
        "Condition": property_data["Condition"],
        "HasParking": int(property_data["HasParking"]),
        "HasLift": int(property_data["HasLift"]),
        "TargetYear": target_year
    }
    
    df = pd.DataFrame([feature_data])

    # Make the prediction
    predicted_price = float(pipeline.predict(df)[0])
    
    price_per_sqm = predicted_price / area if area > 0 else 0
    confidence_score = 0.85 

    return {
        "PredictedPrice": round(predicted_price, 2),
        "PredictedPricePerSquareMeter": round(price_per_sqm, 2),
        "ConfidenceScore": confidence_score
    }


def parse_args():
    parser = argparse.ArgumentParser(
        description="Predikcija cijene nekretnine na osnovu sačuvanog modela."
    )
    parser.add_argument(
        "--json",
        type=str,
        help="Podaci o nekretnini kao JSON string."
    )
    parser.add_argument(
        "--file",
        type=str,
        help="Putanja do .json fajla sa podacima o nekretnini."
    )
    return parser.parse_args()


if __name__ == "__main__":
    args = parse_args()

    if args.file:
        with open(args.file, "r", encoding="utf-8") as f:
            input_data = json.load(f)
    elif args.json:
        input_data = json.loads(args.json)
    else:
        print(
            "Nisu proslijeđeni podaci. Koristi --json ili --file.\n"
            "Primjer: python predict.py --json "
            '\'{"Area": 65, "NumberOfRooms": 3, "YearBuilt": 2005, '
            '"PropertyTypeId": 1, "Condition": "Dobro stanje", '
            '"LocationId": 4, "HasParking": 1, "HasLift": 1, "TargetYear": 2030}\''
        )
        sys.exit(1)

    try:
        results = predict_price(input_data)
        print(f"Predviđena cijena: {results['PredictedPrice']:,.2f} KM")
    except ValueError as e:
        print(f"Greška: {e}")
        sys.exit(1)