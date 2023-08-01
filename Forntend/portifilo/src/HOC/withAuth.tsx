import React from 'react';
import { ValueDefinations } from '../Utility/ValueDefination';
import jwtDecode from 'jwt-decode';


const withAuth = (WrappedComponent: any) => {

    const isTokenExpired = (exp : any) => {
        const currentTime = Date.now() / 1000;
        return exp < currentTime;
    }

    return(props: any) => {
        const accessToken: any = localStorage.getItem(ValueDefinations.TOKEN);

        if(accessToken){
            const decode: any = jwtDecode(accessToken);
            if(isTokenExpired(decode.exp))
            {
                window.location.replace("/sessionExpired");
                return null;
            }
            return <WrappedComponent {...props} />;
        }


        window.location.replace("/acessDenied");
        return null;

    };
};

export default withAuth;


