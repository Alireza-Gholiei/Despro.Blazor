namespace Despro.Blazor.Base.Services;

public interface IStorageService
{
    Task SetCookieAsync(string name, string value, int seconds);
    Task DeleteCookieAsync(string name);
    Task<string> GetCookieAsync(string name);

    Task SetLocalStorageAsync(string name, string value);
    Task DeleteLocalStorageAsync(string name);
    Task<string> GetLocalStorageAsync(string name);

    Task SetSessionStorageAsync(string name, string value);
    Task DeleteSessionStorageAsync(string name);
    Task<string> GetSessionStorageAsync(string name);
}