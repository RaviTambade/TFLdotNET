## 🌐 Client-Side (Browser) UI Architecture – ASCII View

```
+-----------------------------------------------------------+
|                         BROWSER                           |
|                                                           |
|  +--------------------+       +------------------------+  |
|  |   UI COMPONENTS    |       |   APPLICATION STATE    |  |
|  |--------------------|       |------------------------|  |
|  |  HTML Elements     |<----> |  JS Variables / Objects|  |
|  |  (Input, Button,   | Data  |  ViewModel / State     |  |
|  |   Table, Div)      |Binding|  (React State, etc.)   |  |
|  +---------^----------+       +-----------^------------+  |
|             |                             |               |
|             | Event Binding               | State Update  |
|             | (click, change, submit)     |               |
|  +---------+----------+       +-----------+------------+  |
|  |   EVENT HANDLERS   |      |   BUSINESS LOGIC        |  |
|  |--------------------|      |-------------------------|  |
|  | onClick(),         |----->| Validation              |  |
|  | onChange(),        |      | Calculations            |  |
|  | onSubmit()         |      | Decision Making         |  |
|  +---------^----------+      +-----------^-------------+  |
|            |                             |                |
|            |                             |                |
|            |                             |                |
|  +---------+-----------------------------+-------------+  |
|  |           CLIENT-SIDE STORAGE                        | |
|  |------------------------------------------------------| |
|  |  localStorage     sessionStorage     cookies         | |
|  |  (persistent)     (tab/session)     (small data)     | |
|  +-------------------^-----------------^---------------+  |
|                      |                 |                  |
|                      | Read / Write    |                  |
|                      |                 |                  |
|  +-------------------+-----------------+---------------+  |
|  |           DATA ACCESS LAYER (CLIENT)                |  |
|  |------------------------------------------------------| |
|  |  AJAX / fetch() / axios                              | |
|  |  HTTP GET | POST | PUT | DELETE                      | |
|  +-------------------^----------------------------------+ |
|                      |                                    |
|                      | JSON / XML                         |
+----------------------|------------------------------------+
                       |
                       |
           +-----------+----------+
           |   BACKEND SERVER     |
           |----------------------|
           | REST API / GraphQL   |
           | Authentication       |
           | Database             |
           +----------------------+
```


### 1️⃣ UI Components (HTML / JSX / Templates)

```
<input>  <button>  <table>  <div>
```

* Only **display data**
* Never talk directly to the database
* Reflect data from **application state**

👉 *“UI is a mirror of state”*



### 2️⃣ Data Binding (UI ↔ State)

```
State --------> UI   (One-way binding)
UI -----------> State (Two-way binding)
```

Examples:

* React → one-way
* Angular → two-way
* Vanilla JS → manual


### 3️⃣ Event Binding (User → Code)

```
User Clicks Button
        |
        v
  onClick() / addEventListener()
        |
        v
 Business Logic
```

Examples:

* `onclick`
* `onchange`
* `onsubmit`

👉 *Events never directly update UI — they update state*


### 4️⃣ Business Logic (Client Side Brain)

Handles:

* Validation
* Calculations
* Conditional flows
* Deciding **when to call API**
* Deciding **what to store locally**

### 5️⃣ Client-Side Storage

```
localStorage    → survives browser restart
sessionStorage  → lives until tab closed
cookies         → small, server-aware
```

Use cases:

* JWT token
* Theme preference
* User language
* Cached data

### 6️⃣ External Data Access (AJAX / Fetch)

```
fetch('/api/orders')
      |
      v
   Backend API
      |
      v
    JSON Response
      |
      v
 Update State → UI auto-refresh
```

Technologies:

* `fetch()`
* `XMLHttpRequest`
* `axios`


## 🎯 One-Liner Mental Model for Students

> **UI reacts to State
> Events change State
> State triggers UI
> APIs feed State
> Storage preserves State**



# 🛒 E-Commerce UI – React Component Identification

## Core Functional Areas → UI Responsibility

| Feature            | UI Responsibility               |
| ------------------ | ------------------------------- |
| Product Catalog    | Browse, search, filter products |
| Shopping Cart      | Add/remove/update quantities    |
| Authentication     | Login, register, logout         |
| Order Processing   | Review order, place order       |
| Payment Processing | Collect payment details         |
| Shipment           | Address, tracking, status       |

---

## 🧩 High-Level React UI Component Map

```
<App />
 ├── <Header />
 │     ├── <Logo />
 │     ├── <SearchBar />
 │     ├── <CategoryMenu />
 │     ├── <CartIcon />
 │     └── <UserMenu />
 │
 ├── <Router>
 │     ├── <HomePage />
 │     ├── <ProductCatalogPage />
 │     ├── <ProductDetailsPage />
 │     ├── <CartPage />
 │     ├── <CheckoutPage />
 │     ├── <LoginPage />
 │     ├── <RegisterPage />
 │     ├── <OrdersPage />
 │     └── <OrderDetailsPage />
 │
 └── <Footer />
```


