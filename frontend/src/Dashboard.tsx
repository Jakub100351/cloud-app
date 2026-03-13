import { useState, useEffect } from 'react';
import axios from 'axios';

export default function Dashboard() {
  const [data, setData] = useState([]);
  const [error, setError] = useState('');

  useEffect(() => {
    // ZADANIE 4.1 i 4.2: Odpytywanie backendu na porcie 8081
    axios.get('http://localhost:8081/api/tasks')
      .then(response => {
        setData(response.data);
        setError(''); // Wszystko git, czyścimy błędy
      })
      .catch(err => {
        // ZADANIE 4.4: Walidacja i wyłapywanie odpowiednich statusów błędów API
        if (err.response) {
            // Serwer odpowiedział błędem np. 400, 404, 500
            setError(`Błąd z API! Status: ${err.response.status}`);
        } else {
            // Brak połączenia (serwer wyłączony)
            setError('Nie można połączyć się z API. Sprawdź kontener na porcie 8081.');
        }
      });
  }, []);

  return (
    <div style={{ padding: '20px' }}>
      <h2>Dashboard Zadań</h2>
      
      {/* Wyświetlenie ładnego komunikatu błędu z walidacji */}
      {error && <div style={{ color: 'red', fontWeight: 'bold', marginBottom: '15px' }}>{error}</div>}

      <ul>
        {data.length > 0 ? (
          data.map((item: any) => <li key={item.id}>{item.title}</li>)
        ) : (
          <p>Brak zadań w bazie.</p>
        )}
      </ul>
    </div>
  );
}