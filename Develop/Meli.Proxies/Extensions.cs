using System;
using Newtonsoft.Json;

namespace Meli.Proxies;
public static class Extensions
{
    /// <summary>
    /// Deserialize the HTTP content asynchronous.
    /// </summary>
    /// <typeparam name="T">The type of content.</typeparam>
    /// <param name="content">The content.</param>
    /// <returns>The task.</returns>
    public static async Task<T> DeserializeHttpContentAsync<T>(this HttpContent content)
    {
        if (content == null) throw new ArgumentNullException("The content cannot be null and void",nameof(content));
        var stream = await content.ReadAsStreamAsync().ConfigureAwait(false);
        if (stream == null || !stream.CanRead)
        {
            return default;
        }

        using var sr = new StreamReader(stream);
        using var jtr = new JsonTextReader(sr);

        var js = new JsonSerializer { FloatParseHandling = FloatParseHandling.Decimal};
        return js.Deserialize<T>(jtr)!;
    }
}

