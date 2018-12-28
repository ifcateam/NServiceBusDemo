---
title: Migrating delayed messages from persistence to SQL Server transport
summary: How to migrate the delayed messages stored in NHibernate or SQL persistence to the format used by native delayed delivery in SQL Server transport 3.1.
reviewed: 2017-07-07
component: SqlTransport
related:
- transports/sql
---


## Prerequisites

include: sql-prereq

The database created by this sample is `NsbSamplesNativeTimeoutMigration`.


## Running the project

 1. Start the Endpoint.NHibernate and Endpoint.SqlPersistence projects.
 1. Close them after `The timeout has been requested. Press any key to exit` is displayed.
 1. Open `MigrateFromNHibernate.sql` script in SQL Server Management Studio.
 1. Replace the endpoint name value in the script with `Samples.SqlServer.NativeTimeoutMigration`
 1. Run the script. The outcome should be `(1 row(s) affected)`.
 1. Repeat the steps 3-5 for the `MigrateFromSql.sql` script.
 1. Start the Endpoint.Native project.
 1. Observe the message `Hello from MyHandler` displayed two times meaning that the delayed messages have been successfully picked up by the native handling in the SQL Server transport.


## Migration scripts

NOTE: It may be necessary to modify the presented scripts to introduce batching, especially when there's a large number of timeouts that need to be migrated (in the range of thousands).

The following script can be used to migrate the delayed messages from NHibernate persistence (assuming SQL Server is used as a database):

snippet: MigrateFromNHibernate


The following script can be used to migrate the delayed messages from SQL persistence (assuming SQL Server is used as a database):

snippet: MigrateFromSql
