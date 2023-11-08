using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUP : MonoBehaviour
{
    [SerializeField] private Sprite none;
    [SerializeField] private GameObject[] buttons = new GameObject[3];
    public static bool IsSelected;
    public Sprite[] itemTextures;
    public bool[] isTaken;
    private Image[] spriteChanging = new Image[3];
    private int[] chosenIndices = new int[3];

    private void Update()
    {
        if (IsSelected)
        {
            IsSelected = false;
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void NewUpgrade()
    {
        Time.timeScale = 0.1f;
        int[] uniqueIndices = GetUniqueIndices(3);

        if (uniqueIndices == null)
        {
            ResetButtons();
            NewUpgrade();
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            AssignItemToButton(i, uniqueIndices[i]);
        }
    }

    private int[] GetUniqueIndices(int count)
    {
        int[] uniqueIndices = new int[count];
        for (int i = 0; i < count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, itemTextures.Length);
            while (Array.Exists(uniqueIndices, index => index == randomIndex) || isTaken[randomIndex])
            {
                randomIndex = UnityEngine.Random.Range(0, itemTextures.Length);
            }
            uniqueIndices[i] = randomIndex;
        }
        return uniqueIndices;
    }

    private void AssignItemToButton(int buttonIndex, int itemIndex)
    {
        buttons[buttonIndex].GetComponent<UpgradeButton>().id = itemIndex;
        spriteChanging[buttonIndex] = buttons[buttonIndex].GetComponent<Image>();
        spriteChanging[buttonIndex].sprite = itemTextures[itemIndex];
        chosenIndices[buttonIndex] = itemIndex;
    }

    private void ResetButtons()
    {
        // Сбрасываем спрайты на кнопках
        for (int i = 0; i < 3; i++)
        {
            spriteChanging[i].sprite = none;
        }
    }
}
