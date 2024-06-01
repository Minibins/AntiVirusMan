using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nokia3310 : MonoBehaviour, IDamageble
{
    public void OnDamageGet(float Damage,IDamageble.DamageType type)
    {
        TouchScreenKeyboard.Open("Hello World!",TouchScreenKeyboardType.NamePhonePad);
    }
}
