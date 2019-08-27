# SimpleLoggingClient

## Issue:
Logging tools primarly log to console, DB, or a file. The issue is adding overhead to the application itself. When an application continually logs to a DB or file, there is a decent amount of overhead. The overhead is significant when parcing strings for log into a relational database. 
## Solution:
SimpleLoggingClient logs items to the console based on a boolean value that is populated during development. The LogLevel determines if a message should be sent to a queue. Currently, we are support RabbitMQ. By sending messages to the queue, the overhead is removed and a highly availble logging service can be implemented to parse, save, and analyze as needed. 
## Future:
Additional MQ support will be added. 
Listener and Logging Service will be built in Docker containers. 
