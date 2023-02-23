const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');
const helmet = require('helmet');
const morgan = require('morgan');

// defining the Express app
const app = express();
// defining an array to work as the database (temporary solution)
const ads = [
  { title: 'Hello, world (again)!' }
];

const sessions = [{
  key: "ABCD-EFGH-IJKL-MNOP",
  playerMax: 4,
  users: []
}]

const board = [
  "Hello", "Hello",
  "Hello", "Hello",
  "Hello", "Hello",
  "Hello", "Hello",
  "Hello", "Hello",
  "Hello", "Hello",
  "Hello", "Hello",
  "Hello", "Hello",
  "Hello", "Hello",
  "Hello", "Hello",
  "Hello", "Hello",
  "Hello", "Hello",
  "Hello"]


// adding Helmet to enhance your Rest API's security
app.use(helmet());

// using bodyParser to parse JSON bodies into JS objects
app.use(bodyParser.json());

// enabling CORS for all requests
app.use(cors());

// adding morgan to log HTTP requests
app.use(morgan('combined'));

app.post("/register", (req, res) => {
  let options = {
    "sessionKey": req.header("SessionKey"),
    "username": req.header("Username")
  };
  console.log("Somebody tried to register!");
  console.log(`Username: ${options.username} Key: ${options.sessionKey}`);

  if (sessions.find(x => x.key === options.sessionKey)) {
    console.log("Session Exists")
    res.status(200).send("Success.");
  }

  else{
    console.log("Session Doesnt exist, Erroring out")
    res.status(403).send("The Session Key you entered doesn't exist")
  }


})


app.post("/s", (req, res) => {
  console.log("Somebody sent a button key!");
  let options = {
    "sessionKey": req.header("SessionKey"),
    "username": req.header("Username")
  };

  console.log(options)
  console.log(`Username: ${options.username} Number: ${req.body.number}, Pressed: ${req.body.pressed}`);

  res.status(200).send("Success.");
})

// starting the server
app.listen(8080, () => {
  console.log('listening on port 8080');
});