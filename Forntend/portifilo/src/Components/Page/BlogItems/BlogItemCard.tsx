import React, { useEffect, useState } from 'react';
import blogItemModel from '../../../Interfaces/blogItemModel';
import { Link } from 'react-router-dom';
import { ValueDefinations } from '../../../Utility/ValueDefination';
import { Buffer } from 'buffer';
import axios from 'axios';
import { toastNotify } from '../../../Helpers/Helper';

interface props {
  blogItem: blogItemModel;
}

function BlogItemCard(props: props) {

    const [adminImageData, setAdminImageData] = useState<string>("null");
    const [blogImageData, setBlogImageData] = useState<string>("null");

    const delay = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms));


    const fetchAdminImageWithAuthorization = async () => {
        await delay(5);
        const adminImageUrl = `http://localhost:5010/Image/admin/${props.blogItem.adminId}`;
        const authToken = localStorage.getItem(ValueDefinations.TOKEN);
        const response = await axios.get(adminImageUrl, {
            headers: {
              Authorization: `Bearer ${authToken}`,
            },
            responseType: 'arraybuffer',
        });
        const imageType = response.headers['content-type'];
        const base64Image = Buffer.from(response.data, 'binary').toString('base64');
        const imageDataUrl = `data:${imageType};base64,${base64Image}`;
        setAdminImageData(imageDataUrl);
    }

    const fetchBlogImageWithAuthorization = async () => {
        await delay(15);
        const adminImageUrl = `http://localhost:5010/Image/blog/${props.blogItem.id}`;
        const authToken = localStorage.getItem(ValueDefinations.TOKEN);
        const response = await axios.get(adminImageUrl, {
            headers: {
              Authorization: `Bearer ${authToken}`,
            },
            responseType: 'arraybuffer',
        });
        const imageType = response.headers['content-type'];
        const base64Image = Buffer.from(response.data, 'binary').toString('base64');
        const imageDataUrl = `data:${imageType};base64,${base64Image}`;
        setBlogImageData(imageDataUrl);
    }

    const fetchImages = async () => {

        try {
                await fetchAdminImageWithAuthorization();
                await fetchBlogImageWithAuthorization();
        }catch (error) {
            toastNotify('Error fetching images:', "error");
        }
    }

    useEffect(() => {
        fetchImages();
    });

    return (

        <>
            <div className="flex items-center justify-center">
                <div className="bg-white shadow-lg rounded-lg overflow-hidden my-6 w-1/2">
                    <img src={blogImageData} alt={props.blogItem.imageName} className="w-full h-32 object-cover" />
                    <div className="p-4">
                        <h2 className="text-lg font-bold mb-2">{props.blogItem.tittle}</h2>
                        <div className="flex items-center mb-2">
                            <img src={adminImageData} alt={props.blogItem.adminId} className="w-6 h-6 rounded-full mr-2" />
                            <p className="text-gray-600">{props.blogItem.author}</p>
                        </div>

                        <div className="flex items-center justify-between">
                            <p className="text-gray-500 text-sm">Updated: <span className="font-semibold">{props.blogItem.modified}</span></p>
                            <Link to={`/blog/${props.blogItem.id}`} className="bg-gray-500 hover:bg-gray-600 text-white px-4 py-2 rounded-md">Read</Link>
                        </div>
                    </div>

                </div>
            </div>
        </>

    )
}

export default BlogItemCard;
