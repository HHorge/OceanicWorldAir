import React from "react";
import { useState } from "react";
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import ToggleButtonGroup from 'react-bootstrap/ToggleButtonGroup'
import ToggleButton from 'react-bootstrap/ToggleButton'

import { Icons } from '../constants/iconsEnum'

import '../styles/form.css'

const FormComponent = (props) => {
    const { addedPackages, setAddedPackages, startCity, endCity, setModalShow, setOrder} = props;

    const [weight, setWeight] = useState(0)
    const [height, setHeight] = useState(0)
    const [width, setWidth] = useState(0)
    const [depth, setDepth] = useState(0)
    const [value, setValue] = useState([]);

    const handleAddPackage = () => {

        let parcel = {
         
                Weigth: weight,
                Depth: depth,
                Width: width,
                Height: height,
                RecordedDelivery: false,
                Weapons: value.includes(1),
                LiveAnimals: false,
                CautiousParcels: value.includes(2),
                RefrigeratedGoods: value.includes(3),
            
        }

        addedPackages.push(parcel)
        setAddedPackages(addedPackages)
        document.getElementById("package-input").reset();
        setWeight(0)
        setHeight(0)
        setWidth(0)
        setDepth(0)
        setValue([])
    }

    const handleChange = (val) => setValue(val);

    const isDisabled = () => {
        return !(weight && height && width && depth && startCity !== null && endCity !== null)
    }

    const handleWeight = (event) => {
        setWeight(event.target.value);
    }
    const handleHeight = (event) => {
        setHeight(event.target.value);
    }
    const handleWidth = (event) => {
        setWidth(event.target.value);
    }
    const handleDepth = (event) => {
        setDepth(event.target.value);
    }

    const handleSubmit = () => {
        let request = {
            Parcels: addedPackages,
            StartCityId: startCity,
            DestinationCityId: endCity
        }
        fetch('https://wa-oa-dk2.azurewebsites.net/RouteFinding/FindRoute', {
            method: 'post',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(request)
        }).then((response) => response.json())
        .then((data) => setOrder(data))
        .then(() => setModalShow(true));

    }

    return (
        <Form action="/submit" id="package-input">
            <Form.Group className="mb-3 mt-3" controlId="formWeight">
                <div className="custom-row">
                    <img className="icon" src={Icons.Weight}></img>
                    <Form.Control type="number" required placeholder="kg" onChange={handleWeight} />

                </div>
            </Form.Group>

            <Form.Group className="mb-3" controlId="formHeight">
                <div className="custom-row">
                    <img className="icon" src={Icons.Height}></img>
                    <Form.Control type="number" required placeholder="cm" onChange={handleHeight} />

                </div>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formWidth">
                <div className="custom-row">
                    <img className="icon" src={Icons.Width}></img>
                    <Form.Control type="number" required placeholder="cm" onChange={handleWidth} />

                </div>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formDepth">
                <div className="custom-row">
                    <img className="icon" src={Icons.Depth}></img>
                    <Form.Control type="number" required placeholder="cm" onChange={handleDepth} />

                </div>
            </Form.Group>
            <div className="custom-row">
                <ToggleButtonGroup type="checkbox" value={value} onChange={handleChange}>
                    <ToggleButton id="tbg-btn-1" variant="secondary" value={1}>
                        <img className="icon" src={Icons.Gun}></img>
                    </ToggleButton>
                    <ToggleButton id="tbg-btn-2" variant="secondary" value={2}>
                        <img className="icon" src={Icons.Fragile}></img>
                    </ToggleButton>
                    <ToggleButton id="tbg-btn-3" variant="secondary" value={3}>
                        <img className="icon" src={Icons.Fridge}></img>
                    </ToggleButton>
                </ToggleButtonGroup>
            </div>
            <div className="button-row mt-3">
                <div>
                    <img className="icon" src={Icons.Packages}></img>
                    <span>: {addedPackages.length}</span>
                </div>
                <Button type="button" disabled={isDisabled()} onClick={handleAddPackage}><img className="icon" src={Icons.Plus}></img></Button>
                <Button type="button" disabled={addedPackages.length < 1} onClick={() => handleSubmit()}><img className="icon" src={Icons.PaperPlane}></img></Button>
            </div>
            <div className="Spacer" style={{ height: "50px" }}>
            </div>
        </Form>
    )
}

export default FormComponent;