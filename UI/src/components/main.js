import React from "react";
import { useState } from "react";
import Map from "./map";
import Circle from "./circle";
import FormComponent from "./form";
import NavbarComponent from "./nav";
import { SignInButton } from "./loginButton";
import { useIsAuthenticated } from "@azure/msal-react";

import { CITIES } from "../constants/cities";

import '../styles/main.css'

const Main = () => {

    const [startCity, setStartCity] = useState(null);
    const [endCity, setEndCity] = useState(null);
    const [addedPackages, setAddedPackages] = useState([])

    const isAuthenticated = useIsAuthenticated();

    const cityNodes = CITIES.map((e, i) => <Circle key={i} x={e.x} y={e.y} id={e.id} startCity={startCity} endCity={endCity} setStartCity={setStartCity} setEndCity={setEndCity}></Circle>)

    console.log(addedPackages)



    const content = <div className="content">
        <div className="map-container">
            <Map />
            {cityNodes}
        </div>
        <FormComponent addedPackages={addedPackages} setAddedPackages={setAddedPackages} startCity={startCity} endCity={endCity} />
    </div>
    
    return (
        <>
            <NavbarComponent />
            {isAuthenticated ?  content : <SignInButton /> }
        </>
    )
}

export default Main;