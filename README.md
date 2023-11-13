# Arival
# 2fa service lunch help

### 1- production mode and real database 

**step 1** : the api are exist in the Arival.Api

**step 2** : please connect database to your Postgres database in appsettings.json --> ArivalConnection

**step 3** : you need have this table in your database with below script 

`CREATE TABLE IF NOT EXISTS public."UserOtp"
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Otp" character varying(255) COLLATE pg_catalog."default",
    "CreatedTime" timestamp without time zone,
    "PhoneNumber" character varying(20) COLLATE pg_catalog."default",
    CONSTRAINT "UserOtp_pkey" PRIMARY KEY ("Id")
)
`

**step 4**: if you execute project Arival.Api it runs on the address : http://localhost:58997

**step 5**: by Postman or others api test tools you can request to the address : http://localhost:58997/api/Otp/OtpLogin to request verification code
sample request body :

`{
    "PhoneNumber" : "09124272246"
}`

sample success response body : 

`{
    "isSuccess": true,
    "errorList": null,
    "result": {
        "code": "606626"
    }
}`

**step 6**: regarding validate verification code which received from step 5 you can call the address http://localhost:58997/api/Otp/OtpVerify 
sample request body : 

`{
    "PhoneNumber" : "09124272246",
    "VerificationCode":"606626"
}`

sample success response body : 

`{
    "isSuccess": true,
    "errorList": null,
    "result": {
        "verificationCode": "606626",
        "isCorrect": true
    }
}`

**step 7** : you can test OtpLogin login multiple time depend **MaximumActiveCodePerPhone** configuration variable in appsettings.json when maximum limit exceed the response like :

`{
    "isSuccess": false,
    "errorList": [
        {
            "errorCode": 2,
            "errorDescription": "The number of active codes exceeds the limit : 2"
        }
    ],
    "result": null
}`

**step 7** : by **CodeExpirationTimestamp** configuration variable in appsettings.json you can change expiration time for verification code

### 2- UnitTests and In-memory database 

the unit tests are exist in Arival.Tests project and UnitTests class

the main tests which tests the flows are : 
CreateOtpAndVerifyShouldWorkSuccess()
CreateOtpMaximumExceedShouldWorkSuccess()
