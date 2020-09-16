```

upstream gui-server{
    server gui-server:8080;
}

server{

    listen 5000

    location <optional modifier> <location_match> {
        proxy_pass: http://gui-server;
    }

    location /api {
        proxy_pass: localhost:8080; 
    }

    location = /50x.html{
        # handle all server errors
        root /usr/share/nginx/html;
    }
}

```

### [location](https://dev.to/danielkun/nginx-everything-about-proxypass-2ona)

Reference: http://nginx.org/en/docs/http/ngx_http_proxy_module.html. 
Case 1: if proxy_pass directive is a URI (i.e., has content after .com e.g., localhost:5000/item), then cut-off

- proxy_pass url will cut of location url == url received by upstream. E.g., location `/webapp/`, proxy_pass `http://localhost:5000/api/`, request send from web app `/webapp/foo?bar=baz`, so url received by upstream is `http://localhost:5000/api/foo?bar=baz`. For more examples, see location link. 

- to overcome this location cut-off, using `$uri ` or `$request_uri`. (`$request_uri` wil preserves the query parameter while `uri` discards). E.g., location `/webapp/`, proxy_pass `http://localhost:5000/api$request_uri`, request send from web app `/webapp/foo?bar=baz`, so url received by upstream is `http://localhost:5000/api/webapp/foo?bar=baz`.

Case 2: if proxy_pass directive is not a URI (i.e., stop at .com, e.g., localhost:5000), then no cut-off

- the request body is added after proxy_pass directive. e.g., location /item { proxy_pass http://localhost:5000}, when server receives, it reads as `http:localhost:5000/item`
- when location is specified using a regex, then proxy_pass should be specified without a URI

### proxy_pass
When a request matches a location in a proxy_pass directive inside, the request is forwarded to the URL given by the proxy-pass (i.e., "http://gui-server"). 

Proxy_pass can be used when 
- case 1: there is a nginx instance that handles many things, and delegates some of those requests to other servers
- case 2: use it to deliver static files for a frontend, while server-side render API. E.g., a WebApp is running on localhost:5000 and it can talk to API endpoints localhost:8080. In the above example, WebApp is listening at port 5000, and all requests to "localhost:5000/api" will be forwarded to "localhost:8000". This way, there will be no CORS issue. 

### up-stream
It defines a groups of servers. Servers can listen on different ports. E.g., Here we define a server called "gui-server" (I call it upstream title), it is listening to "gui-server" at port 8080. And in "proxy_pass", we could redirect to this server named "gui-server" using the upstream title. 

### Optional modifier
- (none) The lcoation given will be matched against the beginning of the request URI to determine a match
- =: exact match
- ~: case-sensitive regular expression match. e.g., `~ /api/(...)`: (...) capture anything matched. [Regex reference](https://www.computerhope.com/jargon/r/regex.html)
- ~*: case-insensitive regexp match
- ^~: if this block is selected as the best non-regular exp match, regular expression matching will not taking place. 

### nginx.conf vs nginx.conf.d
If we write in nginx.conf, then we need to wrap into http block, and add "event" block
```
event { }
http {
    upstream{

    }

    server{

    }
}
```

But if write in nginx.conf.d, then we don't need to wrap into http block. 

### http and websocket entry point can use the same upstream server
To include websocket location, we could add it with other http locations under server block. 

## Websocket vs socket.io
- Websocket is a protocol, just like http protocol. Example, in http, we request `http://localhost`, in websocket, we request `ws://localhost:`
- [Ref](https://www.nginx.com/blog/websocket-nginx/). Javascript Websocket package is already in React, don't need to install additional dependency package, in React, 
```
const ws = new Websocket("ws://localhost:8080/guiserver")
```
it will connect to server run at port 8080 with endpoint `/guiserver`. If want to use nginx to proxy pass, use `const ws = new Websocket("ws://localhost:3001/guiserver")`, where react app docker container is running on port `3001:3000`, open port 3001. And inside `nginx.conf`, add proxy_set_header and proxy_http_version enable NGINX to properly handle WebSocket protocol. `proxy_http_version` must be 1.1, not 1.0. 

- To establish a Websocket connection (a.k.a Websocket handshake), this process starts with client sending a regular HTTP request to the server, with an "Upgrade header" included in this request to inform the server that the clients wish to establish a Websocket connection. In this way, Websockets allow both HTTP and Websocket protocols to be communicated over the same port, and the server can handle a standard HTTP request connection as well as an HTTP Upgrade request. 

```
map $http_upgrade $connection_upgrade{
    default upgrade; 
    '' close; 
}
upstream guiserver {
    gui-server:8080
}
server{

    location ~ /guiserver {
        proxy_pass: http://gui-server;
        # ws endpoint, need to add below proxy_set_header
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade; 
        proxy_set_header Connection $connection_upgrade;
        proxy_set_header Host $host; 
    }

    location ~ /handler {
        proxy_pass: http://gui-server; 
        # normal http endpoint
    }
    location /{
        #static files
    }
    }

```

- socket.io is not a Websocket implementation, but mostly used for websocket purpose. If want to use socket.io to do websocket, then both server and client need to use socket.io, otherwise they cannot communicate. For example, if React client is using socket.io, fastapi server is using plain Websocket, they cannot talk. That is because socket.io server requires both the websocket protocol for connection initiation, and the socket.io format (additional metadata) on top of it. E.g., client sent url `ws://localhost/guiserver` reaches backend server will become `ws://localhost/guiserver/?EIO=3&transport=polling&t=NHsNcrh`. 
- JS Websocket vs socket.io: socker.io is good for broadcast. For example, when multiple clients connect to server, if use JS Websocket, when event triggered, server will send message to client one by one. But if use socket.io, then all clients will be notified at the same time, that's called broadcasting. 