# 🧠 Feature-Wise UI Component Breakdown (React)

## 1️⃣ Product Catalog (Browse Products)

```
<ProductCatalogPage>
 ├── <FilterPanel />
 │     ├── <CategoryFilter />
 │     ├── <PriceFilter />
 │     └── <RatingFilter />
 │
 ├── <SortDropdown />
 │
 └── <ProductList>
       ├── <ProductCard />
       ├── <ProductCard />
       └── <ProductCard />
```

### Key Interactions

* Filters → update catalog state
* ProductCard click → navigate to details

## 2️⃣ Product Details

```
<ProductDetailsPage>
 ├── <ImageGallery />
 ├── <ProductInfo />
 │     ├── <Title />
 │     ├── <Price />
 │     ├── <Description />
 │
 ├── <QuantitySelector />
 └── <AddToCartButton />
```


## 3️⃣ Shopping Cart

```
<CartPage>
 ├── <CartItemList>
 │     ├── <CartItem />
 │     ├── <CartItem />
 │     └── <CartItem />
 │
 ├── <CartSummary />
 │     ├── <Subtotal />
 │     ├── <Tax />
 │     └── <Total />
 │
 └── <ProceedToCheckoutButton />
```

## 4️⃣ Authentication (Login / Register)

```
<LoginPage>
 ├── <LoginForm>
 │     ├── <EmailInput />
 │     ├── <PasswordInput />
 │     └── <LoginButton />
```

```
<RegisterPage>
 ├── <RegisterForm>
 │     ├── <NameInput />
 │     ├── <EmailInput />
 │     ├── <PasswordInput />
 │     └── <RegisterButton />
```

## 5️⃣ Checkout & Order Processing

```
<CheckoutPage>
 ├── <ShippingAddressForm />
 ├── <OrderSummary />
 ├── <PaymentMethodSelector />
 └── <PlaceOrderButton />
```

## 6️⃣ Payment Processing (UI Only)

```
<PaymentSection>
 ├── <PaymentMethodTabs />
 │     ├── <CardPaymentForm />
 │     ├── <UPIPaymentForm />
 │     └── <NetBankingForm />
```

⚠️ *Payment logic handled by gateway SDK, UI only collects details*

## 7️⃣ Shipment & Order Tracking

```
<OrderDetailsPage>
 ├── <OrderStatus />
 ├── <ShipmentTimeline />
 │     ├── Ordered
 │     ├── Packed
 │     ├── Shipped
 │     └── Delivered
 └── <TrackingInfo />
```


# 🌐 Complete E-Commerce UI Architecture – ASCII Diagram

```
+--------------------------------------------------+
|                    <App />                       |
|--------------------------------------------------|
|  <Header />                                      |
|   - Logo                                         |
|   - SearchBar                                    |
|   - CartIcon                                     |
|   - UserMenu                                     |
|                                                  |
|  <Router>                                        |
|   +--------------------------------------------+ |
|   | <ProductCatalogPage>                       | |
|   |  - Filters                                 | |
|   |  - ProductList                             | |
|   +--------------------------------------------+ |
|                                                  |
|   +--------------------------------------------+ |
|   | <CartPage>                                 | |
|   |  - CartItems                               | |
|   |  - Summary                                 | |
|   +--------------------------------------------+ |
|                                                  |
|   +--------------------------------------------+ |
|   | <CheckoutPage>                             | |
|   |  - Address                                 | |
|   |  - Payment                                 | |
|   |  - PlaceOrder                              | |
|   +--------------------------------------------+ |
|                                                  |
|   +--------------------------------------------+ |
|   | <OrderDetailsPage>                         | |
|   |  - Status                                  | |
|   |  - Shipment                                | |
|   +--------------------------------------------+ |
|                                                  |
|  </Router>                                       |
|                                                  |
|  <Footer />                                      |
+--------------------------------------------------+
```

## 🧠 Mentor Takeaway for Students

> **Every business feature becomes a Page
> Every Page is composed of Components
> Components communicate via Props & State
> Pages talk to APIs, not Components**



# 🧱 E-Commerce React Folder Structure (Feature-Oriented)

