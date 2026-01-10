import AppRoutes from "./routes";
import Header from "../components/layout/Header";
import Footer from "../components/layout/Footer";
import "../styles/global.css";

function App() {
  return (
    <div className="App">
      <Header />
      <main style={{ minHeight: "80vh", padding: "1rem" }}>
        <AppRoutes />
      </main>
      <Footer />
    </div>
  );
}

export default App;
