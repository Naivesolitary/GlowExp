
using Newtonsoft.Json;

public class ProfileSetting
{

    private readonly List<UserProfile> _profiles = new();
    private readonly string _appDataDirectory;
    private readonly string _ProfileFilePath;
    private UserProfile? _userProfile;

    public ProfileSetting()
    {

        _appDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GlowExp");
        _ProfileFilePath = Path.Combine(_appDataDirectory, "profile.json");
        if (!Directory.Exists(_appDataDirectory)) { Directory.CreateDirectory(_appDataDirectory); }


    }

    public  void SaveProfile(UserProfile profile)
    {
        if (profile == null) return;
        _profiles.Add(profile);  // Adding profile to the collection
        GetPreferredCurrencyCode(profile);
        

        try
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
            };

            string json = JsonConvert.SerializeObject(_profiles, settings);
            File.WriteAllText(_ProfileFilePath, json);
           
        }
        catch (Exception ex) { Console.WriteLine($"Error saving User Info: {ex.Message}"); }

    }




    public string GetPreferredCurrencyCode(UserProfile profile) => profile?.PreferredCurrency ?? "USD";

    public bool ProfileExists()
    {
        return File.Exists(_ProfileFilePath);
    }

    public UserProfile? LoadProfile()
    {
        if (!File.Exists(_ProfileFilePath)) return null;

        try
        {
            string json = File.ReadAllText(_ProfileFilePath);
            var profiles = JsonConvert.DeserializeObject<List<UserProfile>>(json);
            return profiles?.FirstOrDefault(); // assuming only one user
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading user profile: {ex.Message}");
            return null;
        }
    }

    public void OverwriteProfile(UserProfile profile)
    {
        if (profile == null) return;

        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
        };

        try
        {
            string json = JsonConvert.SerializeObject(new List<UserProfile> { profile }, settings);
            File.WriteAllText(_ProfileFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error overwriting profile: {ex.Message}");
        }
    }

    public void DeleteProfile()
    {
        if (File.Exists(_ProfileFilePath))
            File.Delete(_ProfileFilePath);

        string transactionFilePath = Path.Combine(_appDataDirectory, "transaction.json");
        if (File.Exists(transactionFilePath))
            File.Delete(transactionFilePath);
    }






}

