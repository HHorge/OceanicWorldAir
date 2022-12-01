import React from "react";
import { useState } from "react";
import Map from "./map";
import Circle from "./circle";
import FormComponent from "./form";
import NavbarComponent from "./nav";

import { CITIES } from "../constants/cities";

import '../styles/main.css'

const Main = () => {

    const [startCity, setStartCity] = useState(null);
    const [endCity, setEndCity] = useState(null);
    const [addedPackages, setAddedPackages] = useState([])

    const cityNodes = CITIES.map((e, i) => <Circle key={i} x={e.x} y={e.y} id={e.id} startCity={startCity} endCity={endCity} setStartCity={setStartCity} setEndCity={setEndCity}></Circle>)

    return (
        <>
        <NavbarComponent />
        <div className="content">
            <div className="map-container">
                <Map></Map>
                {cityNodes}
            </div>
            <FormComponent addedPackages={addedPackages} setAddedPackages={setAddedPackages} />
        </div>
        </>
    )
}

export default Main;