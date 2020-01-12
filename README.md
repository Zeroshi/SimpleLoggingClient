**Simple Logging Client**

**Issue**:

Logging is a daunting task to standardize. Newer developers may attempt to log
the wrong information which results in missing items when troubleshooting.
Logging can also allocate up to 10% of the resources when parsing objects to
place in a tabular database.

**Solution**:

SimpleLoggingClient creates the signature for:

Application:

-   Log

-   Error

External Transaction:

-   Transaction

-   Error

Internal Transactions:

-   Transaction

-   Error

Message Queues

-   Messages

-   Error

Relational Databases

-   Transaction

-   Error

**Design**:

Fig. 1. Simple Logging Client Design. An application references
SimpleLoggingClient and sets up the queue information needed
(Username/Password). Logging is handled as a single object as to be used with
dependency injection (DI). The log/error is sent to the queue. A separate
application—that is currently in development—will gather messages from the
queue. The messages logic path will be determined by type. The messages will be
broken into transactions and stored into the proper database/tables.

