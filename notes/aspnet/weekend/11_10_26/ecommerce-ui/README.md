cd src

:: Core app folder
mkdir app
type nul > app\App.jsx
type nul > app\routes.jsx
type nul > app\store.js

:: Pages
mkdir pages
mkdir pages\product pages\cart pages\checkout pages\auth pages\orders

type nul > pages\HomePage.jsx
type nul > pages\product\ProductCatalogPage.jsx
type nul > pages\product\ProductDetailsPage.jsx
type nul > pages\cart\CartPage.jsx
type nul > pages\checkout\CheckoutPage.jsx
type nul > pages\auth\LoginPage.jsx
type nul > pages\auth\RegisterPage.jsx
type nul > pages\orders\OrdersPage.jsx
type nul > pages\orders\OrderDetailsPage.jsx

:: Components
mkdir components
mkdir components\layout components\common components\navigation components\product

type nul > components\layout\Header.jsx
type nul > components\layout\Footer.jsx
type nul > components\layout\MainLayout.jsx

type nul > components\common\Button.jsx
type nul > components\common\Input.jsx
type nul > components\common\Modal.jsx
type nul > components\common\Loader.jsx
type nul > components\common\ErrorMessage.jsx

type nul > components\navigation\SearchBar.jsx
type nul > components\navigation\CategoryMenu.jsx
type nul > components\navigation\UserMenu.jsx

type nul > components\product\ProductCard.jsx
type nul > components\product\ProductList.jsx
type nul > components\product\ImageGallery.jsx

:: Features (business logic + state)
mkdir features
mkdir features\product features\cart features\auth features\order features\payment

type nul > features\product\productSlice.js
type nul > features\product\productService.js
type nul > features\product\productApi.js

type nul > features\cart\cartSlice.js
type nul > features\cart\cartService.js

type nul > features\auth\authSlice.js
type nul > features\auth\authService.js
type nul > features\auth\authApi.js

type nul > features\order\orderSlice.js
type nul > features\order\orderService.js

type nul > features\payment\paymentService.js

:: Hooks
mkdir hooks
type nul > hooks\useAuth.js
type nul > hooks\useCart.js
type nul > hooks\useFetch.js

:: Services (API / storage)
mkdir services
type nul > services\apiClient.js
type nul > services\storageService.js
type nul > services\authTokenService.js

:: Utils
mkdir utils
type nul > utils\constants.js
type nul > utils\validators.js
type nul > utils\formatters.js

:: Styles
mkdir styles
type nul > styles\global.css
type nul > styles\theme.css

:: Assets
mkdir assets
mkdir assets\images assets\icons
