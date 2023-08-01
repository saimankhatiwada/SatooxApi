import React, { useState } from 'react'
import { inputHelper, toastNotify } from "../Helpers/Helper";
import { authResponse } from '../Interfaces/interface';
import { useLoginUserMutation } from "../apis/AuthenticationApis";
import { useDispatch } from 'react-redux';
import { setLoggedInUser } from "../Storage/Redux/userAuthSlice";
import { useNavigate } from 'react-router-dom';
import jwtDecode from 'jwt-decode';
import { Loader } from '../Components/Layout/Layouts';
import { ValueDefinations } from "../Utility/ValueDefination";


function Login() {

    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [loginUser] = useLoginUserMutation();
    const [loading, setLoading] = useState(false);
    const [userInput, setUserInput] = useState({
        email: "",
        password: ""
    });

    const handleUserInput = (e: React.ChangeEvent<HTMLInputElement>) => {
        const tempData = inputHelper(e, userInput);
        setUserInput(tempData);
    }

    const handleSubmit = async (e:React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setLoading(true);
        const response: authResponse = await loginUser({
            email: userInput.email,
            password: userInput.password,
        });

        if(response.data)
        {
            var decode: any = jwtDecode(response.data.token);
            var firstName = decode.given_name;
            var lastName = decode.family_name;
            var email = decode.email;
            var isActive = Boolean(decode.isActive.toLowerCase());
            localStorage.setItem(ValueDefinations.TOKEN, response.data.token);
            dispatch(setLoggedInUser({ firstName, lastName, email, isActive }));
            toastNotify("Logged in", "success");
            navigate("/");
        }

        setLoading(false);
    };


    return (
        <div>
            {loading &&
                <Loader />
            }
            <div className="min-h-screen flex items-center justify-center">
                <div className="bg-white p-8 shadow-lg rounded-md max-w-md w-full">
                    <h2 className="mb-8 text-3xl text-gray-700 font-bold text-center">Log in</h2>
                    <form method="post" onSubmit={handleSubmit}>
                        <div className="mb-6">
                            <label className="block mb-2 font-medium text-gray-700">Email</label>
                            <input type="email" id="email" className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-gray-500 focus:border-gray-500 text-gray-600" name="email" required value={userInput.email} onChange={handleUserInput}/>
                        </div>
                        <div className="mb-6">
                            <label className="block mb-2 font-medium text-gray-700">Password</label>
                            <input type="password" id="password" className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-gray-500 focus:border-gray-500 text-gray-600" name="password" required value={userInput.password} onChange={handleUserInput}/>
                        </div>
                        <div className="flex items-center justify-between mb-6">
                            <label className="flex items-center">
                                <input type="checkbox" id="remember" className="mr-2" />
                                <span className="text-gray-700">Remember me</span>
                            </label>
                            <a href="#" className="text-gray-500 hover:text-gray-600">Forgot password?</a>
                        </div>
                        <button type="submit" className="w-full py-2 px-4 bg-gray-500 text-white rounded-md hover:bg-gray-600">Log in</button>
                    </form>
                </div>
            </div>
        </div>
    )
}

export default Login;
