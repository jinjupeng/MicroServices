https://github.com/CodeMazeBlog/cqrs-validation-mediatr-fluentvalidation

https://github.com/dotnet-architecture/eShopOnContainers

https://codewithmukesh.com/blog/cqrs-in-aspnet-core-3-1/


[Command line]
```
dotnet ef migrations add InitialCreate --context IdentityDbContext --output-dir Infrastructure/Migrations
```
[Package Manager Console]
```
Add-Migration InitialCreate -Context IdentityDbContext -OutputDir Infrastructure/Migrations
```
[Command line]
```
dotnet ef database update
```

[Package Manager Console]
```
Update-Database
```