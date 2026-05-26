import './Transacoes.css';
import { useRef, useState, useEffect, useContext } from 'react';
import AuthContext from '../../Context/AuthProvider';
import useAxiosPrivate from '../../Hooks/useAxiosPrivate';
import Header from '../../Components/Header/Header';
import SideBar from '../../Components/SideBar/SideBar';
import DataList from '../../Components/DataList/DataList';

const transacaoUrl = '/Transacoes';
const categoriaUrl = '/Categoria';
const contaUrl = '/ContasBancarias';

const Transacoes = () => {

    const { setAuth } = useContext(AuthContext);
    const errRef = useRef();
    const axiosPrivate = useAxiosPrivate();

    const [categorias, setCategorias] = useState([]);
    const [contas, setContas] = useState([]);
    const [transacoes, setTransacoes] = useState([]);
    const [errMsg, setErrMsg] = useState('');

    useEffect(() => {
        setErrMsg('');
    }, []);

    useEffect(() => {
        const fetchDados = async () => {
            try {
                const [resTransacoes, resCategorias, resContas] = await Promise.all([
                    axiosPrivate.get(transacaoUrl),
                    axiosPrivate.get(categoriaUrl),
                    axiosPrivate.get(contaUrl),
                ]);
                setTransacoes(resTransacoes.data);
                setCategorias(resCategorias.data);
                setContas(resContas.data);
                console.log('categorias:', resCategorias.data);
            } catch (err) {
                setErrMsg('Erro ao carregar dados');
            }
        };
        fetchDados();
    }, [axiosPrivate]);

    const handleAdd = async (formData) => {
        try {
        const payload = {
            tipo: parseInt(formData.Tipo),
            dataTransacao: formData.DataTransacao,
            valor: parseInt(formData.Valor),
            categoriaId: parseInt(formData.CategoriaId),
            contaBancariaId: parseInt(formData.ContaBancariaId),
        };
        await axiosPrivate.post(transacaoUrl, payload);
        const response = await axiosPrivate.get(transacaoUrl);
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
        console.log('formData:', formData); 
        try {
            await axiosPrivate.put(`${transacaoUrl}/${id}`, {
                tipo: parseInt(formData.Tipo),
                dataTransacao: formData.DataTransacao,
                valor: parseInt(formData.Valor),
                categoriaId: formData.CategoriaId ? parseInt(formData.CategoriaId) : null,
                contaBancariaId: parseInt(formData.ContaBancariaId),
            });
            const response = await axiosPrivate.get(transacaoUrl);
            setTransacoes(response.data);
        } catch (err) {
            setErrMsg('Erro ao editar transação');
            errRef.current.focus();
        }
    };

    const handleDelete = async (id) => {
        try {
            await axiosPrivate.delete(`${transacaoUrl}/${id}`);
            const response = await axiosPrivate.get(transacaoUrl);
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
                        groups={[...transacoes]
                            .sort((a, b) => new Date(b.dataTransacao) - new Date(a.dataTransacao))
                            .map((transacao) => ({
                            id: transacao.id,
                            rawValues: {
                                Tipo: transacao.tipo,
                                DataTransacao: transacao.dataTransacao,
                                Valor: transacao.valor,
                                CategoriaId: transacao.categoriaId,
                                ContaBancariaId: transacao.contaBancariaId,
                            },
                            fields: [
                                { label: "Tipo",           value: transacao.tipo === 0 ? 'Receita' : 'Despesa' },
                                { label: "Data",           value: new Date(transacao.dataTransacao).toLocaleDateString('pt-BR') },
                                { label: "Valor",          value: `R$ ${transacao.valor}` },
                                { label: "Categoria",      value: transacao.categoria?.nome },
                                { label: "Conta Bancária", value: transacao.contaBancaria?.nomeBanco },
                            ]
                        }))}
                        formFields={[
                            { name: "Tipo", label: "Tipo", type: "select", options: [
                                { value: 0, label: "Receita" },
                                { value: 1, label: "Despesa" },
                            ]},
                            { name: "DataTransacao", label: "Data", type: "datetime-local" },
                            { name: "Valor", label: "Valor", type: "number" },
                            { name: "CategoriaId", label: "Categoria", type: "select",
                            options: categorias.map(c => ({ value: c.id, label: c.nome }))
                            },
                            { name: "ContaBancariaId", label: "Conta Bancária", type: "select",
                            options: contas.map(c => ({ value: c.id, label: c.nomeBanco }))
                            },
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