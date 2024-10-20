## Introduction
Resenhando is a website for reviews of musical artists, albums and music. <br/>
This is also part of my software development portfolio. <br/>
Rewriting the code also means implementing new technologies and best practices that I have learned over the years.

## This is version 2 (Resenhando 2.0)
### http://www.resenhando.co/
> When this README was written (10/20/2024), the online version was still 1.0. <br/>
> The replacement will only happen when I have a complete MVP in dev mode.

## Technical Project
 > This repository exclusively covers the Web API structure 

### BACKEND: (in this repository)
  * Overall
    * .Net Core 8;
    * Entity Framework;
    * SQL Server database (Tables generated and maintained by EF Migrations).
    * Applied software modeling concepts of DDD.
  * Authentication and Access:
    * Asp.Net Identity
    * JWT Bearer token for authentication.
  * Third-party APIs
    * Spotify API
    * YouTube API (to be implemented)
  * Unit Tests:
    * XUnit (to be implemented)
 

### FRONTEND: [Web Page repository](https://github.com/raphaelcordon/resenhando2-webPage)
    - React-Vite;
    - Style: daisy-UI, Tailwind CSS, Fontawesome;
- Docker:
    - Docker-compose integrating containers for application and database;
    - In production, also integrates the implementation of NGINX on a virtual machine for reverse proxy management and caching.
