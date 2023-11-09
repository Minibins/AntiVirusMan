using System;
using System.Collections.Generic;
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
        
        if (itemTextures.Length < count)
        {
            return null;
        }

        for (int i = 0; i < count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, itemTextures.Length);
            int attempts = 0;
            
            while ((Array.Exists(uniqueIndices, index => index == randomIndex) || isTaken[randomIndex]) && attempts < itemTextures.Length)
            {
                randomIndex = UnityEngine.Random.Range(0, itemTextures.Length);
                attempts++;
            }
            
            if (attempts >= itemTextures.Length)
            {
                return null;
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

    public void ResetButtons()
    {
        for (int i = 0; i < 3; i++)
        {
            spriteChanging[i].sprite = none;
        }
    }
}
