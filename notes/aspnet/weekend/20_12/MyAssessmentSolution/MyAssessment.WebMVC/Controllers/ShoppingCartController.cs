
//Skeleton code for ShoppingCart feature implementation
//Agile Methodology

//User story: As a user, I want to be able to :
//add items to my shopping cart, 
//remove items from it, and 
//view the contents of my cart 
//so that I can manage my purchases easily.

//Acceptance Criteria:
//1. Users can add items to the shopping cart.
//2. Users can remove items from the shopping cart.
//3. Users can view the contents of their shopping cart.

//Dotnet develper have been asked to implement ShoppingCart feature in an e-commerce application.

//TDD: Test Driven Development
//Test cases to be written before implementing the ShoppingCart feature
//1. Test case for adding item to cart
//2. Test case for removing item from cart
//3. Test case for viewing cart contents

//Test case for adding item to cart
//[TestMethod]
//statement:
//Cicking "Add to Cart" button should add the item to the shopping cart
//Expected Result: Item is added to the cart and cart count is updated

//Test case for removing item from cart
//[TestMethod]
//statement:
//Cicking "Remove from Cart" button should remove the item from the shopping cart
//Expected Result: Item is removed from the cart and cart count is updated

//Test case for viewing cart contents
//[TestMethod]
//statement:
//Cicking "View Cart" button should display the contents of the shopping cart
//Expected Result: Cart contents are displayed correctly



//OOM: Object Oriented Modeling

//Add functionality to implement shopping cart features
//functionality such as 
//adding items to the cart, 
//removing items from the cart, and 
//viewing the cart contents.

//Receipy for implementing ShoppingCart
//1. Create a ShoppingCart model
//2. Create Services for ShoppingCart operations
//3. Add INterface for ShoppingCart Service
//4. Implement ShoppingCart Service
//5. Configure Dependency Injection in Proram.cs
//6. Use buider.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
//7. Create ShoppingCartController to handle HTTP requests
//8. Implement methods for adding, removing, and viewing cart items as action methods 
//   in the ShoppingCartController.