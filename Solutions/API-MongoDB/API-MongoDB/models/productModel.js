const mongoose = require('mongoose');

const ProductSchema = new mongoose.Schema({
  name: {type: String, required: true},
  category: String,
  price: {type: String,required: true  },
  quantityInStock: {type: String}
});

module.exports = mongoose.model('customers', ProductSchema);