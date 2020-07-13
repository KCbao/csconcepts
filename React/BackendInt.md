## Serve with FastAPI
1. `npm run build` to build your React app, it will create a build folder under my-app
2. copy all files under /my-app/build/static, and under /my-app/build and paste to /my-app/static (/static is new create folder)
3. [FastAPI static files](https://fastapi.tiangolo.com/tutorial/static-files/), [Starlette](https://www.starlette.io/staticfiles/) under /my-app, create a file called `main.py`, and install "pip install aiofiles"
```
from fastapi import FastAPI
from fastapi.staticfiles import StaticFiles
from starlette.responses import FileResponse
import uvicorn

app = FastAPI()

app.mount("/static", StaticFiles(directory="static"), name="index")

@app.get("/.*")
async def read_index():
    return FileResponse("static/index.html")

if __name__ == "__main__":
    uvicorn.run(app, host="127.0.0.1", port=8000)

```

and hit run to see my-app serve on 127.0.0.1:8000

### Problem with FastAPI
FastAPI is not able to catch all endpoints that routes request to our SPA (single page application). For example, if we type "127.0.0.1:300/manifest", we get 404 error. The single-page application only has one HTML, React routing just makes React to render selected parts defined by URl. These route endpoints are hidden in React index.html and FastAPI cannot pick them up, so we have the 404 error. 

### Fix: use Nginx and docker
Too complicated

- also mentioned in [Jack's post](https://gitter.im/tiangolo/fastapi?at=5e1bf699cb2aaa2d782c3116)

## Serve using Flask
[catch-all in Flask](https://flask.palletsprojects.com/en/1.1.x/patterns/singlepageapplications/), [also at](https://flask.palletsprojects.com/en/0.12.x/quickstart/#variable-rules).

-`__name__ `is just a convenient way to get the import name of the place the app is defined
- To run, in your terminal, first set `$env:FLASK_APP = "mainFast.py"`
- then in your mainFast.py, write `if __name__ == __main__: app.run()`, then hit "Run" button in VS code
- This launches a very simple builtin server, it's good enough for testing but prob not for production. 
- If you run the server, and note that the server is only accessible from your own computer, not from any other in the network. This is the default because in debugging mode. If you have debugger disabled, you can make the server publicly available simply by adding `--host=0.0.0.0`

Analogy: say FastAPI is one restaurant, it only allow you to order food on the menu (path operation, "/" leads to "index.html"). Flask is another restaurant, it allows you to order food on the menu, but if your order is not on the menu (not specified in your path operation), it will give you a big combo (that's your react), and let you eat yourself (so you can use react router to find that path). 

Wildcard: what to do if the path is not defined. 

## Set up Axios and Enable CORS
- "axios" will make AJAX requests from the client to the back-end server
- Anytime a client makes a request for a resource that resides on another machine as defined by protocol, IP address/ domain name, or port number, then we need to add additional headers associated with CORS. 
- [CORS](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS) is a mechanism that uses additional HTTP headers to tell browsers to give a web app running at one origin，access to selected resources from a different origin. 
- the brwoser wil send to the server
```
GET /resources/public-data/HTTP/1.1
Host: bar.other
User-Agent: Mozilla/5.0
Accept: text/html, application/xhtml+xml
Accept-Language: en-us, en
Accept-Encoding: gzip, deflate
Connection: keep-alive
Origin: https://foo.example
```
"Origin" shows that invocation is coming from "https://foo.example"
in response of "GET request", the server response like below
```
HTTP/1.1 200 OK
Date: Mon, 01 Dec 2008
Server: Apache/2
Access-Control-Allow-Origin: *
Keep-Alive: timeout=2, max=100
Connection: Keep-Alive
....

```
In response, the server sends back an "Access-Control-Allow-Origin" header, it is "*" meaning that resource can be accessed by any domain. If the resource owner at "https://bar.other" wish to restrict access to the resource to requests only from "https://foo.example", then they would send "Access-Control-Allow-Origin: https://foo.example". To allow access to the resource, the "Access-Control-Allow-Origin" header should contain the value that was sent in the request's "Origin" header. 