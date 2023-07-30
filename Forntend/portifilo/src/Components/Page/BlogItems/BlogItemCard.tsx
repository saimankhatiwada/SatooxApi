import React from 'react';
import blogItemModel from '../../../Interfaces/blogItemModel';
import { Link } from 'react-router-dom';

interface props {
  blogItem: blogItemModel;
}

function BlogItemCard(props: props) {

  return (
    <div className="flex items-center justify-center">
        <div className="bg-white shadow-lg rounded-lg overflow-hidden my-6 w-1/2">
            <img src="https://via.placeholder.com/150" alt="Blog Image" className="w-full h-32 object-cover" />
            <div className="p-4">
                <h2 className="text-lg font-bold mb-2">{props.blogItem.tittle}</h2>
                <div className="flex items-center mb-2">
                    <img src="https://via.placeholder.com/150" alt="Profile Image" className="w-6 h-6 rounded-full mr-2" />
                    <p className="text-gray-600">{props.blogItem.author}</p>
                </div>

                <div className="flex items-center justify-between">
                    <p className="text-gray-500 text-sm">Updated: <span className="font-semibold">{props.blogItem.modified}</span></p>
                    <Link to={`/blog/${props.blogItem.id}`} className="bg-gray-500 hover:bg-gray-600 text-white px-4 py-2 rounded-md">Read</Link>
                </div>
            </div>

        </div>
    </div>
  )
}

export default BlogItemCard;
