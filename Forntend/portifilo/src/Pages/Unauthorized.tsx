import React from 'react'
import { NavLink } from 'react-router-dom';

function Unauthorized() {
  return (
        <div className="flex items-center justify-center h-screen">
            <div className="text-center">
                <h1 className="text-6xl font-bold text-gray-400">401</h1>
                <p className="text-2xl text-gray-400 mt-1">Unauthorized</p>
                <div className="flex justify-center mt-8">
                    <NavLink to="/login" className="text-white bg-gray-500 hover:bg-gray-600 mx-8 px-6 py-3 rounded-full inline-block transition-colors duration-300">Login</NavLink>
                    <NavLink to="/register" className="text-white bg-gray-500 hover:bg-gray-600 px-6 py-3 rounded-full inline-block transition-colors duration-300">Register</NavLink>
                </div>
            </div>
        </div>
    )
}

export default Unauthorized;
