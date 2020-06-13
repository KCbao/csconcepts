## AppBar
- `<AppBar position="fixed" color="primary">
- things inside `<AppBar>` are vertically stacked, but obviously you want your header content to be horizontally, so insert `<Toobar>` inside `AppBar`
- In Scrolling Section, it have several options responding to user scroll actions
    1. Wrap `<ElevationScroll>` around `<AppBar>`
    2. `useScrollTrigger`: is an event listener, `disableHysterisis`: whether or not use will see a delay when scrolling, `thereshold: 0`: controls how far user has to scroll before it triggers this event listener, `:0` means as soon as user starts to scroll, it will trigger
    3. `React.cloneElement(children, {elevation: trigger ? 4:0,}),`: it will clone the component that you wrap <ElevationScroll> with, i.e., in our case, the `<AppBar>` block (also indicate as children here), and replace with a new component with elevation depending on whether or not the trigger has been set, if event listener has set the trigger, it will add a floating state of 4, otherwise, it will remain flat, i.e., an elevation of 0. 

## Styling
- Before ppl use  JSS (it is a JS to CSS compiler)
- similar to JSS, material-ui can put styling as JS in your code
### Theming:
    1. to customize the theme, first need to use `ThemeProvider`
    2. wrap the component you want the theme to apply to with `ThemeProvider`
    3. go to material-ui web, under /Customization/Theming/Overview/API/createMuiTheme
    ```
    const theme = createMuiTheme({
    <!-- what we pass in  -->
    })
    ```

    Once create the theme with createMuiTheme, it creates an instance of the default MuiTheme, and overwrite the attributes in default MuiTheme with what we pass in, and return the instance to the constant theme
    4. pass theme to `ThemeProvider` by `<ThemeProvider theme={Theme}`
- default Theme, all params are stated under material-ui web /Customization/Theme/Default Theme, within Default Theme
    1. palette object is for colors
    2. in `Theme.js` you created, you can add color by `name: "color number"`
    ```
    const theme = createMuiTheme({
        common:{
            arcBlue: "#0B72B9"
        }
    })
    ```
    3. OR set color variable and refer to it by "`${}`", not it's back tic not quote sign
    ```
    const arcBlue = "#0B72B9"
    const theme = createMuiTheme({
        common:{
            blue: `${arcBlue}`
        }
    })
    ```
### Typography
manage the styling for text in the same way as palette manges color system
- Default theme: 
"rem": responsive font size depending on `fontSize` you set under `typography` Object
- Change typography
    1. import {Typography} from "@material-ui/core";
    2. wrap `<Typography>` block around the text you want to set
    3. set variant by `<Typography variant="h1" color="white">` 

### Inline Styling
That is, write styling in the same JS script

1. `import {makeStyles} from "@material-ui/core/styles";`
2. create CSS style in Header.js, below are similar to CSS style except it uses camel cases, and it have to use "" to wrap up values
this `root` is not convention, it's just a name, so we could set it to any name we like
```
const useStyles = makeStyles({
    root:{
        border:0
        <!-- use "" to wrap up values -->
        boxShadow: '0 3px',
    }
})
```
3. in component function `Header`, present a hook "useStyles()" which hook to styles
```
const classes = useStyles(); 
```
4. at compile time, this useStyles JSS will be converted into CSS and we can use in JSX by 
```
return <Button className={classes.root}/>;
```

5. To change the theme style, 
```
const useStyles = makeStyles({
    theme => (
        object name: value
    )
})
```

6. show content after Appbar
```
const useStyles = makeStyles({
    theme => (
        toolbar: {...theme.mixins.toolbar}
    )
})
```
And after `<ElevationScroll>` block in function component
`<div className = {classes.toolbar}>`


### Add AppBar logo
1. import logo `import logo from "../../assets/mda_logo.png"
2. inside `<Toolbar>` block, put `<img src={logo} alt ="mda logo" className={classes.logo} />`, alt: what will show when logo is not present
3. add style
```
const useStyles = makeStyles({
    theme => (
        toolbar: {...theme.mixins.toolbar}, 
        logo: {
            height:3em
            }
    )
})

```

