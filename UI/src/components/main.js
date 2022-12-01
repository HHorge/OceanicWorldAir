import React from "react";
import { useState } from "react";
import Map from "./map";
import Circle from "./circle";
import FormComponent from "./form";
import NavbarComponent from "./nav";

import '../styles/main.css'

const Main = () => {

    const [startCity, setStartCity] = useState(null);
    const [endCity, setEndCity] = useState(null);


    // document.addEventListener('click', printMousePos, true);
    // function printMousePos(e) {

    //     let cursorX = e.pageX;
    //     let cursorY = e.pageY;

    //     console.log("x: " + (cursorX - 12) + ", y: " + (cursorY - 12));
    // }

    let data = [
        { isActive: true, city: "Morocco", x: 138, y: 151, id: 0 },
        { isActive: true, city: "Morocco", x: 96, y: 198, id: 1 },
        { isActive: true, city: "Morocco", x: 21, y: 104, id: 2 },
        { isActive: true, city: "Morocco", x: 66, y: 110, id: 3 },
        { isActive: true, city: "Morocco", x: 0, y: 203, id: 4 },
        { isActive: true, city: "Morocco", x: 22, y: 249, id: 5 },
        { isActive: true, city: "Morocco", x: 85, y: 269, id: 6 },
        { isActive: true, city: "Morocco", x: 134, y: 275, id: 7 },
        { isActive: true, city: "Morocco", x: 57, y: 399, id: 8 },
        { isActive: true, city: "Morocco", x: 172, y: 369, id: 9 },
        { isActive: true, city: "Morocco", x: 178, y: 453, id: 10 },
        { isActive: true, city: "x", x: 179, y: 305, id: 11 },
        { isActive: true, city: "x", x: 191, y: 204, id: 12 },
        { isActive: true, city: "x", x: 194, y: 116, id: 13 },
        { isActive: true, city: "x", x: 269, y: 175, id: 14 },
        { isActive: true, city: "x", x: 249, y: 236, id: 15 },
        { isActive: true, city: "x", x: 272, y: 272, id: 16 },
        { isActive: true, city: "x", x: 243, y: 354, id: 17 },
        { isActive: true, city: "x", x: 251, y: 430, id: 18 },
        { isActive: true, city: "x", x: 277, y: 472, id: 19 },
        { isActive: true, city: "x", x: 176, y: 85, id: 20 },
        { isActive: true, city: "x", x: 305, y: 306, id: 21 },
        { isActive: true, city: "x", x: 326, y: 201, id: 22 },
        { isActive: true, city: "x", x: 340, y: 264, id: 23 },
        { isActive: true, city: "x", x: 406, y: 253, id: 24 },
        { isActive: true, city: "x", x: 392, y: 427, id: 25 },
        { isActive: true, city: "x", x: 353, y: 483, id: 26 },
        { isActive: true, city: "x", x: 336, y: 406, id: 27 },
        { isActive: true, city: "x", x: 201, y: 528, id: 28 },
        { isActive: true, city: "x", x: 332, y: 351, id: 29 },
        { isActive: true, city: "x", x: 92, y: 67, id: 30 },
        { isActive: true, city: "x", x: 284, y: 109, id: 31 }
    ]

    const circles = data.map((e, i) => <Circle key={i} x={e.x} y={e.y} id={e.id} startCity={startCity} endCity={endCity} setStartCity={setStartCity} setEndCity={setEndCity}></Circle>)

    return (
        <>
        <NavbarComponent />
        <div className="content">
            <div className="map-container">
                <Map></Map>
                {circles}
            </div>
            <FormComponent />
        </div>
        </>
    )
}

export default Main;