## Basics

### Start a project
`ng new <project name>`
### Serve the app
Go to the workspace directory and launch the application `ng serve --open`.
- The `ng serve` command builds the app, starts the development server, watches the source files, and rebuilds the app as you make changes to those files
- `--open` flag opens a browser to http://localhost:4200/ by default.

### Angular components
Angular is also a component-leading app
Similar to React: it has
- app.component.ts: class mode, typescript
- app.component.html
- app.component.css

### NG vs NPM
NPM contains and manages many packages and modules, and NG is one such module which is a "core module" of Angular.
- When `npm start` start, load compile your app using npm, it actually internally uses or loads `ng` module if it is an angular project. 
- When `ng serve` NG (angular-cli) provided by NG can be used to directly start, build your angular app




