import { useState, useEffect } from 'react';
import { PieChart, Pie, Cell, Tooltip, Legend } from 'recharts';
import useAxiosPrivate from '../../Hooks/useAxiosPrivate';
import './Dashboard.css';

const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042', '#A28DFF', '#FF6B6B', '#4ECDC4'];

const transacaoUrl = '/Transacoes';
const categoriaUrl = '/Categoria';

const Dashboard = () => {

    const [categorias, setCategorias] = useState([]);
    const [transacoes, setTransacoes] = useState([]);
    const [errMsg, setErrMsg] = useState('');
    const axiosPrivate = useAxiosPrivate();

    useEffect(() => {
        const fetchDados = async () => {
            try {
                const response = await axiosPrivate.get(transacaoUrl);
                setTransacoes(response.data);
            } catch (err) {
                setErrMsg('Erro ao carregar dados');
            }
        };
        fetchDados();
    }, [axiosPrivate]);

    const transacoesMesAtual = transacoes.filter((t) => {
    const data = new Date(t.dataTransacao);
    const agora = new Date();
    return data.getMonth() === agora.getMonth() && 
           data.getFullYear() === agora.getFullYear();
    });

    const totalReceitas = transacoesMesAtual
    .filter(t => t.tipo === 0)
    .reduce((acc, t) => acc + t.valor, 0);

    const totalDespesas = transacoesMesAtual
    .filter(t => t.tipo === 1)
    .reduce((acc, t) => acc + t.valor, 0);

    const gastosPorCategoria = transacoesMesAtual
    .filter(t => t.tipo === 1)
    .reduce((acc, t) => {
        const nome = t.categoria?.nome || 'Sem categoria';
        acc[nome] = (acc[nome] || 0) + Math.abs(t.valor);
        return acc;
    }, {});

    const dadosGrafico = Object.entries(gastosPorCategoria).map(([nome, valor]) => ({
        name: nome,
        value: valor,
    }));

    return (
        <div className="dashboard">
            <h2>Dashboard - {new Date().toLocaleString('pt-BR', { month: 'long', year: 'numeric' })}</h2>

            <div className="dashboard-resumo">
                <div className="dashboard-card receita">
                    <h3>Total de Receitas</h3>
                    <p>R$ {totalReceitas.toFixed(2)}</p>
                </div>
                <div className="dashboard-card despesa">
                    <h3>Total de Despesas</h3>
                    <p>R$ {Math.abs(totalDespesas).toFixed(2)}</p>
                </div>
                <div className="dashboard-card saldo">
                    <h3>Saldo do Mês</h3>
                    <p>R$ {(totalReceitas - Math.abs(totalDespesas)).toFixed(2)}</p>
                </div>
            </div>

            <div className="dashboard-grafico">
                <h3>Gastos por Categoria</h3>
                {dadosGrafico.length > 0 ? (
                    <PieChart width={400} height={400}>
                        <Pie
                            data={dadosGrafico}
                            cx={200}
                            cy={200}
                            outerRadius={120}
                            dataKey="value"
                            label={({ percent }) => `${(percent * 100).toFixed(0)}%`}
                        >
                            {dadosGrafico.map((_, index) => (
                                <Cell key={index} fill={COLORS[index % COLORS.length]} />
                            ))}
                        </Pie>
                        <Tooltip formatter={(value) => `R$ ${value.toFixed(2)}`} />
                        <Legend />
                    </PieChart>
                ) : (
                    <p>Nenhuma despesa registrada neste mês.</p>
                )}
            </div>
        </div>
    );
};

export default Dashboard;