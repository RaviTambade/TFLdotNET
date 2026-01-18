# React Front End App to consume  Secure Web API

 Let’s wire up your **JWT Web API** with a **React frontend** so the client can log in, store tokens, and refresh them automatically.  

## 🛠️ Step 1: React Project Setup
Create a React app (if you don’t already have one):

```bash
npx create-react-app jwt-auth-demo
cd jwt-auth-demo
npm install axios
```

We’ll use **Axios** for API calls.


## 🛠️ Step 2: Auth Service (React)

Create a file `src/services/authService.js`:

```javascript
import axios from "axios";

const API_URL = "http://localhost:8080/auth"; // Spring Boot backend

// Save tokens in localStorage
export const login = async (username, password) => {
  const response = await axios.post(`${API_URL}/login`, null, {
    params: { username, password }
  });

  const { accessToken, refreshToken } = response.data;
  localStorage.setItem("accessToken", accessToken);
  localStorage.setItem("refreshToken", refreshToken);

  return response.data;
};

export const refreshToken = async () => {
  const refreshToken = localStorage.getItem("refreshToken");
  const response = await axios.post(`${API_URL}/refresh`, null, {
    params: { refreshToken }
  });

  const { accessToken } = response.data;
  localStorage.setItem("accessToken", accessToken);

  return accessToken;
};

export const logout = () => {
  localStorage.removeItem("accessToken");
  localStorage.removeItem("refreshToken");
};
```

## 🛠️ Step 3: Axios Interceptor for Auto Refresh

Create `src/services/axiosInstance.js`:

```javascript
import axios from "axios";
import { refreshToken } from "./authService";

const instance = axios.create({
  baseURL: "http://localhost:8080",
});

// Attach token to every request
instance.interceptors.request.use(
  async (config) => {
    let token = localStorage.getItem("accessToken");
    config.headers["Authorization"] = `Bearer ${token}`;
    return config;
  },
  (error) => Promise.reject(error)
);

// Handle expired tokens
instance.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      const newToken = await refreshToken();
      axios.defaults.headers.common["Authorization"] = `Bearer ${newToken}`;
      return instance(originalRequest);
    }

    return Promise.reject(error);
  }
);

export default instance;
```

## 🛠️ Step 4: React Components

### Login Component (`src/components/Login.js`)

```javascript
import React, { useState } from "react";
import { login } from "../services/authService";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = async () => {
    try {
      await login(username, password);
      alert("Login successful!");
    } catch (err) {
      alert("Login failed!");
    }
  };

  return (
    <div>
      <h2>Login</h2>
      <input placeholder="Username" onChange={(e) => setUsername(e.target.value)} />
      <input placeholder="Password" type="password" onChange={(e) => setPassword(e.target.value)} />
      <button onClick={handleLogin}>Login</button>
    </div>
  );
}

export default Login;
```

### Protected Component (`src/components/Dashboard.js`)

```javascript
import React, { useEffect, useState } from "react";
import axiosInstance from "../services/axiosInstance";

function Dashboard() {
  const [message, setMessage] = useState("");

  useEffect(() => {
    axiosInstance.get("/admin/dashboard")
      .then((res) => setMessage(res.data))
      .catch((err) => setMessage("Access denied"));
  }, []);

  return (
    <div>
      <h2>Dashboard</h2>
      <p>{message}</p>
    </div>
  );
}

export default Dashboard;
```


## 🧩 Workflow
1. User logs in → React calls `/auth/login` → stores `accessToken` + `refreshToken`.  
2. Axios attaches `accessToken` to every request.  
3. If `accessToken` expires → interceptor calls `/auth/refresh` → gets a new token automatically.  
4. Protected routes (`/admin/**`, `/user/**`) are accessible based on roles.  
 




Let’s build a **simple React app** with **protected routes for Admin and User**, consuming your **Spring Boot JWT API**.  

## 🛠️ Step 1: Install Dependencies
```bash
npx create-react-app jwt-client
cd jwt-client
npm install axios react-router-dom
```

---

## 🛠️ Step 2: Auth Service (`src/services/authService.js`)
Handles login, logout, and token storage.

```javascript
import axios from "axios";

const API_URL = "http://localhost:8080/auth";

export const login = async (username, password) => {
  const response = await axios.post(`${API_URL}/login`, null, {
    params: { username, password }
  });

  const { accessToken, refreshToken } = response.data;
  localStorage.setItem("accessToken", accessToken);
  localStorage.setItem("refreshToken", refreshToken);

  return response.data;
};

export const logout = () => {
  localStorage.removeItem("accessToken");
  localStorage.removeItem("refreshToken");
};

export const getAccessToken = () => localStorage.getItem("accessToken");
export const getRefreshToken = () => localStorage.getItem("refreshToken");
```

---

## 🛠️ Step 3: Axios Instance (`src/services/axiosInstance.js`)
Adds JWT to requests and refreshes automatically.

