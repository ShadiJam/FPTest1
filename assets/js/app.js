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

const Nav = () => 
    <nav className="pt-navbar pt-dark pt-fixed-top">
        <div className="pt-navbar-group pt-align-left">
            <div className="pt-navbar-heading">Blueprint</div>
            <input className="pt-input" placeholder="Search files..." type="text" />
        </div>
        <div className="pt-navbar-group pt-align-right">
            <button className="pt-button pt-minimal pt-icon-home">Home</button>
            <button className="pt-button pt-minimal pt-icon-document">Tasks</button>
            <span className="pt-navbar-divider"></span>
            <button className="pt-button pt-minimal pt-icon-user"></button>
            <button className="pt-button pt-minimal pt-icon-notifications"></button>
            <button className="pt-button pt-minimal pt-icon-cog"></button>
        </div>
    </nav>

const Breadcrumbs = () =>
    <ul className="pt-breadcrumbs">
        {["Home", "About", "Story"].map(x => 
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
            <th>Project</th>
            <th>Description</th>
            <th>Technologies</th>
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

const reactApp = () => 
    render(
    <Router history={hashHistory}>
        <Route path="/" component={Home}/>
    </Router>,
    document.querySelector('.app'))

reactApp()

// Flow types supported (for pseudo type-checking at runtime)
// function sum(a: number, b: number): number {
//     return a+b;
// }
//
// and runtime error checking is built-in
// sum(1, '2');