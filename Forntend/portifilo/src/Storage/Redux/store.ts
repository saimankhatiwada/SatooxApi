import { configureStore, getDefaultMiddleware } from "@reduxjs/toolkit";
import { blogItemReducer } from "./blogItemSlice";
import { authenticationApi, blogItemApi } from "../../apis/api";
import { userAuthReducer } from "./userAuthSlice";

const store = configureStore({
    reducer: {
        userAuthStore: userAuthReducer,
        blogItemStore: blogItemReducer,
        [blogItemApi.reducerPath]: blogItemApi.reducer,
        [authenticationApi.reducerPath]: authenticationApi.reducer
    },
    middleware: ( getDefaultMiddleware ) =>
                  getDefaultMiddleware()
                  .concat(authenticationApi.middleware)
                  .concat(blogItemApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>;

export default store;
