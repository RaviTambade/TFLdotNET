// database-init.js
const mongoose = require('mongoose');
const config = require('../config/config'); 


exports.initializeDatabase = async function() {
    try {
      await mongoose.connect(config.mongoURI);
      console.log('MongoDB connected');
  
      // Initialize Product collection if empty
      /*const productCount = await Product.countDocuments();
      if (productCount === 0) {
        const defaultProduct = new Product({
          name: 'Televison',
          category: 'Appliances',
          price: '500.99',
          quantityInStock: '100',
        });
        await defaultProduct.save();
        console.log('Product collection initialized');
      } else {
        console.log('Product collection already initialized');
      }*/
  
      // Initialize Customer collection if empty
      /*const customerCount = await Customer.countDocuments();
      if (customerCount === 0) {
        const defaultCustomer = new Customer({
          name: 'Vivan Pradhan',
          email: 'vivan@gmail.com',
          phone: '9898989890',
        });
        await defaultCustomer.save();
        console.log('Customer collection initialized');
      } else {
        console.log('Customer collection already initialized');
      }*/
  
      /*await mongoose.connection.close();
      console.log('Database connection closed');*/
    } catch (err) {
      console.error('Error initializing database:', err.message);
    }
  }
  
  // Run the initialization
  //initializeDatabase();
  

