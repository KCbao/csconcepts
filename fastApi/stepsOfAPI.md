
mafea-common
    /mafea_common: JSON schema classes
    /tests: provides examples of valid JSON object to JSON schema
## Step 1: JSON Schema
In this step, we need to write models so that programs can use them to validate the data coming through API is valid. Since our program is Python, so we use a package called "pydantic" to construct classes for each JSON schema. 

## Step 2: Write valid JSON for each JSON Schema
In this step, we use "Pytest", it is a testing framework that allows us to write test codes using Python. 

- To run a test python file, in terminal console, type "pytest <file name>"

## API: with FastAPI