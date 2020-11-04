import axios from 'axios';
import { user, pagedUsers } from './users';

const URL = 'https://reqres.in/api/users';

export const getData = async () => {
  try {
    const response = await axios.get<pagedUsers>(`${URL}?pages=1&per_page=50`);
    const users = response.data;
    return users;
  } catch (error) {
    console.log(error);
  }
};

// // exports.getData = getData;
// module.exports = {
//     getData
// }
