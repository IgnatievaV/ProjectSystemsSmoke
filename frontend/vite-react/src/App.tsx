import { useEffect, useState } from "react";
import { formatGreeting } from "./greeting";

export function App() {
  const [greeting, setGreeting] = useState("Loading...");

  useEffect(() => {
    fetch("/hello/Repo%20view")
      .then((r) => r.text())
      .then((text) => setGreeting(formatGreeting(text)))
      .catch(() => setGreeting("Smoke.Web is not running"));
  }, []);

  return (
    <main>
      <h1>Standalone Vite + React app</h1>
      <p className="greeting">{greeting}</p>
    </main>
  );
}
