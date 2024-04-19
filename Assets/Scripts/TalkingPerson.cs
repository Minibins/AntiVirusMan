using UnityEngine;
using UnityEngine.UI;
using DustyStudios;
using UnityEngine.Events;

using Dialogue = UiElementsList.PanelsStruct.Dialogue;
using CustomButton = UiElementsList.ButtonsStruct.InteractButton;
public class TalkingPerson : MonoBehaviour
{
    [SerializeField] SerializableQueue<Replica> Dialogue;
    [SerializeField] Sprite sprite;
    Dialogue dialogue
    {
        get => UiElementsList.instance.Panels.DialogueBox;
    }

    CustomButton next
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
        if(Dialogue.Count > 0) Dialogue.Dequeue().Say();
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
    [System.Serializable]
    class Replica
    {
        [SerializeField]
        public string phrase;
        public UnityEvent Action;

        public static Text Box;
        public void Say()
        {
            Box.text = phrase.Replace("Username",DustyStudios.SystemUtilities.User.Username());
            Action.Invoke();
        }
    }
}

