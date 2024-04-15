# Palindrome service

Web-API for working with palindromes.

This server is meant to be accessed by a WPF app found by this link: https://github.com/TypicalMedic/macroscope-practice-client

## Installation guide

### With Docker

1. Download Docker Desktop: https://www.docker.com/products/docker-desktop/
2. Clone this repository with command:
   
        git clone https://github.com/TypicalMedic/macroscope-practice-server.git

3. Go to project root folder:

        cd macroscope-practice-server

4. To configure the application copy .env.sample into .env file:

        cp ServerSide/.env.sample ServerSide/.env
    
    This file contains environment variables that will share their values across the application:
    
    - `REQUEST_LIMIT` variable sets max number of concurrent requests.

5. Launch server with the following command:

        docker compose -f ServerSide/docker-compose.yml up -d

    When containers are up server starts at http://127.0.0.1:5015/swagger. You can open it in your browser.
