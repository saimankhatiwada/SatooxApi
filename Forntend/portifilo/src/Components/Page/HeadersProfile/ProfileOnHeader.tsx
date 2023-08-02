import React, { useEffect, useState } from 'react'
import { withAuth } from '../../../HOC/HOC';
import jwtDecode from 'jwt-decode';
import { ValueDefinations } from '../../../Utility/ValueDefination';
import axios from 'axios';
import { Buffer } from 'buffer';
import { toastNotify } from '../../../Helpers/Helper';
import { userModel } from '../../../Interfaces/interface';

function ProfileOnHeader() {

    const [userImageData, setUserImageData] = useState<string>("null");

    const fetchUserImageWithAuthorization = async () => {
        var userImageUrl = "";
        const authToken = localStorage.getItem(ValueDefinations.TOKEN);
        if(authToken)
        {
            const decode: any = jwtDecode(authToken);
            userImageUrl = `http://localhost:5010/Image/user/${decode.sub}`;
        }

        const response = await axios.get(userImageUrl, {
            headers: {
              Authorization: `Bearer ${authToken}`,
            },
            responseType: 'arraybuffer',
        });
        const imageType = response.headers['content-type'];
        const base64Image = Buffer.from(response.data, 'binary').toString('base64');
        const imageDataUrl = `data:${imageType};base64,${base64Image}`;
        setUserImageData(imageDataUrl);
    }

    useEffect(() => {
        try{
            fetchUserImageWithAuthorization();
        }catch(error){
            toastNotify("User Image not found", "error");
        }
    });

    return (
       
        <img src={userImageData} alt={userImageData} className="w-10 h-10 rounded-full mr-4 flex-shrink-0" />

    )
}

export default withAuth(ProfileOnHeader);
