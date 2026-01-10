import React from "react";
import { Link } from "react-router-dom";

const OrdersPage = () => {
  // Placeholder orders
  const orders = [
    { id: "101", date: "2026-01-10", status: "Delivered", total: 120 },
    { id: "102", date: "2026-01-11", status: "Shipped", total: 90 },
  ];

  return (
    <div className="orders-page container" style={{ marginTop: "2rem" }}>
      <h1>Your Orders</h1>

      {orders.length === 0 ? (
        <p>You have no orders.</p>
      ) : (
        <ul>
          {orders.map((order) => (
            <li key={order.id} style={{ marginBottom: "1rem" }}>
              <p><strong>Order ID:</strong> {order.id}</p>
              <p><strong>Date:</strong> {order.date}</p>
              <p><strong>Status:</strong> {order.status}</p>
              <p><strong>Total:</strong> ${order.total}</p>
              <Link to={`/orders/${order.id}`}>
                <button>View Details</button>
              </Link>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default OrdersPage;
