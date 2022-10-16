# SqlDependencyAndPrinter
  For our food ordering site, it automatically prints out the order as it arrives.
  
  I made the connection to the database on the site with the scaffolding dbcontext
  
  Install-Package SqlTableDependency -Version 8.5.8
  
  and We need to open this setting for MsSql "alter database [<dbname>] set enable_broker with rollback immediate;"
  for control "SELECT NAME, is_broker_enabled FROM SYS.DATABASES"
  
  
  I made the process for adding, but you can trigger the program according to the update or deletion processes.
  
  Finally.Do not forget to select the default printer on the computer to be used
  
  
  
