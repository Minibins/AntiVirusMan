using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] private string settingName;
    [SerializeField] private UnityAction Interaction;

    protected virtual void Start()
    {
        GetComponent<Button>().onClick.AddListener(Interaction);
        GetComponentInChildren<Text>().text = settingName;
    }
}