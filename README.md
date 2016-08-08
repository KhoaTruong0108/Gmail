# Gmail

Deloyment instruction

Requirements:

    .Net 4.5
    Oauth 2.0
    Entiy Framework 6.0
    MS SQL Server

Setup environment

    Install .Net 4.5
    Install MS SQL Server
    Turn on IIS features

Configure project

    Edit connectionString to SQL server in Config file (\kt.GmailWeb.WebApp\Web.config).

Run server

    Attach Source into IIS 
    Or using IIS express from Visual Studio 2012

Notes:

    Add new features in next version: 
      Store Email in Local Database -> reduce connection time to Gmail
      Syn local emails with Gmail
