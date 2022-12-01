import React from "react";
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';

import { Icons } from '../icons/iconsEnum'

import '../styles/form.css'

const FormComponent = (props) => {
    const { addedPackages, setAddedPackages } = props;
    return (
        <Form>

            <Form.Group className="mb-3 mt-3" controlId="formWeight">
                <div className="custom-row">
                    <img className="icon" src={Icons.Weight}></img>
                    <Form.Control type="number" required placeholder="kg" />

                </div>
            </Form.Group>

            <Form.Group className="mb-3" controlId="formHeight">
                <div className="custom-row">
                    <img className="icon" src={Icons.Height}></img>
                    <Form.Control type="number" required placeholder="cm" />

                </div>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formWidth">
                <div className="custom-row">
                    <img className="icon" src={Icons.Width}></img>
                    <Form.Control type="number" required placeholder="cm" />

                </div>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formDepth">
                <div className="custom-row">
                    <img className="icon" src={Icons.Depth}></img>
                    <Form.Control type="number" required placeholder="cm" />

                </div>
            </Form.Group>
            <div className="custom-row">
                <img className="icon" src={Icons.Type}></img>
                <Form.Select aria-label="Default select example">
                    <option>type</option>
                    <option value="1">Animal</option>
                    <option value="2">Guns</option>
                    <option value="3">Drugs</option>
                </Form.Select>
            </div>
            <div className="button-row">
                <div>
                    <img className="icon" src={Icons.Packages}></img>
                    <span>:{addedPackages}</span>
                </div>
                <Button type="submit">Submit form</Button>
            </div>
            <div className="Spacer" style={{ height: "50px" }}>
            </div>
        </Form>
    )
}

export default FormComponent;