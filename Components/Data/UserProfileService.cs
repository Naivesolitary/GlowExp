public class UserProfileService
{
    private UserProfile? _userProfile;
    public UserProfileService()
    {
        LoadProfile();
    }

    public UserProfile? GetProfile() => _userProfile;

    public void Clear() => _userProfile = null;
    private void LoadProfile()
    {
        var setting = new ProfileSetting();
        _userProfile = setting.LoadProfile();
    }

    public void RefreshProfile() => LoadProfile();

    public string GetPreferredCurrencyCode()
    {
        return _userProfile?.PreferredCurrency ?? "USD";
    }

    public string GetUsername()
    {
        return _userProfile?.Username ?? "Friend";
    }

    public bool IsProfileLoaded() => _userProfile != null;
}
