# NZWalksAPI

Welcome to NZWalksAPI! This is a backend API developed by Keith as part of Sameer Saini's <a href="https://www.udemy.com/share/106wyO3@4IAJ1kj6CT2dmoWnhVY22XJe3LmTGBrQge2LB-GT4bkKHasv8q7jgeWMMds3DptZxQ==/">"Build Web API Course"</a>. The API serves data related to popular walking trails and hikes in New Zealand.

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the API](#running-the-api)
- [API Documentation](#api-documentation)


## Introduction
NZWalksAPI is a RESTful API designed to provide information about popular walking trails and hikes in New Zealand. It allows users to retrieve details about various trails, such as trail name, location, difficulty level, duration, and more. This API serves as the backend for the NZWalks web application, which enables users to discover and plan their hiking adventures in New Zealand.

## Features
- Retrieve a list of all available walking trails.
- Get detailed information about a specific trail.
- Filter trails based on location, difficulty, duration, and more.
- Add new trails to the database.

## Technologies
The API is built using the following technologies:
- Node.js: JavaScript runtime environment
- SQL: SQL database for storing trail information
- Swagger/OpenAPI: Documentation and API specification

## Getting Started
To run this API locally on your machine, follow the steps below.

### Prerequisites
- Node.js and npm must be installed on your system.
- MongoDB server should be running locally or accessible via a cloud service.

### Installation
1. Clone this repository to your local machine using:
   ```
   git clone https://github.com/keithgaines/NZWalksAPI.git
   ```

2. Navigate to the project directory:
   ```
   cd NZWalksAPI
   ```

3. Install the required dependencies:
   ```
   npm install
   ```

### Running the API
1. Make sure your MongoDB server is up and running.

2. Start the API server using:
   ```
   npm start
   ```

3. The API should now be accessible at `http://localhost:3000`. You can test the endpoints using API clients like Postman or cURL.

## API Documentation
The API endpoints and their usage are documented using Swagger/OpenAPI. To access the documentation, start the server and visit `http://localhost:3000/api-docs` in your browser. The Swagger UI will provide an interactive interface to explore the API.
