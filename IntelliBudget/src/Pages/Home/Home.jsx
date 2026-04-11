import Header from "../../Components/Header/Header";
import SideBar from "../../Components/SideBar/SideBar";
import './Home.css';

const Home = () => {
    return (
        <>
            <Header />
            <div className="layout">
                <SideBar />
            </div>
        </>
    );
}

export default Home;