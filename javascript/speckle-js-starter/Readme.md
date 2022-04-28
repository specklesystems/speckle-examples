# Starter Speckle web viewer project

> This is a basic `html/js/css` webpage that uses our `@speckle/viewer` package and some basic queries through our `graphQL` API.
> If you want to know more about Speckle as a developer platform, check our [Developer Docs](https://speckle.guide/dev)

The app allows you to view the main geometry of a stream and combine it with different design options (which would exist as different `branches` in the stream)

![alt](https://link)

The stream will be organised as follows:

- `main` branch: Will contain the main geometry.
- Any other existing branches should contain the different design options to visualise along side the `main` geometry.

![Stream branches screenshot](https://link)

## Usage

This demo should run directly in your browser. Just double click the `index.html` file on this folder.

## Repo structure

- `index.html`: Main html file
- `app.js`: Main `js` module for the website
- `speckleQueries.js` contains several `graphQL` queries we'll be using.
- `speckleUtils.js` contains the functions to interact with the Speckle API.
- `styles.css`: Styles to used by `index.html`
