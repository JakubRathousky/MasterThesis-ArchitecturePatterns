import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Overview } from './components/Overview';
import 'semantic-ui-css/semantic.min.css'
import './custom.css'

export default class App extends Component
{
  static displayName = App.name;

  render()
  {
    return (
      <Layout>
        <Route exact path='/' component={Overview} />
      </Layout>
    );
  }
}
