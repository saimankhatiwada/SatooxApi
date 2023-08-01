import { Header, Footer } from "../Components/Layout/Layouts";
import { Home, Blog, NotFound, BlogDetails, Login, Register, Unauthorized, SessionExpired } from "../Pages/page";
import { Route, Routes } from "react-router-dom";
import { useEffect } from "react";
import { useDispatch } from 'react-redux';
import { setLoggedInUser } from "../Storage/Redux/userAuthSlice";
import jwtDecode from "jwt-decode";
import { ValueDefinations } from "../Utility/ValueDefination";

function App() {

    const dispatch = useDispatch();

    useEffect(() => {
        const localToken = localStorage.getItem(ValueDefinations.TOKEN);
        if(localToken){
            var decode: any = jwtDecode(localToken);
            var firstName = decode.given_name;
            var lastName = decode.family_name;
            var email = decode.email;
            var isActive = Boolean(decode.isActive.toLowerCase());
            dispatch(setLoggedInUser({firstName, lastName, email, isActive}));
        }

    })


    return (
        <div>
            <Header />
                <div className=" pb-5">
                    <Routes>
                        <Route path="/" element={<Home />} />
                        <Route path="/login" element={<Login />} />
                        <Route path="/register" element={<Register />} />
                        <Route path="/sessionExpired" element={<SessionExpired />} />
                        <Route path="/blog" element={<Blog />} />
                        <Route path="/blog/:blogId" element={<BlogDetails />} />
                        <Route path="/acessDenied" element={<Unauthorized />} />
                        <Route path="*" element={<NotFound />} />
                    </Routes>
                </div>
            <Footer />
        </div>
    );
}

export default App;
