import React, { useState } from "react";

const CheckoutPage = () => {
  // Example state for form inputs
  const [shippingAddress, setShippingAddress] = useState({
    name: "",
    address: "",
    city: "",
    state: "",
    zip: "",
    country: "",
  });

  const [paymentMethod, setPaymentMethod] = useState("card");

  // Handle form input change
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setShippingAddress({ ...shippingAddress, [name]: value });
  };

  // Handle order submission
  const handlePlaceOrder = (e) => {
    e.preventDefault();
    alert("Order placed successfully!");
    // TODO: Integrate API call to backend
  };

  return (
    <div className="checkout-page container">
      <h1>Checkout</h1>

      <form onSubmit={handlePlaceOrder} style={{ maxWidth: "600px", marginTop: "2rem" }}>
        <h2>Shipping Address</h2>
        <input
          type="text"
          name="name"
          placeholder="Full Name"
          value={shippingAddress.name}
          onChange={handleInputChange}
          required
        />
        <input
          type="text"
          name="address"
          placeholder="Street Address"
          value={shippingAddress.address}
          onChange={handleInputChange}
          required
        />
        <input
          type="text"
          name="city"
          placeholder="City"
          value={shippingAddress.city}
          onChange={handleInputChange}
          required
        />
        <input
          type="text"
          name="state"
          placeholder="State"
          value={shippingAddress.state}
          onChange={handleInputChange}
          required
        />
        <input
          type="text"
          name="zip"
          placeholder="ZIP / Postal Code"
          value={shippingAddress.zip}
          onChange={handleInputChange}
          required
        />
        <input
          type="text"
          name="country"
          placeholder="Country"
          value={shippingAddress.country}
          onChange={handleInputChange}
          required
        />

        <h2>Payment Method</h2>
        <select
          name="paymentMethod"
          value={paymentMethod}
          onChange={(e) => setPaymentMethod(e.target.value)}
          style={{ marginBottom: "1rem" }}
        >
          <option value="card">Credit / Debit Card</option>
          <option value="upi">UPI</option>
          <option value="netbanking">Net Banking</option>
        </select>

        <h2>Order Summary</h2>
        <div style={{ border: "1px solid #ccc", padding: "1rem", borderRadius: "6px", marginBottom: "1rem" }}>
          <p>Subtotal: $100</p>
          <p>Tax: $10</p>
          <p>Total: $110</p>
        </div>

        <button type="submit">Place Order</button>
      </form>
    </div>
  );
};

export default CheckoutPage;
