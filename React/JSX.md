## JSX: a syntax extension to JavaScript, use it with React to describe what UI should look like
```
<!-- JSX -->
<div>
<h1> Hello! </h1>
</div>
```
When it gets compiled, it actually runs 
```
<!-- block name, css, content -->
React.createElement('div', {className:'App'}, React.createElement('h1', null, 'Hello'))
```
behind the scene. They are equivalent, but `createElement` is cumbersome if you have many children parts, so we adapt JSX. 

### ES6, 7, 10
Javascript is a script language that satisfies ECMAScript. So as ES evolves, you need to update your JS writing stype and syntax



### Convention words
```
<!-- HTML -->
<div class='style'>
<h1> Hello! </h1>
</div>
```
we use `class` attribute to note the css styling for this division, but `class` is a convention word in JS, and this component codes are sat in App.js. So instead we use `className` in JSX
```
<!-- JSX -->
<div className='style'>
<h1> Hello! </h1>
</div>
```

### One root per Component only
For each compoenent, we only allow one root, the following is not allowed, becuz we have another `<h1>` outside of division block
```
<div class='style'>
<h1> Hello! </h1>
</div>
<h1> Yea!</h1>
```

## npm: development package manager

## Folders when initialize a React app
- node_modules: built-in tools, generated automatically when create React app
- public: note there will only be one `index.html` if you create single-page application, if you create multi-page application, you would add more `html` files under this folder
- public/index.html: note there is `<div id="root"/>` where you will find it renders components from `src/index.js`, there will be unique "root"
- public/manifest.json: stores meta-data of the initialized app
- src/serviceWorker.js: used when we re-cache things
- src/App.text.js: allow us to create unit-test on each components
- for new create Component, convention is to capitalize it

## DOM (Document Object Model)
There are three DOMs:
- HTTP DOM
- XML DOM
- Core DOM

HTML DOM is contructed as a tree of Objects, each element is an object
```
Document - Root Element (<html>) - Element <head>- Element <title>- Text "My title

```
HTML DOM defines 
- all HTML elements as objects
- properties of HTML elements
- methods to access all HTML elements
- events for all HTML elements 

JaveScript can change all HTML elements, attributes, CSS stypes, remove existing HTML elements and attributes, add new HTML elements and attributes, and react to all existing HTML events and create new HTML events.

## Rendering Elements
- Elements are the smallest building blocks of React apps (they are what components are make of)
- React elements are immutable, once created, cannot change its children or attributes
- each element is like a frame in a movie, it defines the UI at certain point in time

