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
                    AndroidJavaObject accountManager = currentActivity.Call<AndroidJavaObject>("getSystemService", "account");
                    AndroidJavaObject[] accounts = accountManager.Call<AndroidJavaObject[]>("getAccounts");

                    if(accounts.Length > 0)
                    {
                        return accounts[0].Get<string>("name");
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
