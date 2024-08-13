# Setup Guide

## Running with Docker

### Prerequisites

1. **Install Docker**: Follow the instructions to install Docker from the official documentation: [Install Docker Engine](https://docs.docker.com/engine/install/).

### Steps to Set Up the Environment

1. **Create a Docker Network**:
   ```bash
   docker network create my-network
   ```

2. **Run the PostgreSQL Database**:
   ```bash
   docker run --name postgres -d --network my-network -p 5432:5432 -e POSTGRES_PASSWORD=1 postgres
   ```

3. **Create Database and Tables**:
    - Execute the SQL script located at `Shorturler.Database/database.sql` to set up your database schema.

4. **Verify Configuration**:
    - Ensure the database connection string is set as follows:
      ```
      Host=postgres;Database=postgres;Username=postgres;Password=1
      ```
    - Confirm that the constant **ShortRequestHandler.Host** is correctly defined:
      ```
      http://localhost:8080/short/
      ```

5. **Build the Application**:
   ```bash
   docker build -t shorturler .
   ```

6. **Run the Application**:
   ```bash
   docker run -d -p 8080:8080 -p 8081:8081 --network my-network --name shorturler-container shorturler
   ```

---

## Local Run with Docker

### Prerequisites

1. **Install Docker**: Follow the instructions to install Docker from the official documentation: [Install Docker Engine](https://docs.docker.com/engine/install/).

### Steps to Set Up the Environment

1. **Run the PostgreSQL Database**:
   ```bash
   docker run --name postgres -d -p 5432:5432 -e POSTGRES_PASSWORD=1 postgres
   ```

2. **Create Database and Tables**:
    - Execute the SQL script located at `Shorturler.Database/database.sql` to set up your database schema.

3. **Verify Configuration**:
    - Ensure the database connection string is set as follows:
      ```
      Host=localhost;Database=postgres;Username=postgres;Password=1
      ```
    - Confirm that the constant **ShortRequestHandler.Host** is correctly defined:
      ```
      http://localhost:<your port>/short/
      ```

4. **Run the Application**:
    - Start your application container, ensuring it connects to the database.

---

## Testing the Application

1. **Make a POST Request**:
   Use the following `curl` command to send a POST request to the application:
   ```bash
   curl --location 'http://localhost:8080/short' \
   --header 'Content-Type: application/json' \
   --data '"https://i.giphy.com/j25gvZG8VT5pllIXR2.webp"'
   ```

   You should receive a response containing a short URL, such as:
   ```
   http://localhost:8080/short/2a7ce276-eaff-4647-9980-f8f9f84dab5c
   ```

2. **Open the Short URL in Your Browser**:
    - Copy and paste the short URL into your browser to verify it works.

3. **Congratulations!**:
    - You have successfully set up and tested your application.

