using DustyStudios;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggsForDummies : Upgrade
{
    public static bool isLookingForReferences =true;
    [SerializeField] GameObject glass;
    public static GameObject Glass;
    private void Awake()
    {
        Glass = glass;
    }
  //  [DustyConsoleCommand("lookref","Use Easter Eggs For Dummies")]
    public static string LookRef()
    {
        isLookingForReferences=true;
        return "Now you should click anywhere";
    }
}
