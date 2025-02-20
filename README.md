This project is a template with a Clean Architecture, written in the C# programming language, designed to provide the foundational capabilities for developing software. My goal with this project is to implement a comprehensive set of essential features required for a backend project. This project includes the following:

<ul>
<li>Infrastructure for developing RESTful services with documentation support using Swagger.</li>
<li>Support for connecting to four powerful databases: MongoDB, PostgreSQL, MSSQL using the Repository Pattern .</li>
<li>Multilingual error management and message generation system .</li>
<li>FileManager contains AWS-compatible storage and on-disk saved files.</li>
<li>implemention of SMS Service and Mail Service on FarazSMS,SMSIR,Mailzila,Smtp Mail.</li>
<li>Implementation of SMS and Mail services using FarazSMS, SMSIR, Mailzila, and SMTP Mail.</li>
<li>Log database using MongoDB.</li>
<li>Stimulsoft Report Generator</li>
</ul>
The project structure is based on Clean Architecture and follows the hierarchy shown below. <br><br>

<img width="200" src="http://45.149.77.10:9000/test/DotnetClean.png">

<b>How to Run the Project<b>
1) Clone the project.
2) Add build configurations and set the environment variables.
3) Run the project and navigate to the following URL: "http://localhost:{port}/scalar".

<b>To run the project, you can add the SampleConfigs.json file to the project configurations via Program.cs or use SecretStorage.</b>

<b>Request Flow</b>

RESTful requests follow the flow below from the moment they are sent until a response is received.

Note that the FillUserContext filter is responsible for extracting information from the token and populating the UserContext object, which is injected into other layers and classes of the project using Dependency Injection.

This allows access to user information throughout the entire project.

<img src="http://45.149.77.10:9000/test/RequestFlow.png">


# Support

I am available to answer your questions about this project.

- My Telegram ID: mrabiee1996
- My LinkedIn: mrabiee1996
