
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';


import { useState } from "react";

function App() {

  const [transAddress, setTransAddress] = useState("");
  const [trans, settrans] = useState([]);
  const [wallet, setwallet] = useState("");
  const [wals, setwals] = useState([]);




  const API = (e) => {
    e.preventDefault();


    let inputobj = {
      "transAddress": transAddress
    }
    fetch("https://localhost:7025/api/controller/PostHash", {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(inputobj),
    }).then((response) => response.json()
    ).then(inputobj => {
      settrans(inputobj);
      alert(
        ' Transaction Address :    ' +  inputobj.transAddress + '             ' +
        ' Transaction From : ' +  inputobj.transactionFrom + '           ' +
        ' Transaction To : ' +  inputobj.transactionTo + '          ' +
        ' Transaction Amount : ' +  inputobj.transactionAmount + '         ' +
        ' Transaction Gas Price : ' +  inputobj.gasPrice)
    }).catch((err) => {
      alert('error:' + err.message);
    });
  }

  const AAPI = (e) => {
    e.preventDefault();
    let wal = {
      "wallet": wallet
    }
    fetch("https://localhost:7025/api/controller/PostAdres", {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(wal),
    }).then((res) => res.json()
    ).then(wal => {
      setwals(wal);
      alert("Transaction Hash " + wal.trans);

    }).catch((err) => {
      alert('error:' + err.message);
    });
  }

  return (
    <div className="row" align="center">
      <div className="offset-lg-3 col-lg-6" style={{ marginTop: '100px' }}>

        <br></br>
        <h1>&nbsp;Transaction Hash </h1>
        <br></br>
        <br></br>
        <Form>
          <Row> &nbsp;&nbsp;

            <Form.Group as={Col} >
              <Form.Label> Wallet Address </Form.Label>
              <Form.Control placeholder="wallet" id="id"
                value={wallet}
                onChange={(event) => {
                  setwallet(event.target.value);
                }} />
            </Form.Group>
          </Row>
          <div>  &nbsp;
            <button className="btn btn-success mt-4" onClick={AAPI}>
              SAVE
            </button> &nbsp;
          </div>


        </Form>
        <br></br>
        <br></br>
        <h1>&nbsp;Transaction Details </h1>
        <br></br>
        <Form>
          <Row> &nbsp;&nbsp;

            <Form.Group as={Col} >
              <Form.Label>Transaction Hash </Form.Label>
              <Form.Control placeholder="Transaction" id="id"

                value={transAddress}
                onChange={(event) => {
                  setTransAddress(event.target.value);
                }} />
            </Form.Group>
          </Row>
          <div>  &nbsp;
            <button className="btn btn-success mt-4" onClick={API}>
              view
            </button> &nbsp;
          </div>
        </Form>
        <br></br>



        <br></br>

      </div>
    </div>
  );
}



export default App;