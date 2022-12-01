import React from 'react';
import { useState, useEffect } from 'react';
import '../styles/circle.css'

const Circle = (props) => {

    const [color, setColor] = useState("grey");
    const { x, y, id, setStartCity, setEndCity, startCity, endCity} = props 

    useEffect(() => {
        getColor()
    },[startCity, endCity])
    
    const getColor = () => {
       if(startCity === id) setColor("green")
       else if(endCity === id) setColor("red")
       else setColor("grey")
    }

    
    const handleClick = () => {
        id === startCity ? setStartCity(null) : startCity === null ? setStartCity(id) : id === endCity ? setEndCity(null) : setEndCity(id);
        
        
        console.log(color)
    }

    console.log("startCity", startCity, "id", id)
    return (
        <div onClick={() => {handleClick()}} className='city-circle' style={{"backgroundColor": color, "top": `${y}px`, "left": `${x}px`}}>  
        </div>
    )
}

export default Circle;