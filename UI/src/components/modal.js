import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';

import { Icons } from '../constants/iconsEnum'

export const ModalComponent = (props) => {
  const { order } = props;


  const titles = ["Fastest", "Cheapest", "Recommended"]
  const content = order.map((e, i) => {
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
          <Button type="button" onClick={() => { }}><img className="icon" src={Icons.PaperPlane}></img></Button>
        </div>
      </div>
    )
  })

  return (
    <Modal
      {...props}
      size="lg"
      aria-labelledby="contained-modal-title-vcenter"
      centered
    >
      <Modal.Header closeButton>
        <Modal.Title id="contained-modal-title-vcenter">
          Order Sent
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>

        <h4>{order[0] && order[0].startDestination} to {order[0] && order[0].endDestination}</h4>
        {content}
      </Modal.Body>
      <Modal.Footer>
        <Button onClick={() => window.location.reload(false)}>Close</Button>
      </Modal.Footer>
    </Modal>
  );
}