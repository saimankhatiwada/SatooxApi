import { configureStore, getDefaultMiddleware } from "@reduxjs/toolkit";
import { blogItemReducer } from "./blogItemSlice";
import { blogItemApi } from "../../apis/api";

const store = configureStore({
    reducer: {
        blogItemStore: blogItemReducer,
        [blogItemApi.reducerPath]: blogItemApi.reducer
    },
    middleware: ( getDefaultMiddleware ) => getDefaultMiddleware().concat(blogItemApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>;

export default store;
