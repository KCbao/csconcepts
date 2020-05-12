## What is a REST API?
REST determines how the API looks like. It is a set of rules needed to be followed when create API. One of the rules states that you should be able to get a piece of data (called a resource) when you link to a specific URL. Each URL is called a request while the data sent back to you is called a response. 

### Request
A request is made up of four things:
- the endpoint: start point+path+query parameters e.g., https://api.github.com/users/zellwk/repos?sort=pushed
  - the start point of the API e.g., https://api.github.com
  - path: determines which service you're requesting for. e.g., get a list of repo by a certain user through Github's API. path: /users/:username/repos: (:) denotes a variable
  - query params: give you the option to modify your request with key-value pairs. It always begins with a (?), each param pair is separated by (&). e.g., param name: sort, type: string, description: can be one of `create`, `updated`, `pushed`, `full_name`, default `full-name`
- the method (GET, POST, PUT, PATCH, DELETE)
- the headers: infor such as authentication tokens, or cookies
- the data: contains information you want to be sent to the server. This option is only used with `POST`, `PUT`, `PATCH`, or `DELETE`


### Response
A JSON format file

### Dtypes most common with REST API
string, integer, boolean, object (key-value pairs in JSON format), array

## XML (eXtensible Markup Language)
XML is a tool to store and transport data. e.g., 
```
<note>
  <to>Tove</to>
  <from>Jani</from>
  <heading>Reminder</heading>
  <body>Don't forget me this weekend!</body>
</note>
```
The XML above is quite self-descriptive:

- It has sender information.
- It has receiver information
- It has a heading
- It has a message body.

ML and HTML were designed with different goals:

- XML was designed to carry data - with focus on what data is
- HTML was designed to display data - with focus on how data looks
- XML tags are not predefined like HTML tags are