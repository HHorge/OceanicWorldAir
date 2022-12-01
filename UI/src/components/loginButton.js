import React from 'react';
import '../styles/login.css'
import loginMethod from './loginMethod';

const LoginBtn= (props) => {

    const loginSession = new loginMethod();

    return (
        <div onClick={() => { loginSession.login(); } } className='loginbtn'> </div>
    )
}

export default LoginBtn;
