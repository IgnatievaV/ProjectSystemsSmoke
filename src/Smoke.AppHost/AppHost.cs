var builder = DistributedApplication.CreateBuilder(args);

// Project references generate the Projects.* classes (check navigation into generated code).
var web = builder.AddProject<Projects.Smoke_Web>("web");
builder.AddProject<Projects.Smoke_Blazor>("blazor").WithReference(web);

builder.Build().Run();
