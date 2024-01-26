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
                #if UNITY_ANDROID
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
                #elif UNITY_STANDALONE_WIN
                return Environment.UserName;
                #elif UNITY_STANDALONE_OSX
                return Environment.UserName;
                #elif UNITY_STANDALONE_LINUX
                return Environment.GetEnvironmentVariable("USER");

                #endif
                return "Antivirus";
            }
        }
    }
}
