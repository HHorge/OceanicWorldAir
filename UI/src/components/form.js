import React from "react";
import { useState } from "react";
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import ButtonGroup from 'react-bootstrap/ButtonGroup'
import ToggleButton from 'react-bootstrap/ToggleButton'

import { Icons } from '../constants/iconsEnum'

import '../styles/form.css'

const FormComponent = (props) => {
    const { addedPackages, setAddedPackages, startCity, endCity } = props;

    const [weight, setWeight] = useState(0)
    const [height, setHeight] = useState(0)
    const [width, setWidth] = useState(0)
    const [depth, setDepth] = useState(0)
    //const [type, setType] = useState(0)

    const [radioValue, setRadioValue] = useState('1');

    const radios = [
      { name: 'Active', value: '1' },
      { name: 'Radio', value: '2' },
      { name: 'Radio', value: '3' },
    ];

    const handleAddPackage = () => {

        let package1 = { startCity: startCity, endCity: endCity, weight: weight, height: height, width: width, depth: depth }
        console.log("addedPackages", addedPackages, "package1", package1)
        addedPackages.push(package1)
        setAddedPackages(addedPackages)
        document.getElementById("package-input").reset();
        setWeight(0)
        setHeight(0)
        setWidth(0)
        setDepth(0)
        //console.log( event.value) // from elements property

    }

    const isDisabled = () => {
        console.log(weight, height, width, depth, startCity, endCity)
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
            <ButtonGroup>
        {radios.map((radio, idx) => (
          <ToggleButton
            key={idx}
            id={`radio-${idx}`}
            type="radio"
            variant={idx % 2 ? 'outline-success' : 'outline-danger'}
            name="radio"
            value={radio.value}
            checked={radioValue === radio.value}
            onChange={(e) => setRadioValue(e.currentTarget.value)}
          >
             <img className="icon" src={Icons.Packages}></img>
          </ToggleButton>
        ))}
      </ButtonGroup>
            </div>
            <div className="button-row mt-3">
                <div>
                    <img className="icon" src={Icons.Packages}></img>
                    <span>: {addedPackages.length}</span>
                </div>
                <Button type="button" disabled={isDisabled()} onClick={handleAddPackage}><img className="icon" src={Icons.Plus}></img></Button>
                <Button type="submit" disabled={addedPackages.length === 0}><img className="icon" src={Icons.PaperPlane}></img></Button>
            </div>
            <div className="Spacer" style={{ height: "50px" }}>
            </div>
        </Form>
    )
}

export default FormComponent;