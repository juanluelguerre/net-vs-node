import axios from 'axios';
import { from, Observable } from 'rxjs';
import { pagedUsers, user } from './users';

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

// Observable sample
export const getObservableSample = async () => {
  
  var observable = new Observable((observer) => {
    observer.next('Hello Juanlu!');
    observer.next('Hello Eva!');
    observer.complete();
    // observer.next('Bye'); // Undefined. After complete(). Nothing to show !
  });

  return observable;
}

// Using 'from' to create an observable from an array of items. Users in this case.
export const getObservableUsers = async () =>{
  const data = await getData() as pagedUsers;
  const obs = from(data.data);
  
  return obs;
}
