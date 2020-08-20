## MongoDB
- NoSQL 

## Install mongodb on Windows
1. create the data folders to store your databases
2. setup alias shorts for mongo and mongod. 
    - open GitBash
    - Change dir to your home dir by `cd ~`
    - create bash_profile `touch .bach_profile`
    - put in the following in .bash_profile
    - 
    ```
    alias mongod="C:/Program\ Files/MongoDB/Server/4.2/bin/mongod.exe"
    alias mongo="C:/Program\ Files/MongoDB/Server/4.2/bin/mongo.exe"
    ```
    - verify the setup by 
        - close down current terminal
        - re-launch git bash
        - type `mongo --version`, you shall see mongoDB shell version, build env, etc

## BSON
`pip install bson`: for BSON (binary JSON) encoding and decoding

## Install pymongo in python
need to install bson first, then install pymongo to avoid some errors
- `python -m pip install pymongo`

## Motor
`pip install motor`: asyc python driver for mongoDB
`client = motor.motor_asyncio.AsyncIOMotorClient(Databse_URL)`: create a new connection to a single MongoDB instance at host:port (database_url)

### Get database
- `client.<database name>` or `client['database_name']`: a single instance of MongoDB can support multiple independent databases. 

## MongoDB terminology
- database: a collection of multiple tables
- a collection: one table
- document: one row in the table
- field: column
- table joins: MongoDB doesn't support
- "_id": MongoDB will use "_id" as primary key (unique index) to the document

## Mongod
Mongod is the mongodb daemon process, mainly used to start mongodb service. We could use one window to type in `mongod` to start mongoDB, and in another window, through `mongo` to link to database. 

## The mongo Shell
1. to connect to MongoDB instance running on your localhost default 27017 by typing `mongo`, connect to a non-default port `mongo --port 28015`, this will lead to a JS interative window, where you can type more commands in, need to end command with ";" because of JS. 
In that interative window: 
## Shundown running instance and restart
`db.adminCommand({shutdown: 1})

### listing databases 
`show databases`
### go to a particular databse 
`use <your_db_name>`, if this <your_db_name> is not present, then MongoDB server will create the database for you
### insert data
After switch to <you_db_name>, 
- `db.myCollection.insert({})`: insert documents as many as you want
- `db.myCollection.insertOne({})`: insert single document
- `db.myCollection.insertMany({})`: similar to `insert`, it can insert more than one document

### Query data
`db.myCollection.find({age: {$lt: 25}})`, or `db.myCollection.find().pretty()`: will display document in pretty-printed JSON format

`$lt`: special token, stand for less than

### Remove a document
`db.myCollection.remove({name: "john"})`: delete a collection

### Exit the shell
`quit()`, or Ctrl-C

## Replication
Main objective: replicate data and sync data across multiple server, improve the usability and security. 
Why replicate: - disaster recovery, allow you to recover data from hardware trouble or server break down. 

### Replication Main theory
Replication needs at least two nodes, one and only one must be "primary node", rest are secondary nodes. Common are one primary-one secondary, or one primary-multiple secondary. 

- primary node: receives all read and write operations from client app, it then records all changes to its datasets in its operation log (oplog)
- secondary node: secondaries replicate the primary's oplog and apply the operations to their datasets in an asyc process. 

All replicata set members contain a copy of oplog in "local.oplog.rs", allowing them to maintain the current state of the database. 
