import React from "react";
import Map from "./map";
import Circle from "./circle";

import '../styles/main.css'

const Main = () => {
    document.addEventListener('click', printMousePos, true);
    function printMousePos(e) {

        let cursorX = e.pageX;
        let cursorY = e.pageY;
        
        console.log("x: "+(cursorX-12)  + ", y: " + (cursorY-12));
    }

let d = 5;

    let data = [
        { city: "Morocco", x: 138, y: 151},
        { city: "Morocco", x: 138, y: 151},
        { city: "Morocco", x: 96, y: 198 },
        
    ]

    const circles = data.map((e, i) => <Circle key={i} city={e.city} color="grey" x={e.x} y={e.y}></Circle>)
    return (
        <div className="container">
            <Map></Map>
            {circles}
        </div>
    )
}

export default Main;