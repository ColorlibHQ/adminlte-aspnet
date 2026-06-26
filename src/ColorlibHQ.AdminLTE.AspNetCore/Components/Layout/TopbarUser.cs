namespace ColorlibHQ.AdminLTE.AspNetCore.Components.Layout;

/// <summary>The user shown in the topbar's account dropdown.</summary>
public sealed class TopbarUser
{
    /// <summary>Display name.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Optional role / subtitle.</summary>
    public string? Role { get; set; }

    /// <summary>Optional avatar image URL.</summary>
    public string? AvatarUrl { get; set; }

    /// <summary>Profile-page link.</summary>
    public string ProfileUrl { get; set; } = "#";

    /// <summary>Sign-out link.</summary>
    public string LogoutUrl { get; set; } = "#";
}
