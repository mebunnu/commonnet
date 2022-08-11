namespace CommonNet.Dapper;

public interface IDapper
{
    //var commandDefinition = new CommandDefinition(commandText: SQL.UPDATE_QUERY, parameters: new
    //{
    //    id = value.Id,
    //    name = value.Name
    //}, cancellationToken: cancellationToken);
    Task<IEnumerable<T>> GetList<T>(CommandDefinition commandDefinition);

    Task<T> GetItem<T>(CommandDefinition commandDefinition);

    ValueTask<int> ExecuteAsync(CommandDefinition commandDefinition);
}

public class Dapper : IDapper
{
    public static string ConnectionString;

    public async Task<IEnumerable<T>> GetList<T>(CommandDefinition commandDefinition)
    {
        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            IEnumerable<T> data = await connection.QueryAsync<T>(commandDefinition).ConfigureAwait(false);
            scope.Complete();
            return data;
        }
        catch
        {
            scope.Dispose();
            throw;
        }
    }

    public async Task<T> GetItem<T>(CommandDefinition commandDefinition)
    {
        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            T data = await connection.QuerySingleOrDefaultAsync<T>(commandDefinition);
            scope.Complete();
            return data;
        }
        catch
        {
            scope.Dispose();
            throw;
        }
    }

    public async ValueTask<int> ExecuteAsync(CommandDefinition commandDefinition)
    {
        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            int data = await connection.ExecuteAsync(commandDefinition).ConfigureAwait(false);
            scope.Complete();
            return data;
        }
        catch
        {
            scope.Dispose();
            throw;
        }
    }
}
