import './Header.css';

const Header = () => {
    return (
        <header className="header">
            <div className="header-title">
                <h1>IntelliBudget</h1>
            </div>
            <div className="home-icon">
                <img src="src\assets\transferir (1).png" alt="login" />
            </div>
            <div className="header-logo">
                <img src="src\assets\IntelliBudgetLogo.png" alt="login" />
            </div>
        </header>
    );
}

export default Header;