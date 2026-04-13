import './Login.css';
import{ useRef, useState, useEffect } from 'react';
import useAuth from '../../Hooks/useAuth';
import axios from '../../Api/axios';

const LOGIN_URL = '/auth/login';

const Login = () => {
    const { setAuth } = useAuth();
    const userRef = useRef();
    const errRef = useRef();

    const [email, setEmail] = useState('');
    const [pwd, setPwd] = useState('');
    const [errMsg, setErrMsg] = useState('');
    const [success, setSuccess] = useState(false);

    useEffect(() =>
    {    
            userRef.current.focus();
    }, [])

    useEffect(() =>
    {
        setErrMsg('');
    }, [email, pwd])

    const handleSubmit = async (e) =>
    {
        e.preventDefault();

        try {
            const response = await axios.post(LOGIN_URL, {
                email: email,
                senha: pwd
            },
            {
                headers: { 'Content-Type': 'application/json' },
                withCredentials: true
            }
            );
            console.log(JSON.stringify(response));

            setAuth({ email: email, accessToken: response.data.token });
            localStorage.setItem('accessToken', response.data.token);
            setEmail('');
            setPwd('');
            setSuccess(true);
        } catch (err) {
            if (!err?.response) {
                setErrMsg('No Server Response');
            } else if (err.response?.status === 400) {
                setErrMsg('Missing Email or Password');
            } else if (err.response?.status === 401) {
                setErrMsg('Unauthorized');
            } else {
                setErrMsg('Login Failed');
            }
            errRef.current.focus();
        }
    };

      

    return (
        <> 
            {success ? (
               (() => { window.location.href = "/home"; return null; })()
            ) : (
        <div className="login-container">
            <section className="login">
                <p ref={errRef} className={errMsg ? "errmsg" : "offscreen"} 
                aria-live="assertive">{errMsg}</p>
                <div className="login-image">
                    <img src="src\assets\IntelliBudgetLogo.png" alt="login" />
                </div>
                <h1>Login</h1>
                <form onSubmit={handleSubmit}>
                    <input
                        type="email"
                        id="email"
                        ref={userRef}
                        autoComplete="email"
                        onChange = {(e) => setEmail(e.target.value)}
                        value = {email}
                        placeholder='Email'
                        required
                    />
                    <input
                        type="password"
                        id="password"
                        onChange = {(e) => setPwd(e.target.value)}
                        value = {pwd}
                        placeholder='Senha'
                        required
                    />
                    <button>Login</button>
                    <p>
                        Need an Account?<br />
                        <span className="line">
                            {/*put router link here*/}
                            <a href="#">Sign Up</a>
                        </span>
                    </p>
                </form>
            </section>
        </div>
            )}
        </>
    );
}

export default Login;