Kafka 
- good at dealing with large amount of data split into small chunks
- Topics: to link consumer and producer
- Kafka has its own BD, so if anything is missed during transmission, we can rollback
- Change SQLite to PostgreSQL to be better at dealing with Geospatial data
- Kafka supports JSON schema, it can accept JSON object directly. but if you want to save more space in RAM, suggest to serialize to Bytes, pass to consumer, and consumer then can de-serialize it back to JSON object. Avron is able to help the serialize and de-serialize process. 
- K-stream: is the abstraction of a record stream (of key-value pairs). KStream can be created directly from one or many Kafka topics (using StreamsBuilder.stream operator) or as a result of transformations on an existing KStream.right now K-stream can only be written in Java. 
- Every CSC will be dockerized into independent docker container, and in "docker.compose.yml", we can specify the ports so that each container knows how to communicate with each other. Presumely, each CSC is talking directly to broker, and make changes in common DB. 