# create migrations
dotnet-ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
dotnet-ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb

# update database
dotnet-ef database update -c PersistedGrantDbContext
dotnet-ef database update -c ConfigurationDbContext


# clear all table definitions
dotnet-ef database update 0 -c PersistedGrantDbContext
dotnet-ef database update 0 -c ConfigurationDbContext

# remove migrations
dotnet-ef migrations remove -c PersistedGrantDbContext
dotnet-ef migrations remove -c ConfigurationDbContext



