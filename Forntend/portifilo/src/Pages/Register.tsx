import React, { useState } from 'react'
import { inputHelper } from "../Helpers/Helper";
import { useRegisterUserMutation } from "../apis/AuthenticationApis";
import { authResponse } from '../Interfaces/interface';
import { useDispatch } from 'react-redux';
import { setLoggedInUser } from "../Storage/Redux/userAuthSlice";
import { useNavigate } from 'react-router-dom';
import { Loader } from '../Components/Layout/Layouts';
import jwtDecode from 'jwt-decode';
import { ValueDefinations } from "../Utility/ValueDefination";


function Register() {

    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [registerUser] = useRegisterUserMutation();
    const [loading, setLoading] = useState(false);
    const [userInput, setUserInput] = useState({
        firstName: "",
        lastName: "",
        email: "",
        imagePath: "N/A",
        imageName: "N/A",
        password: "",
        isActive: true
    });

    const handleUserInput = (e: React.ChangeEvent<HTMLInputElement>) => {
        const tempData = inputHelper(e, userInput);
        setUserInput(tempData);
    }

    const handleSubmit = async (e:React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setLoading(true);
        const response: authResponse = await registerUser({
            firstName: userInput.firstName,
            lastName: userInput.lastName,
            email: userInput.email,
            imagePath: "N/A",
            imageName: "N/A",
            password: userInput.password,
            isActive: true
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
            navigate("/");
        }

        setLoading(false);
    };

    return (
        <div>
            {loading && <Loader />}
            <div className="min-h-screen flex items-center justify-center">
                <div className="bg-white p-8 shadow-lg rounded-md max-w-md w-full">
                    <h2 className="mb-8 text-3xl font-bold text-center text-gray-700">Register</h2>
                    <form method="post" onSubmit={handleSubmit}>
                        <div className="mb-6">
                            <label className="block mb-2 font-medium text-gray-700">First Name</label>
                            <input type="text" id="first_name" className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-gray-500 focus:border-gary-500 text-gray-600" name="firstName" required value={userInput.firstName} onChange={handleUserInput}/>
                        </div>
                        <div className="mb-6">
                            <label className="block mb-2 font-medium text-gray-700">Last Name</label>
                            <input type="text" id="last_name" className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-gray-500 focus:border-gray-500 text-gray-600" name="lastName" required value={userInput.lastName} onChange={handleUserInput}/>
                        </div>
                        <div className="mb-6">
                            <label className="block mb-2 font-medium text-gray-700">Email</label>
                            <input type="email" id="email" className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-gray-500 focus:border-gray-500 text-gray-600" name="email" required value={userInput.email} onChange={handleUserInput} />
                        </div>
                        <div className="mb-6">
                            <label className="block mb-2 font-medium text-gray-700">Password</label>
                            <input type="password" id="password" className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-gray-500 focus:border-gray-500" name="password" required value={userInput.password} onChange={handleUserInput}/>
                        </div>
                        <button type="submit" className="w-full py-2 px-4 bg-gray-500 text-white rounded-md hover:bg-gray-600">Register</button>
                    </form>
                </div>
            </div>
        </div>
    )
}

export default Register;
