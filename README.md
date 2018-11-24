Comments for reviewers:
Dear Reviewer:

Please refer to the funda1 application as a POC application providing the basic requested functionality of presenting the top 10 makelaars
by their asset count.
The application is a .net core console application. (swagger was considered as one of the options but because of time consideration
it wasn’t done.)
The application was done under a time constrains thus lacks a lot of best practices and demands a lot of refactoring needed in order to
bring it into production level application.
among the missing items I would (partially...) count the followings: 
unit testing
logging
configuration file/s
complete DI (Partially implemented…)
more granular projects separations\layering.
Better exception handling
Etc.
I would also change the architecture to be a more micro services oriented and as part of this
change I would publish reports read from the funda api to a message broker (e.g RabbitMQ, or Azure service bus etc) the message subscribers
would aggregate and analyse the reports etc. make it a more dynamic system.

BTW: because of the rate limit please wait around 1 minute between the different calls.

Thank you!!
