
//Controller Customer CRUD Request/response

const Customer = require('../models/customerModel');
const CustomerService = require('../services/customerservice');

exports.getAllCustomers = async (req, res) => {
    try {
        const customers = await CustomerService.getAllCustomers(req, res);
        res.status(200).json(customers);
    } catch (error) {
        res.status(500).json({ message: error.message });
    }
    
};

exports.getCustomerById = async (req, res) => {
    try {
        const customer = await CustomerService.getCustomerById(req, res);
        if (!customer) return res.status(404).json({ message: 'Customer not found' });
        res.status(200).json(customer);
    } catch (error) {
        res.status(500).json({ message: error.message });
    }
};

exports.createCustomer = async (req, res) => {
    const { name, email, phone } = req.body;

    if (!name || !email || !phone) {
        return res.status(400).json({ message: 'Name, email, and phone are required' });
    }
    await CustomerService.createCustomer(req, res);
    try {
        const newCustomer = await CustomerService.createCustomer(req, res);
        res.status(201).json(newCustomer);
    } catch (error) {
        res.status(500).json({ message: error.message });
    }
};

exports.updateCustomer = async (req, res) => {
    const { name, email, phone } = req.body;
    if (!name || !email || !phone) {
        return res.status(400).json({ message: 'Name, email, and phone are required' });
    }

    try {
        const updatedCustomer = await CustomerService.updateCustomer(req, res);
        if (!updatedCustomer) return res.status(404).json({ message: 'Customer not found' });
        res.status(200).json(updatedCustomer);
    } catch (error) {
        res.status(500).json({ message: error.message });
    }
}
    
exports.deleteCustomer = async (req, res) => {
    try {
        const deletedCustomer = await CustomerService.deleteCustomer(req, res);
        if (!deletedCustomer) return res.status(404).json({ message: 'Customer not found' });
        res.status(200).json({ message: 'Customer deleted successfully' });
    } catch (error) {
        res.status(500).json({ message: error.message });
    }
};