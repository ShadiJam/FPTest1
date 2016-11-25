// 'using' statements
import "babel-polyfill"
import fetch from "isomorphic-fetch"
import React, {Component} from 'react'
import {render} from 'react-dom'
import { Router, Route, Link, browserHistory, hashHistory } from 'react-router'
import * as BLUE from '@blueprintjs/core'

console.log(BLUE);

// Utility methods
// --------------

const log = (...a) => console.log(...a)

const get = (url) =>
    fetch(url, {credentials: 'same-origin'})
    .then(r => r.json())
    .catch(e => log(e))

const post = (url, data) => 
    fetch(url, { 
        method: 'POST',
        credentials: 'same-origin',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(data)
    })
    .catch(e => log(e))
    .then(r => r.json())
// ----------------
/*
class RegisterBox extends Component {
    constructor(props){
        super(props)
        this.state = {}
    }

    _handleSubmit(eventObject) {
        eventObject.preventDefault()
        //forms by default will refresh the page
        var formEl = eventObject.target
        window.form = formEl
        var inputEmail = formEl.theEmail.value, 
            inputPassword = formEl.thePassword.value
        // the .value property on an input reveals what the user has entered for this input 
        var promise = post('/account/register',{
            email: inputEmail,
            password: inputPassword
        })
        promise.then(
            (resp) => console.log(resp),
            (err) => console.log(err)
        )
    }
    render() {
        return (
            <form id="register-form" onSubmit={this._handleSubmit}>
                <hr />
                <p> Or Create an account: </p>
                <div className="pt-input-group">
                    <input name="theEmail" className="pt-round" ref="Email" type="email" placeholder="user@email.com" required/>
                    <input name="thePassword" className="pt-round" ref="Password" type="password" placeholder="Your Password"/>
                </div>
                    <button type="submit">Go</button>
            </form> 
        )
    }
}
class LoginBox extends Component {
    constructor(props){
        super(props)
        this.state = {}
    }

    _handleSubmit(eventObject) {
        eventObject.preventDefault()
        //forms by default will refresh the page
        var formEl = eventObject.target
        window.form = formEl
        var inputEmail = formEl.theEmail.value, 
            inputPassword = formEl.thePassword.value
        // the .value property on an input reveals what the user has entered for this input 
        var promise = post('/account/login',{
            email: inputEmail,
            password: inputPassword
        })
        promise.then(
            (resp) => console.log(resp),
            (err) => console.log(err)
        )
    }
    render() {
        return (
            <form id="login-form" onSubmit={this._handleSubmit}>
                <hr/>
                <p> Login: </p>
                <div className="pt-input-group">
                    <input name="theEmail" className="pt-round" ref="Email" type="email" placeholder="user@email.com" required/>
                    <input name="thePassword" className="pt-round" ref="Password" type="password" placeholder="Your Password"/>
                </div>
                    <button type="submit">Go</button>
            </form> 
        )
    }
}

class Login extends Component {
    constructor(props){
        super(props)
        this.state = {}
    }
    render(){
        var err 
        if(this.state.errors){
            err = <ul className="compose-errors">
                {this.state.errors.map(x => <li>{x}</li>)}
                </ul>
        } 
        return (
            <div className="login-stuff">
                <hr />
                <LoginBox />
                <hr />
                <RegisterBox />
            </div>
        )
    }
}
*/
const Error = () => <div>Page Not Found</div>
const Event = (event) =>
    <a className="event" href={`#/status/${event.id}`}>
        <h1>{event.name}</h1>
    </a>
const Layout = ({children}) =>
    <div>
        <div>
            <div><Nav/></div>
        </div>
        <hr/>
        {children}
    </div>
const Nav = () => 
    <nav className="pt-navbar pt-dark pt-fixed-top">
        <div className="pt-navbar-group pt-align-left">
            <div className="pt-navbar-heading">Event Advance 2016</div>
            <input className="pt-input" placeholder="Search files..." type="text" />
        </div>
        <div className="pt-navbar-group pt-align-right">
            <button className="pt-button pt-minimal pt-icon-home">Home</button>
            <button className="pt-button pt-minimal pt-icon-send-to-graph">Notifications</button>
            <span className="pt-navbar-divider"></span>
            <button className="pt-button pt-minimal pt-icon-user">Events</button>
            <button className="pt-button pt-minimal pt-icon-pin">Tasks</button>
            <button className="pt-button pt-minimal pt-icon-cog">Account</button>
        </div>
    </nav>





class Home extends Component {
    constructor(props){
        super(props)
        this.state = {
            items: []
        }
    }
    componentDidMount(){
        get('api/event').then(events => {
            events = events.reverse()
            this.setState({items: events})
        }).catch(e => log(e))
    }
    render(){
        return <div className="grid grid-3-600">
                    {this.state.items.map(Event)}
                <hr/>
                <a className="createEventLink" href="#/create-event">
                    <button>Create New Event</button>
                </a>
            </div>
    }
} 
class CreateEvent extends Component {
    constructor(props){
        super(props)
        this.state = {}
    }
    _handleSubmit(eventObject){
        eventObject.preventDefault()
        var formE1 = eventObject.target
        window.form = formE1
        var inputName = formE1.theName.value,
            inputLocation = formE1.theLocation.value,
            inputStartDate = formE1.theStart.value,
            inputEndDate = formE1.theEnd.value
        var promise = post('/api/event', {
            name: inputName,
            location: inputLocation,
            startDate: inputStartDate,
            endDate: inputEndDate
        })
        promise.then(
            (resp) => console.log(resp),
            (err) => console.log(err)
        )
    }
    render(){

        return (
            <form className="new-event-form" onSubmit={this._handleSubmit}>

            <div>
                <input ref="theName" ref="Name" type="text" placeHolder="Event Name" required />
                <input ref="theLocation" ref="Location" type="text" placeHolder="Event Location" required />
                <input ref="theStartDate" ref="StartDate" type="dateTime" placeHolder="Event Start Date" required />
                <input ref="theEndDate" ref="EndDate" type="dateTime" placeHolder="Event End Date" required />
            </div>
                <a className="add-event" href={`#/status/${x.id}`}>
                <button type="submit">Add Event</button>
                </a>
        </form>
        )
    }
}    

class EventPage extends Component {
    constructor(props){
        super(props)
        this.state = { id: props.params.id }
    }
    componentDidMount(){
        get('api/event/'+this.state.id).then(x => {
            this.setState({ item: x })
        })
    }
    render() {
        const item = this.state.item
        if(!item)
            return <div/>
        
        return<div className="event-page">
            <h5>{item.name}</h5>
            <hr/>
            <h1>{item.location}</h1>
            <hr/>
            <p>{item.startDate}</p>
            <p>{item.endDate}</p>
            <hr/>
            <a className="create-advance" href="/create-advance">
            <button>Create New Advance</button>
            </a>
        </div>
    }
}


const reactApp = () => 
    render(
    <Layout>
        <Router history={hashHistory}>
            <Route path="/" component={Home}/>
            <Route path="/status/:id" component={EventPage}/>
            <Route path="/create-event" component={CreateEvent}/>
            <Route path="*" component={Error}/>
        </Router>
    </Layout>,
    document.querySelector('.app'))

reactApp()

// Flow types supported (for pseudo type-checking at runtime)
// function sum(a: number, b: number): number {
//     return a+b;
// }
//
// and runtime error checking is built-in
// sum(1, '2');