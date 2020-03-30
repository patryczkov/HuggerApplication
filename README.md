# HuggerApplication
Welcome to our project repository - Hugger. 

This is the culmination of our learning at Codecool. 
We decided to create applications to meet new people, based on Tinder, OkCupid, Badoo and improve them with the functionality we missed.

The API was written in several assumptions:
- code first approach using fluent API and Entity Framework Core,
- The database was originally located on Azure using MS SQL Server, but due to financial constraints it was moved to our domain using PostgreSQL,
- documentation was written using Swagger, so that the person working on the front can quickly see the queries and test them,
- Google API was used to connect to GDrive to store photos,
- thin controller - it is only supposed to send response codes,
- threading - asynchronous methods were used to ensure responsiveness of services,
- JWT - verifies the user's access level to profile editing and logs into the service
- React Native - to make application run smoothly on phone,

https://github.com/patryczkov/HuggerApplication/blob/dev/DatabaseSchema.png?raw=true


