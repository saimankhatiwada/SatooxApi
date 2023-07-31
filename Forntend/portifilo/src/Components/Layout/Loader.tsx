import React from 'react'

let loading = require("../../Assests/loading.gif");

function Loader() {
  return (
    <div className="flex items-center justify-center h-screen">
        <img className="mx-auto absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2" src={ loading } alt="loading" loading="lazy" />
    </div>
  )
}

export default Loader;
