import { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';

import { Icons } from '../constants/iconsEnum'

export const ModalComponent = (props) => {
  const { order, addedPackages } = props;
const [sent, setSent] = useState(false)
  


  const titles = ["Fastest", "Cheapest", "Recommended"]

  const handleClick = (order) => {
    
   const request =  {
      id: order.id,
      startDestination: order.startDestination,
      endDestination: order.endDestination,
      price: order.price,
      time: order.time,
      parcels: addedPackages
    }

    fetch('https://wa-oa-dk2.azurewebsites.net/DbAdd/CreateBooking', {
      method: 'post',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request)
  }).then(() => setSent(true) )

    window.open(`mailto:test@example.com?subject=Receipt for order: ${order.id}&body=From ${order.startDestination} to ${order.endDestination}%0D%0APrice: $${order.price}%0D%0ADelivery time: ${order.time} hours`);
  }

  const routeContent = order.map((e, i) => {
    return (
      <div key={i}>
        <h5>{titles[i]}</h5>
        <div  style={{ display: 'flex', justifyContent: 'space-between', alignItems:'center'}}>
          <p >
            <img className="icon" src={Icons.Dollar}></img><span>{e.price}</span>
          </p>
          <p>
            <img className="icon" src={Icons.Time}></img><span>{e.time}h</span>
          </p>
          <Button type="button" onClick={() => { handleClick(e)}}><img className="icon" src={Icons.PaperPlane}></img></Button>
        </div>
      </div>
    )
  })

  const packageSentContent = (
    <>
    <Modal.Header closeButton>
        <Modal.Title id="contained-modal-title-vcenter">
          Success
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
       <h4>Order sent!</h4>
      </Modal.Body>
      <Modal.Footer>
        <Button onClick={() => window.location.reload(false)}>Close</Button>
      </Modal.Footer>
    </>
  )

  const modalContent = (
    <>
    <Modal.Header closeButton>
        <Modal.Title id="contained-modal-title-vcenter">
          Routes
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <h4>{order[0] && order[0].startDestination} to {order[0] && order[0].endDestination}</h4>
        {routeContent}
      </Modal.Body>
    </>
  )

  return (
    <Modal
      {...props}
      size="lg"
      aria-labelledby="contained-modal-title-vcenter"
      centered
    >
      {sent ? packageSentContent : modalContent}
    </Modal>
  );
}