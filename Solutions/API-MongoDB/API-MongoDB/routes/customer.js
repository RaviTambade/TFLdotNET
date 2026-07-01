// routes/customer.js
//const Customer = require('../models/customerModel');
const CustomerController = require('../controllers/customercontroller');
const router = require('express').Router();

//e a new customer
router.post('/', async (req, res) => {
  const { name, email, phone } = req.body;
  CustomerController.createCustomer(req, res);
  if (!name || !email || !phone) {
    return res.status(400).json({ error: 'Name, email, and phone are required' });
  }
   
});

// Get all customers
router.get('/', async (req, res) => {
  try {
    const customers = await CustomerController.getAllCustomers(req, res);
    if (!customers) return res.status(404).json({ error: 'No customers found' });
    res.json(customers);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});


// Get a single customer by ID
router.get('/:id', async (req, res) => {
  try {
    const customer = await CustomerController.getCustomerById(req, res);
    if (!customer) return res.status(404).json({ error: 'Customer not found' });
    res.json(customer);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});

// Update a customer by ID
router.put('/:id', async (req, res) => { 
  const { name, email, phone } = req.body;
  if (!name || !email || !phone) {
    return res.status(400).json({ error: 'Name, email, and phone are required' });
  }
  try {
    const updatedCustomer = await CustomerController.updateCustomer(req, res);
    if (!updatedCustomer) return res.status(404).json({ error: 'Customer not found' });
    res.json(updatedCustomer);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});


// Delete a customer by ID
router.delete('/:id', async (req, res) => {
  try {
    const deletedCustomer = await Customer.findByIdAndDelete(req.params.id);
    if (!deletedCustomer) return res.status(404).json({ error: 'Customer not found' });
    res.json({ message: 'Customer deleted successfully' });
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});

module.exports = router;