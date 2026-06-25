import './ChatBot.css';
import { useState, useRef, useEffect, useContext } from 'react';
import Header from '../../Components/Header/Header';
import SideBar from '../../Components/SideBar/SideBar';
import useAxiosPrivate from '../../Hooks/useAxiosPrivate';

const ChatBot = () => {
    const axiosPrivate = useAxiosPrivate();
    const [mensagens, setMensagens] = useState([
        {
            role: 'assistant',
            conteudo: 'Olá! Sou o IntelliBudget AI 💰 Estou aqui para te ajudar com suas finanças. Como posso te ajudar hoje?',
        },
    ]);
    const [input, setInput] = useState('');
    const [carregando, setCarregando] = useState(false);
    const bottomRef = useRef(null);
    const inputRef = useRef(null);

    useEffect(() => {
        bottomRef.current?.scrollIntoView({ behavior: 'smooth' });
    }, [mensagens, carregando]);

    const enviar = async () => {
        const texto = input.trim();
        if (!texto || carregando) return;

        const novaMensagem = { role: 'user', conteudo: texto };
        const novaLista = [...mensagens, novaMensagem];
        setMensagens(novaLista);
        setInput('');
        setCarregando(true);

        try {
            const historico = novaLista.map((m) => ({
                Role: m.role,
                Conteudo: m.conteudo,
            }));

            const response = await axiosPrivate.post('/Mensagens', {
                Mensagem: texto,
                Historico: historico,
            });

            setMensagens((prev) => [
                ...prev,
                { role: 'assistant', conteudo: response.data.resposta },
            ]);
        } catch {
            setMensagens((prev) => [
                ...prev,
                { role: 'assistant', conteudo: '⚠️ Não consegui processar sua mensagem. Tente novamente.' },
            ]);
        } finally {
            setCarregando(false);
            inputRef.current?.focus();
        }
    };

    const handleKeyDown = (e) => {
        if (e.key === 'Enter' && !e.shiftKey) {
            e.preventDefault();
            enviar();
        }
    };

    return (
        <>
            <Header />
            <div className="layout">
                <SideBar />
                <main className="chat-main">
                    <div className="chat-container">

                        {/* Cabeçalho do chat */}
                        <div className="chat-header">
                            <div className="chat-avatar">
                                <span>🤖</span>
                            </div>
                            <div className="chat-header-info">
                                <h2 className="chat-header-name">IntelliBudget AI</h2>
                                <span className="chat-header-status">
                                    <span className="chat-status-dot" />
                                    Online
                                </span>
                            </div>
                        </div>

                        {/* Lista de mensagens */}
                        <div className="chat-messages">
                            {mensagens.map((msg, i) => (
                                <div
                                    key={i}
                                    className={`chat-bubble-row ${msg.role === 'user' ? 'chat-bubble-row--user' : 'chat-bubble-row--bot'}`}
                                >
                                    {msg.role === 'assistant' && (
                                        <div className="chat-bubble-avatar">🤖</div>
                                    )}
                                    <div className={`chat-bubble ${msg.role === 'user' ? 'chat-bubble--user' : 'chat-bubble--bot'}`}>
                                        {msg.conteudo}
                                    </div>
                                </div>
                            ))}

                            {carregando && (
                                <div className="chat-bubble-row chat-bubble-row--bot">
                                    <div className="chat-bubble-avatar">🤖</div>
                                    <div className="chat-bubble chat-bubble--bot chat-bubble--typing">
                                        <span /><span /><span />
                                    </div>
                                </div>
                            )}

                            <div ref={bottomRef} />
                        </div>

                        {/* Input */}
                        <div className="chat-input-area">
                            <textarea
                                ref={inputRef}
                                className="chat-input"
                                placeholder="Digite sua mensagem..."
                                value={input}
                                onChange={(e) => setInput(e.target.value)}
                                onKeyDown={handleKeyDown}
                                rows={1}
                                disabled={carregando}
                            />
                            <button
                                className="chat-send-btn"
                                onClick={enviar}
                                disabled={!input.trim() || carregando}
                                aria-label="Enviar mensagem"
                            >
                                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                                    <line x1="22" y1="2" x2="11" y2="13" />
                                    <polygon points="22 2 15 22 11 13 2 9 22 2" />
                                </svg>
                            </button>
                        </div>

                    </div>
                </main>
            </div>
        </>
    );
};

export default ChatBot;
