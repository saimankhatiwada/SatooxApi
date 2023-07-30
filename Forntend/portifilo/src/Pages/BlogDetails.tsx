import React from 'react'
import { useParams } from 'react-router-dom'
import { useGetBlogByIdQuery } from '../apis/BlogItemApis';
import { useDispatch } from 'react-redux';
import { useEffect } from "react";
import { setBlogItem } from "../Storage/Redux/blogItemSlice";

function BlogDetails() {

    const dispatch = useDispatch();

    const { blogId } = useParams();

    const { data, isLoading } = useGetBlogByIdQuery(blogId);

    useEffect( () => {
        if(!isLoading){
            dispatch(setBlogItem(data));
        }
    }, [isLoading]);

    if(isLoading)
    {
        return <div>Loading</div>;
    }
    return (
        <div>
            Loaded
        </div>
    )
}

export default BlogDetails
