# JSON Web Token (JWT)
Imagine being at a big music fetival with lot of stages and fun things to do. As soon as you get there, they give you a special wristband. This wristband is like your key to everything at the festival- Security guards at each stage simply look at your wristband to know if you can enter.They don't  need to call the ticket office or check a list.
This is similar to how JSON Web Token (JWT) function in the world of web programming.J

JWTs are like digital wristbands for online services.In the digital world, when you log into a website or app, it needs a way to remember  that you are authenticate (Like having a ticket to the festival);Without JWT, you would have to log in again every time your switch pages or request data.That would be like going to the ticket boot every time you want to enter a new stage at the festival-not practical.

JWT is a URL-safe, compack string for tranferring claims between two parties, made of three dot-sparated parts:
1. Header (token type and encrytption method).
2. Payload (user data and info)
3. Signature (verifies token integrity).

## Where it is used?
JWTs are widely used in web applicaitons for user authenticadtion and information exchange. They're especially popular in Single Page Applications (SPAs) and for implmenting token based authentication in RESTful API


## How it's impolemented?
When a user logsin, the server issures a JWT.
This token is stored by the users browser  and sent back with each request to the server. Like the festival wirstbandm it quickly proves the users identity and access rights , elimninating the need for repeated logins.
