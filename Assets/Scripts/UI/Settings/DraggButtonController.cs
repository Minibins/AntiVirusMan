using UnityEngine;

public class DraggButtonController : MonoBehaviour
{
    [SerializeField] private DraggableButton[] _buttons;
    [SerializeField] private GameObject[] _buttonsList;
    public static bool Draggble;

    public void SaveButtonsPosition()
    {
        foreach (DraggableButton button in _buttons)
        {
            button.ResetPosition();
        }
    }

    public void LoadPosition()
    {
        foreach (var button in _buttonsList)
        {
            button.GetComponent<LoadButtonPosition>().LoadPosition();
        }
    }

    public void Dragg(bool isDragg)
    {
        Draggble = isDragg;
    }
}