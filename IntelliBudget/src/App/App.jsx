import { Routes, Route } from "react-router-dom";
import Register from "../Pages/Register/Register.jsx";
import Login from "../Pages/Login/Login.jsx";
import Home from "../Pages/Home/Home.jsx";
import Contas from "../Pages/Contas/Contas.jsx";
import Transacoes from "../Pages/Transacoes/Transacoes.jsx";
import RequireAuth from "../Components/RequireAuth.jsx";
import Metas from "../Pages/Metas/Metas.jsx";
import ChatBot from "../Pages/ChatBot/ChatBot.jsx";

function App() {
  return (
    <main className="App">
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/register" element={<Register />} />

        <Route element={<RequireAuth />}>
          <Route path="/home" element={<Home />} />
          <Route path="/contas" element={<Contas />} />
          <Route path="/transacoes" element={<Transacoes />} />
          <Route path="/metas" element={<Metas />} />
          <Route path="/chatbot" element={<ChatBot />} />
        </Route>
      </Routes>
    </main>
  );
}

export default App;