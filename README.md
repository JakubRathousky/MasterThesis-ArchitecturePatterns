ResSys project is about implementation example of microservice ecosystem
where each service is implemented by one of the modern popular architectures
such as Hexagonal, Clean and Onion.

Projects simulates Reservation system of films and books.
Contains service for evidance of new supply arrival (Logistic),
catalog services which store data about each product (book and film).
Those books and films are available for reservation for a given 
interval. Last service Statistics serves for statistical outputs.

Solution is made od three user interfaces: Reservation and Statistics,
both in React adapting MVVM, and the last one in Statistic
designed by MVC, implemented by .Net Razor.

![Alt text](images/Deployment_diagram.png?raw=true "Deployment diagram")

## Projects

| project | path  | techstack | description |
|----------|---|---|-------|
| Statistic Gateway | src/APIGateways/ResSys.AdministrationGateway   |  C#, Ocelot | gateway for redirecting of client-to-host requests | 
| Logistic Gateway | src/APIGateways/ResSys.LogisticGateway   |  C#, Ocelot, Razor | gateway for redirecting of client-to-host requests | 
| Reservation Gateway | src/APIGateways/ResSys.ReservationGateway   |  C#, Ocelot | gateway for redirecting of client-to-host requests | 
| Statistics | src/services/ResSys.AdminStatistic   |  C#, MassTransit, RabbitMQ, AutoFac | statistic outputs, implementation Hexagonal | 
| Author Catalog | src/APIGateways/ResSys.AuthorCatalog   |  C#, MassTransit, RabbitMQ | simulation of external service | 
| Book Catalog | src/APIGateways/ResSys.BookCatalog   |  C#, MassTransit, RabbitMQ | handles collection of book | 
| Film Catalog | src/APIGateways/ResSys.FilmCatalog   |  C#, MassTransit, RabbitMQ | handles collection of film | 
| Logistic Catalog | src/APIGateways/ResSys.Logistic   |  C#, MassTransit, RabbitMQ | handles processing of new supply, implemantation Clean | 
| Reservation system | src/APIGateways/ResSys.ReservationSystem   |  C#, MassTransit, RabbitMQ | handles reservation requests, implementation Onion | 
| Common | src/APIGateways/ResSys.Common   |  C#, MassTransit, RabbitMQ, AutoFac | contains helper classes shared via all projects | 
| HealthChecker | src/Web/HealthChecker   |  C# | monitors status of services | 
| LogisticWebAppSpa | src/Web/ResSys.LogisticWebAppSpa   |  C#, React | handles supplies |
| ReservationWebAppSpa | src/Web/ResSys.ReservationWebAppSpa   |  C#, React | handles reservation | 

## Installation
### Prerequisites:
- install `docker`
- install `docker-compose`

### Workflow
- go to `src` folder
- run `docker-compose build` and wait for all images to download (cca 10 minutes)
- run `docker-compose up`
  - **note: HealthChecker will keep throwing errors for a while before the services get live**
- navigate to `localhost:5027/healthchecks-ui` in your browser to check out the healthcheck 

## Tests
- run `dotnet test` in `test` folders of project you want to test

## Development
- if you change the source code of a project `<XYZ>`, you can restart respective service by typing the following
  - `docker-compose build <XYZ>`
  - `docker-compose up -d <XYZ>`

## Frontends
- HealthChecker : http://localhost:5027/healthchecks-ui#/healthchecks
- Logistic : http://localhost:5051/
- ReservationSystem : http://localhost:5053/
- Statistics : http://localhost:5009/

## Services
Table below shows distribution of assigned Ip address and ports. This setting is set in `docker-compose.yml` file.

| service | image  | ip   | port |
|----------|---|---|---|
| rabbitmq    | rabbitmq:3-management   |  172.28.1.1  | 4369, 5671, 5672, 25672, 15671, 15672   | 
| admin-statistic  | none   | 172.28.1.2  | 5009  |
| author-catalog         | none   | 172.28.1.3   | 5011  |
| book-statistic         | none   | 172.28.1.4   | 5005  |
| film-catalog         | none   | 172.28.1.5   | 5003  |
| logistic         | none   | 172.28.1.6   | 5007  |
| reservation         | none   | 172.28.1.7   | 5013  |
| health-checker         | none   | 172.28.1.8   | 5027  |
| db         | mcr.microsoft.com/mssql/server   | 172.28.1.13   | 1433  |
| mongo         | mongo   | 172.28.1.10   | 27017  |
| reservation-gateway         | none   | 172.28.1.11   | 5021  |
| logistic-gateway         | none   | 172.28.1.12   | 5023  |
| reservation-gateway         | none   | 172.28.1.11   | 5021  |
| logistic-spa         | none   | 172.28.1.51   | 5051  |
| reservation-spa         | none   | 172.28.1.53   | 5053  |

### Description
#### Rabbitmq
RabbitMQ is Message Broker used for message communication.
Other services communicates via this channel.

#### Admin-statistic
Statistic service stores data about books, films and reservations in
relation database. Example of usage various different technologies
in each service.
| endpoint | method  | desc  |
|----------|---|---|
| /stat/monthDistribution    | GET   |   returns number of created reservation in each month  |

