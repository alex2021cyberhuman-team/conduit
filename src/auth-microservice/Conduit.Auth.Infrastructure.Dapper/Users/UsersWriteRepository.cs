﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Conduit.Auth.Domain.Users;
using Conduit.Auth.Domain.Users.Repositories;
using Conduit.Auth.Infrastructure.Dapper.Connection;
using Conduit.Auth.Infrastructure.Dapper.Extensions;
using Conduit.Auth.Infrastructure.Dapper.Users.Mappings;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace Conduit.Auth.Infrastructure.Dapper.Users
{
    public class UsersWriteRepository : IUsersWriteRepository
    {
        private readonly IApplicationConnectionProvider _provider;
        private readonly Compiler _compiler;

        public UsersWriteRepository(IApplicationConnectionProvider provider,
            Compiler compiler)
        {
            _provider = provider;
            _compiler = compiler;
        }

        public async Task<User> CreateAsync(User user,
            CancellationToken cancellationToken = default)
        {
            var connection = await _provider.CreateConnectionAsync();
            var insertedUser = await connection.Get(_compiler)
                .Query(UsersColumns.TableName)
                .AsInsert(user.AsColumns(), true)
                .InsertGetIdAsync<User>(user.AsColumns(),
                    cancellationToken: cancellationToken);
            return insertedUser;
        }

        public async Task<User> UpdateAsync(User user,
            CancellationToken cancellationToken = default)
        {
            var connection = await _provider.CreateConnectionAsync();
            var updatedRows = await connection.Get(_compiler)
                .Query(UsersColumns.TableName)
                .Where(UsersColumns.Id, user.Id)
                .UpdateAsync(user.AsColumns(),
                    cancellationToken: cancellationToken);
            return updatedRows switch
            {
                0 => throw new InvalidOperationException(
                    "No one row has been updated."),
                > 1 => throw new InvalidOperationException(
                    "Several rows have been updated."),
                _ => user
            };
        }
    }
}