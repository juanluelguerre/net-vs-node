
import {user, pagedUsers} from './users';
import { getData, getObservableUsers } from './service'
import { filter, map, } from 'rxjs/operators';

// getData().then( users => {
//     console.log(`${users?.total} users found !`);
//     users?.data.forEach(u => {
//         console.log(`   - ${u.first_name} ${u.last_name} - ${u.email}`);
//     });
// });

// getObservableUsers().then ( data  => {

//     data.subscribe(
//         (u) => console.log(`   - ${u.first_name} ${u.last_name} - ${u.email}`),
//         (error) => console.log(`error: ${error}`),
//         () => { /* console.log('No more user found !') */ }
//     );
// });

//
// 'pipe', is used to add operatros: filter, map, scan, reduce, ...
// Operators samples: https://www.learnrxjs.io/learn-rxjs/operators
//
getObservableUsers().then ( data  => {

    data
        .pipe(
            filter( u => u.last_name.startsWith('F')),
            // map using deconstructor
            map( u => ({ ...u, email: u.email.replace('@rqres.in', '@rxjs.dev') }) ),                
        )
        .subscribe(
            (u) => console.log(`   - ${u.first_name} ${u.last_name} - ${u.email}`),
            (error) => console.log(`error: ${error}`),
            () => { /* console.log('No more user found !') */ }
        );
});
