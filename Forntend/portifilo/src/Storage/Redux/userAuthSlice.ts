import { createSlice } from "@reduxjs/toolkit";
import { userModel } from "../../Interfaces/interface";

export const initialstate : userModel = {
    firstName: "",
    lastName: "",
    email: "",
    isActive: false
};

export const userAuthSlice = createSlice({
    name: "UserAuth",
    initialState: initialstate,
    reducers:{
        setLoggedInUser: ( state, action ) => {
            state.firstName = action.payload.firstName;
            state.lastName = action.payload.lastName;
            state.email = action.payload.email;
            state.isActive = action.payload.isActive;
        },
    },
});

export const { setLoggedInUser } = userAuthSlice.actions;
export const userAuthReducer = userAuthSlice.reducer;
