import React, { Component } from 'react';
import { Container, Grid, Divider, Icon, Button } from 'semantic-ui-react'
import LogisticApi from '../repositories/logistic';

export class SupplyOverview extends Component
{
  static displayName = SupplyOverview.name;

  componentDidMount = () =>
  {
    LogisticApi.getSupplies().then(result =>
    {
      console.log(result.data);
      this.setState({ supplies: result.data });
    });
  }

  synchronize = () =>
  {
    LogisticApi.synchronize().then(result =>
    {
      this.setState({ syncCalled: true });
    });
  }

  renderSupplies = () =>
  {
    return (

      this.state?.supplies.map(x =>
      {
        let style = {};
        if (!x.synchronized)
          style = { color: 'red' };

        return (
          <Grid.Row key={x.id} border={1}>
            <Grid.Column width={4}>
              <p style={style}> {x.storageDate.substring(0, 10)}</p>
            </Grid.Column>
            <Grid.Column width={5}>
              <p>Books: {x.books.length}</p>
            </Grid.Column>
            <Grid.Column width={5}>
              <p>Films: {x.films.length}</p>
            </Grid.Column>
            <Grid.Column width={2}>
              <Icon name="angle double down" />
            </Grid.Column>
            <Grid.Column width={16}>
              <Divider />
            </Grid.Column>
          </Grid.Row>
        )
      })
    )
  }



  render()
  {
    let msg = this.state?.syncCalled ? (<Grid.Row key={"notifikace"}>
      <Grid.Column width={13}>
        <h3><strong>Synchronization complated</strong></h3>
      </Grid.Column>
    </Grid.Row>
    ) : "";

    return (
      <Container>
        <Grid>
          {msg}
          <Grid.Row key={"nadpis"}>
            <Grid.Column width={13}>
              <h1><strong>Supply overview:</strong></h1>
            </Grid.Column>
            <Grid.Column width={3}>
              <Button content='Synchronize' primary onClick={() => this.synchronize()} />
            </Grid.Column>
          </Grid.Row>
          <Grid.Row key={"devider"}>
            <Grid.Column width={16}>
              <Divider />
            </Grid.Column>
          </Grid.Row>
          {this.renderSupplies()}
        </Grid>

      </Container>
    );
  }
}
