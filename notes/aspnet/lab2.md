 
- ✅ Cart Total (Subtotal, Tax, Grand Total)
- ✅ Checkout Page (User details form)
- ✅ Success Message using `TempData`
- ✅ Optional: Clear Cart after checkout

---

# ✅ Step 7: Add **Cart Total Calculation**

### ✅ Modify Cart View (`Views/ShoppingCart/Index.cshtml`)

Add total section below the table:

```html
@model List<CartItem>

<h2>Your Shopping Cart</h2>

@if (!Model.Any())
{
    <p>Your cart is empty.</p>
}
else
{
    <table class="table table-bordered">
        <thead>
            <tr>
                <th>Product</th>
                <th>Price (₹)</th>
                <th>Quantity</th>
                <th>Total</th>
                <th>Remove</th>
            </tr>
        </thead>
        <tbody>
        @foreach (var item in Model)
        {
            <tr>
                <td>@item.Name</td>
                <td>@item.Price</td>
                <td>@item.Quantity</td>
                <td>@(item.Price * item.Quantity)</td>
                <td>
                    <a asp-action="RemoveFromCart" asp-route-id="@item.ProductId" class="btn btn-danger btn-sm">X</a>
                </td>
            </tr>
        }
        </tbody>
    </table>

    <!-- Cart Summary Section -->
    @{
        var subtotal = Model.Sum(x => x.Price * x.Quantity);
        var tax = subtotal * 0.18M; // 18% GST
        var total = subtotal + tax;
    }
    <div class="card p-3">
        <h4>Order Summary</h4>
        <p>Subtotal: ₹@subtotal</p>
        <p>GST (18%): ₹@tax</p>
        <h5><strong>Grand Total: ₹@total</strong></h5>
        <a asp-action="Checkout" class="btn btn-primary">Proceed to Checkout</a>
    </div>
}
```

---

# ✅ Step 8: Add **Checkout Page**

### ✅ Add Action Methods in `ShoppingCartController`

```csharp
public IActionResult Checkout()
{
    var cart = GetCart();
    if (!cart.Any())
    {
        TempData["Error"] = "Your cart is empty!";
        return RedirectToAction("Index");
    }
    return View();
}

[HttpPost]
public IActionResult Checkout(string fullName, string email, string address)
{
    TempData["Message"] = $"Thank you {fullName}! Your order has been placed.";
    HttpContext.Session.Remove(SessionKey); // ✅ Clear Cart
    return RedirectToAction("Index");
}
```

---

### ✅ Create `Views/ShoppingCart/Checkout.cshtml`

```html
@{
    ViewData["Title"] = "Checkout";
}

<h2>Checkout</h2>

<form method="post" asp-action="Checkout" class="mt-3">
    <div class="form-group mb-2">
        <label>Full Name</label>
        <input type="text" name="fullName" class="form-control" required />
    </div>
    <div class="form-group mb-2">
        <label>Email</label>
        <input type="email" name="email" class="form-control" required />
    </div>
    <div class="form-group mb-2">
        <label>Shipping Address</label>
        <textarea name="address" class="form-control" required></textarea>
    </div>
    <button type="submit" class="btn btn-success mt-3">Place Order</button>
</form>
```

---

# ✅ Step 9: Display Messages in `_Layout.cshtml`

Add this just below the `<body>` opening tag:

```html
@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}
@if (TempData["Error"] != null)
{
    <div class="alert alert-danger">@TempData["Error"]</div>
}
```

---

# ✅ Step 10: Test Complete Flow

✔ Add products to cart
✔ View totals
✔ Click Checkout → Fill form → Place order
✔ Cart clears and success message displays ✅

 