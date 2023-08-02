import React, { useState } from 'react'
import ShoppingCartRoundedIcon from '@mui/icons-material/ShoppingCartRounded';
import { NavLink, useNavigate } from 'react-router-dom';
import { RootState } from "../../Storage/Redux/store";
import { userModel } from '../../Interfaces/interface';
import { useSelector, useDispatch } from 'react-redux';
import { initialstate, setLoggedInUser } from '../../Storage/Redux/userAuthSlice';
import { toastNotify } from '../../Helpers/Helper';
import { ValueDefinations } from '../../Utility/ValueDefination';
import { ProfileOnHeader } from "../../Components/Page/HeadersProfile/hP";


let logo = require("../../Assests/logo.png");

function Header() {
    const userData : userModel = useSelector((state: RootState ) => state.userAuthStore);
    const [isProfileClicked, setProfileClicked] = useState(false);

    const dispatch = useDispatch();
    const navigate = useNavigate();

    const handleProfileClicked= () => {
        setProfileClicked(isProfileClicked =>!isProfileClicked);
    }

    const handleLogout = () => {
        localStorage.removeItem(ValueDefinations.TOKEN);
        dispatch(setLoggedInUser({...initialstate}));
        toastNotify("Logout success", "success");
        navigate("/");
    }

    return (
        <nav className='flex-no-wrap relative flex w-full items-center justify-between bg-[#FBFBFB] py-2 shadow-md shadow-black/5 dark:bg-neutral-600 dark:shadow-black/10 lg:flex-wrap lg:justify-start lg:py-4 sticky top-0 z-50'>
            <div className='flex w-full flex-wrap items-center justify-between px-3'>
                <div className="!visible hidden flex-grow basis-[100%] items-center lg:!flex lg:basis-auto">
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

                { userData.isActive &&
                    <>
                        <div className="relative flex items-center">
                            <span className="[&>svg]:w-5">
                                <ProfileOnHeader props={userData} />
                            </span>
                            <button className="text-neutral-500 transition duration-200 hover:text-neutral-700 hover:ease-in-out focus:text-neutral-700 disabled:text-black/30 motion-reduce:transition-none dark:text-neutral-200 dark:hover:text-neutral-300 dark:focus:text-neutral-300 lg:px-2 [&.active]:text-black/90 dark:[&.active]:text-neutral-400" onClick={handleProfileClicked}>
                                <span className="[&>svg]:w-5">
                                    <span>{userData.firstName} {userData.lastName}</span>
                                </span>
                                {isProfileClicked &&
                                    <ul className="absolute right-0 mt-2 py-2 w-36 bg-[#FBFBFB] shadow-black/5 dark:bg-neutral-600 dark:shadow-black/10 rounded-md shadow-lg">
                                        <li className="px-4 py-2 text-neutral-500 transition duration-200 hover:text-neutral-700 hover:ease-in-out focus:text-neutral-700 disabled:text-black/30 motion-reduce:transition-none dark:text-neutral-200 dark:hover:text-neutral-300 dark:focus:text-neutral-300 lg:px-2 [&.active]:text-black/90 dark:[&.active]:text-neutral-400">
                                            <NavLink to="/profile">Profile</NavLink>
                                        </li>
                                        <li className="px-4 py-2 text-neutral-500 transition duration-200 hover:text-neutral-700 hover:ease-in-out focus:text-neutral-700 disabled:text-black/30 motion-reduce:transition-none dark:text-neutral-200 dark:hover:text-neutral-300 dark:focus:text-neutral-300 lg:px-2 [&.active]:text-black/90 dark:[&.active]:text-neutral-400" onClick={handleLogout}>Logout</li>
                                    </ul>
                                }
                            </button>

                        </div>

                    </>
                }

                { !userData.isActive &&
                    <>
                        <div className="relative flex items-center">
                            <NavLink className="text-neutral-500 transition duration-200 hover:text-neutral-700 hover:ease-in-out focus:text-neutral-700 disabled:text-black/30 motion-reduce:transition-none dark:text-neutral-200 dark:hover:text-neutral-300 dark:focus:text-neutral-300 lg:px-2 [&.active]:text-black/90 dark:[&.active]:text-neutral-400" to="/login">
                                <span className="[&>svg]:w-5">
                                    Login
                                </span>
                            </NavLink>
                        </div>

                        <div className="relative flex items-center">
                            <NavLink className="text-neutral-500 transition duration-200 hover:text-neutral-700 hover:ease-in-out focus:text-neutral-700 disabled:text-black/30 motion-reduce:transition-none dark:text-neutral-200 dark:hover:text-neutral-300 dark:focus:text-neutral-300 lg:px-2 [&.active]:text-black/90 dark:[&.active]:text-neutral-400" to="/register">
                                <span className="[&>svg]:w-5">
                                    Register
                                </span>
                            </NavLink>
                        </div>
                    </>
                }


            </div>
        </nav>
    )
}

export default Header;
