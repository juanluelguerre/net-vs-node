import { Foo } from "./Foo";
import { user, pagedUsers } from "./users";
import {
  getSampleAsync,
  getUsersAsync,
  getObservableUsersAsync,
} from "./service";
import { filter, map } from "rxjs/operators";
import { async } from "rxjs";

// var foo = new Foo();
// foo.id = 42;
// foo.is_verified = true;

var foo = {
    id: 42,
    is_verified: true
}

var { id } = foo;
console.log(`${id}`); // 42

// LESSON 1) - PROMISES
// Just return an users collections
getUsersAsync().then((users) => {
  console.log(`${users?.total} users found !`);
  users?.data.forEach((u) => {
    console.log(`   . ${u.first_name} ${u.last_name} - ${u.email}`);
  });
});

// LESSON 2) - ASYNC & AWAIT
// Just return an users collections using async & await
async function runUsersAsync() {
  const users = await getUsersAsync();

  console.log(`${users?.total} users found !`);
  users?.data.forEach((u) => {
    console.log(`   - ${u.first_name} ${u.last_name} - ${u.email}`);
  });
}
runUsersAsync();

// LESSON 3) - OBSERVABLES
getSampleAsync().then((data) => {
  data.subscribe((s) => {
    console.log(`- ${s}`);
  });
});

// Return an observable users collections
getObservableUsersAsync().then((data) => {
  data.subscribe(
    (u) => console.log(`   * ${u.first_name} ${u.last_name} - ${u.email}`),
    (error) => console.log(`error: ${error}`),
    () => {
      /* console.log('No more user found !') */
    }
  );
});

async function runObservableUsersAsync() {
  const data = await getObservableUsersAsync();

  data.subscribe(
    (u) => console.log(`   # ${u.first_name} ${u.last_name} - ${u.email}`),
    (error) => console.log(`error: ${error}`),
    () => {
      /* console.log('No more user found !') */
    }
  );
}
runObservableUsersAsync();

// LESSON 5) - Pipes
// 'pipe', is used to add operatros: filter, map, scan, reduce, ...
// Operators samples: https://www.learnrxjs.io/learn-rxjs/operators
getObservableUsersAsync().then((users) => {
  users
    .pipe(
      filter((u) => u.last_name.startsWith("F")),
      map((u) => ({ ...u, email: u.email.replace("@reqres.in", "@rxjs.dev") }))
    )
    .subscribe(
      (u) => console.log(`   - ${u.first_name} ${u.last_name} - ${u.email}`),
      (err) => console.log(`error: ${err}`),
      () => {
        /*  console.log('No more user found !') */
      }
    )
    .unsubscribe();
});

async function runObservableUsers_with_pipe_Async() {
  const users = await getObservableUsersAsync();
  users
    .pipe(
      filter((u) => u.last_name.startsWith("F")),
      map((u) => ({ ...u, email: u.email.replace("@reqres.in", "@rxjs.dev") }))
    )
    .subscribe(
      (u) => console.log(`   - ${u.first_name} ${u.last_name} - ${u.email}`),
      (error) => console.log(`error: ${error}`),
      () => {
        /*  console.log('No more user found !') */
      }
    )
    .unsubscribe();
}

runObservableUsers_with_pipe_Async();
