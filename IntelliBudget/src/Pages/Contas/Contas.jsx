import './Contas.css';
import { useRef, useState, useEffect, useContext} from 'react';
import AuthContext from '../../Context/AuthProvider';
import { PluggyConnect } from 'react-pluggy-connect';
import axios from '../../Api/axios';
import Header from '../../Components/Header/Header';
import SideBar from '../../Components/SideBar/SideBar';
import DataList from '../../Components/DataList/DataList';
import useAxiosPrivate from '../../Hooks/useAxiosPrivate';
import { authAxios } from '../../Api/axios';

const connectTokenUrl = '/Pluggy/connect-token';
const contaBancariaUrl = '/ContasBancarias'; 
const transacaoUrl = '/Transacoes';

const Contas = () => {

    const { setAuth } = useContext(AuthContext);
    const errRef = useRef();
    const axiosPrivate = useAxiosPrivate();

    const [contas, setContas] = useState([]);
    const [banco, setbanco] = useState('');
    const [tipoConta, setTipoConta] = useState('');
    const [saldo, setSaldo] = useState('');

    const [data, setData] = useState('');
    const [valor, setValor] = useState('');
    const [categoria, setCategoria] = useState('');

    const [connectToken, setConnectToken] = useState('');

    const [errMsg, setErrMsg] = useState('');
    const [success, setSuccess] = useState(false);


    useEffect(() =>
    {
        setErrMsg('');
    }, [connectToken, banco, tipoConta, saldo])

    useEffect(() => {
        const fetchContas = async () => {
            try {
                const response = await axiosPrivate.get(contaBancariaUrl);
                setContas(response.data);
            } catch (err) {
                setErrMsg('Erro ao carregar contas');
            }
        };
        fetchContas();
    }, [axiosPrivate]);

    const handleConnect = async () =>
    {
        try {
            const response = await axiosPrivate.get(connectTokenUrl,);
            setConnectToken(response.data);
            setSuccess(true);    
        }catch (err) {
            if (!err?.response) {
                setErrMsg('No Server Response');
            } else if (err.response?.status === 401) {
                setErrMsg('Unauthorized');
            } else {
                setErrMsg('Login Failed');
            }
            errRef.current.focus();
        }
    };

    const handleAdd = async (formData) => {
        try {
            await axiosPrivate.post(contaBancariaUrl, {
                nomeBanco: formData.Banco,
                tipoConta: formData.TipoConta,
                saldo: parseFloat(formData.Saldo),
            });


            const response = await axiosPrivate.get(contaBancariaUrl);
            setContas(response.data);
        } catch (err) {
            if (!err?.response) {
                setErrMsg('No Server Response');
            } else if (err.response?.status === 400) {
                setErrMsg('Dados inválidos');
            } else if (err.response?.status === 401) {
                setErrMsg('Unauthorized');
            } else {
                setErrMsg('Erro ao cadastrar conta');
            }
            errRef.current.focus();
        }
    };

    const handleEdit = async (id, formData) => {
        try {
            await axiosPrivate.put(`${contaBancariaUrl}/${id}`, {
                nomeBanco: formData.Banco,
                tipoConta: formData.TipoConta,
                saldo: parseFloat(formData.Saldo),
            });
            const response = await axiosPrivate.get(contaBancariaUrl);
            setContas(response.data);
        } catch (err) {
            setErrMsg('Erro ao editar conta');
            errRef.current.focus();
        }
    };

    const handleDelete = async (id) => {
        try {
            await axiosPrivate.delete(`${contaBancariaUrl}/${id}`);
            const response = await axiosPrivate.get(contaBancariaUrl);
            setContas(response.data);
        } catch (err) {
            setErrMsg('Erro ao excluir conta');
            errRef.current.focus();
        }
    };


    const handleSuccess = async (itemData) => {
        try {
            const item = itemData.item;

            await authAxios().post('/ItemBancoUsuario', {
                itemId: item.id,
                status: item.status,
            });

            await authAxios().post('/ItemBancoUsuario/sync', {
                itemId: item.id,
            });

            setConnectToken('');
        } catch (err) {
            if (!err?.response) {
                setErrMsg('No Server Response');
            } else if (err.response?.status === 400) {
                setErrMsg('Dados inválidos');
            } else if (err.response?.status === 401) {
                setErrMsg('Unauthorized');
            } else {
                setErrMsg('Erro ao cadastrar conta');
            }
            errRef.current.focus();
        }
    };

    const handleError = (error) => {
        console.error('Erro ao conectar:', error);
        setConnectToken('');
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
                        title="Contas Bancárias"
                        groups={contas.map((conta) => ({
                            id: conta.id,
                            fields: [
                                { label: "Banco",         value: conta.nomeBanco },
                                { label: "Tipo de Conta", value: conta.tipoConta },
                                { label: "Saldo",         value: `R$ ${conta.saldo}` },
                            ]
                        }))}
                        formFields={[
                            { name: "Banco",     label: "Banco" },
                            { name: "TipoConta", label: "Tipo de Conta" },
                            { name: "Saldo",     label: "Saldo", type: "number" },
                        ]}
                        onAdd={handleAdd}
                        onEdit={handleEdit}
                        onDelete={handleDelete}
                    />

                    <button className='conectar-btn' onClick={handleConnect}>
                        Conectar conta
                    </button>

                    {connectToken && (
                        <PluggyConnect
                            connectToken={connectToken}
                            includeSandbox={true}
                            onSuccess={handleSuccess}
                            onError={handleError}
                        />
                    )}
                </main>
            </div>
        </>
    );
}

export default Contas;