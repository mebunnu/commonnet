namespace CommonNet.Azure.KeyVault;

/// <summary>
/// keyvault class
/// </summary>
public class KeyVault
{
    /// <summary>
    /// returns the secret using keyvault name and key.
    /// </summary>
    /// <param name="keyVaultName">name of the vault</param>
    /// <param name="key">key</param>
    /// <returns>secret</returns>
    public async Task<string> GetSecret(string keyVaultName, string key)
    {
        SecretClient secretClient = new(vaultUri: new Uri($"https://{keyVaultName}.vault.azure.net/"), credential: new DefaultAzureCredential());

        KeyVaultSecret secret = await secretClient.GetSecretAsync("ConnectionString");

        return secret.Value;
    }
}
