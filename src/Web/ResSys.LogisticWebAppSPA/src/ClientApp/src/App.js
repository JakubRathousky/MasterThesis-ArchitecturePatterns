import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { SupplyOverview } from './components/SupplyOverview';
import { NewSupply } from './components/NewSupply';

import 'semantic-ui-css/semantic.min.css'
import './custom.css'

export default class App extends Component
{
  static displayName = App.name;

  render()
  {
    return (
      <Layout>
        <Route exact path='/' component={SupplyOverview} />
        <Route path='/newSupply' component={NewSupply} />
      </Layout>
    );
  }
}
