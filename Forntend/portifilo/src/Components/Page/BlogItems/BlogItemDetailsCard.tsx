import React from 'react'
import blogItemModel from '../../../Interfaces/blogItemModel';
import BlogItemDescription from './BlogItemDescription';


interface props {
    blogItem: blogItemModel;
}

function BlogItemDetailsCard(props: props) {

    return (
        <div>
            <div className="max-w-3xl mx-auto py-8 mb-1">
                <h1 className="text-3xl text-start font-bold mb-4">{props.blogItem.tittle}</h1>
                <div className="mt-8 flex items-center justify-between">
                    <div className="flex items-center">
                        <img src="https://via.placeholder.com/150" alt="Author Image" className="w-10 h-10 rounded-full mr-4 flex-shrink-0" />
                        <h2 className="text-gray-600 text-xl font-bold mb-2">{props.blogItem.author}</h2>
                    </div>
                    <p className="text-gray-600 flex items-center mb-4">Published on &nbsp;<span className="font-semibold">{props.blogItem.published}</span></p>
                </div>
                <img src="https://via.placeholder.com/150" alt="Blog Post Image" className="w-full mt-3 mb-6 rounded-lg" />

            </div>

            <div className="max-w-3xl mx-auto py-1 mb-1 prose">
                <BlogItemDescription data={props.blogItem.description} />
            </div>

        </div>
    )
}

export default BlogItemDetailsCard;
