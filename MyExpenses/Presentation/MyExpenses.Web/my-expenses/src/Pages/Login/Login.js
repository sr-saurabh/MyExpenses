import React, { forwardRef, useState, useEffect } from 'react';
import './Login.css';
import { InputText } from "primereact/inputtext";
import { FloatLabel } from "primereact/floatlabel";
import { Button } from 'primereact/button';
import { Password } from 'primereact/password';
import { GoogleLogin } from '@react-oauth/google';
import { Divider } from 'primereact/divider';
import { jwtDecode } from "jwt-decode";
import { useNavigate } from 'react-router-dom';
import { register, login, googleLogin } from '../../Services/authService';
import { getCurrentUserProfile } from '../../Services/userServices';

const Login = () => {

    const initialFormData = {
        email: '',
        password: '',
        confirmPassword: ''
    };
    const [formData, setFormData] = useState(initialFormData);
    const [formError, setFormError] = useState({});
    const [isSignup, setSignup] = useState(false);
    const navigate = useNavigate();

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
        const formError = vlaidateForm(formData);
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
                const token = response.data.data;
                console.log(token);
                localStorage.setItem('token', token);
                getProfileData();
            }).catch((error) => {
                console.log(error);
            });
        }

    };

    const getProfileData = () => {
        getCurrentUserProfile(true).then((response) => {
            const profileData = response.data;
            localStorage.setItem('profileData', JSON.stringify(profileData));

            if (profileData == null)
                navigate('/register-user');
            else
                navigate('/dashboard');
        }).catch((error) => {
            console.log(error);
        });
    }


    const vlaidateForm = (formData) => {
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
            navigate('/dashboard');
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
            <div className='form-container d-flex flex-column bg-white rounded-3 opacity-75 overflow-hidden'>
                <div className='form-description d-flex flex-column justify-content-end'>
                    <div className='p-3 pb-0 fw-medium'>
                        ExpenseManager is your ultimate tool for effortlessly managing and sharing expenses. Whether youre tracking personal finances, splitting bills with friends, or managing group expenses, ExpenseMate makes it easy. With a user-friendly interface, you can categorize your spending, set budgets, and view insightful reports to stay on top of your finances.
                    </div>
                    <div className='p-2 link'>
                        <Button label={isSignup ? "Login" : "Signup"} severity='info' raised className='text-black rounded-3 opacity-75' size='large' onClick={() => setSignup(!isSignup)} />
                    </div>
                </div>
                <div className='p-3'>
                    <form className='d-flex flex-column  justify-content-center' onSubmit={handleSubmit}>
                        <div className='mb-3 mt-2'>
                            <FloatLabel>
                                <InputText className='w-100' id="email" value={formData.email} invalid={!formError.email === ""} onChange={(e) => handleChange(e)} />
                                <label htmlFor="email">Email</label>
                            </FloatLabel>
                            <div className='text-danger'>{formError?.email}</div>
                        </div>
                        <div className='my-3 password-container'>
                            <FloatLabel className='w-100'>
                                <Password className='w-100' inputClassName='w-100' inputId="password" invalid={!formError.password === ""} value={formData.password} onChange={(e) => handleChange(e)} toggleMask header={passwordHeader} footer={passwordFooter} feedback={isSignup} />
                                <label htmlFor="password">Password</label>
                            </FloatLabel>
                            <div className='text-danger'>{formError?.password}</div>
                        </div>
                        {isSignup &&
                            <div className='my-3 password-container'>
                                <FloatLabel className='w-100'>
                                    <Password className='w-100' inputClassName='w-100' inputId="confirmPassword" feedback={false} value={formData.confirmPassword} onChange={(e) => handleChange(e)} />
                                    <label htmlFor="confirmPassword">Confirm Password</label>
                                </FloatLabel>
                                <div className='text-danger'>{formError?.confirmPassword}</div>

                            </div>
                        }
                        <Button label="Submit" severity='info' className='rounded-3' iconPos='right' />
                    </form>
                    <Divider align="center">
                        <span className="">OR</span>
                    </Divider>
                    <div className='d-flex justify-content-center'>
                        <GoogleLogin
                            onSuccess={credentialResponse => {
                                onGoogleLogin(credentialResponse);
                            }}
                            onError={() => {
                                console.log('Login Failed');
                            }}
                        />
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Login;