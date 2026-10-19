import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// Proxies /hello to Smoke.Web (profile "http").
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    proxy: { "/hello": "http://localhost:5131" },
  },
});