```javascript
import axios from "axios";
import { getAccessToken, getRefreshToken } from "./authService";

const instance = axios.create({
  baseURL: "http://localhost:8080",
});

instance.interceptors.request.use(
  (config) => {
    const token = getAccessToken();
    if (token) config.headers["Authorization"] = `Bearer ${token}`;
    return config;
  },
  (error) => Promise.reject(error)
);

instance.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;
    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      const refreshToken = getRefreshToken();
      if (refreshToken) {
        const res = await axios.post("http://localhost:8080/auth/refresh", null, {
          params: { refreshToken }
        });
        localStorage.setItem("accessToken", res.data.accessToken);
        originalRequest.headers["Authorization"] = `Bearer ${res.data.accessToken}`;
        return instance(originalRequest);
      }
    }
    return Promise.reject(error);
  }
);

export default instance;
```

## 🛠️ Step 4: Protected Route Component (`src/components/ProtectedRoute.js`)
Restricts access based on role.

```javascript
import React from "react";
import { Navigate } from "react-router-dom";
import jwt_decode from "jwt-decode";

const ProtectedRoute = ({ children, role }) => {
  const token = localStorage.getItem("accessToken");
  if (!token) return <Navigate to="/login" />;

  try {
    const decoded = jwt_decode(token);
    const userRole = decoded.role;
    if (userRole !== role) return <Navigate to="/unauthorized" />;
    return children;
  } catch {
    return <Navigate to="/login" />;
  }
};

export default ProtectedRoute;
```

## 🛠️ Step 5: Pages

### Login (`src/components/Login.js`)
```javascript
import React, { useState } from "react";
import { login } from "../services/authService";
import { useNavigate } from "react-router-dom";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      await login(username, password);
      navigate("/"); // redirect after login
    } catch {
      alert("Login failed!");
    }
  };

  return (
    <div>
      <h2>Login</h2>
      <input placeholder="Username" onChange={(e) => setUsername(e.target.value)} />
      <input placeholder="Password" type="password" onChange={(e) => setPassword(e.target.value)} />
      <button onClick={handleLogin}>Login</button>
    </div>
  );
}

export default Login;
```

### Admin Page (`src/components/Admin.js`)
```javascript
import React, { useEffect, useState } from "react";
import axiosInstance from "../services/axiosInstance";

function Admin() {
  const [message, setMessage] = useState("");

  useEffect(() => {
    axiosInstance.get("/admin/dashboard")
      .then(res => setMessage(res.data))
      .catch(() => setMessage("Access denied"));
  }, []);

  return <h2>Admin Page: {message}</h2>;
}

export default Admin;
```

### User Page (`src/components/User.js`)
```javascript
import React, { useEffect, useState } from "react";
import axiosInstance from "../services/axiosInstance";

function User() {
  const [message, setMessage] = useState("");

  useEffect(() => {
    axiosInstance.get("/user/profile")
      .then(res => setMessage(res.data))
      .catch(() => setMessage("Access denied"));
  }, []);

  return <h2>User Page: {message}</h2>;
}

export default User;
```

### Unauthorized Page (`src/components/Unauthorized.js`)
```javascript
import React from "react";

function Unauthorized() {
  return <h2>🚫 Unauthorized Access</h2>;
}

export default Unauthorized;
```

## 🛠️ Step 6: App Routing (`src/App.js`)
```javascript
import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./components/Login";
import Admin from "./components/Admin";
import User from "./components/User";
import Unauthorized from "./components/Unauthorized";
import ProtectedRoute from "./components/ProtectedRoute";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/admin" element={
          <ProtectedRoute role="ADMIN">
            <Admin />
          </ProtectedRoute>
        } />
        <Route path="/user" element={
          <ProtectedRoute role="USER">
            <User />
          </ProtectedRoute>
        } />
        <Route path="/unauthorized" element={<Unauthorized />} />
      </Routes>
    </Router>
  );
}

export default App;
```


## 🔑 How It Works
- User logs in → React stores `accessToken` + `refreshToken`.  
- `ProtectedRoute` checks JWT role (`ADMIN` or `USER`).  
- If role matches → route loads. Otherwise → redirected to `/unauthorized`.  
- Axios automatically refreshes expired tokens using `/auth/refresh`.  
- Admin/User pages consume Spring Boot endpoints (`/admin/dashboard`, `/user/profile`).  



👉 You now have a **complete React app with protected routes** integrated with your **Spring Boot JWT API**.  



 Let’s add a **navigation bar** to your React app so users can easily switch between **Login, Admin, and User pages**.  


## 🛠️ Step 1: Install React Router (already done)
We’ll use `react-router-dom` for navigation.

## 🛠️ Step 2: Create a Navbar Component (`src/components/Navbar.js`)

```javascript
import React from "react";
import { Link, useNavigate } from "react-router-dom";
import { logout } from "../services/authService";

function Navbar() {
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  return (
    <nav style={{ padding: "10px", background: "#282c34", color: "white" }}>
      <Link to="/login" style={{ margin: "0 10px", color: "white" }}>Login</Link>
      <Link to="/admin" style={{ margin: "0 10px", color: "white" }}>Admin</Link>
      <Link to="/user" style={{ margin: "0 10px", color: "white" }}>User</Link>
      <button onClick={handleLogout} style={{ marginLeft: "20px" }}>Logout</button>
    </nav>
  );
}

export default Navbar;
```

