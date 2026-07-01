const express = require('express');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const cors = require('cors');
const config = require('./config/config');
const mongoDBMgr=require('./bootstrap/database-init');

const app = express();

// Middleware
app.use(express.json());
app.use(cors());
mongoDBMgr.initializeDatabase();
app.use('/api/customers', require('./routes/customer'));

// Start server
const PORT = process.env.PORT || 5000;
app.listen(PORT, () => console.log(`Server running on port ${PORT}`));