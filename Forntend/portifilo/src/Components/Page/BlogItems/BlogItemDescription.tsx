import React from 'react'
import DOMPurify from "dompurify";

interface htmlData{
    data: string;
}

function BlogItemDescription(props: htmlData) {
  return (

    <div dangerouslySetInnerHTML={{ __html: DOMPurify.sanitize(props.data) }}></div>
  )
}

export default BlogItemDescription