#### Author-catalog
Author service simulates behavior of external service which is blackbox to us.
Our services have to communicate with it by synchronous communication.
| endpoint | method  | desc  |
|----------|---|---|
| /authors                  | GET    | returns list of authors
| /authors/{authorRegNum} | GET    | returns author with given registration number
| /authors                  | POST   | saves new author
| /authors/{id}           | PUT    | updates author with given Id
| /authors/{id}           | DELETE | deleted author with given Id
#### Book-statistic
Book catalog stores data about all books and their amounts.
Listens messages about new supplies and notifies about creation
of new books.
| endpoint | method  | desc  |
|----------|---|---|
| /books        | GET    | returns all books
| /books/{id} | GET    | returns book with given Id
| /books/{id} | PUT    | updates book with given Id

#### Film-catalog
Film catalog stores data about all films and their amounts.
Listens messages about new supplies and notifies about creation
of new films.
| endpoint | method  | desc  |
|----------|---|---|
| /films        | GET    | returns all films
| /films/{id} | GET    | returns film with given Id
| /films/{id} | PUT    | updates film with given Id
#### Logistic
Reservation server handles logic for creating new supplies and fires events
vis RabbitMQ to other services.
| endpoint | method  | desc  |
|----------|---|---|
| /supply    | GET   |  returns list of previous supplies   |
| /supply    | POST   |  saves new supply   | 
| /sychronize    | GET   |  activates synchronization process of catalogs  |

#### Reservation
Reservation server handles logic for reservation creation and notifies other
services about new reservation creation.
| endpoint | method  | desc  |
|----------|---|---|
| /reservation    | GET   |  returns list of reservations   |
| /reservation/{id}    | GET   |  returns reservation with given Id   | 
| /reservation/book/{id}/{date}    | GET   |  returns number of active reservations of given book in specified date  |
| /reservation/film/{id}/{date}    | GET   |  returns number of active reservations of given film in specified date  | 
| /reservation    | POST   |  saves new reservation   | 
| /reservation/{id}    | DELETE   |  deactivates reservation with given Id   | 
#### Health-checker
Monitors life of services, overview available on adress:
http://localhost:PORT/healthchecks-ui#/healthchecks 


#### Mssql
MSSQL database for storing data used by Statistic service.

#### Mongo
Document based database used for simple collection storing.
Specifically used by Film-catalog, Book-catalog, reservation and logistic services.
#### Reservation-gateway
Gateway which is used to redirect requests from reservation frontend to services.
#### Logistic-gateway
Gateway which is used to redirect requests from logistic frontend to services.

#### Logistic-spa
Logistic UI which shows list of created supply requests and a form for creating
a new one.
#### Reservation-spa
Reservation UI is used to show available store items (films and books).
User can perform reservation by filling in number to store items (represents
demanding number of items to reserve) and after send request to server.
### Volumes
Serves for MongoDB server data storage
- mongodbdata:
Serves for MSSQL server data storage
- mssqldbdata
Used by RabbitMQ for data persistence
- rabbitmqdata

## Manual
Application does not contain any created intern data, such as films, books and reservations.
The only data, that are seeded, are authors with Registration number from 1 to 9.

So the first step is to start the application and navigate to HealthChecker web.
This web shows if all services started and are running. If so, we are ready to
begin our workflow.

![HealthChecker web app](images/HealthChecker.png?raw=true "HealthChecker web app")

The process begins in Logistic web app. Front page displays all historical requests.
If any row contain red colored date, it means that there was a problem
in time of processing the creating request. By pressing "synchronize" you can start
new attempt.

To create new supply request, we navigate through upper menu "New supply" 
to form, which handles supply logic. To add book or film press the plus button, 
a new input row will appear.

![New supply input row](images/NewSupply.png?raw=true "Form for supply request creation")

Fill the row with your data, please fill all of the input fields, this form
does not handle incorrectly submitted data neither notifies user. Fill
as many rows as you want and click "Submit" button.

![Filled supply form](images/NewSupplyFilled.png?raw=true "Filled supply form")

If all data are correctly filled, you will be redirected to the front page, which should contain your request.

![Supply overview with not synchronized supply](images/LogisticOverviewError.png?raw=true "Supply overview with not synchronized supply")

For this demonstration I have skipped creation of film, so the overview
signalizes that there was a problem and not all supplies have been created.

After clicking "Synchronize" button, logistic service will try to repair
all supplies with problems. 
![Supply overview with synchronized supply](images/LogisticOverviewSuccess.png?raw=true "Supply overview with synchronized supply")

As we can see, synchronization was successfully processed. With supplies
in store, we can offer them for reservation. To perform this act,
we go to Reservation web app. Reservation app contains only the front page,
this front page shows available supplies to reserve. 

![Reservation app with supply overview](images/Reservation.png?raw=true "Supply overview in reservation app")

We can choose any of the offered supplies, just specify amount of
films and books you want to borrow to specified date. By
clicking on "Reserve" button we submit the form and reservation
should be created. To see list of created reservation
we have to go to Statistics web app.

![Overview of active reservations](images/ReservationOverview.png?raw=true "Overview of active reservations")

"Deactivate" buttons triggers deactivation of reservation.
After this action, reservation will deactivate and reserved
supplies will be available again. After deactivation
refresh page and reservation should not be visible anymore.

"Month distribution" page shows basic summarization of
reservations created in each month. Fifth month shows number 1
as we have created only one reservation.

![Month distribution of created reservations](images/MonthDistribution.png?raw=true "Month distribution of created reservations")