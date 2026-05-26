import Header from "../../Components/Header/Header";
import SideBar from "../../Components/SideBar/SideBar";
import Dashboard from "../../Components/Dashboard/Dashboard";
import './Home.css';

const Home = () => {
    return (
        <>
            <Header />
            <div className="layout">
                <SideBar />
                <Dashboard />
            </div>
        </>
    );
}

export default Home;