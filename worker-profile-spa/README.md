# Worker Profiles SPA

This sample app demonstrates how to call an API using the access token retrieved during authentication. It uses [auth0-spa-js](https://github.com/auth0/auth0-spa-js).

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 8.1.2.

## Configuration

The sample needs to be configured with your Auth0 domain and client ID in order to work. In the root of the sample, copy `auth_config.json.example` and rename it to `auth_config.json`. Open the file and replace the values with those from your Auth0 tenant:

```json
{
  "domain": "<YOUR AUTH0 DOMAIN>",
  "clientId": "<YOUR AUTH0 CLIENT ID>",
  "audience": "<YOUR AUTH0 API AUDIENCE IDENTIFIER>"
}
```

For the scope of this project, you should leave the preset configuration.

And in `spa_config.json`, set the API url (ex. {`https://<API HOST>/api/v1`)

```json
{
  "apiBaseUrl": "<WEB API URL>"
}
```

## Run

You will need to have installed:

- [Node Js](https://nodejs.org/en/download/)

In the project's root directory, run `npm install` in order to download the required packages for the app to work.

To build and run a production bundle and serve it, run `npm run prod`. The application will run on `http://localhost:4200`.

## About access levels

### 'Admin' users

Access to all the pages

### 'Standard' users

Access to `Create` and `Profiles` pages

You can also sign up for a new `Standard` user account (using email , not the SSO option of Auth0).
