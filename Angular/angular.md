## Installation
1. install `node`: `brew install node`
2. install angular-cli `sudo npm install -g @angular-cli`
3. now you may encounter "ng: command not found", solve by this link [resolve ng on Mac OS](https://github.com/angular/angular-cli/issues/5021#issuecomment-377030706)
4. check `ng` is installed by `ng --version`

## Start a project
`ng new <project name>`
## Serve the app
Go to the workspace directory and launch the application `ng serve --open`.
- The `ng serve` command builds the app, starts the development server, watches the source files, and rebuilds the app as you make changes to those files
- `--open` flag opens a browser to http://localhost:4200/ by default.

## Folder Structure
1. `package.json`: details of your application and dependendies (and their versions)
2. `node_modules`: all your dependendies that you are `npm install` are stored in here
3. `src`: source folder
4. `environment`: it contains a different environ for testing and production
5. `index.html`: inside it has `<app-root>`: this tag
6. `app` folder: its your by default root module, all your app-related stuff shall be put in here
7. `component` files e.g., `app.component.html`, `app.component.css`, `app.component.ts`, `app.components.spec.ts`
8. `app/server`: add server component, all components shall be stored under '/app' and has its own folder

- by convention, component usually named as "xxxComponent"

## Angular Basics
When Angular gets started, it first exectutes `main.ts` file, in `main.ts`, it is running `platformBrowserDynamic().bootstrapModule(AppModule).catch(err => console.error(err));` it bootstrap AppModule, which is defined in `app.module.ts`, in `app.module.ts`, it has bootstrap `AppComponent`, which is defined in `app.component.css, .html, .spec.ts, .ts`. And now Angular knows how to handle `app-root` in the `index.html`. 

## Angular components
Angular is also a component-leading app
Similar to React: it has
- app.component.ts: class mode, typescript
- app.component.html
- app.component.css
- app.component.spec.ts: used only for testing

### Create a component
`ng g component <component name>`: to create a component <component name>  with `<name>.component.ts, .html, .css`

### Add Component decorator
1. `import {Component} from '@angular/core'`
2. 
```
// selector: id of this component that we can later refer on， must by unique. selector could also set to be attribute, e.g., '[app-server]', then refered as `<div app-server>` in app.component.html
// templateUrl: location of the html template file that this component is referring on
@Component({
    selector: 'app-server',
    templateUrl: './server.component.html'
    // or inline template:`to write html line here`
    // e.g., template:
    `
    <app-server></app-server>
    `
    //styleUrls: ["./app.component.css"] you could include multiple css in this list
    // or set inline style: takes a list of string
    styles: [
        `
        .h3 {
            color: blue;
        }    
        `
    ]

})

```
- note: you must have either `templateUrl`: which refer to a separate html file, or use `template` to include html within this `.ts`. 

### add component styles
1. add styleUrls in `.ts`

## Databinding
- a communication between typescript code (business logic) and template (html)
- from typescript code to template: 
    1. string interpolation `{{data}}`
    2. property binding `[property]="data"`
- from template to typescript code
    1. React to user events: event binding `(event) = "expression"`
- combine of both: two-way-binding `[(ngModel)] = "data"`

### Render component
1. in `app.component.ts`, under declaration, add path of your component name, e.g., `../serverComponent`
2. under `app.component.html`, render `<app-server></app-server>`, where `app-server` is defined in your serverComponent decorator "selector" section.  


## React JSX vs Angular html
- React component function return JSX, versus Angular component, the file `.html` is the html file. React component is a combination of js and html in a `.js` file, except return JSX will be the similar effect as html.

## Module
- Angular uses module to bundle components to packages. It is okay for an app to have only one module, the AppModule. For more complex app projects, then we might need more modules
- A module contain components, service providers, and other code files whose scope is defined by the containing "NgModule"
- Every Angular App has at least one NgModule class, "the root module", which is conventionally named "AppModule" and resides in the file named "app.module.cs"

### NgModule Metadata
just include files under "app" folder doesn't mean it will be rendered. 
- "declarations": contains the components that belong to this "NgModule", e.g., you want to render chatComponent, you need to add to this declaration
- "exports": the subset of declarations that should be visible and usable in the component templates of other "NgModules"
- "imports": allow us to add other modules whose exported classes are needed by component templates declared in this "NgModule"
- "providers": creators of services that this "NgModules" contributes to the global collection of services
- "bootstrap": tell Angular what components are included (need to be rendered) when the app starts (i.e., look at `main.ts`) 

### String Interpolation
`{{<any thing could return string>}}`: anything return string, e.g., string itself, a variable that refers to a string, a method that returns a string. But not a block code

### Property binding
- `[<name>]`: tell Angular that we are using property binding
```
in .html
<button class="btn btn-primary" [disable]="!allowNewServe" />
```
this.<variable name> to get the variable defined within class

### Event Binding
```
<button class="btn btn-primary" [disable]="!allowNewServe" onclick=""/>

OR 
<button class="btn btn-primary" [disable]="!allowNewServe" (clink)="onCreateServer()"/>
where onCreateServer is a method we define in .ts that change the variable state of "createServer"
```

### Model
define required fields (and also their types) of an object, it is used for fastly creating new object of the same field. 
1. create a model file under "chat" called "chat.model.ts"
2. in chat.model.ts, 
```
export class Chat{
    public userAvatar: string;
    public userName: string;
    public text: string; 
    public date: Date;

    constructor(userAvatar: string, name: string, text: string, date: Date){
        this.userAvatar = userAvatar; 
        this.userName = name;
        this.text = text; 
        this.date = date;
    }
}
```
- `public`: meaning it can be accessed by outside as an extended object, `avatar`: name of the method, 
`string`: type of this field, 
- `constructor`: add constructor methods so that we can add a new chat object by `new Chat`. (items): items that are needed as input of this constructor. `this.userAvatar = userName`: then this field of Chat model is defined as input userName
3. in chat.component.ts, create an object of this model by 
```
import {Chat} from './chat.model';

chat: Chat[]= [
    new Chat('avatar', 'Ata', 'Hello Michael', new Date('1995-12-17T03:24:00'))
  ];

```
- `Chat[]`: tells Angular it is a list of Chat model object. `Chat`: just mean one Chat object
- `new Chat`: will initiate an object of model Chat, which is allowed since we have a constructor in model Chat. 

### *ngFor
Allow us to loop through list of items. In chat.component.html, 
```
<div class="row">
    <div class="col-xs-12">
        <a href="#" class="list-group-item clearfix" *ngFor="let chat of chats">
            <div class="pull-left">
                <h4 class="list-group-item-heading">{{chat.userName}}</h4>
                <p class="list-group-item">{{chat.text}}</p>
                <p class="list-group-item">{{chat.date}}</p>
            </div>
            <span class="pull-right">
                <img src={{chat.userAvatar}} alt={{chat.userName}}>
            </span>
        </a>
    </div>
</div>

```
- `*ngFor="let chat of chats`: chats is the list of objects we create in chat.component.ts, where each object is an example of Chat model. 
-` {{chat.userName}}`: now we could bind property of object chat user double brackets

## HTTP calls and Backend Interaction
### How Angular interact Database
Angular <=> Server (which is just an API, could be RESTful, or GraphQL etc) <=> Database

Never talk to DB directly inside your Angular app, cuz that's not save, other users can inspect your browser and easily retrieve data in DB. 

### Anatomy of a HTTP request
1. URL (API endpoint)
2. HTTP verb (GET, POST, etc)
3. Headers (Metadata): it is attached with a HTTP request, it's optional to define it yourself, as there is always a default one. 
4. Body: optional

### Send a Post Request
add HTTPClient
1. inside "app.module.ts", first `import {HTTPClientModule} from '@angular/common/http';`
2. add "HTTPClientModule" in "import"
3. add request function inside "chat.component.ts" class
```
constructor(private http:HttpClient){}

ngOnInit(){
this.fetchChats();
}

private fetchChats(){
    this.http.get('https://vic-webapi.azurewebsite.net/chat/message/query-next?nextChatMessageId=90').subscribe(oldChats =>{
      console.log(oldChats);
    });

  }
```
- first define "http" as an "HttpClient" in constructor
- `this.http.get`: return a "Observable" that wraps your HTTP request
- `subscribe`: it is a method to Oberserable in order to get a response from HTTP request observable, you must include this subscibe, it takes in a function
- `ngOnInit(){<methods>}`: when you start rendering this component, these are the methods you need to render
- open browser, in "inspect/Network", you will see items. Click on the item, and the "Header" is what is send along with your HTTP request. 


### Use RxJS Operators to transform JSON
To transform Response JSON to array, we can either
1. do inside "subscribe"
2. use "pipe": a method on Observable object through several user-defined operators before it reaches "subscribe". It is better than method 1 as it allows us to write a cleaner code, and leave subscribe to do other work

Below is method 2 for example
```
private fetchChats(){
    this.http.get<{[key: string]: Chat}>('https://vic-webapi.azurewebsite.net/chat/message/query-next?nextChatMessageId=90')
        .pipe(map( responseData =>{
          const chatArray: Chat[] = [];

          for (const key in responseData){
            if (responseData.hasOwnProperty(key)){
              chatArray.push({...responseData[key], id: key});
            }
            
          }
        }
          
        ))
        .subscribe(oldChats=>{
                    console.log(oldChats);
                  });

  }
```
- "map": here inside pipe, we use an operation called map, so we need to `import {map} from 'rxjs/operators', it will map an object into another thing, e.g., here it's an array. 
- "push": a JS array method, it adds new items to the end of an array, and returns the new length.
- `{...responseData[key], id: key}`: copy everything in responseData, plus add another field to this chatArray called "id"
- "if": it's a good practice to check the responseData has property "key"
- "get<{[key: string]: Chat}>": inside <> is the type of the dataResponse we expect to get from HTTP GET request. In this case, we expect to get a JSON object with key is a string, and content includes fields of a Chat model. An alternative way is to include it in "map" as `map( (responseData: {[key: string]: Chat}) => {blablabla})`. 

### injective
Add injectable when you will have another services (here is HTTP services ) inside current services (ChatStorageService)
- note you can either add "ChatStorageService" in "providers" inside app.module.ts, or use "{providedIn: 'root'}" to mount to the root

```
import { Injectable } from '@angular/core';

@Injectable({providedIn: 'root'})
export class ChatStorageService{

}

```
### add HTTPClient in Server
1. inside "app.module.ts", first `import {HTTPClientModule} from '@angular/common/http';`
2. add "HTTPClientModule" in "import"
3. inside "chat-storage.module.ts", 
```
import {HttpClient} from '@angular/common/http';

@Injectable({providedIn: 'root'})
export class ChatStorageService{
    constructor(private http: HttpClient){
        storeChat(){
            
        }
    }
}
```
- create an object "http" of type "HttpClient"
- "storeChat" is a method in class

## Routing
1. add a script "app-routing.module.ts"
```
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './core/helpers/auth.guard';
import { LoginComponent } from './login/login.component';
import {ChatComponent} from './chat/chat.component';


const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard], pathMatch:'full'},
  { path: 'login', component: LoginComponent },
  { path: 'chat', component: ChatComponent},

  // otherwise redirect to home
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

```
- `import {NgModule}`
- NgModule import, export to make sure RouterModule is added to our app
- also in "app.module.ts", import "AppRoutingModule", which is the class name defined here
- "pathMatch": 
3. in "app.component.html": add `<router-outlet></router-outlet>`


## Header
Material UI toolbar often used to make headers

### NG vs NPM
NPM contains and manages many packages and modules, and NG is one such module which is a "core module" of Angular.
- When `npm start` start, load compile your app using npm, it actually internally uses or loads `ng` module if it is an angular project. 
- When `ng serve` NG (angular-cli) provided by NG can be used to directly start, build your angular app