4. add some `marginBottom` to show content below AppBar
Before, our content below AppBar is set to appear by using `theme.mixin.toolbar`, now since we re-set logo height, we need to add logo height to theme.mixin.toolbar for content to be seen, to do that, add `marginBottom`
```
onst useStyles = makeStyles({
    theme => (
        toolbar: {
            ...theme.mixins.toolbar, 
            marginBottom: "2em"
            }, 
        logo: {
            height:3em
            }
    )
})
```

### Navigation Tab overview
This is done by material-ui Tab component
1. import Tab and Tabs
```
import Tab from "@material-ui/core/Tab';
import Tabs from "@material-ui/core/Tabs";
```

2. how tab works, `React.useState(0)` initialize `value`, and display on 
`<Tabs value={value}>`, then when event `handleChange` is trigger, it will pass in event `setValue` and newValue `...a11yProps(0)`
in to `setValue(newValue)` to set new state, after it's done, it causes the re-render and update the change on browser

3. wrap `<Tabs>` with `<Tab label="Home"/>` inside

4. Set styling to tabs
```
<Tabs className = {classes.tabContainer}>

</Tabs>
```
set `marginLeft` to push text as far as from logo image`
```
const useStyles = makeStyles({
    theme => (
        tabContainer: {
            marginLeft:"auto",
        }
    )
})
```

Change Tab font `<Tab className={classes.tab} label="Home" indicatorColor="primary"/>`
set styles `textTransfrom` to disable all uppercase, `fontWeight` to bold, `fontSize`, `minWidth`and `marginWidth` to change spacing between tabs, `indicatorColor`: underline color

```
const useStyles = makeStyles({
    theme => (
        tab: {
            fontFamily:"Raleway",
            textTransform: "none", 
            fontWeight: "700", 
            fontSize: "1rem", 
            minWidth: 10, 
            marginLeft: "25px"
        }
    )
})
```

### Button
1. `import Button from "@material-ui/core"`
2. add button block `<Button className={classes.button} />`
3. add styles
`borderRadius`: make it rounded, and other font options, `disableRipple`: disable Ripple affect 
```
onst useStyles = makeStyles({
    theme => (
        button: {
            borderRadius: "50px",
            marginLeft: "50px",
            marginRight: "25px", 
            fontFamily: "Releway",
            disableRipple,
        }
    )
})

```
4. put button arrow
`<Button> <ButtonArrow height={10} width={10} color={theme.palette.common.blue}/></Button>`

### Navigation 
- in Header.js, `import {Link} from "react-router-dom"
- turn tab to a link object by adding link
```
<Tab className={classes.tab} component={Link} to="/" label="Home" indicatorColor="primary"/>
```
- active refresh fix
`import {useEffect} from 'react';`
- create a function inside header before return
if window lcoation is "/" and state value is not 0, then set state value to 0, so correct tab will be rendered. 
The final `[value]` tells the hook that we are depending on the current value, if the state value hasn't changed, then we don't want to run this code again, to avoid running into infinite loop 
```
useEffect(()=>{
    if (window.location.pathname === "/" && value != 0){
        setValue(0)
    }
}, [value])

```

### Pass in props in Route
`<Route path="/" exact render={()=> <OurStory value={value} setValue={setValue}/>}></Route>`

### Add navigation on Logo
1. turn a logo to a button
```
<Button component={Link} to="/">
<img src={logo} alt="MDA logo">
</Button>

```
2. on external link

```
<Button target="_blank" href="http://google.com">
<img src={logo} alt="MDA logo">
</Button>

```

## Grid
Go to public/index.html, under `<body>` change to `<body style="margin:0>` to delete any default margin
- each grid is either a container or an item
- direction: how grid item is layout， that's our main axis
- alignmentItems: how it display by y-axis. If horizontal is main axis, then vertical is y-axis, and vice-versa
- your item will be distributed accordingly 
- justify: how your grid item start according to direction
- alignItems: how your grid item start according to vertical-axis to your direction-axis
- `<Grid container direction="column" spacing={2}>`:
spacing=2, that is 2*8=16px space

