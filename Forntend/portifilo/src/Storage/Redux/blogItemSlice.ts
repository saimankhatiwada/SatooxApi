import { createSlice } from "@reduxjs/toolkit";

const initialstate = {
    blogItem: [],
};

export const blogItemSlice = createSlice({
    name: "BlogItem",
    initialState: initialstate,
    reducers:{
        setBlogItem: ( state, action ) => {
            state.blogItem = action.payload;
        },
    },
});

export const { setBlogItem } = blogItemSlice.actions;
export const blogItemReducer = blogItemSlice.reducer;