## Components and Props
- each components is a reusable piece
- components are like JS functions, accept arbitrary inputs (called props) and return JSX describing what should appear on the screen
- Props are read-only, all react components much act like pure functions (i.e., doesn't change its input) w.r.t their props

### ES6 to create a component Person.js


```
<!-- import react lib -->
import React from 'react'; 
<!-- <name> usually in lowercase e.g. person -->
var <name> = (<props list>) => {

<!-- <blablabla> is some JSX -->
return <blablabla>
};
<!-- export <name> which holds this above function -->
export default <name>; 

OR
class Person extends Component{
    render(){
        return <blablabla>
    }
}
```

### To use Person.js compoenent in App.js
```
<!-- first import Person, note its capitalized -->
<!-- no need to specify as Person.js becuz it's buit-in workflow -->
import Person from './<relative path>/Person'

function App(){
    return (
        <div> 
        <Person/>
        </div>
    );
}
```

### Re-use components
Each component can either be static or dynamic, change as function state changes or props change. 


### Pass in arguments: props
props allow you to pass data from a parent (wrapping) component to a child (embedded)component.
- you can pass in any names you want, convention is to use `props`
- pass in props as attribute from parent (wrapping) component `App` ` <Person name = "KC", age = "26"/>` to a child (embeded) compoenent `Person`
- read props in components `{props.name}`
- when using class-based components, use `this.props.name`, `this` refers to the class
```
class Clock extends React.Component{
    render(){
        return <p>{this.props.name}</p>;
    }
}
```

- pass in props in comopenents as `props.children`: `.children` is a reserved word
```
` <Person name = "KC", age = "26"> I am good </Person>
```
all fields between `<Person>` and `</Person>` will be treated to `props.children`

### State to manage inside component data
- Changes to state also trigger an UIupdate, i.e., re-render
- only class-based components can define and use state, and you define and use it within the same component
```

class App extends Component{
    <!-- yes -->
    <!-- initialize state (a reserved word) -->
    state = {
        persons: [
            {name: 'KC', age:26}, 
            {name: 'CC', age: 27}
        ]
    }
    render(){
        return(
         <div className="App">
         <Person name={this.state.persons[0].name}, age = {this.state.persons[0].age}>I am good </Person>
         <Person name={this.state.persons[1].name}, age = {this.state.persons[1].age}>I am good </Person>
         </div>
        ) 
    }
}

const person= ()=>{
<!-- not accept state -->
}
```

### Update state in class-based component
```
import React, {Component} from 'react'
class App extends Component{
    <!-- yes -->
    <!-- initialize state (a reserved word) -->
    state = {
        persons: [
            {name: 'KC', age:26}, 
            {name: 'CC', age: 27}
        ], 
        otherState: "I am awesome"
    }
    <!-- add a new method switchNameHandler, good convention to name is Handler to show it's for an event -->
    <!-- this.setState is a default method to mutate state inherited from parent class Component -->
    <!-- Don't do: this.state.persons[0].name = 'Casie' -->
    switchNameHandler = () =>{
        this.setState({
            persons: [
            {name: 'Casie', age:26}, 
            {name: 'CC', age: 27}
        ]
        })
        <!-- it overwrite exising persons, and leave unmemtioned field otherState -->
    }
    render(){
        return(
         <div className="App">
         <!-- inside this button block is what will show on button -->
         <!-- don't put this.switchNameHandler(), cuz it will render switchNameHandler right away, not when clicked -->
         <button onClick={this.switchNameHandler}>Switch Name </button>
         <Person name={this.state.persons[0].name}, age = {this.state.persons[0].age}>I am good </Person>
         <Person name={this.state.persons[1].name}, age = {this.state.persons[1].age}>I am good </Person>
         </div>
        ) 
    }
}

const person= ()=>{
<!-- not accept state -->
}
```

### Update state in function-based component (since React 16.8)
- all hooks start with `useXXX` keyword
- these React hooks allows to use functionality within component functions
```
import React, {useState} from 'react'
const app = props =>{
    <!-- useState returns an array with exactly two items -->
    <!-- first element personsState is always the current state -->
    <!-- second item setPersonState is a function that allows us to update the state s.t the React will be aware of the change, and re-reder DOM -->
    const [personsState, setPersonsState] = useState({
        persons: [
            {name: 'KC', age:26}, 
            {name: 'CC', age: 27}
        ], 
        otherState: "I am awesome"
    })
   
   <!-- allow function inside a function  -->
   <!-- pass on setPersonsState to it -->
    const switchNameHandler = () =>{
        setPersonsState({
            persons: [
            {name: 'Casie', age:26}, 
            {name: 'CC', age: 27}
        ], 
        <!-- copy currentstate.otherState in to avoid losing it during replacement -->
        otherState: personsState.otherState;
        })
        <!-- it overwrite exising persons, and leave unmemtioned field otherState -->
    }
    return(
        <div className="App">
        <replace this.state to personsState -->
        <!-- change this.switchNameHandler to switchNameHandler refers to the const defined above -->
        <button onClick={switchNameHandler}>Switch Name </button>
        <Person name={personsState.persons[0].name}, age = {this.state.persons[0].age}>I am good </Person>
        <Person name={personsState.persons[1].name}, age = {this.state.persons[1].age}>I am good </Person>
        </div>
    ) 

}

export default app;
```

### Difference b/t useState hook and setState
- setState merge current state with new state
- useState hook replace current state with new state

### Stateless/Presentational/Dumb vs Stateful/Container/Smart component
- Stateless Component: any component without state
- Stateful Component: with state (no matter it's setState, or useState)
- You want more stateless component and less stateful component, for good maintenance

### Pass in method reference between components
```
import React, {useState} from 'react'
const app = props =>{
    const [personsState, setPersonsState] = useState({
        persons: [
            {name: 'KC', age:26}, 
            {name: 'CC', age: 27}
        ], 
        otherState: "I am awesome"
    })
   
    const switchNameHandler = () =>{
        setPersonsState({
            persons: [
            {name: 'Casie', age:26}, 
            {name: 'CC', age: 27}
        ], 
        otherState: personsState.otherState;
        })
    }
    return(
        <div className="App">
        <button onClick={switchNameHandler}>Switch Name </button>
        <Person name={personsState.persons[0].name}, age = {this.state.persons[0].age, 
        <!-- pass in a method to Person.js -->
        click = {this.switchNameHandler}}>I am good </Person>
        <Person name={personsState.persons[1].name}, age = {this.state.persons[1].age}>I am good </Person>
        </div>
    ) 

}

export default app;

<!-- AND IN Person.js -->
const person = (props)=> {
    return (
        <!-- css style id person -->
        <div className= "person">
        <h1>{props.name}</h1>
        <!-- call it props click -->
        <p onClick={props.click}>Your age: {props.age}</p>
        </div>
    )
}
```

### Data Binding
```
import React, {useState} from 'react'
const app = props =>{
    const [personsState, setPersonsState] = useState({
        persons: [
            {name: 'KC', age:26}, 
            {name: 'CC', age: 27}
        ], 
        otherState: "I am awesome"
    })
   
    <!-- pass in newName as props -->
    const switchNameHandler = (newName) =>{
        setPersonsState({
            persons: [
            {name: newName, age:26}, 
            {name: 'CC', age: 27}
        ], 
        otherState: personsState.otherState;
        })
    }
    return(
        <div className="App">
        bind, t
        <button onClick={this.switchNameHandler.bind(this, "Max!")}>Switch Name </button>
        <Person name={personsState.persons[0].name}, age = {this.state.persons[0].age, 
        <!-- pass in a method to Person.js -->
        click = {this.switchNameHandler.bind(this, "Max!!!!")}}>I am good </Person>
        <Person name={personsState.persons[1].name}, age = {this.state.persons[1].age}>I am good </Person>
        </div>
    ) 

}

export default app;

<!-- same Person.js -->
const person = (props)=> {
    return (
        <div className= "person">
        <h1>{props.name}</h1>
        <p onClick={props.click}>Your age: {props.age}</p>
        <!-- onChange method fires up whenever the value of this input changes -->
        <input type="text" onChange={/> 
        </div>
    )
}
```

### Add two way Binding
Let button change state using user input
```
import React, {useState} from 'react'
const app = props =>{
    const [personsState, setPersonsState] = useState({
        persons: [
            {name: 'KC', age:26}, 
            {name: 'CC', age: 27}
        ], 
        otherState: "I am awesome"
    })
   
    <!-- pass in newName as props -->
    const switchNameHandler = (newName) =>{
        setPersonsState({
            persons: [
            {name: newName, age:26}, 
            {name: 'CC', age: 27}
        ], 
        otherState: personsState.otherState;
        })
    }
     
    <!-- add another handler -->
    const nameChangeHandler = (event)=>{
        setPersonsState({
            persons: [
            {name: newName, age:26}, 
            {name: {event.target.value}, age: 27}
        ]
    }
    return(
        <div className="App">
        bind, t
        <button onClick={this.switchNameHandler.bind(this, "Max!")}>Switch Name </button>
        <Person name={personsState.persons[0].name}, age = {this.state.persons[0].age, 
        <!-- pass in a method to Person.js -->
        click = {this.switchNameHandler.bind(this, "Max!!!!")}}>I am good </Person>
        <Person name={personsState.persons[1].name}, age = {this.state.persons[1].age}, 
        changed={this.nameChangehandler}>I am good </Person>
        </div>
    ) 

}

export default app;

<!-- same Person.js -->
const person = (props)=> {
    return (
        <div className= "person">
        <h1>{props.name}</h1>
        <p onClick={props.click}>Your age: {props.age}</p>
        <!-- onChange method fires up whenever the value of this input changes -->
        <!-- dont use props.changed() -->
        <input type="text" onChange={props.changed} value={props.name}/> 
        two-way binding, onChange lisens to changes and update changed, and output will reflect on name
        </div>
    )
}
```

### Add styling with stylesheets
Method 1: use .css
1. add Person.css, it is global, so can be added under any folder, but convention we add it under folder 'Person', with 'Person.js' to indicate it is the styling for Person component
2. in Person.css
```
.Person {
    width: 60px;

}
```
3. in Person.js, add `className`, import App.css
```
import './Person.css';

const person = (props)=> {
    return (
        <div className= "Person">
        <h1>{props.name}</h1>
        <p onClick={props.click}>Your age: {props.age}</p>
        <!-- onChange method fires up whenever the value of this input changes -->
        <!-- dont use props.changed() -->
        <input type="text" onChange={props.changed} value={props.name}/> 
        two-way binding, onChange lisens to changes and update changed, and output will reflect on name
        </div>
    )
}
```
3. import in App.js
```
import './App.css';
```

Method 2: inline style within .js
1. in `App.js`, add const style
2. add style in button
```
import React, {useState} from 'react';
import Person from './Person/Person';


const app = props =>{
    const style = {
        backgroundColor: 'white';
    }
    const [personsState, setPersonsState] = useState({
        persons: [
            {name: 'KC', age:26}, 
            {name: 'CC', age: 27}
        ], 
        otherState: "I am awesome"
    })
   
    <!-- pass in newName as props -->
    const switchNameHandler = (newName) =>{
        setPersonsState({
            persons: [
            {name: newName, age:26}, 
            {name: 'CC', age: 27}
        ], 
        otherState: personsState.otherState;
        })
    }
     
    <!-- add another handler -->
    const nameChangeHandler = (event)=>{
        setPersonsState({
            persons: [
            {name: newName, age:26}, 
            {name: {event.target.value}, age: 27}
        ]
    }
    return(
        <div className="App">
        bind, t
        <button 
        <!-- style attribute conventionally defined in JSX -->
        style = {style}
        onClick={switchNameHandler.bind(this, "Max!")}>Switch Name 
        </button>
        <Person name={personsState.persons[0].name}, age = {this.state.persons[0].age, 
        <!-- pass in a method to Person.js -->
        click = {switchNameHandler.bind(this, "Max!!!!")}}>I am good </Person>
        <Person name={personsState.persons[1].name}, age = {this.state.persons[1].age}, 
        changed={nameChangehandler}>I am good </Person>
        </div>
    ) 

}

export default app;
```

## Work with conditionals
- everything before return are JS, inside return are JSX
- below code block wants if we click a button, then persons name block show, otherwise, it is invisible
```
import React, {useState} from 'react';
import Person from './Person/Person';


const app = props =>{
    const style = {
        backgroundColor: 'white';
    }
    const [personsState, setPersonsState] = useState({
        persons: [
            {name: 'KC', age:26}, 
            {name: 'CC', age: 27}
        ], 
        otherState: "I am awesome"
    })
   
    <!-- pass in newName as props -->
    const switchNameHandler = (newName) =>{
        setPersonsState({
            persons: [
            {name: newName, age:26}, 
            {name: 'CC', age: 27}
        ], 
        otherState: personsState.otherState;
        })
    }
     
    <!-- add another handler -->
    const nameChangeHandler = (event)=>{
        setPersonsState({
            persons: [
            {name: newName, age:26}, 
            {name: {event.target.value}, age: 27}
        ]
    }

    let persons = null; 
    <!-- if then set persons to JSX -->
    if (this.state.showPersons){
        persons = (
        <div>
         <!-- use map to map JS to JSX -->
         <!-- inside map, the name `person` can use any name to represent the item in the JS array -->
        {this.state.persons.map(person => {
            return (
                <!-- here we return a JSX object with name is JS object's name -->
                <Person name = {person.name} age = {persons.age}>
            )
        })}
        </div>  
        )
    }
    return(
        <div className="App">
        <button 
        style = {style}
        onClick={switchNameHandler.bind(this, "Max!")}>Switch Name 
        </button>
        {persons}
        </div> 
    ) 

}

export default app;
```

## Work with Lists
### Output as a list
- map(): can used to map a JS object to JSX object
```
import React, {useState} from 'react';
import Person from './Person/Person';


const app = props =>{
    const style = {
        backgroundColor: 'white';
    }
    const [personsState, setPersonsState] = useState({
        persons: [
            {name: 'KC', age:26}, 
            {name: 'CC', age: 27}
        ], 
        otherState: "I am awesome"
    })
   
    <!-- pass in newName as props -->
    const switchNameHandler = (newName) =>{
        setPersonsState({
            persons: [
            {name: newName, age:26}, 
            {name: 'CC', age: 27}
        ], 
        otherState: personsState.otherState;
        })
    }
     
    <!-- add another handler -->
    const nameChangeHandler = (event)=>{
        setPersonsState({
            persons: [
            {name: newName, age:26}, 
            {name: {event.target.value}, age: 27}
        ]
    }

    let persons = null; 
    <!-- if then set persons to JSX -->
    if (this.state.showPersons){
        persons = (
        <div>
         <!-- use map to map JS to JSX -->
         <!-- inside map, the name `person` can use any name to represent the item in the JS array -->
        {this.state.persons.map(person => {
            return (
                <!-- here we return a JSX object with name is JS object's name -->
                <Person name = {person.name} age = {persons.age}>
            )
        })}
        </div>  
        )
    }
    return(
        <div className="App">
        <button 
        style = {style}
        onClick={switchNameHandler.bind(this, "Max!")}>Switch Name 
        </button>
        {persons}
        </div> 
    ) 

}

export default app;
```

### List and State
- delete a person from list by index
- note that set `persons.splice(Index, 1)` take in-place change, not a good practice in JS, instead, we shall always set a copy of persons, and mutate the copy, it's called updating array immutatbly

```
import React, {useState} from 'react';
import Person from './Person/Person';


const app = props =>{
    const style = {
        backgroundColor: 'white';
    }
    const [personsState, setPersonsState] = useState({
        persons: [
            {name: 'KC', age:26}, 
            {name: 'CC', age: 27}
        ], 
        otherState: "I am awesome"
    })
   
    <!-- pass in newName as props -->
    const deletePersonHandler = (personIndex) =>{
        fetch all persons
        <!-- const persons = this.state.persons; -->
        <!-- good practice is to set a copy to persons by .slice() with no args in () -->
        const persons = this.state.persons.slice();
        <!-- OR use ... method -->
        <!-- ... split original array this.state.persons to list, then [] to add to another array -->
        const persons = [...this.state.persons]
        <!-- remove one element from array persons -->
        persons.splice(personIndex, 1);
        this.setState({persons: persons});
    }
     
    <!-- add another handler -->
    const nameChangeHandler = (event)=>{
        setPersonsState({
            persons: [
            {name: newName, age:26}, 
            {name: {event.target.value}, age: 27}
        ]
    }

    let persons = null; 
    <!-- if then set persons to JSX -->
    if (this.state.showPersons){
        persons = (
        <div>
         <!-- pass in two args to map -->
        {this.state.persons.map((person, index) => {
            return (
                click = {() => this.deletePersonHandler(index)}
                <!-- here we return a JSX object with name is JS object's name -->
                <Person name = {person.name} age = {persons.age}>
            )
        })}
        </div>  
        )
    }
    return(
        <div className="App">
        <button 
        style = {style}
        onClick={switchNameHandler.bind(this, "Max!")}>Switch Name 
        </button>
        {persons}
        </div> 
    ) 

}

export default app;
```

### List and Keys
- put key so DOM only render the updated part, not the entire list
- ensure every element in array has a unique key, so React can compare future with past for DOM to update list where it needs to be updated

```
import React, {useState} from 'react';
import Person from './Person/Person';


const app = props =>{
    const style = {
        backgroundColor: 'white';
    }
    const [personsState, setPersonsState] = useState({
        persons: [
            <!-- add id to list -->
            {'id':"person1" name: 'KC', age:26}, 
            {'id':"person2" name: 'CC', age: 27}
        ], 
        otherState: "I am awesome"
    })
   
    <!-- pass in newName as props -->
    const deletePersonHandler = (personIndex) =>{
        fetch all persons
        const persons = [...this.state.persons]
        <!-- remove one element from array persons -->
        persons.splice(personIndex, 1);
        this.setState({persons: persons});
    }
     
    <!-- add another handler -->
    const nameChangeHandler = (event)=>{
        setPersonsState({
            persons: [
            {name: newName, age:26}, 
            {name: {event.target.value}, age: 27}
        ]
    }

    let persons = null; 
    <!-- if then set persons to JSX -->
    if (this.state.showPersons){
        persons = (
        <div>
        {this.state.persons.map((person, index) => {
            return (
                click = {() => this.deletePersonHandler(index)}
                <!-- here we return a JSX object with name is JS object's name -->
                <Person name = {person.name} age = {persons.age} id = {persons.id}>
            )
        })}
        </div>  
        )
    }
    return(
        <div className="App">
        <button 
        style = {style}
        onClick={switchNameHandler.bind(this, "Max!")}>Switch Name 
        </button>
        {persons}
        </div> 
    ) 

}

export default app;
```

### Flexible lists
Let click button in Person.js link to state change

```
import React, {useState} from 'react';
import Person from './Person/Person';


const app = props =>{
    const style = {
        backgroundColor: 'white';
    }
    const [personsState, setPersonsState] = useState({
        persons: [
            <!-- add id to list -->
            {'id':"person1" name: 'KC', age:26}, 
            {'id':"person2" name: 'CC', age: 27}
        ], 
        otherState: "I am awesome"
    })
   
    <!-- pass in newName as props -->
    const deletePersonHandler = (personIndex) =>{
        fetch all persons
        const persons = [...this.state.persons]
        <!-- remove one element from array persons -->
        persons.splice(personIndex, 1);
        this.setState({persons: persons});
    }
     
    <!-- add id -->
    const nameChangeHandler = (event, id)=>{
        <!-- JS method .find() takes a function and exectute this function to every element in array -->
        const personIndex = this.state.persons.findIndex(p => {
            return p.id == id;
        });
        <!-- also {} means making an copy of this object -->
        const person= {...this.state.persons[personIndex]};
        person.name = event.target.value;
        const persons = [...this.state.persons];
        persons[personIndex] = person;
        this.setState({persons: person});
    }

    let persons = null; 
    <!-- if then set persons to JSX -->
    if (this.state.showPersons){
        persons = (
        <div>
        {this.state.persons.map((person, index) => {
            return (
                click = {() => this.deletePersonHandler(index)}
                <Person name = {person.name} age = {persons.age} id = {persons.id} change={(event) => this.nameChangedHandler(event, persons.id)}>
            )
        })}
        </div>  
        )
    }
    return(
        <div className="App">
        <button 
        style = {style}
        onClick={switchNameHandler.bind(this, "Max!")}>Switch Name 
        </button>
        {persons}
        </div> 
    ) 

}

export default app;
```
## Convert a Function component to a class
- `class Clock extends React.Component`
- add a simply empty method to it called `render()`

## LIfecycle
- `componentDidMount()` to start the timer, `componentWillUnmount` to stop the timer


## Styling the React

## ReactDOM
- it only gets re-render if state or props change
```
<!-- JS -->
function Person(props){
    return (
        <!-- css style id person -->
        <div className= "person">
        <h1>{props.name}</h1>
        <p>Your age: {props.age}</p>
        </div>
    )
}

<!-- reuse person compoenent -->
var app = (
    <div>
        <Person name="Max", age = "28"/>
        <Person name="Casie", age = "26"/>
    </div>
); 

ReactDOM.render(app, document.querySelector('#app')
```
`ReactDOM.render(<Person name="Max", age = "28"/>, document.querySelector('#p1'))`: allows us to render a JS function `Person` as a component to DOM to HTML script where its block `id = "p1"`, `document.querySelector` is a JS syntax



## Planning an App
- Component Tree
- Applcation State (Data)
- Components vs Containers (i.e., which components shall be stateful, which shall be stateless)

## Auxs function component
- under src/hoc/Aux.js
- use to return props.children so that we don't need to have an overall `<div>` block around each of our component function

## Add a Router to React
- npm install --save react-router-dom

### Multi-page feeling in a single-page applcation
The single-page application only has one HTML, routing just makes React to render selected parts defined by URl
- A router package has :
    1. parse URL/Path
    2. read configuration so that React can know which part to render when read in this URL/Path
    3. it will then render the part depending on which part user is visited

- in App.js, we need to wrap page with `<BrowserRouter>` block so it can uses Reuter n our entire packages
- load `Route` object from `react-router-dom`
- `<Route path = "/" exact render={()=><h1>Home</h1>}/>`: if path is `"/"`, then render the following code`<h1>Home</h1>`
add `exact` means only path is exactly equals to `/`, it will render, without `exact` says any part start with `/` will be rendered
this is `render` is usually good for short JSX
- `<Route path="/" exact component={ManifestData}>`: with this `componenent` method, we could pass in a component to render

### Use Link to switch pages
Use previous set up with `href`, every time it routes, browser will have to re-load. After change to link, you won't see refresh cursor on browser when switching pages. React just render the parts that needed to be renders without loading a new page, so the state remains the same
```
<!-- Before: -->
import {Route} from 'react-router-dom'
<li><a href="/">Manifest Page</a></li>

<!-- Now -->
import {Route, Link} from 'react-router-dom'
<li><Link to='/'>Manifest Page</Link></li>

<!-- also we have options like -->
<li><Link to={
    {
        pathname:'/manifest',
        hash:'#submit',
        search:'?quick-submit=true'
    }
}>Manifest Page</Link></li>
```
Link will auto-create `href` anchor. `search`: query in URL e.g., `www.google.com/search?q=123`, `hash`: # hashtag in URL


### React.Component
no need to import any lib, just wrap it around block to represent as a big `<div>` around