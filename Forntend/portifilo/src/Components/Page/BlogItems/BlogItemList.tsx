import React from 'react'
import { useEffect } from "react";
import { useGetBlogQuery } from "../../../apis/BlogItemApis";
import { blogItemModel } from "../../../Interfaces/interface";
import BlogItemCard from './BlogItemCard';
import { setBlogItem } from "../../../Storage/Redux/blogItemSlice";
import { useDispatch } from 'react-redux';

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
        return <div>Loading</div>;
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

export default BlogItemList;