import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';

export const ModalComponent = (props) => {
  const { order } = props;
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

        <h4>{order.startDestination} to {order.endDestination}</h4>
        <p>
          <b>$: </b><span>{order.price}</span> 
        </p>
        <p>
        <b>Time: </b><span>{order.time}h</span> 
        </p>
      </Modal.Body>
      <Modal.Footer>
        <Button onClick={props.onHide}>Close</Button>
      </Modal.Footer>
    </Modal>
  );
}