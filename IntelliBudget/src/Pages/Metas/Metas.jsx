import './Metas.css';
import { useRef, useState, useEffect, useContext } from 'react';
import AuthContext from '../../Context/AuthProvider';
import useAxiosPrivate from '../../Hooks/useAxiosPrivate';
import Header from '../../Components/Header/Header';
import SideBar from '../../Components/SideBar/SideBar';
import DataList from '../../Components/DataList/DataList';

const metasUrl = '/Metas';

const Metas = () => {
    const { setAuth } = useContext(AuthContext);
    const errRef = useRef();
    const axiosPrivate = useAxiosPrivate();

    const [metas, setMetas] = useState([]);
    const [errMsg, setErrMsg] = useState('');

    useEffect(() => {
        setErrMsg('');
    }, []);

    useEffect(() => {
        const fetchDados = async () => {
            try {
                const response = await axiosPrivate.get(metasUrl);
                setMetas(response.data);
            } catch (err) {
                setErrMsg('Erro ao carregar metas');
            }
        };
        fetchDados();
    }, [axiosPrivate]);

    const handleAdd = async (formData) => {
        if (!formData.Nome || !formData.Descricao || !formData.ValorAtual || !formData.ValorAlvo || !formData.Prazo) {
            setErrMsg('Preencha todos os campos obrigatórios');
            errRef.current.focus();
            return;
        }
        try {
            await axiosPrivate.post(metasUrl, {
                nome: formData.Nome,
                descricao: formData.Descricao,
                valorAtual: parseFloat(formData.ValorAtual),
                valorAlvo: parseFloat(formData.ValorAlvo),
                prazo: new Date(formData.Prazo).toISOString(),
            });
            const response = await axiosPrivate.get(metasUrl);
            setMetas(response.data);
            setErrMsg('');
        } catch (err) {
            if (!err?.response) {
                setErrMsg('No Server Response');
            } else if (err.response?.status === 400) {
                setErrMsg('Dados inválidos');
            } else if (err.response?.status === 401) {
                setErrMsg('Unauthorized');
            } else {
                setErrMsg('Erro ao cadastrar meta');
            }
            errRef.current.focus();
        }
    };

    const handleEdit = async (id, formData) => {
        try {
            await axiosPrivate.put(`${metasUrl}/${id}`, {
                nome: formData.Nome,
                descricao: formData.Descricao,
                valorAtual: parseFloat(formData.ValorAtual),
                valorAlvo: parseFloat(formData.ValorAlvo),
                prazo: new Date(formData.Prazo).toISOString(),
            });
            const response = await axiosPrivate.get(metasUrl);
            setMetas(response.data);
            setErrMsg('');
        } catch (err) {
            setErrMsg('Erro ao editar meta');
            errRef.current.focus();
        }
    };

    const handleDelete = async (id) => {
        try {
            await axiosPrivate.delete(`${metasUrl}/${id}`);
            const response = await axiosPrivate.get(metasUrl);
            setMetas(response.data);
        } catch (err) {
            setErrMsg('Erro ao excluir meta');
            errRef.current.focus();
        }
    };

    return (
        <>
            <Header />
            <div className="layout">
                <SideBar />
                <main className="main-content">
                    <p ref={errRef} className={errMsg ? "errmsg" : "offscreen"}
                        aria-live="assertive">{errMsg}</p>

                    <DataList
                        title="Metas"
                        groups={[...metas]
                            .sort((a, b) => new Date(a.prazo) - new Date(b.prazo))
                            .map((meta) => ({
                                id: meta.id,
                                rawValues: {
                                    Nome: meta.nome,
                                    Descricao: meta.descricao,
                                    ValorAtual: meta.valorAtual,
                                    ValorAlvo: meta.valorAlvo,
                                    Prazo: meta.prazo,
                                },
                                fields: [
                                    { label: "Nome",        value: meta.nome },
                                    { label: "Descrição",   value: meta.descricao },
                                    { label: "Valor Atual", value: `R$ ${meta.valorAtual?.toFixed(2)}` },
                                    { label: "Valor Alvo",  value: `R$ ${meta.valorAlvo?.toFixed(2)}` },
                                    { label: "Prazo",       value: new Date(meta.prazo).toLocaleDateString('pt-BR') },
                                    {
                                        fullWidth: true,
                                        render: () => {
                                            const pct = meta.valorAlvo > 0
                                                ? Math.min(100, (meta.valorAtual / meta.valorAlvo) * 100)
                                                : 0;
                                            const color = pct >= 80
                                                ? '#22c55e'
                                                : pct >= 50
                                                ? '#a855f7'
                                                : '#3b82f6';
                                            return (
                                                <div className="meta-progress-wrapper">
                                                    <div className="meta-progress-header">
                                                        <span className="meta-progress-label">Progresso</span>
                                                        <span className="meta-progress-pct" style={{ color }}>
                                                            {pct.toFixed(1)}%
                                                        </span>
                                                    </div>
                                                    <div className="meta-progress-track">
                                                        <div
                                                            className="meta-progress-fill"
                                                            style={{ width: `${pct}%`, background: color }}
                                                        />
                                                    </div>
                                                </div>
                                            );
                                        },
                                    },
                                ],
                            }))}
                        formFields={[
                            { name: "Nome",       label: "Nome",        type: "text" },
                            { name: "Descricao",  label: "Descrição",   type: "text" },
                            { name: "ValorAtual", label: "Valor Atual", type: "number" },
                            { name: "ValorAlvo",  label: "Valor Alvo",  type: "number" },
                            { name: "Prazo",      label: "Prazo",       type: "date" },
                        ]}
                        onAdd={handleAdd}
                        onEdit={handleEdit}
                        onDelete={handleDelete}
                    />
                </main>
            </div>
        </>
    );
};

export default Metas;