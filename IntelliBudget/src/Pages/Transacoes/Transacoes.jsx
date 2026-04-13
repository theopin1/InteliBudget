import './Transacoes.css';
import { useRef, useState, useEffect, useContext } from 'react';
import AuthContext from '../../Context/AuthProvider';
import axios from '../../Api/axios';
import Header from '../../Components/Header/Header';
import SideBar from '../../Components/SideBar/SideBar';
import DataList from '../../Components/DataList/DataList';

const transacaoUrl = '/Transacoes';

const Transacoes = () => {

    const { setAuth } = useContext(AuthContext);
    const errRef = useRef();

    const [transacoes, setTransacoes] = useState([]);
    const [errMsg, setErrMsg] = useState('');

    useEffect(() => {
        setErrMsg('');
    }, []);

    useEffect(() => {
        const fetchTransacoes = async () => {
            try {
                const response = await axios.get(transacaoUrl);
                setTransacoes(response.data);
            } catch (err) {
                setErrMsg('Erro ao carregar transações');
            }
        };
        fetchTransacoes();
    }, []);

    const handleAdd = async (formData) => {
        try {
            await axios.post(transacaoUrl, {
                tipo: parseInt(formData.Tipo),
                dataTransacao: formData.DataTransacao,
                valor: parseFloat(formData.Valor),
                categoriaId: parseInt(formData.CategoriaId),
                contaBancariaId: parseInt(formData.ContaBancariaId),
            });
            const response = await axios.get(transacaoUrl);
            setTransacoes(response.data);
        } catch (err) {
            if (!err?.response) {
                setErrMsg('No Server Response');
            } else if (err.response?.status === 400) {
                setErrMsg('Dados inválidos');
            } else if (err.response?.status === 401) {
                setErrMsg('Unauthorized');
            } else {
                setErrMsg('Erro ao cadastrar transação');
            }
            errRef.current.focus();
        }
    };

    const handleEdit = async (id, formData) => {
        try {
            await axios.put(`${transacaoUrl}/${id}`, {
                tipo: parseInt(formData.Tipo),
                dataTransacao: formData.DataTransacao,
                valor: parseFloat(formData.Valor),
                categoriaId: parseInt(formData.CategoriaId),
                contaBancariaId: parseInt(formData.ContaBancariaId),
            });
            const response = await axios.get(transacaoUrl);
            setTransacoes(response.data);
        } catch (err) {
            setErrMsg('Erro ao editar transação');
            errRef.current.focus();
        }
    };

    const handleDelete = async (id) => {
        try {
            await axios.delete(`${transacaoUrl}/${id}`);
            const response = await axios.get(transacaoUrl);
            setTransacoes(response.data);
        } catch (err) {
            setErrMsg('Erro ao excluir transação');
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
                        title="Transações"
                        groups={transacoes.map((transacao) => ({
                            id: transacao.id,
                            fields: [
                                { label: "Tipo",            value: transacao.tipo === 0 ? 'Receita' : 'Despesa' },
                                { label: "Data",            value: new Date(transacao.dataTransacao).toLocaleDateString('pt-BR') },
                                { label: "Valor",           value: `R$ ${transacao.valor}` },
                                { label: "Categoria",       value: transacao.categoriaId },
                                { label: "Conta Bancária",  value: transacao.contaBancariaId },
                            ]
                        }))}
                        formFields={[
                            { name: "Tipo",           label: "Tipo (0 = Receita, 1 = Despesa)", type: "number" },
                            { name: "DataTransacao",  label: "Data da Transação", type: "datetime-local" },
                            { name: "Valor",          label: "Valor", type: "number" },
                            { name: "CategoriaId",    label: "ID da Categoria", type: "number" },
                            { name: "ContaBancariaId",label: "ID da Conta Bancária", type: "number" },
                        ]}
                        onAdd={handleAdd}
                        onEdit={handleEdit}
                        onDelete={handleDelete}
                    />
                </main>
            </div>
        </>
    );
}

export default Transacoes;