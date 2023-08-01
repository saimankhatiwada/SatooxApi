import ReactDOM from 'react-dom/client';
import './index.css';
import App from './Container/App';
import React from 'react';
import { BrowserRouter }from "react-router-dom";
import { Provider } from 'react-redux';
import { Store } from './Storage/storage';
import { ToastContainer } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
    <Provider store={Store}>
        <BrowserRouter>
            <ToastContainer />
            <App />
        </BrowserRouter>
    </Provider>
);

