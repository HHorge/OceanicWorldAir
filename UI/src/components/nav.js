import React from "react";
import { Navbar } from "react-bootstrap";

const NavbarComponent = () => {
    return (
        <Navbar className="navbar navbar-expand-sm navbar-light bg-light justify-content-md-center">
            <a className="navbar-brand" href="#">
                <img src="https://i.imgur.com/wMdQ64j.png" style={{ height: "80px" }}></img>
            </a>
        </Navbar>
    )
}

export default NavbarComponent;