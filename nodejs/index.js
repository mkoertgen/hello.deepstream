// load DS module
const deepstream = require('deepstream.io-client-js');
const ds = deepstream(process.env.DEEPSTREAM_URL);

//Load express module with `require` directive

//DS part
ds.login({}, (success, data) => {
    if (success) {
        console.log('Login Success', success)
        console.log('Login Data', data)
        doTheSetup();
        // data will be an object with {id: 'user-id'} plus
        // additional data specified in clientData
        // start application
        // client.getConnectionState() will now return 'OPEN'
    } else {
        // extra data can be returned from the permissionHandler as client data
        // both successful and unsuccesful logins

        // client.getConnectionState() will now return
        // 'AWAITING_AUTHENTICATION' or 'CLOSED'
        // if the maximum number of authentication
        // attempts has been exceeded.
    }
})

ds.on('error', function (err) {
    console.log(err)
});

function doTheSetup() {

    ds.event.subscribe(process.env.DEEPSTREAM_EVENT, (data) => {
        console.log(data);
    })

    ds.event.emit(process.env.DEEPSTREAM_EVENT,
        'Hello from NodeJs'
    );

    var myRecord = ds.record.getRecord('test/johndoe');
    myRecord.set({
        firstname: 'John',
        lastname: 'Doe'
    });

    // or just a path
    myRecord.set('hobbies', ['sailing', 'reading']);
    myRecord.get(); // returns the entire data
    myRecord.get('hobbies[1]'); // returns 'reading'

    myRecord.subscribe(data => {}); // get notified when any data changes
    myRecord.subscribe('firstname', firstname => {}); // get notified when first name changes



    ds.event.emit('test-event', {
        some: 'data'
    });

    ds.event.subscribe('test-event', function (eventData) { /*do stuff*/ });



    ds.rpc.make('multiply-numbers', {
        a: 6,
        b: 7
    }, function (err, result) {
        //result === 42
        console.log('#Error:', err);
        console.log('#Result:', result)
    });

    ds.rpc.provide('multiply-numbers', function (data, response) {
        response.send(data.a * data.b);
    });

}

ds.on('error', function (err) {
    console.log(err)
});