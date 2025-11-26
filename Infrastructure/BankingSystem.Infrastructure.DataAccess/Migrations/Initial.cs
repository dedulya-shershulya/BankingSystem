using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace BankingSystem.Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
        create type transaction_type as enum
        (
            'deposit',
            'withdraw'
        );    

        create table bank_accounts
        (
            account_id bigint primary key generated always as identity ,
            account_pin bigint not null ,
            balance bigint not null
        );

        create table transactions
        (
            transaction_id bigint primary key generated always as identity,
            account_id bigint references bank_accounts(account_id),
            transaction_type transaction_type not null ,
            amount bigint not null 
        );

        create table admin_password
        (
            password text not null
        );

        insert into admin_password(password)
        values ('admin');
        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        drop table bank_accounts;
        drop table transactions;
        drop table admin_password;

        drop type transaction_type;
        """;
}