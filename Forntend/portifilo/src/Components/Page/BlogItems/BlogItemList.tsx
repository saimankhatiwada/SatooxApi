import React from 'react'
import { useEffect } from "react";
import { useGetBlogQuery } from "../../../apis/BlogItemApis";
import { blogItemModel } from "../../../Interfaces/interface";
import BlogItemCard from './BlogItemCard';
import { setBlogItem } from "../../../Storage/Redux/blogItemSlice";
import { withAuth } from "../../../HOC/HOC";
import { useDispatch } from 'react-redux';
import Loader from '../../Layout/Loader';


function BlogItemList() {

    const dispatch = useDispatch();

    const { data, isLoading } = useGetBlogQuery(null);

    useEffect( () => {
        if(!isLoading){
            dispatch(setBlogItem(data));
        }
    }, [isLoading]);

    if(isLoading)
    {
        return (
            <Loader />
        )
    }

    return (
        <div className="mb-8">
            {data.length > 0 &&
                data.map( (blogItem: blogItemModel) => (
                    <BlogItemCard blogItem={blogItem} key={blogItem.id} /> )
                )
            }
        </div>

    )
}

export default withAuth(BlogItemList);
