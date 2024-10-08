using System.Collections;
using System.Linq;
using UnityEngine;

public class Door : PlayersCollisionChecker
{
    private UiElementsList.ButtonsStruct.InteractButton InteractButton
    {
        get => UiElementsList.instance.Buttons.Interact;
    }

    private AbstractPortal portal;
    private Door anotherEnd;
    [SerializeField] private Sprite DoorSprite;

    private void Start()
    {
        portal = GetComponent<AbstractPortal>();
        anotherEnd = portal.secondPortal.GetComponent<Door>();
        EnterAction += () => SetEnterUI(true);
        ExitAction += () => SetEnterUI(false);
    }

    private void SetEnterUI(bool IsEnable)
    {
        InteractButton.image.sprite = DoorSprite;
        var Button = InteractButton.button;
        Button.gameObject.SetActive(IsEnable);
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() => StartCoroutine(Teleport()));
    }

    public IEnumerator Teleport()
    {
        try
        {
            anotherEnd.EnteredThings.Add(EnteredThings.Last());
            portal.Teleport(EnteredThings.Last().transform);
        }
        catch
        {
        }
        finally
        {
            var ButtonAction = InteractButton.button.onClick;
            ButtonAction.RemoveAllListeners();
            ButtonAction.AddListener(() => StartCoroutine(anotherEnd.Teleport()));
        }

        yield return new WaitForFixedUpdate();
        InteractButton.button.gameObject.SetActive(true);
    }
}