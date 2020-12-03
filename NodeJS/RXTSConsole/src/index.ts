
import {user, pagedUsers} from './users';
import { getUsers, getObservableSample, getObservableUsers } from './service'
import { filter, map, } from 'rxjs/operators';
import { async } from 'rxjs';

console.log('--- SAMPLE ---');
getObservableSample().then( data => {
    data.subscribe(s => {
        console.log(`- ${s}`);
    });
});

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


// TODO: How await to finish previous async actions ??? 
console.log('--- Observable Users ---');
 

//
// 'pipe', is used to add operatros: filter, map, scan, reduce, ...
// Operators samples: https://www.learnrxjs.io/learn-rxjs/operators
//
getObservableUsers().then ( data  => {

    data
        .pipe(
            filter( u => u.last_name.startsWith('F')),
            // map using deconstructor
            map( u => ({ ...u, email: u.email.replace('@reqres.in', '@rxjs.dev') }) ),                
        )
        .subscribe(
            (u) => console.log(`   - ${u.first_name} ${u.last_name} - ${u.email}`),
            (error) => console.log(`error: ${error}`),
            () => { /* console.log('No more user found !') */ }
        );
});
