import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const blogItemApi = createApi({
    reducerPath:"BlogItemApi",
    baseQuery: fetchBaseQuery({
        baseUrl:"http://localhost:5010/"
    }),
    tagTypes: ["Blog"],
    endpoints: (builder) => ({
        getBlog: builder.query({
            query: () => ({
                url:"blog"
            }),
            providesTags: ["Blog"]
        }),
        getBlogById: builder.query({
            query: (id) => ({
                url:`blog/${id}/get`
            }),
            providesTags: ["Blog"]
        }),
    }),
});


export const { useGetBlogQuery, useGetBlogByIdQuery } = blogItemApi;
export default blogItemApi;
