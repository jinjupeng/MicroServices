


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

[EasyNetQ](https://www.cnblogs.com/shanfeng1000/p/13035758.html)
[MongoDB](https://medium.com/@marekzyla95/mongo-repository-pattern-700986454a0e)
[cqrs-validation-mediatr-fluentvalidation](https://github.com/CodeMazeBlog/cqrs-validation-mediatr-fluentvalidation)
[eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers)
[cqrs-in-aspnet-core-3-1](https://codewithmukesh.com/blog/cqrs-in-aspnet-core-3-1/)