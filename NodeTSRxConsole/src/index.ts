// https://khalilstemmler.com/blogs/typescript/node-starter-project/

import { userInfo } from 'os';
import {user, pagedUsers} from './users';
import { getData  } from './service'

getData().then( users => {
    console.log(`${users?.total} users found !`);
    users?.data.forEach(u => {
        console.log(`   - ${u.first_name} ${u.last_name} - ${u.email}`);
    });
});