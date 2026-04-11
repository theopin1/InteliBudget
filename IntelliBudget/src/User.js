import { useState, useEffect } from "react";
import useAxiosPrivate from "../Hooks/useAxiosPrivate ";
import { useNavigate, useLocation } from "react-router-dom";

const User = () => 
{
    const [user, setUser] = useState(null);
    const axiosPrivate = useAxiosPrivate();
    const navigate = useNavigate();
    const location = useLocation(); 

    useEffect(() => 
    {
        let isMounted = true;
        const controller = new AbortController();

        const getUser = async () =>
        {
            try 
            {
                const response = await axiosPrivate.get('/user', {signal: controller.signal});
                console.log(response.data);
                isMounted && setUser(response.data);
            } catch (err)
                {
                    console.error(err);
                    navigate('/login', { state: { from: location }, replace: true }); 
                }
        };

        getUser();

        return () =>
        {
            isMounted = false;
            controller.abort();
        }
    }, []);

    return 
    (
        <article> 
                <h2>Users List</h2>
                {users?.lenght
                   ?(
                    <ul>
                        {users.map((user, i) => <li key={i}>{user?.username}</li>)}
                    </ul>
                   ) : <p>No users to display</p>
                }
        </article>
    );
};

export default User;