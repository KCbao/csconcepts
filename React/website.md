## What is index.html on a website?
- index.html is the name used for the homepage of the website， Basically, if no file is requested, the server knows which one to serve up by default.  If you don't put in an index.html file in a directory, by default most web servers will display a file listing of all the files in that directory. 

## What is web server?
- it is a software, or a hardware dedicated to run this software （a hardware专门用来跑这个software）
- Its primary function is to store, process and deliver web pages to clients
- When user send website request, it pulls content from the server and delivers it to the web
    1. the communication between user clients and server using HTTP
    2. These delivered web pages are most frequently HTML documents (contain the page's semantic content and structure), which may include image, style sheets (contains its style, could be defined using a style sheet language such as CSS), and scripts (Javascript) in addition to the text content. 
    3. Web servers process files written in different programming languages such as PHP, Python, Java, and others by interpret them into through `mod_php, mod_python, etc`. Web servers then turn them to static HTML files and serve these files in the browser of web users. 
    4. E.g., you want to open FB on your laptop and enter the URL in the search bar of google. Now, the laptop will send an HTTP request to view the facebook webpage to another computer known as the webserver. This computer (webserver) contains all the files (usually in HTTP format) which make up the website like text, images, gif files, etc. After processing the request, the webserver will send the requested website-related files to your computer and then you can reach the website.

- Famous web servers:
    1. Apache (aka Apache HTTP server): a software that establishs a connection between a server and the browsers of website visitors (Firefox, Google Chrome, Safari, etc.)E.g., to reach FB, their browser sends a request to your server, and Apache returns a response with all the requested files (text, images, etc.). The server and the client communicate through the HTTP protocol, and the Apache software is responsible for the smooth and secure communication between the two machines. It is a file server, good at serving static files. E.g., browser requests a static html file, apache will hand that to you. 
    2. NGINX: better performance than Apache in handling heavy-traffic requests, used in Airbnb, Netflix
    3. Tomcat (official name: Apache Tomcat): more for Java based logic, for example, talk to database, interact to backend services. 
    
    The typical request flow in a three-tier architecture is browser sends request first to hit the Apache web server, if it just need to return files like text, image, then it will directly return. If the request needs to perform logic, Apache server then passes request to the middle-tier Tomcat server and then Tomcat interacts with databases and other resources in the back-end tier. Tomcat aggregates the results and passes them to the Apache HTTP Server, and the HTTP server then sends the final response back to the client. 


## What is Node.js


## What is web framework
- A web application framework is a software framework that is designed to support the development of dynamic websites, web applications and web services. For example, many frameworks provide libraries for database access, templating frameworks and session management, and they often promote code reuse.
- A web framework uses a webserver to deliver the requests to client, but it is not the web server.