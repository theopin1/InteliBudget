import { useLocation, Navigate, Outlet } from "react-router-dom";
import useAuth from "../Hooks/useAuth";

const RequireAuth = () =>
{
    const { auth } = useAuth();
    const location = useLocation()
    
    const token = auth?.accessToken || localStorage.getItem('accessToken');

    return (
        token
            ? <Outlet />
            : <Navigate to="/login" state={{ from: location }} replace />
    );
}

export default RequireAuth;