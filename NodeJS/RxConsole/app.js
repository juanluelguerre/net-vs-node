const service = require('./service');

service.getUsersAsync().then( users => {
    console.log(`${users.total} users found !`);
    users.data.forEach(u => {
        console.log(`   - ${u.first_name} ${u.last_name} \t ${u.email}`);
    });
});

