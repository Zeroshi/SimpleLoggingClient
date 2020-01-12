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
![Simple Logging Client Design](https://cgufeg.ch.files.1drv.com/y4mO_Yq_sKBabT4egaSlQdg5wMG1RBGXtv1UUwy-lW-oiVLg47dKLHRh5ippTsSFt1SOT4QZ6lQ0xNnXuvdzZ5byJ6DZV2Ga3oIO0GXBs21hAE2-CKPz8V_UBpc_k2TwSJDwNcXN9qUQtW0WeP25IvtWW5OIlOWhSQBq-xoNCXCU3sp400XwEgDPKifiEUJmkCs8oP2IZuNba9pwKKIDKPMrQ?width=1353&height=696&cropmode=none)

Fig. 1. Simple Logging Client Design. An application references
SimpleLoggingClient and sets up the queue information needed
(Username/Password). Logging is handled as a single object as to be used with
dependency injection (DI). The log/error is sent to the queue. A separate
application—that is currently in development—will gather messages from the
queue. The messages logic path will be determined by type. The messages will be
broken into transactions and stored into the proper database/tables.