### Grid item container
this item itself is a container
```
<Grid item container justify="flex-end"> 
    <Grid item>

    </Grid>
</Grid>
```

### Add navigation to Grid
`<Grid item component={Link} to="/"/>`: similar to what we see in Button 
## Footer
### CSS of image
```
adornment:{
    width: "25em",
    verticalAlignment: "bottom", 
    [theme.breakpoints.down("md"):{width: "24em"}, 
     theme.breakpoints.down("sm"]:{width: "21em"}
}

```
`verticalAlignment`: let the image to sit at the bottom
`theme.breakpoints.down`: make sure when your device screen getting smaller, the image position will shrink relatively, these breakpoints are for device screen size, say screen size is smaller than md, then image width shrinks from 25em to 24 em

OR we can style directly like below
```
<Grid item style={{marginLeft: "5em"}}>
```

## Add copyright symbol
Find symbol on [Toptal](https://www.toptal.com/designers/htmlarrows/symbols/), use its HTML code, include semi-colon, and paste in JSX.

## Material UI icons
```
import WorkIcon from "@material-ui/icons/work"
```
how to find [material icons](material-ui.com/components/material-icons), type in the icon you need and it will tell you how to import icon from @material-ui/icons

### Use icons and change size
```
<FilterListIcon color="secondary" style={{fontSize: "5em"}}>

```

## List
1. `import {List, ListItem, ListItemText, ListItemAvatar} from "@material-ui/core"`
2. so we have list block, inside we have listItem, inside we could have listItemAvatar (logo), logo could be additionally wrapped by `<Avatar>` (from @material-ui/core) or listItemText. primary text will follow primary css, secondary text will follow secondary css styling
```
<List>
    <ListItem>
        <!-- item 1 -->
        <ListItemAvatar>
            <Avatar>logo </Avatar>
        </ListItemAvatar>

        <ListItemText primary="blabla" secondary ="blablabla"/>
    </ListItem>


</List>


```

## Switches
1. `import {Switch, FormGroup, FormControlLabel} from "@material-ui/core"`
2. define constant state in Function
```
const [twitterChecked, setTwitterChecked] = React.useState(false)
```
3. add `<FormGroup>` block
```
<FormGroup row>
    <FormControlLabel style={{marginRight: "2em"}}
                      control={<Switch checked={twitterChecked} color="primary"
                      onChange={()=> setTwitterChecked(!twitterChecked)}/>} 
                      label="Twitter"
                      labelPlacement="start"/>
<FormGroup>
```
onChange is a function, takes in `()`, trigger function `setTwitterChecked` to change `twitterChecked` state. Since it is a switch, so `twitterChecked` shall only have two states, on and off. `labelPlacement`: to decide the position of label text. `<FormGroup row>` tells form how to stack it's item, row means horizontally

4. how to style label?
open browser, insepct switch section, you will see MuiFormControlLabel-label, (part before - is your override class., part after -
is your subclass, see below. Through Inspect Elements and Styles windows in browser, we can see where goes wrong in our code

Go to your Theme.js
```
under overrides:{
    MuiFormControlLabel:{
        label:{
            color: "black"

        }
    }
}
```

if your inspect show something like
```
<svg class="MuiSvgIcon-root MuiSelect-icon>
```
then in your Theme.js, you want to change color, you need to write under overrided (since it's for MuiInput)
```
MuiSvgIcon:{
    root:{
        "&.MuiSelect-icon": {fill: "secondary"}
    }
}

```
where "&" means it's the main class


## Radio
Similar to switch, also wrapped by `<FormControlLabel>`,  but now `<FormGroup>` is replaced by `<RadioGroup>`
## Tables
- pass in `columnspan={number}` or `rowspan={}` allow you to have different col/row spacing in a table
1. `import {Table, TableHead, TableBody, TableRow, TableContainer, TableCell, Paper} from "@material-ui/core"
2. 
```
<Table>
    <TableHead />
    <TableBody>
        <TableRow>
            <TableCell> Id <TableCell/>
        <TableRow />
    <TableBody />

</Table>

```

### Table sorting and selecting
```
{stableSort(rows, getComparator(order, orderBy))
                .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                .map((row, index)
```
- `slice`: to slice table by rows per page, and then `map` each row to index

```
<TablePagination
          rowsPerPageOptions={[5, 10, 25]}
          component="div"
          count={rows.length}
          rowsPerPage={rowsPerPage}
          page={page}
          onChangePage={handleChangePage}
          onChangeRowsPerPage={handleChangeRowsPerPage}
        />
```
- `count`: how many total rows
- `rowsPerPage`: state of rowsPerPage

I copied the entire code for table sorting and selecting, save it to "EnhancedBSMDSTable.js". And done the following things
1. change "EnhancedTableHead" to "EnhancedBSMDSTable"
2. change "rows" to "props.rows" as we are gonna pass data in as props
3. in "Manifestdata.js" import "EnhancedBSMDSTable" and build a block in return as `<EnhancedBSMDSTable rows={Rows}>` where 
Rows are our table content initialized in "Manifestdata.js"
4. set all id in `const headcells` and also in `<TableCell>`
5. in `<TableCell key={headCell.id}>`, change `align={numeric....}` to `align="center"`
6. you can hide sort icon and let it show only when you click the header by adding `<TableSortLabel hideSortIcon ...>` in `TableSortLabel`
## Select
use for one or more options
1. `import {Select, MenuItem} from "@material-ui/core"`;
2. add state and set state `const [topicRel, setTopicRel] = React.useState([])` note now `useState` takes in an array, before `use.State(false)` indicate this value state is boolean. 
3. add state options
`const origTextMsgRelOptions= ['equal', 'include'];`

4. below code allows to select multiple options and display in text field. 
`multiple value={}`: allows multiple options being selected, and display the current state in state variable `binaryAttachType`.  `onChange`: once select is clicked, will trigger this event, and event.target.value will be set as new state. `binarAttachTypeOptions` will map option to `MenuItem` object, with key is option itself and display value is option itself of the `option` object.`displayEmpty` and `renderValue` will be the place holder. It tells that if the state variable `binaryAttachType` array has options/content in it, then we don't render place holder, otherwise, if will render the function which leads to a string "select attachment type" on this empty text field. `renderValue` have to take in a function, for our example below, we have an undefined function when condition if true, and false will lead to `()=>"select attachment type"` function. `MenuProps={{styles:{zIndex:1302}}}`: avoid dropdown menu being too long and being cut off by footer
```
<Select labelId="binaryAttachType" id="binaryAttachType"
        MenuProps={{styles:{zIndex:1302}}}
        multiple 
        displayEmpty
        renderValue={binaryAttachType.length>0 undefined : ()=> "select attachment type"}
        value={binaryAttachType}
        onChange={event => setBinaryAttachType(event.target.value)}>
    {binaryAttachTypeOptions}.map(option=> <MenuItem key={option} value={option}> {option} </MenuItem>
</Select>
```

5. if just want single selection, just delete `multiple` in the above code

### Table Styling
- `<TableCell align="center">` to make content center in the table cell
- `<TableCell style={{max.Width: "20em"}}>`: so if the body text is larger than 20em, it will by pushed to a second line


## Dialogs
- `open`: state of this dialog, true for dialog window is popping out, false is not
- `onClick={handleClickOpen}`: handle state for variable `open`

## Card

```
<Card>
    <CardContent>

    </CardContent>

    <CardActions>

    </CardActions>

</Card>

```
- card size can be set in `classes` by `width`, and `height`
## Particles
1. ` npm install react-particles-js`
2. `import Particle from "react-particles-js"` and in you desired `div` block, write
```
<Particles canvasClassName={classes.particles}
    params={{
        particles:{
            color:{
                value:....
            }
        }
    }}
/>
```
note the default `particle` color and `line_linked` are white color, need to change its color in order for it to be visible in our background. Also CSS in Particles are not called `className`, it's called `canvasClassName`.


## TextField
when textfield has something filled, it will trigger the event and make `OrigTextMsg` state change
```
<TextField
onChange={event => setOrigTextMsg(event.target.values)}>

</TextField>
```
## Relative Length
- em: relative to the font-size of the element (2em means 2 times the size of the current font)
- rem: relative to font-size of the root element
- vw: relative to 1% of the width of the viewport


## Sending out messages
There are several options following request axios function:
- `get` option: 
- `then` option: function will run only when completion of success request 
- `catch`option:  if the get request returns with an error, `catch` function will run
- `finally` option: always execute no matter what after the request
```
axios.get(<URL to make get request to>)
    .then(function (response) {
    //handle success

})
    .catch( function (error) {
    //handle error

    })
    .finally( function (){
    // always exectute

    })
```
In my code, I clicked a button to send request. Then this button has 
```
<Button onClick={onConfirm}>

</Button>
```
and write a `onConfirm` function outside
```
function onConfirm = () =>{
    axios.get (blablbal)
}
```
To test, in browser, right click button and inspect, you would see
```
data:..., status: 200, blablabla
```
that shows your axios actually works

## Circular Progress: a Circular spinner
Idea: set up a progress function outputs a degree value show the circular degrees, so it will indicate progress

1. set up a loading state `const [loading, setLoading] = React.useState(false)`
2. bind it to `onConfirm` function
```
const onConfirm = () =>{
    setLoading(true) //when it starts, set loading state to true
    axios.get(bla)
        .then(res=> setLoading(false)) // when request succeeds, set loading state to false
        .catch(res=> setLoading(false))// some error happens, set loading state to false
}
```
3. bind `onConfirm` to Button
`<Button onClick={onConfirm}/>`
4. In Button, set `CircularProgress`. That means, if loading is true, values in Button show `CircularProgress`, else, show text "Click Button". Loading state value is set to false by default, but when we click the button, it triggers setLoading to set loading state to true, and send get request, and loading state changes depends on request response. 

```
<Button onClick={onConfirm}>
{loading ? {CircularProgress}: "Click Button"}

</Button>
```

## Snackbars
A note show in the window, we want to use it at below scenario: when click the button, it sends HTTP GET request to workflow manager, when response is "Success", snackbar pops out and shows that "Request is success". If response is "Error", snackbar pops out and shows "need to send again"
1. `open`: if open snackbar, `message`: message shown on snackbar, `backgroundColor`: color of snachbar
`const [sentMsg, setSentMsg] = React.useState({open:false, message:"", backgroundColor:""})`
2. `anchorOrigin`: position of snackbar, `onClose`: when click outside of snackbar, snackbar will be closed, `onClose` takes in a function, which will trigger `setSendMsg` function, which keeps all states of `sendMsg` except set state `open` to false. `autoHideDuration`: unit in milliseconds, so here 4000 milisenconds = 4 sec, how long this snackbar window will be closed, here says snackbar window will be auto-closed after 4 secs after it pops out. 

```
<Snackbar
    open={sentMsg.open}
    message={sentMsg.message}
    ContentProps={
        style:{
            backgroundColor: sendMsg.backgroundColor
        },
        anchorOrigin={{
            vertical:"top"
            horizontal:"center"
        }}
    }
    onClose={()=>setSendMsg({..sendMsg, open:false})
    autoHideDuration={4000}}


```
3. We need to set when to open snackbar, we set within`onConfirm` function
```
const onConfirm = () =>{
    setLoading(true) //when it starts, set loading state to true
    axios.get(bla)
        .then(res=> {setLoading(false)
                    setSendMsg({open:true, message:"Request is success", backgoundColor:"green"})
        }) // when request succeeds, set loading state to false
        .catch(res=> {setLoading(false)
        setSendMsg({open:true, message:"need to send again", backgoundColor:"red"}
        })// some error happens, set loading state to false
}
```

## Menus
-  whether or not menu windows is poped out is controlled by `open`, if `anchorEl` is there, `Boolean(anchorEl)` is true, and if `anchorEl` is null, `Boolean(anchorEl)` is false
- `onClose` handles `handleClose` function to change state of `anchorEl`
- `keepMounted`: tell the children element of the menu (our menu items) stay mounted on the DOM, whether or not it is visible on the screen, suitable on search engine optimization that you don't want your info to dissapear
- `id`: set id to this menu, `aria-control`: control web access bility and people who are broswing using their special device that whether there is a menu screen options poped up so they can read off the options. To that effect, we set `aria-control` equal to the menu item that it is managing, or the button is triggering. `id` will link button and menu item it is managing together. 
- `aria-haspopup`="true": control web access bility and people who are broswing using their special device to be aware that this button is triggered to a pop up menu

```
import React from 'react';
import Button from '@material-ui/core/Button';
import Menu from '@material-ui/core/Menu';
import MenuItem from '@material-ui/core/MenuItem';

export default function SimpleMenu() {
  const [anchorEl, setAnchorEl] = React.useState(null);

  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <div>
      <Button aria-controls="simple-menu" aria-haspopup="true" onClick={handleClick}>
        Open Menu
      </Button>
      <Menu
        id="simple-menu"
        anchorEl={anchorEl}
        keepMounted
        open={Boolean(anchorEl)}
        onClose={handleClose}
      >
        <MenuItem onClick={handleClose}>Profile</MenuItem>
        <MenuItem onClick={handleClose}>My account</MenuItem>
        <MenuItem onClick={handleClose}>Logout</MenuItem>
      </Menu>
    </div>
  );
}
```

Selected Menu
- `selectedIndex` keep track of the item we selected last
- add below mapping function
```

 {options.map((option, index) => (
          <MenuItem
            key={option}
            disabled={index === 0}
            selected={index === selectedIndex}
            onClick={(event) => handleMenuItemClick(event, index)}
          >
            {option}
```
Cutomize Menu
```
const StyledMenuItem = withStyles((theme) => ({
  root: {
    '&:focus': {
      backgroundColor: theme.palette.primary.main,
      '& .MuiListItemIcon-root, & .MuiListItemText-primary': {
        color: theme.palette.common.white,
      },
    },
  },
}))(MenuItem);
```
- `focus` means whenever this MenuItem is clicked on
- `& .MuiListItemIcon-root, & .MuiListItemText-primary'`: JSS points to any listitemIcon inside selected item 
- `Mui` is a class specified in material ui for us to overwrite any default material ui styles

Want menu to appear when mouse hover on tab rather than click and add navigation in
- change `onClick` in Tab to `onMouseOver`
- also you want menu to dissapear when mouse is not around menu, to do that, add `MenuListProps={{onMouseLeave:handleClose}}` in Menu, not in Tab, actually you can do similar stuff in other material components, `MenuListProps` is a prop in Menu, and we set it equal to a javascript function with an object inside `{onMouseLeave:handleClose}`: set `onMouseLeave` prop to `handleClose` function so we can properly track the right anchor element
- add `component {Link} to ` to add navigation in
- `onClick={()=>{handleClose(); props.setValue(3)}}`: upon click the menu item, you can do multiple things, first thing is to `handleClose`, second thing is to `setValue` so that correct tab will be highlighted. 
```
 <Tab
        aria-owns={anchorEl ? "support-menu" :undefined}
        aria-haspopup={anchorEl ? "support-menu" :undefined}
        onMouseOver={event=> handleClick(event)}
      >
 <Menu
        id="simple-menu"
        anchorEl={anchorEl}
        keepMounted
        open={Boolean(anchorEl)}
        onClose={handleClose}
        MenuListProps={{onMouseLeave:handleClose}}
      >
    <MenuItem component={Link} to="/contactUs" onClick={()=>{handleClose(); props.setValue(3)}}>contact us</MenuItem>
</Menu>
```
### REM vs PX
if want absolute position, use px for pixel units, if want its location to relative to your device, use rem (responsive units)