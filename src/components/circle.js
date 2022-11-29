import React from 'react';
import '../styles/circle.css'

const Circle = (props) => {

    const {color, x, y, city} = props 
    return (
        <div onClick={() => window.open(`https://en.wikipedia.org/wiki/${city}`)} className='city-circle' style={{"backgroundColor": color, "top": `${y}px`, "left": `${x}px`}}>
        </div>
    )
}

export default Circle;