import { Header, Footer } from "../Components/Layout/Layouts";
import { Home, Blog, NotFound, BlogDetails } from "../Pages/page";
import { Route, Routes } from "react-router-dom";

function App() {
  return (
    <div>
        <Header />
            <div className=" pb-5">
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/blog" element={<Blog />} />
                    <Route path="/blog/:blogId" element={<BlogDetails />} />
                    <Route path="*" element={<NotFound />} />
                </Routes>
            </div>
        <Footer />
    </div>
  );
}

export default App;
