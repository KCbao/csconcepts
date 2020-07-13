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
    2. NGINX (pronounce engine-x): better performance than Apache in handling heavy-traffic requests, used in Airbnb, Netflix. It can also act as a reverse proxy server. You place it in front of your current application server setup. NGINX faces the web and passes request to your application server, to makes your website run faster, reduces downtime, consumes less server resources and improces security. It also acts as a load balancer for multiple application servers, it loads balance traffic across them to scale performance, increase reliability and uptime. 
    3. Tomcat (official name: Apache Tomcat): more for Java based logic, for example, talk to database, interact to backend services. 
    
    The typical request flow in a three-tier architecture is browser sends request first to hit the Apache web server, if it just need to return files like text, image, then it will directly return. If the request needs to perform logic, Apache server then passes request to the middle-tier Tomcat server and then Tomcat interacts with databases and other resources in the back-end tier. Tomcat aggregates the results and passes them to the Apache HTTP Server, and the HTTP server then sends the final response back to the client. 


## What is application server?
Application server is a software program that runs on a computer that converts source code or compiled binaries into machine instruction. 


## What is web framework
- A web application framework is a software framework that is designed to support the development of dynamic websites, web applications and web services. For example, many frameworks provide libraries for database access, templating frameworks and session management, and they often promote code reuse.
- A web framework uses a webserver to deliver the requests to client, but it is not the web server.

## 0.0.0.0
means all IPv4 addresses on the local machine. If a host has two ip addresses, 192.168.1, and 10.1.2.1, and a server running on the host lisens on 0.0.0.0, it will be reable at both of these IPs. i.e., http://192.169.1:8080 and http://10.1.2.1:8080 can access

## 本地IP
本机有三块网卡， loopback (虚拟网卡)， ethernet (有限网卡, public IP address)， wlan （无限网卡, router's IP）。 你的本机IP是你真实网卡的IP， i.e.， 有限无限各一个， 而127.0.0.1 叫做loopback的IP。 
- on windows, open cmd, and type `ipconfig`, default gateway is router's IP, most of the time it's either 192.168.0.1 or 192.168.1.1. 
- to the Internet, all the computers in your household appear to share a single IP, that is "type my ip on browser show you: the public IP". So if something is traced to a particular IP address, it will only be to the household, cannot tell which computer in a house a particular connection. `ipconfig` show you address on the private house network run by the router. 
- the router runs its own in-house network, usually using 192.168.xx range of IP, (it is reserved for private networks)

## WSGI (web service gateway interface)
It is an API that allows web servers to talk to Python web applications. 

## npm start in React
Ref: [Your first week with React](https://books.google.ca/books?id=yJc9DwAAQBAJ&pg=PT26&lpg=PT26&dq=your++first+week+with+react+development+environment+including&source=bl&ots=bxYnUWIjKe&sig=ACfU3U22VT5qLWHoCrF0ZOmCI5m7iLHzFg&hl=en&sa=X&ved=2ahUKEwj80d2Jm4jqAhVbZ80KHVT0C7AQ6AEwAHoECAoQAQ#v=onepage&q=your%20%20first%20week%20with%20react%20development%20environment%20including&f=false)
`npm start` as indicated in `package.json` is actually `start: react-scripts starts`, it sets up a convinient development environment- including a web server, compiler and testing tools. 



## React server and Flask server
In dev mode, in order to run the application, I have to run the react server with npm start, and flask server with Python. His question is to serve the react build folder with flask. 


## API
APIs consist of three parts: 
1. user: the person who makes the request
2. client: the computer that sends the request to the server
3. server: the computer that resonds to the request

