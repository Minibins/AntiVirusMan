using UnityEngine;

public class Nokia3310 : MonoBehaviour, IDamageble
{
    TouchScreenKeyboard keyboard;
    public void OnDamageGet(float Damage,IDamageble.DamageType type)
    {
        keyboard = TouchScreenKeyboard.Open("Hello World!",TouchScreenKeyboardType.PhonePad);
    }
    private void Update()
    {
        if(keyboard == null || keyboard.status != TouchScreenKeyboard.Status.Visible) return;
        keyboard.text = "";
    }
}