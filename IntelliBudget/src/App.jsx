import { Routes, Route } from "react-router-dom";
import Register from "./Register.jsx";
import Login from "./Login.jsx";

function App() {
  return (
    <main className="App">
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/register" element={<Register />} />
      </Routes>
    </main>
  );
}

export default App;