using System;
using DustyStudios;
using DustyStudios.SystemUtilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using CustomButton = UiElementsList.ButtonsStruct.InteractButton;

public class TalkingPerson : MonoBehaviour
{
    [SerializeField] private SerializableQueue<Replica> Dialogue;
    [SerializeField] private Sprite sprite;

    private UiElementsList.PanelsStruct.Dialogue dialogue
    {
        get => UiElementsList.instance.Panels.DialogueBox;
    }

    private CustomButton next
    {
        get => UiElementsList.instance.Buttons.Interact;
    }


    public void Talk()
    {
        Replica.Box = dialogue.text;
        openMenu(true);
        Dialogue.Dequeue().Say();
    }

    public void Continue()
    {
        if (Dialogue.Count > 0) Dialogue.Dequeue().Say();
        else openMenu(false);
    }

    private void openMenu(bool open)
    {
        dialogue.Panel.SetActive(open);
        next.button.gameObject.SetActive(open);
        next.image.sprite = sprite;
        next.button.onClick.RemoveAllListeners();
        next.button.onClick.AddListener(Continue);
    }

    [Serializable]
    private class Replica
    {
        [SerializeField] public string phrase;
        public UnityEvent Action;

        public static Text Box;

        public void Say()
        {
            Box.text = phrase.Replace("Username", User.Username());
            Action.Invoke();
        }
    }
}