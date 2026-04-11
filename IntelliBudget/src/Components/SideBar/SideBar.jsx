import './SideBar.css';
import { MdSmartToy, MdAttachMoney, MdTrackChanges, MdLogout, MdSettings, MdAccountBalance } from 'react-icons/md';

const SideBar = () => {
    return (
        <aside className="sidebar">
            <h2>Menu</h2>
            <nav className="sidebar-nav">
                 <ul>
                    <li><a href="/Transacoes"><MdAttachMoney />Explorar gastos</a></li>
                    <li><a href="#"><MdSmartToy />Chatbot</a></li>
                    <li><a href="#"><MdTrackChanges />Metas</a></li>
                    <li><a href="/Contas"><MdAccountBalance />Contas</a></li>
                    <li><a href="#"><MdSettings />Configurações</a></li>
                    <li><a href="#"><MdLogout />Sair</a></li>
                </ul>
            </nav>
        </aside>
    );
}

export default SideBar;