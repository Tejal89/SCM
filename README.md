# SCM
A sample .Net Core application in Supply Chain management domain with  TDD approach.
NOTE: Use Visual Studio 2019 to run the application
1. In this application, I have tried to resolve problem statement of generating packing slip as per different criteria.
2. .Net Core 3.1 is used (Visual Studio 2019)
3. Decision of what action to take is based on product category. A product can be under multiple categories hence we can apply various combinations of criteria and extend the application as needed. 
4. Repository pattern is used. I have tried to write clean code by separating layers into projects.
5. HTML Template is used for packing slip.
6. Bootstrap is used for a little UI enhancement.
7. Instructions related to Email. You need to change smtp settings(user and password), if you want Email functionality to work.
	Gmail by default blocks doesn't allow this. To allow it, you will have to allow less secure apps from gmail using the link
	https://myaccount.google.com/lesssecureapps?pli=1
8. A try to include NuGet package has been made by using package MailKit for email sending
