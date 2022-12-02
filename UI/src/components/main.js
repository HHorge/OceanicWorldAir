import React from "react";
import { useState } from "react";
import Map from "./map";
import Circle from "./circle";
import FormComponent from "./form";
import NavbarComponent from "./nav";
import {ModalComponent} from './modal'
import { SignInButton } from "./loginButton";
import { useIsAuthenticated } from "@azure/msal-react";

import { CITIES } from "../constants/cities";

import '../styles/main.css'

const Main = () => {

    const [startCity, setStartCity] = useState(null);
    const [endCity, setEndCity] = useState(null);
    const [addedPackages, setAddedPackages] = useState([])
    const [order, setOrder] = useState([])
    const [modalShow, setModalShow] = React.useState(false);

    const isAuthenticated = useIsAuthenticated();

    const cityNodes = CITIES.map((e, i) => <Circle key={i} x={e.x} y={e.y} id={e.id} startCity={startCity} endCity={endCity} setStartCity={setStartCity} setEndCity={setEndCity}></Circle>)

    const content = <div className="content">
        <div className="map-container">
            <Map />
            {cityNodes}
        </div>
        <FormComponent addedPackages={addedPackages} setAddedPackages={setAddedPackages} startCity={startCity} endCity={endCity} setModalShow={setModalShow} setOrder={setOrder}/>
    </div>
    const modal = 
        <ModalComponent
            order={order}
            show={modalShow}
            addedPackages={addedPackages}
            onHide={() => setModalShow(false)}
        />
    
    return (
        <>
            <NavbarComponent />
            {isAuthenticated ? content : <SignInButton />}
            {modal}
        </>
    )
}

export default Main;