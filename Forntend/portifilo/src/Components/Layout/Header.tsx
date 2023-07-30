import React from 'react'
import ShoppingCartRoundedIcon from '@mui/icons-material/ShoppingCartRounded';
import { NavLink } from 'react-router-dom';

let logo = require("../../Assests/logo.png");

function Header() {
  return (
    <nav className='flex-no-wrap relative flex w-full items-center justify-between bg-[#FBFBFB] py-2 shadow-md shadow-black/5 dark:bg-neutral-600 dark:shadow-black/10 lg:flex-wrap lg:justify-start lg:py-4'>
        <div className='flex w-full flex-wrap items-center justify-between px-3'>
            <div className="!visible hidden flex-grow basis-[100%] items-center lg:!flex lg:basis-auto" id="navbarSupportedContent12">
                <NavLink className="mb-4 ml-2 mr-5 mt-3 flex items-center text-neutral-900 hover:text-neutral-900 focus:text-neutral-900 dark:text-neutral-200 dark:hover:text-neutral-400 dark:focus:text-neutral-400 lg:mb-0 lg:mt-0" to="/">
                    <img className='h-10' src={ logo } alt="logo" loading="lazy" />
                </NavLink>
                <ul className="list-style-none mr-auto flex flex-col pl-0 lg:flex-row">
                    <li className="mb-4 lg:mb-0 lg:pr-2">
                        <a className="text-neutral-500 transition duration-200 hover:text-neutral-700 hover:ease-in-out focus:text-neutral-700 disabled:text-black/30 motion-reduce:transition-none dark:text-neutral-200 dark:hover:text-neutral-300 dark:focus:text-neutral-300 lg:px-2 [&.active]:text-black/90 dark:[&.active]:text-zinc-400" href="#">
                            Portfolio
                        </a>
                    </li>

                    <li className="mb-4 lg:mb-0 lg:pr-2">
                        <NavLink className="text-neutral-500 transition duration-200 hover:text-neutral-700 hover:ease-in-out focus:text-neutral-700 disabled:text-black/30 motion-reduce:transition-none dark:text-neutral-200 dark:hover:text-neutral-300 dark:focus:text-neutral-300 lg:px-2 [&.active]:text-black/90 dark:[&.active]:text-neutral-400" to="/blog">
                            Blogs
                        </NavLink>
                    </li>

                    <li className="mb-4 lg:mb-0 lg:pr-2">
                        <a className="text-neutral-500 transition duration-200 hover:text-neutral-700 hover:ease-in-out focus:text-neutral-700 disabled:text-black/30 motion-reduce:transition-none dark:text-neutral-200 dark:hover:text-neutral-300 dark:focus:text-neutral-300 lg:px-2 [&.active]:text-black/90 dark:[&.active]:text-neutral-400" href="#">
                            Courses
                        </a>
                    </li>
                </ul>
            </div>

            <div className="relative flex items-center">
                <a className="mr-4 text-secondary-500 transition duration-200 hover:text-secondary-400 hover:ease-in-out focus:text-secondary-400 disabled:text-black/30 motion-reduce:transition-none" href="#">
                    <span className="[&>svg]:w-5">
                        <ShoppingCartRoundedIcon className='text-white' />
                    </span>
                </a>
            </div>

            <div className="relative flex items-center">
                <a className="text-neutral-500 transition duration-200 hover:text-neutral-700 hover:ease-in-out focus:text-neutral-700 disabled:text-black/30 motion-reduce:transition-none dark:text-neutral-200 dark:hover:text-neutral-300 dark:focus:text-neutral-300 lg:px-2 [&.active]:text-black/90 dark:[&.active]:text-neutral-400" href="#">
                    <span className="[&>svg]:w-5">
                        Login
                    </span>
                </a>
            </div>

            <div className="relative flex items-center">
                <a className="text-neutral-500 transition duration-200 hover:text-neutral-700 hover:ease-in-out focus:text-neutral-700 disabled:text-black/30 motion-reduce:transition-none dark:text-neutral-200 dark:hover:text-neutral-300 dark:focus:text-neutral-300 lg:px-2 [&.active]:text-black/90 dark:[&.active]:text-neutral-400" href="#">
                    <span className="[&>svg]:w-5">
                        Register
                    </span>
                </a>
            </div>
        </div>
    </nav>
  )
}

export default Header
