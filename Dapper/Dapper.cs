namespace CommonNet.Dapper;

/// <summary>
/// dapper class
/// </summary>
public class Dapper
{
    /// <summary>
    /// sets sql connection string
    /// </summary>
    /// <param name="connectionString">connection string of sql</param>
    public void SetConnectionString(string connectionString)
    {
        Keys.SQLCONNECTIONSTRING = connectionString;
    }

    /// <summary>
    /// returns list of data
    /// </summary>
    /// <typeparam name="T">any class type</typeparam>
    /// <param name="commandDefinition">command definition that contains cancellation token, parameters and sql statement etc</param>
    /// <returns>List of T</returns>
    public async Task<IEnumerable<T>> GetList<T>(CommandDefinition commandDefinition)
    {
        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            using SqlConnection connection = new SqlConnection(Keys.SQLCONNECTIONSTRING);
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

    /// <summary>
    /// returns an item
    /// </summary>
    /// <typeparam name="T">any class type</typeparam>
    /// <param name="commandDefinition">command definition that contains cancellation token, parameters and sql statement etc</param>
    /// <returns>item</returns>
    public async Task<T> GetItem<T>(CommandDefinition commandDefinition)
    {
        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            using SqlConnection connection = new SqlConnection(Keys.SQLCONNECTIONSTRING);
            T data = await connection.QuerySingleOrDefaultAsync<T>(commandDefinition).ConfigureAwait(false);
            scope.Complete();
            return data;
        }
        catch
        {
            scope.Dispose();
            throw;
        }
    }

    /// <summary>
    /// used to add or update data
    /// </summary>
    /// <param name="commandDefinition">command definition that contains cancellation token, parameters and sql statement etc</param>
    /// <returns>returns 1 if successfully executed. returns 0 if failed</returns>
    public async ValueTask<int> ExecuteAsync(CommandDefinition commandDefinition)
    {
        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            using SqlConnection connection = new SqlConnection(Keys.SQLCONNECTIONSTRING);
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
