import React from "react";
import { useState } from "react";
import Map from "./map";
import Circle from "./circle";
import FormComponent from "./form";
import NavbarComponent from "./nav";
import Button from 'react-bootstrap/Button'
import LoginMethod from './loginMethod';

import { CITIES } from "../constants/cities";

import '../styles/main.css'

const Main = () => {

    const [startCity, setStartCity] = useState(null);
    const [endCity, setEndCity] = useState(null);
    const [addedPackages, setAddedPackages] = useState([])
    const [loggedIn, setLoggedIn] = useState(false)


    const cityNodes = CITIES.map((e, i) => <Circle key={i} x={e.x} y={e.y} id={e.id} startCity={startCity} endCity={endCity} setStartCity={setStartCity} setEndCity={setEndCity}></Circle>)

    console.log(addedPackages)

    //const LoginSession = new LoginMethod();
    const loginButton = <LoginMethod setLoggedIn={setLoggedIn}></LoginMethod>
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
            {loggedIn ?  content : loginButton }
       
            {console.log(window.sessionStorage)}
        </>
    )
}

export default Main;