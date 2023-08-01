import React, { useEffect, useState } from 'react'
import blogItemModel from '../../../Interfaces/blogItemModel';
import BlogItemDescription from './BlogItemDescription';
import { withAuth } from '../../../HOC/HOC';
import { toastNotify } from '../../../Helpers/Helper';
import { ValueDefinations } from '../../../Utility/ValueDefination';
import { Buffer } from 'buffer';
import axios from 'axios';


interface props {
    blogItem: blogItemModel;
}

function BlogItemDetailsCard(props: props) {

    const [adminImageData, setAdminImageData] = useState<string>("null");
    const [blogImageData, setBlogImageData] = useState<string>("null");

    const fetchAdminImageWithAuthorization = async () => {
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
        const blogImageUrl = `http://localhost:5010/Image/blog/${props.blogItem.id}`;
        const authToken = localStorage.getItem(ValueDefinations.TOKEN);
        const response = await axios.get(blogImageUrl, {
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

    }, []);

    return (
        <div>
            <div className="max-w-3xl mx-auto py-8 mb-1">
                <h1 className="text-3xl text-start font-bold mb-4">{props.blogItem.tittle}</h1>
                <div className="mt-8 flex items-center justify-between">
                    <div className="flex items-center">
                        <img src={adminImageData} alt="Author Image" className="w-10 h-10 rounded-full mr-4 flex-shrink-0" />
                        <h2 className="text-gray-600 text-xl font-bold mb-2">{props.blogItem.author}</h2>
                    </div>
                    <p className="text-gray-600 flex items-center mb-4">Published on &nbsp;<span className="font-semibold">{props.blogItem.published}</span></p>
                </div>

                <img src={blogImageData} alt="Blog Image" className="rounded-s mr-4 mt-3" />

            </div>

            <div className="max-w-3xl mx-auto py-1 mb-1 prose">
                <BlogItemDescription data={props.blogItem.description} />
            </div>

        </div>
    )
}

export default withAuth(BlogItemDetailsCard);
