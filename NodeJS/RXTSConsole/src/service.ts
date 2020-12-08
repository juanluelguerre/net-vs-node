import axios from 'axios';
import { from, Observable } from 'rxjs';
import { pagedUsers, user } from './users';


// Observable sample
export const getSampleAsync = async () => {
  
  var observable = new Observable((observer) => {
    observer.next('Hello Juanlu!');
    observer.next('Hello Eva!');
    observer.complete();
    // observer.next('Bye'); // Undefined. After complete(). Nothing to show !
  });

  return observable;
}

const URL = 'https://reqres.in/api/users';

export const getUsersAsync = async () => {
  try {
    const response = await axios.get<pagedUsers>(`${URL}?pages=1&per_page=50`);
    const users = response.data;
    return users;
  } catch (error) {
    console.log(error);
  }
};

// Using 'from' to create an observable from an array of items. Users in this case.
export const getObservableUsersAsync = async () =>{
  const data = await getUsersAsync() as pagedUsers;
  const obs = from(data.data);
  
  return obs;
}
