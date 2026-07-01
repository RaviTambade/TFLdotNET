// server.js
const express = require('express');
const http = require('http');
const path = require('path');
const socketIO = require('socket.io');

const app = express();
const server = http.createServer(app);
const io = socketIO(server);

// Serve static files
app.use(express.static(path.join(__dirname, 'public')));

// Socket connection
io.on('connection', socket => {
  console.log(' New user connected');

  socket.on('chat message', msg => {
    io.emit('chat message', msg); // Broadcast to everyone
  });

  socket.on('disconnect', () => {
    console.log(' User disconnected');
  });
});

// Start server
const PORT = 3000;
server.listen(PORT, () => {
  console.log(` Server running on http://localhost:${PORT}`);
});
