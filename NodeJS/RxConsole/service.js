
// const https = require('https');
// const URL = 'https://reqres.in/api/users';
//
// https.get(`${URL}?page=1&per_page=50`, res => {

//     res.setEncoding("utf8");
//     let body = "";
//     res.on("data", data => {
//         body += data;
//     });

//     res.on('end', () => {
//         body = JSON.parse(body);
//         console.log(body);
//       });      
// });

const axios = require("axios");
const URL = 'https://reqres.in/api/users';

const getData = async () => {
  try {
    const response = await axios.get(`${URL}?pages=1&per_page=50`);
    const users = response.data;
    return users;
  } catch (error) {
    console.log(error);
  }
};

// exports.getData = getData;
module.exports = {
    getData
}

