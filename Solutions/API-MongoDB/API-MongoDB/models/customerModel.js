const mongoose = require('mongoose');

const CustomerSchema = new mongoose.Schema({
 
  custid: {type: Number, required: true},
  firstname: {type: String, required: true},
  lastname: {type: String, required: true},
  email: {type: String,required: true},
  contactnumber: {type: String, required: true},
  imageurl: {type: String, required: true}
});

module.exports = mongoose.model('Customer', CustomerSchema);
