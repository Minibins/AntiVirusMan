using UnityEngine;
using System;
using UnityEngine.Android;
namespace DustyStudios
{
    namespace SystemUtilities
    {
        public static class User
        {
            public static string Username()
            {
                if(!Permission.HasUserAuthorizedPermission("Game characters want to know your username")|| !PlayerPrefs.HasKey("KnowPlayersName"))
                {
                    Permission.RequestUserPermission("Game characters want to know your username");
                }
                else if(PlayerPrefs.GetInt("KnowPlayersName")==1)
                {
                    return getUsername();
                }
                
                return "Antivirus";
            }
            static string getUsername()
            {
                switch(SystemInfo.deviceType)
                {
                    case DeviceType.Desktop:
                    if(SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows || SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX)
                    {
                        return Environment.UserName;
                    }
                    else if(SystemInfo.operatingSystemFamily == OperatingSystemFamily.Linux)
                    {
                        return Environment.GetEnvironmentVariable("USER");
                    }
                    break;
                    case DeviceType.Handheld:
                    try
                    {
                        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                        AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
                        AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
                        string userId = secure.CallStatic<string>("getString", contentResolver, "android_id");
                        if(!string.IsNullOrEmpty(userId))
                        {
                            return userId;
                        }
                    }
                    catch
                    {

                    }
                    break;
                }
                return "Antivirus";
            }
            private class PermissionCallbacker : PermissionCallbacks
            {
                public void OnPermissionGranted(string permissionName)
                {
                    if(permissionName.Equals("Game characters want to know your username"))
                    {
                        PlayerPrefs.SetInt("KnowPlayersName",1);
                    }
                }

                public void OnPermissionDenied(string permissionName)
                {
                    if(permissionName.Equals("Game characters want to know your username"))
                    {
                        PlayerPrefs.SetInt("KnowPlayersName", 0);
                    }
                }

            }
        }
    }
}
