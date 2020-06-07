## JSON schema
- it's a declarative format for "describing the structure of other data"
- it's itself JSON
- Used to validate if the incoming or outgoing JSON is valid
```
# an empty object is a valid schema that will accept any valid JSON
{ }
# e.g., the following are valid: 42, "I am a string", {"an":"structure"}

# use "true" inplace of the empty object to represent a schema that matches anything
true
# this accepts any valid JSON, e.g., 42, "I am a string", {"an":"structure"}

# that matches nothing
false
```
### Declare something is JSON schema
To differentiate it to an arbitrary chunk of JSON, we need to use the keyword `$schema` to declare 
- that something is JSON schema, 
- it also declares which version this schema was written against
```
http://json-schema.org/draft/2019-09/schema#
http://json-schema.org/draft-07/schema#
http://json-schema.org/draft-06/schema#
http://json-schema.org/draft-04/schema#

# note to declare schema w/o specific version (http://json-schema.org/schema#) was deprecated after Draft 4 and shall no longer be used
```
It's recommanded that all JSON Schemas have a `$schema` entry, which must be the root. 

### Declare a unique identifier
`$id`: is the unique identifier for each schema, for now, just set it to a URL at a domain you control, e.g., `{"$id": "http://yourdomain.com/schemas/myschema.json"}.

Note in Draft 4, `$id` is just `id`

### Type-sprcific keywords
JSON Schema defines the following basic types, below is a table of which maps its analogous types in Python
| Javascript      | Python       | 
| -------------   |:------------:| 
| string          | string       | 
| number          | int/float    |   
| object          | dict         |   
| array           | list         |
|boolean          | bool         |
|null             |  None        |