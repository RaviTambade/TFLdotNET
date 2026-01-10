import React from "react";
import { useParams } from "react-router-dom";

const ProductDetailsPage = () => {
  const { id } = useParams();

  // Placeholder product details
  const product = {
    id,
    name: `Product ${id}`,
    price: 50 + parseInt(id) * 10,
    description: `This is the description for Product ${id}.`,
  };

  return (
    <div className="product-details container" style={{ marginTop: "2rem" }}>
      <h1>{product.name}</h1>
      <p><strong>Price:</strong> ${product.price}</p>
      <p>{product.description}</p>
      <button>Add to Cart</button>
    </div>
  );
};

export default ProductDetailsPage;
