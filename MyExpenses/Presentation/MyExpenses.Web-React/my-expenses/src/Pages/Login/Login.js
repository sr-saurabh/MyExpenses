import { GoogleLogin } from '@react-oauth/google';
import { Button } from 'primereact/button';
import { Divider } from 'primereact/divider';
import { InputText } from "primereact/inputtext";
import { Password } from 'primereact/password';
import React, { useEffect, useRef, useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { googleLogin, login, register } from '../../Services/authService.tsx';
import { getCurrentUserProfile } from '../../Services/userServices.tsx';
import './Login.css';

import { Toast } from 'primereact/toast';
import UserContext from '../../Context/UserContext.ts';

const Login = () => {
    const googleLoginRef = useRef(null);
    const toast = useRef(null);

    const initialFormData = {
        email: '',
        password: '',
        confirmPassword: ''
    };

    const [formData, setFormData] = useState(initialFormData);
    const [formError, setFormError] = useState({});
    const [isSignup, setSignup] = useState(false);
    const navigate = useNavigate();
    const { user, setUser } = useContext(UserContext);

    const passwordHeader = <div className="font-bold mb-3">Pick a password</div>;
    const passwordFooter = (
        <>
            <Divider />
            <p className="mt-2">Suggestions</p>
            <ul className="pl-2 ml-2 mt-0 line-height-3">
                <li>At least one lowercase</li>
                <li>At least one uppercase</li>
                <li>At least one numeric</li>
                <li>Minimum 8 characters</li>
            </ul>
        </>

    );

    useEffect(() => {
        // console.log('Form Error:', formError);
    }, [formError]);
    useEffect(() => {
        var token = localStorage.getItem('token');
        if (token) {
            getProfileData();
        }
    }, []);

    const handleSubmit = (e) => {
        e.preventDefault();
        const formError = validateForm(formData);
        if (formError) {
            setFormError(formError);
            if (Object.keys(formError).length > 0)
                return;
        }
        if (isSignup) {
            console.log('Registering User');
            register(formData).then((response) => {
                const token = response.data.data;
                console.log(token);
                localStorage.setItem('token', token);
                getProfileData();
            }).catch((error) => {
                console.log(error);
            });
        }
        else {
            login(formData).then((response) => {
                if (response.data.autCode === 0) {
                    toast.current.show({ severity: 'error', summary: 'Login Failed', detail: response.data.message, life: 3000 });
                    setSignup(true);
                    return;
                }
                else {
                    toast.current.show({ severity: 'success', summary: 'Login Successful', detail: response.data.message, life: 3000 });
                    getProfileData();
                    // navigate('/overview')
                }
                const token = response.data.data;

                localStorage.setItem('token', token);
                getProfileData();
            }).catch((error) => {
                console.log(error);
            });
        }

    };

    const getProfileData = () => {
        // if (user != null) {
        //     navigate('/');
        //     return;
        // }
        const profileData = localStorage.getItem('profileData');
        if (profileData == null || profileData === undefined) {
            getCurrentUserProfile(true).then((response) => {
                if (response.status === 204) {
                    toast.current.show({ severity: 'warning', summary: 'Login Success', detail: "Profile not found", life: 3000 });
                    navigate('/register-user');
                }
                else {
                    const profileData = response.data;
                    setUser(response.data);
                    localStorage.setItem('profileData', JSON.stringify(profileData));
                    navigate('/');
                }
            }).catch((error) => {
                console.log(error);
            });
        }
        else{
            navigate('/');
        }
    }


    const validateForm = (formData) => {
        const formError = {};
        const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
        const passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/;

        if (!formData.email) {
            formError.email = 'Email is required';
        }
        if (!formData.password) {
            formError.password = 'Password is required';
        }
        if (isSignup && !formData.confirmPassword) {
            formError.confirmPassword = 'Confirm Password is required';
        }
        if (isSignup && formData.password !== formData.confirmPassword) {
            formError.confirmPassword = 'Password and Confirm Password do not match';
        }
        if (formData.email && !emailRegex.test(formData.email)) {
            formError.email = 'Invalid Email Format';
        }
        if (formData.password && !passwordRegex.test(formData.password)) {
            formError.password = 'Password must be strong';
        }

        return formError;
    }


    const onGoogleLogin = (credentialResponse) => {

        console.log(credentialResponse);
        var credentials = credentialResponse.credential;
        console.log(credentials);
        //const decodedHeader = jwtDecode(credentials);
        //console.log(decodedHeader);
        var googleCredentials = {
            accessToken: credentials,
        }

        googleLogin(googleCredentials).then((response) => {
            const token = response.data.data;
            console.log(token);
            localStorage.setItem('token', token);
            navigate('/overview');
        }).catch((error) => {
            console.log(error);
        }
        );
    };

    const handleChange = (e) => {
        const { id, value } = e.target;
        setFormData({ ...formData, [id]: value });
    }


    return (
        <div className='login-container'>
            <Toast ref={toast} />
            <div className='form-container d-flex flex-column bg-white rounded-3 opacity-75 overflow-hidden'>
                <div className='d-flex justify-content-center text-teal align-items-end user-select-none'>
                    <span className='fs-1 fw-bold'>MY E </span>
                    <span className='fs-1 fw-medium'>xpenses</span>
                </div>
                <div className='p-3'>
                    <form className='d-flex flex-column  justify-content-center' onSubmit={handleSubmit}>
                        <div className='mb-3 mt-2'>
                            <div className="flex flex-column gap-2">
                                <label htmlFor="email" className='text-black fw-medium'>Email</label>
                                <InputText className='w-100' id="email" value={formData.email} invalid={!formError.email === ""} placeholder='hello@example.com' onChange={(e) => handleChange(e)} />
                            </div>
                            <div className='text-danger'>{formError?.email}</div>
                        </div>
                        <div className='my-3 password-container'>
                            <div className="flex flex-column gap-2">
                                <label htmlFor="password" className='text-black fs-6 d-flex justify-content-between'>
                                    <span className='text-black fw-medium'>Password</span>
                                    <span className='cursor-pointer l-text-primary forgot-password'>Forgot password?</span>
                                </label>
                                <Password className='w-100' inputClassName='w-100' placeholder='Password...' inputId="password" invalid={!formError.password === ""} value={formData.password} onChange={(e) => handleChange(e)} toggleMask header={passwordHeader} footer={passwordFooter} feedback={isSignup} />
                            </div>
                            <div className='text-danger'>{formError?.password}</div>
                        </div>
                        {isSignup &&
                            <div className='my-3 password-container'>
                                <div className="flex flex-column gap-2">
                                    <label htmlFor="password" className='text-black fs-6'>Password</label>
                                    <Password className='w-100' inputClassName='w-100' inputId="confirmPassword" placeholder='Confirm password...' feedback={false} value={formData.confirmPassword} onChange={(e) => handleChange(e)} />
                                </div>
                                <div className='text-danger'>{formError?.confirmPassword}</div>

                            </div>
                        }
                        <Button label={isSignup ? 'Sign up' : 'Login'} className='rounded-3' iconPos='right' />
                    </form>
                    <Divider align="center">
                        <span className="">OR</span>
                    </Divider>
                    <div className='d-flex justify-content-center google-login' ref={googleLoginRef}>
                        <GoogleLogin
                            onSuccess={credentialResponse => {
                                onGoogleLogin(credentialResponse);
                            }}
                            onError={() => {
                                console.log('Login Failed');
                            }}
                        />
                    </div>
                    <div className='d-flex justify-content-center mt-3'>

                        {isSignup &&
                            <p><span className='text-body-tertiary'>Already have an account? </span> <span className='text-teal cursor-pointer' onClick={() => setSignup(!isSignup)} >Sign in here</span></p>
                        }
                        {!isSignup &&
                            <p><span className='text-body-tertiary'>Don't have an account? </span><span className='text-teal cursor-pointer' onClick={() => setSignup(!isSignup)}>Create an account</span></p>
                        }
                    </div>
                    {/* <Button label={isSignup ? "Login" : "Signup"} severity='info' raised className='text-black rounded-3 opacity-75' size='large' onClick={() => setSignup(!isSignup)} /> */}

                </div>
            </div>
        </div>
    );
};

export default Login;