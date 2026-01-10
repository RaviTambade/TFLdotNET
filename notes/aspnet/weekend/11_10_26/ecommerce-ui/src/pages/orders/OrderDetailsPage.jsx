import React from "react";
import { useParams } from "react-router-dom";

const OrderDetailsPage = () => {
  const { id } = useParams(); // Get order ID from route

  // Placeholder order data
  const order = {
    id: id || "12345",
    date: "2026-01-11",
    status: "Shipped",
    items: [
      { name: "Product A", quantity: 1, price: 50 },
      { name: "Product B", quantity: 2, price: 30 },
    ],
    shipping: {
      address: "123 Main St, City, Country",
      trackingNumber: "TRACK123456",
    },
    total: 110,
  };

  return (
    <div className="order-details container" style={{ maxWidth: "700px", marginTop: "2rem" }}>
      <h1>Order Details</h1>
      <p><strong>Order ID:</strong> {order.id}</p>
      <p><strong>Date:</strong> {order.date}</p>
      <p><strong>Status:</strong> {order.status}</p>

      <h2>Items</h2>
      <ul>
        {order.items.map((item, index) => (
          <li key={index}>
            {item.name} - Quantity: {item.quantity} - Price: ${item.price}
          </li>
        ))}
      </ul>

      <h2>Shipping</h2>
      <p>{order.shipping.address}</p>
      <p><strong>Tracking Number:</strong> {order.shipping.trackingNumber}</p>

      <h2>Total</h2>
      <p><strong>${order.total}</strong></p>
    </div>
  );
};

export default OrderDetailsPage;