## 🛠️ Step 3: Update `App.js` to Include Navbar

```javascript
import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./components/Login";
import Admin from "./components/Admin";
import User from "./components/User";
import Unauthorized from "./components/Unauthorized";
import ProtectedRoute from "./components/ProtectedRoute";
import Navbar from "./components/Navbar";

function App() {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/admin" element={
          <ProtectedRoute role="ADMIN">
            <Admin />
          </ProtectedRoute>
        } />
        <Route path="/user" element={
          <ProtectedRoute role="USER">
            <User />
          </ProtectedRoute>
        } />
        <Route path="/unauthorized" element={<Unauthorized />} />
      </Routes>
    </Router>
  );
}

export default App;
```

## 🧩 How It Works
- The **Navbar** is always visible at the top.  
- Links navigate to **Login**, **Admin**, and **User** routes.  
- The **Logout button** clears tokens and redirects to `/login`.  
- Protected routes still enforce role-based access using JWT.  



👉 Now your React app feels like a proper web application with **navigation and protected routes**.  


 Let’s make your **React Navbar smarter** so it only shows links based on the logged-in user’s **role** (Admin or User).  

## 🛠️ Step 1: Decode JWT Role
We’ll use `jwt-decode` to read the role claim from the JWT.

Install it:
```bash
npm install jwt-decode
```

## 🛠️ Step 2: Update Navbar (`src/components/Navbar.js`)

```javascript
import React from "react";
import { Link, useNavigate } from "react-router-dom";
import { logout } from "../services/authService";
import jwt_decode from "jwt-decode";

function Navbar() {
  const navigate = useNavigate();
  const token = localStorage.getItem("accessToken");
  let role = null;

  if (token) {
    try {
      const decoded = jwt_decode(token);
      role = decoded.role; // role claim from JWT
    } catch (err) {
      console.error("Invalid token", err);
    }
  }

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  return (
    <nav style={{ padding: "10px", background: "#282c34", color: "white" }}>
      {!token && <Link to="/login" style={{ margin: "0 10px", color: "white" }}>Login</Link>}
      {role === "ADMIN" && <Link to="/admin" style={{ margin: "0 10px", color: "white" }}>Admin</Link>}
      {role === "USER" && <Link to="/user" style={{ margin: "0 10px", color: "white" }}>User</Link>}
      {token && <button onClick={handleLogout} style={{ marginLeft: "20px" }}>Logout</button>}
    </nav>
  );
}

export default Navbar;
```

## 🧩 How It Works
- If **no token** → only shows **Login** link.  
- If logged in as **Admin** → shows **Admin** link.  
- If logged in as **User** → shows **User** link.  
- If logged in → shows **Logout** button.  


## 🔑 Example Flow
1. Ravi logs in as Admin → Navbar shows **Admin + Logout**.  
2. Guest logs in as User → Navbar shows **User + Logout**.  
3. Not logged in → Navbar shows **Login** only.  



👉 Now your React app has a **dynamic Navbar** that adapts to the user’s role from the JWT.  



Let’s make your React app **redirect users to their role-specific dashboard immediately after login**. This way, Admins go straight to `/admin` and Users go straight to `/user`.


## 🛠️ Step 1: Update Login Component (`src/components/Login.js`)

We’ll decode the JWT after login to check the role and redirect accordingly.

```javascript
import React, { useState } from "react";
import { login } from "../services/authService";
import { useNavigate } from "react-router-dom";
import jwt_decode from "jwt-decode";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      const data = await login(username, password);
      const decoded = jwt_decode(data.accessToken);
      const role = decoded.role;

      if (role === "ADMIN") {
        navigate("/admin");
      } else if (role === "USER") {
        navigate("/user");
      } else {
        navigate("/unauthorized");
      }
    } catch {
      alert("Login failed!");
    }
  };

  return (
    <div>
      <h2>Login</h2>
      <input placeholder="Username" onChange={(e) => setUsername(e.target.value)} />
      <input placeholder="Password" type="password" onChange={(e) => setPassword(e.target.value)} />
      <button onClick={handleLogin}>Login</button>
    </div>
  );
}

export default Login;
```

## 🛠️ Step 2: Flow After Login
1. User enters credentials.  
2. React calls `/auth/login` on Spring Boot API.  
3. JWT is returned → stored in `localStorage`.  
4. `jwt_decode` extracts the `role` claim.  
5. React Router `navigate()` sends the user to the correct dashboard:  
   - **Admin → `/admin`**  
   - **User → `/user`**  
   - Others → `/unauthorized`  



## 🧩 Example
- Ravi logs in with **Admin credentials** → immediately redirected to `/admin`.  
- Guest logs in with **User credentials** → redirected to `/user`.  
- Invalid role → redirected to `/unauthorized`.  



👉 Now your React app feels seamless: users land directly on their **role-specific dashboard** after login.  

