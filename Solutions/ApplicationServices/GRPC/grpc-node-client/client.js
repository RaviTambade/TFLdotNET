const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');
const PROTO_PATH = __dirname + '/greet.proto';

// Load protobuf
const packageDefinition = protoLoader.loadSync(PROTO_PATH, {
  keepCase: true,
  longs: String,
  enums: String,
  defaults: true,
  oneofs: true
});
const greetProto = grpc.loadPackageDefinition(packageDefinition).greet;

// Create gRPC client
//Proxy:
//
const client = new greetProto.Greeter('localhost:5155', grpc.credentials.createInsecure());

// Call the SayHello RPC
client.SayHello({ name: 'Ravi' }, (err, response) => {
  if (err) {
    console.error('Error:', err);
    return;
  }
  console.log('Greeting from server:', response.message);
});
