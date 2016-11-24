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
        
                <p> Or Create an account: </p>
                <div>
                    <input name="theEmail" ref="Email" type="email" placeholder="user@email.com" required/>
                    <input name="thePassword" ref="Password" type="password" placeholder="Your Password"/>
                </div>
                    <button type="submit">Register</button>
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
        
                <p> Login: </p>
                <div>
                    <input name="theEmail" ref="Email" type="email" placeholder="user@email.com" required/>
                    <input name="thePassword" ref="Password" type="password" placeholder="Your Password"/>
                </div>
                    <button type="submit">Login</button>
            </form> 
        )
    }
}
const Layout = ({children}) =>
    <div>
        <div>
            <div><Nav/></div>
            <div><Breadcrumbs/></div>
            <div><Table/></div>
        </div>
        <hr/>
        <div>
        {children}
        </div>
    </div>
const Nav = () => 
    <nav className="pt-navbar pt-dark pt-fixed-top">
        <div className="pt-navbar-group pt-align-left">
            <div className="pt-navbar-heading">EventAdvance</div>
            <input className="pt-input" placeholder="Search files..." type="text" />
        </div>
        <div className="pt-navbar-group pt-align-right">
            <button className="pt-button pt-minimal pt-icon-home">Home</button>
            <button className="pt-button pt-minimal pt-icon-document">Notifications</button>
            <span className="pt-navbar-divider"></span>
            <button className="pt-button pt-minimal pt-icon-user">Events</button>
            <button className="pt-button pt-minimal pt-icon-notifications">Tasks</button>
            <button className="pt-button pt-minimal pt-icon-cog">Advance</button>
        </div>
    </nav>

const Breadcrumbs = () =>
    <ul className="pt-breadcrumbs">
        {["Home", "Tasks", "Advance"].map(x => 
            <li><BLUE.Breadcrumb text={x} /></li>
        )}
    </ul>

const Card = ({title="IM DA BOSS", message="and you ain't", url="#"}) => 
    <div className="pt-card pt-elevation-1 pt-interactive">
        <h5><a href={url}>{title}</a></h5>
        <p>{message}</p>
    </div>

const Table = () => 
    <table className="pt-table pt-interactive pt-bordered">
        <thead>
            <th>Events</th>
            <th>Notifications</th>
            <th>Tasks</th>
        </thead>
        <tbody>
            <tr>
            <td>Blueprint</td>
            <td>CSS framework and UI toolkit</td>
            <td>Sass, TypeScript, React</td>
            </tr>
            <tr>
            <td>TSLint</td>
            <td>Static analysis linter for TypeScript</td>
            <td>TypeScript</td>
            </tr>
            <tr>
            <td>Plottable</td>
            <td>Composable charting library built on top of D3</td>
            <td>SVG, TypeScript, D3</td>
            </tr>
        </tbody>
    </table>

const Home = () => 
    <div>
        <Nav />
        <Breadcrumbs />
        <hr />
        <div className="grid grid-3-600">
            {[
                {title: "TEST TITLE", message: "TEST MESSAGE"},
                {title: "TEST TITLE", message: "TEST MESSAGE"},
                {title: "TEST TITLE", message: "TEST MESSAGE"}
            ].map(x => [<Card {...x} />, " "] )}
        </div>
        <div className="grid">
            <Table />
        </div>
    </div>

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
                <RegisterBox />
                <LoginBox />
            </div>
        )
    }
}

const reactApp = () => 
    render(
    <Layout>
        <Router history={hashHistory}>
            <Route path="/" component={Login}/>
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