- Javascripto is based on Java and C
- Type diagram
    - Number
    - String
    - Boolean
    - Symbol
    - Object (similar to dic in Python)
        - Function
        - Array
        - Date
        - RegExp
    - null
    - undefined
- print item
```
console.log(3/2);
```
- Operators: `+, -, *, /, %`
- Control structure `&&, ||`
```
var name = 'kittens';
if (name == 'puppies'):{
    name += 'woof';
}
else if (name == 'kittens'){
    name += 'meow';
}
else{
    name += '!';
}
```

```
for (var i =0; i<5; i++){

}

for (let property in object){

}

do{
    input = get_input();
}while (inputIsNotValid(Input));
```
- switch
```
switch (action){
    case 'draw':
        drawIt();
        break;
    case 'cat':
        catIt();
        break;
    default:
        donothing();
}
```

- Object
```
// create empty object
var obj = new Object();
// OR
var obj = {};
```
- Array 
```
// empty array
var a = new Array();
a[0] = 'dog';
a.length;
```

- Function
```
function add(){
    var sum = 0;
    for (var i=0, j=arguments.length; i<j; i++){
        sum += arguments[i];
    }
    return sum;
}

- === vs ==
    - ===: equal values and equal type
    - ==: equal to 
```
## Array
- `filter()`: The filter() method creates a new array with all elements that pass the test implemented by the provided function. e.g., `result = words.filter(word => word.length > 6);`, new result is an array of word in words array s.t. word length is greater than 6. 
- `array.push(item)`: add new item into array
- `array1.concat(array2)`: concatenate two arrays


## Object
- `object.hasOwnProperty("property name")`: return if the object contains this property
- `object.property`: return false if this property is empty

## Date
- dateformat: yy, MM (month), dd (date), hh (hour), mm(minutes), ss(seconds), a (indicate am or pm)