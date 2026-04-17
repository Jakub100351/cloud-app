# cloud-app by Jakub Kołodyński, 100351

## Opis projektu
Aplikacja backendowa stworzona w technologii .NET 8, wdrożona w chmurze AWS.  
Projekt wykorzystuje AWS Secrets Manager do przechowywania danych wrażliwych oraz GitHub Actions do automatyzacji procesu CI/CD.

---

## Architektura (AWS)

| Warstwa | Komponent | Usługa |
|--------|----------|--------|
| **Application** | API (.NET 8) | AWS Elastic Beanstalk |
| **Secrets** | Connection String | AWS Secrets Manager |
| **CI/CD** | Pipeline | GitHub Actions |
| **Data** | SQL Server | AWS RDS / lokalnie |

---

## Funkcjonalności

- Pobieranie danych z bazy (GET)
- Dodawanie danych (POST)
- Usuwanie danych (DELETE)
- Ukrycie connection stringa (Secrets Manager)
- Testy jednostkowe (xUnit)
- Automatyczny build i testy (CI/CD)

---

## Endpointy API

- `GET /api/tasks`
- `POST /api/tasks`
- `DELETE /api/tasks/{id}`

---

## Testy

Projekt zawiera test jednostkowy:

- Sprawdza, czy nowy task nie jest ukończony (`IsCompleted == false`)

Uruchomienie testów:
```bash
dotnet test