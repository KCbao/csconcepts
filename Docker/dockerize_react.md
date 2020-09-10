# The environment variable are embedded during the build time. 
Since Create React App produces a static HTML/CSS/JS bundle, it's cannot possibly read environment at runtime. Two ways to solve it:
1. rebuild the app on the server
2. to read environment variables at runtime, you need to load HTML into memory on the server and replace placeholders in runtime. 
