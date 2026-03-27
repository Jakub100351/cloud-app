import { useState, useEffect } from 'react'
import axios from 'axios'
import reactLogo from './assets/react.svg'

function App() {
  const [dataList, setDataList] = useState([])
  const [newTaskName, setNewTaskName] = useState("") // Stan dla nowego zadania

  // Funkcja pobierająca zadania
  const fetchTasks = () => {
    const apiUrl = import.meta.env.VITE_API_URL;
    axios.get(apiUrl)
      .then(response => setDataList(response.data))
      .catch(error => console.error("Błąd API:", error));
  };

  useEffect(() => {
    fetchTasks(); // Pobiera zadania przy starcie
  }, [])

  // Funkcja dodająca nowe zadanie (Do zadania 5.4!)
  const handleAddTask = () => {
    if (!newTaskName) return; // Jak puste to nie dodajemy
    const apiUrl = import.meta.env.VITE_API_URL;
    
    // Wysyłamy posta do bazy
    axios.post(apiUrl, { name: newTaskName })
      .then(() => {
        setNewTaskName(""); // Czyścimy okienko po dodaniu
        fetchTasks(); // Odświeżamy listę, żeby pokazać nowe zadanie!
      })
      .catch(error => console.error("Błąd dodawania:", error));
  };

  return (
    <div style={{ padding: '20px', fontFamily: 'Arial, sans-serif', display: 'flex', flexDirection: 'column', alignItems: 'center', backgroundColor: '#f4f7f6', minHeight: '100vh' }}>
      
      {/* NAGŁÓWEK - Poprawione marginesy */}
      <div style={{ textAlign: 'center', marginBottom: '30px', marginTop: '20px' }}>
        <img src={reactLogo} className="logo react" alt="React logo" style={{ height: '80px', marginBottom: '15px' }} />
        {/* Dałem <br/> żeby tekst przeszedł ładnie do nowej linii i się nie zlewał */}
        <h1 style={{ color: '#2c3e50', margin: '0 0 10px 0', fontSize: '2rem', lineHeight: '1.3' }}>
          projekt testowy nr1,<br/>gratuluje witamy na mojej stronie :)
        </h1>
        <p style={{ color: '#7f8c8d', margin: '0' }}>Podgląd danych z Twojej własnej bazy w Dockerze</p>
      </div>

      {/* FORMULARZ DO DODAWANIA (ZADANIE 5.4) */}
      <div style={{ marginBottom: '20px', display: 'flex', gap: '10px' }}>
        <input 
          type="text" 
          value={newTaskName} 
          onChange={(e) => setNewTaskName(e.target.value)} 
          placeholder="Wpisz nowe zadanie..."
          style={{ padding: '10px', width: '250px', borderRadius: '5px', border: '1px solid #ccc' }}
        />
        <button 
          onClick={handleAddTask}
          style={{ padding: '10px 20px', backgroundColor: '#3498db', color: 'white', border: 'none', borderRadius: '5px', cursor: 'pointer', fontWeight: 'bold' }}
        >
          Dodaj zadanie
        </button>
      </div>

      {/* LISTA ZADAŃ Z BAZY */}
      <div style={{ width: '100%', maxWidth: '600px', backgroundColor: '#ffffff', padding: '20px', borderRadius: '8px', boxShadow: '0 4px 8px rgba(0,0,0,0.1)' }}>
        <ul style={{ listStyleType: 'none', padding: '0', margin: '0' }}>
          {dataList.length > 0 ? (
            dataList.map((item, index) => (
              <li key={index} style={{ padding: '15px', borderBottom: '1px solid #eee', color: '#333', fontSize: '1.1rem' }}>
                <strong>Zadanie:</strong> {item.name}
              </li>
            ))
          ) : (
            <li style={{ textAlign: 'center', color: '#e74c3c', padding: '20px' }}>
              Brak danych w bazie (Baza czyściutka!)
            </li>
          )}
        </ul>
      </div>
    </div>
  )
}

export default App