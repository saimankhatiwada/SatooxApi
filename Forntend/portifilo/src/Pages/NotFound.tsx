import React from 'react'
import { NavLink } from "react-router-dom";

function NotFound() {
  return (
    <div className="flex items-center justify-center h-screen">
        <div className="text-center">
          <h1 className="text-6xl font-bold text-gray-400">404</h1>
          <p className="text-2xl text-gray-400 mt-1">Oops! The page you're looking for was not found.</p>
          <NavLink to="/" className="text-white bg-gray-500 hover:bg-gray-600 px-6 py-3 rounded-full mt-8 inline-block transition-colors duration-300">Go back to home</NavLink>
        </div>
    </div>
  )
}

export default NotFound;
