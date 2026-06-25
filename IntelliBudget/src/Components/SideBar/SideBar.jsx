import './SideBar.css';
import { NavLink } from 'react-router-dom';
import { MdSmartToy, MdHome, MdAttachMoney, MdTrackChanges, MdLogout, MdSettings, MdAccountBalance } from 'react-icons/md';

const SideBar = () => {
    return (
        <aside className="sidebar">
            <h2>Menu</h2>
            <nav className="sidebar-nav">
                 <ul>
                    <li><NavLink to="/home"><MdHome />Home</NavLink></li>
                    <li><NavLink to="/transacoes"><MdAttachMoney />Explorar gastos</NavLink></li>
                    <li><NavLink to="/chatbot"><MdSmartToy />Chatbot</NavLink></li>
                    <li><NavLink to="/metas"><MdTrackChanges />Metas</NavLink></li>
                    <li><NavLink to="/contas"><MdAccountBalance />Contas</NavLink></li>
                    <li><NavLink to="#"><MdSettings />Configurações</NavLink></li>
                    <li><NavLink to="#"><MdLogout />Sair</NavLink></li>
                </ul>
            </nav>
        </aside>
    );
}

export default SideBar;