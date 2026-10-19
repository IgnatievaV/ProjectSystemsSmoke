// Navigation/resolve check: index.html -> site.js -> /hello endpoint in Program.cs
async function loadGreeting(name) {
    const response = await fetch(`/hello/${encodeURIComponent(name)}`);
    document.getElementById("greeting").textContent = await response.text();
}

loadGreeting("Rider");
