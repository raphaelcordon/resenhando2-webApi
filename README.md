# RESENHANDO<br> A web app for reviews of musical artists, albums and music

## This is version 2 (Resenhando 2.0)
### http://www.resenhando.co/
> <p>Rewriting the code also means implementing new technologies and best practices that I have learned over the years.</p>
> When this README was written (10/20/2024), the online version was still 1.0. <br />
> The replacement will only happen when I have a complete MVP in dev mode.

## Technical Project
 > This repository exclusively covers the Web API structure 

### BACKEND: *(in this repository)*
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
   
#### Hosting:
  * Azure Web APP
  * CI/CD:
    * GitHub Actions
 

### FRONTEND: [Web Page repository](https://github.com/raphaelcordon/resenhando2-webPage)
    - React-Vite SPA (Single Page Application);
    - Visit the link above to check the details.

### DATABASE: *to be implemented in production*
    - Microsoft SQL Database running in a VM and hosted in Digital Ocean;
    - repository to be created in GitHub.
    

## What changed between V1 and V2?

### Stacks
|Version|BE|FE|DB| Hosting |
| :---: | --- | --- | --- | --- |
| V1 | Flask (Python) | HTML | Postgres | Heroku |
| V2 | .Net (C#) | React.JS | MS SQL | Azure |

### Architecture
|Version|Architecture|
| :---: | --- |
| V1 | Monolith | 
| V2 | Micro-Services | 

### Database
|Version|DB|
| :---: | --- |
| V1 | Heroku Service | 
| V2 | Dedicated Database | 


## List of repositories
|Version|Name| Link|
| :---: | --- | ---|
| V1 | Resenhando V1 | [Resenhando V1](https://github.com/raphaelcordon/Resenhando) |
| V2 | Project | [Project](https://github.com/users/raphaelcordon/projects/4) |
| V2 | WebAPI | [Web API](https://github.com/raphaelcordon/resenhando2-webApi) |
| V2 | Web Page | [Web Page](https://github.com/raphaelcordon/resenhando2-webPage) |
| V2 | DB | TBD |
