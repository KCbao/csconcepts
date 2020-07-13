## .env file
- called "dotenv file": it must be ignored from version control because it will expose sensitive information as part of the project. 
- it has one environment variable assignment per line
- in debug mode, VScode uses up "launch.json" to configure, so add `"env":{"PYTHONPATH": "C:\\"}` under Python list in "configuration" section