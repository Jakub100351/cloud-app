import { useState, useEffect } from 'react'
import axios from 'axios'

function App() {
  const [dataList, setDataList] = useState([])

  useEffect(() => {
    // Pobieramy URL ze zmiennych środowiskowych (Zadanie 3.3)
    const apiUrl = import.meta.env.VITE_API_URL;

    // Strzał GET przez Axios
    axios.get(apiUrl)
      .then(response => {
        setDataList(response.data);
      })
      .catch(error => {
        console.error("Błąd API:", error);
      });
  }, [])

  return (
    <div style={{ padding: '20px' }}>
      <h1>Lista danych z API</h1>
      <ul>
        {dataList.length > 0 ? (
          dataList.map((item, index) => (
            <li key={index}>{item.name}</li>
          ))
        ) : (
          <p>Brak danych lub ładowanie...</p>
        )}
      </ul>
    </div>
  )
}

export default App