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

@app.get("/")
async def read_index()
    return FileResponse("static/index.html")

if __name__ == "__main__":
    uvicorn.run(app, host="127.0.0.1", port=8000)

```

and hit run to see my-app serve on 127.0.0.1:8000