using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace API.Work.Application.Contract.Localization;

public static class L
{
    private static readonly Dictionary<string, Dictionary<string, string>> _resources = new();
    private static string _currentCulture = "en";

    static L() => LoadLocalizationFiles("Localization");

    public static void SetCulture(string cultureCode) => _currentCulture = cultureCode;

    /// <summary>
    /// Get localized message by key and automatically replace the first placeholder {{...}} with the given value.
    /// </summary>
    public static string Get(string key, params object[] values)
    {
        if (!_resources.TryGetValue(_currentCulture, out var dict) || !dict.TryGetValue(key, out var message))
            return key;

        if (values != null && values.Length > 0)
        {
            int index = 0;
            message = Regex.Replace(message, @"\{.*?\}", match =>
            {
                if (index < values.Length)
                    return values[index++].ToString()!;
                return match.Value;
            });
        }

        return message;
    }


    private static void LoadLocalizationFiles(string folder)
    {
        var basePath = AppContext.BaseDirectory;
        var locPath = Path.Combine(basePath, folder);

        if (!Directory.Exists(locPath)) return;

        foreach (var file in Directory.GetFiles(locPath, "*.json"))
        {
            var culture = Path.GetFileNameWithoutExtension(file);
            var json = File.ReadAllText(file);
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            if (dict != null)
                _resources[culture] = dict;
        }
    }
}