```
src/
│
├── app/
│   ├── App.jsx
│   ├── routes.jsx
│   └── store.js                # Redux / Context setup
│
├── components/                 # Reusable UI components
│   ├── layout/
│   │   ├── Header.jsx
│   │   ├── Footer.jsx
│   │   └── MainLayout.jsx
│   │
│   ├── common/
│   │   ├── Button.jsx
│   │   ├── Input.jsx
│   │   ├── Modal.jsx
│   │   ├── Loader.jsx
│   │   └── ErrorMessage.jsx
│   │
│   ├── navigation/
│   │   ├── SearchBar.jsx
│   │   ├── CategoryMenu.jsx
│   │   └── UserMenu.jsx
│   │
│   └── product/
│       ├── ProductCard.jsx
│       ├── ProductList.jsx
│       └── ImageGallery.jsx
│
├── pages/                      # Route-level components
│   ├── HomePage.jsx
│   │
│   ├── product/
│   │   ├── ProductCatalogPage.jsx
│   │   └── ProductDetailsPage.jsx
│   │
│   ├── cart/
│   │   └── CartPage.jsx
│   │
│   ├── checkout/
│   │   └── CheckoutPage.jsx
│   │
│   ├── auth/
│   │   ├── LoginPage.jsx
│   │   └── RegisterPage.jsx
│   │
│   └── orders/
│       ├── OrdersPage.jsx
│       └── OrderDetailsPage.jsx
│
├── features/                   # Business feature modules
│   ├── product/
│   │   ├── productSlice.js
│   │   ├── productService.js
│   │   └── productApi.js
│   │
│   ├── cart/
│   │   ├── cartSlice.js
│   │   └── cartService.js
│   │
│   ├── auth/
│   │   ├── authSlice.js
│   │   ├── authService.js
│   │   └── authApi.js
│   │
│   ├── order/
│   │   ├── orderSlice.js
│   │   └── orderService.js
│   │
│   └── payment/
│       └── paymentService.js
│
├── hooks/                      # Custom React hooks
│   ├── useAuth.js
│   ├── useCart.js
│   └── useFetch.js
│
├── services/                   # Cross-feature services
│   ├── apiClient.js            # axios / fetch wrapper
│   ├── storageService.js       # localStorage/sessionStorage
│   └── authTokenService.js
│
├── utils/
│   ├── constants.js
│   ├── validators.js
│   └── formatters.js
│
├── styles/
│   ├── global.css
│   └── theme.css
│
├── assets/
│   ├── images/
│   └── icons/
│
└── index.js
```

# 🧠 How This Maps to Your E-Commerce Features

## 🛍 Product Catalog

```
pages/product/ProductCatalogPage.jsx
components/product/ProductList.jsx
features/product/productService.js
```
## 🛒 Shopping Cart

```
pages/cart/CartPage.jsx
features/cart/cartSlice.js
hooks/useCart.js
```

## 🔐 Authentication

```
pages/auth/LoginPage.jsx
features/auth/authService.js
services/authTokenService.js
```


## 💳 Payment Processing

```
pages/checkout/CheckoutPage.jsx
features/payment/paymentService.js
```


## 🚚 Shipment & Order Tracking

```
pages/orders/OrderDetailsPage.jsx
features/order/orderService.js
```

# 🎯 Folder Responsibility Rule (Golden Rule for Students)

| Folder        | Responsibility                |
| ------------- | ----------------------------- |
| `pages/`      | Routing + page composition    |
| `components/` | Pure UI (no API calls)        |
| `features/`   | Business logic + state        |
| `services/`   | Infrastructure (API, storage) |
| `hooks/`      | Reusable logic                |
| `utils/`      | Helpers only                  |


# ⚠️ Common Mistakes to Warn Students About

❌ API calls inside UI components
❌ Business logic in `pages/`
❌ Using `localStorage` directly everywhere
❌ One giant `components` folder
❌ No separation between UI and state


## ✅ Option 1 (Recommended): Create React App using **Vite + JSX**

### 1️⃣ Create the project

```bash
npm create vite@latest ecommerce-ui -- --template react
```

> This creates a React project using **JSX (not TypeScript)**

### 2️⃣ Move into project folder

```bash
cd ecommerce-ui
```

### 3️⃣ Install dependencies

```bash
npm install
```

### 4️⃣ Start development server

```bash
npm run dev
```

📌 App runs at:

```
http://localhost:5173
```

### 📁 Default JSX File Structure (Vite)

```
src/
├── App.jsx
├── main.jsx
├── assets/
└── index.css
```

✔ `.jsx` confirms JSX-based components


## ✅ Option 2 (Legacy but Known): Create React App (CRA)

⚠️ **Not recommended for new projects**, but useful for interviews.

### 1️⃣ Create app

```bash
npx create-react-app ecommerce-ui
```

### 2️⃣ Start app

```bash
cd ecommerce-ui
npm start
```

📌 Runs at:

```
http://localhost:3000
```

## 🧠 Mentor Tip (What to Tell Students)

> **JSX is not optional.**
> JSX is the *default and standard way* React components are written.


## 🔍 How to Verify JSX is Working

Open:

```
src/App.jsx   (Vite)
or
src/App.js    (CRA)
```

Example JSX component:

```jsx
function App() {
  return <h1>E-Commerce UI</h1>;
}

export default App;
```

