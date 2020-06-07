## OpenAPI and JSON schema
OpenAPI defines an API schema, that includes data sent and received by your API using JSON schema, API paths, the possible paramters they take etc

## FastAPI Introduction
1. `from fastapi import FastAPI`: is a Python class that provides all the functionality for your API
2. `app = FastAPI()`: create a FastAPI instance, also referred as in terminal `univorn main:app --reload`
3. create a path operation: path here refers to the last part of the URL after the main website entry starting from the first `/`. 
Operation here refers to the `HTTP methods`
4. `@app.get("/")`: tells FastAPI that the function below is in charge of handling request goint to the path `/` using `get` operation, `@` in Python means "decorator", so this is indeed a "path operation decorator", similarly, you have `@app.post(), @app.put(), @app.delete()`
5. define the path operation function
`async def root(): return {"message":"hello world"}`: this is a Python function, it will be called by FastAPI whenever it receives a request to the URL "/" using `GET`, here this function can return normal Python dtypes, or return Pydantic models
6. runt he development server e.g. `unicorn main:app --reload`

## Path parameter
- can clare path parameters with Python types
## Query Parameter

## Request Body
When you need to send data from a client to your API, you send it as a request body. 

Request body: sent by client to API
Response body: data you API sends to your client
1. Create data model, that's your pydantic class
2. Declare it as a parameter, it will
- read the request body as JSON
- convert the corresponding types (if needed)
- validate the data against its schema defined in (1), if invalid, it returns an error
- give the received data in parameter `item`
- to get each part in this `item` JSON, use e.g., `item.name`


## Response Model
FastAPI will use this "response-model" to
- convert the output data to its type declaration, say FastAPI will filtering out all the data that is not declared in the output model
- validate data
- add a JSON schema for the response

"response_model_exclude_unset"= True/False, when set to True. if your response doesn't have or uses default values in its pydantic classes, then response will not show, if your response include a different values than it's in pydantic classes, then this field will be shown. 

"response_model_exclude" and "response_model_include" = {<field> to be omitted in response}

## Response Status Code
The `status_code` receives a number with the HTTP status code. 

It will
- return the status code in the response
- could either input the 3-digit code, or `from fastapi import status`, then `status_code=status.HTTP_201_CREATED)

## Additional Status Codes
