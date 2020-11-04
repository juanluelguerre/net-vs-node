const service = require('./service');

service.getData().then( users => {
    console.log(`${users.data.total} users found !`);
    users.data.forEach(u => {
        console.log(`   - ${u.first_name} ${u.last_name} - ${u.email}`);
    });
});

