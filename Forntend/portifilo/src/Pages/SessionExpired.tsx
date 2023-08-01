import React, { useEffect } from 'react'
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { initialstate, setLoggedInUser } from '../Storage/Redux/userAuthSlice';
import { toastNotify } from '../Helpers/Helper';
import { ValueDefinations } from '../Utility/ValueDefination';

function SessionExpired() {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    useEffect(() => {
        localStorage.removeItem(ValueDefinations.TOKEN);
        dispatch(setLoggedInUser({...initialstate}));
        toastNotify("Token expired please login again.", "info");
        navigate("/");
    });

    return (
        <div></div>
    )
}

export default SessionExpired;
