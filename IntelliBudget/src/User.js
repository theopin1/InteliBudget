import { useState, useEffect } from "react";
import axios from "../Api/Axios";
import { sign } from "node:crypto";

const User = () => 
{
    const [user, setUser] = useState(null);

    useEffect(() => 
    {
        let isMounted = true;
        const controller = new AbortController();

        const getUser = async () =>
        {
            try 
            {
                const response = await axios.get('/user', {signal: controller.signal});
                console.log(response.data);
                isMounted && setUser(response.data);
            } catch (err)
                {
                    console.error(err);
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