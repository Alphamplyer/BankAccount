﻿using Application.Users.Login;
using MediatR;
using SharedKernel;
using BankAccount.Extensions;
using BankAccount.Infrastructure;

namespace BankAccount.Endpoints.Users;

internal sealed class Login : IEndpoint
{
    public sealed record Request(string Email, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async (Request request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new LoginUserCommand(request.Email, request.Password);

                Result<string> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Users);
    }
}
