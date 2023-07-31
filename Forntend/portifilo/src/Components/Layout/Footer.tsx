import React from 'react'
import { FavoriteBorderOutlined } from '@mui/icons-material'

function Footer() {
  return (
    <footer className='bg-[#FBFBFB] shadow-md shadow-black/5 dark:bg-neutral-600 dark:shadow-black/10 z-20 bottom-0 left-0 w-full p-4 border-t text-center text-white'>
        &copy; Made with <FavoriteBorderOutlined className='text-red-700'/> by Saiman Khatiwada
    </footer>
  )
}

export default Footer
