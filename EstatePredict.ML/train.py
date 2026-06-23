import pandas as pd
import numpy as np
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression
import joblib

# 1. Simulacija podataka (Kasnije ces ovo vuci iz baze ili CSV-a)
data = {
    'Kvadratura': [50, 65, 80, 45, 100, 120, 35, 75],
    'BrojSoba': [2, 2, 3, 1, 4, 4, 1, 3],
    'LokacijaId': [1, 2, 1, 2, 3, 1, 3, 2],
    'Cijena': [75000, 98000, 125000, 68000, 160000, 185000, 55000, 115000]
}

df = pd.DataFrame(data)

# 2. Podjela na inpute (X) i cilj koji predvidjamo (y)
X = df[['Kvadratura', 'BrojSoba', 'LokacijaId']]
y = df['Cijena']

# 3. Podjela na podatke za ucenje i podatke za testiranje (80% za trening, 20% za test)
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

# 4. Inicijalizacija i treniranje modela (Linearna Regresija)
model = LinearRegression()
model.fit(X_train, y_train)

# 5. Testna predikcija u kodu
probni_stan = np.array([[60, 2, 1]]) # 60 kvadrata, 2 sobe, lokacija 1
predvidjena_cijena = model.predict(probni_stan)
print(f"Procjenjena cijena za stan od 60m2 je: {predvidjena_cijena[0]:.2f} EUR")

joblib.dump(model, 'estate_model.pkl')
print("Model uspjesno istreniran i spasen kao 'estate_model.pkl'!")