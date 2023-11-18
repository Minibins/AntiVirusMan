using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUP : MonoBehaviour
{
    [SerializeField] private GameObject FirstButton;
    [SerializeField] private GameObject SecondButton;
    [SerializeField] private GameObject ThreeButton;
    [SerializeField] private Sprite none;
    
    public Sprite[] itemtextures;
    [SerializeField] private bool[] IsTakenTemplate;
    public static bool[] isTaken;
    public int a;
    private Image spritechangingFirst;
    private Image spritechangingSecond;
    private Image spritechangingThree;
    private void Start()
    {
        isTaken = IsTakenTemplate;
        string isTakenDebug= Convert.ToString(isTaken);
        Debug.Log(isTakenDebug);
    }
    public void NewUpgrade()
    {
        Time.timeScale = 0.1f;
        a = UnityEngine.Random.Range(0, itemtextures.Length);
        if (!isTaken[a])
        {
            FirstButton.GetComponent<UpgradeButton>().id = a;
            spritechangingFirst = FirstButton.GetComponent<Image>();
            spritechangingFirst.sprite = itemtextures[a];

            a = UnityEngine.Random.Range(0, itemtextures.Length);
            if (!isTaken[a] && FirstButton.GetComponent<UpgradeButton>().id != a)
            {
                SecondButton.GetComponent<UpgradeButton>().id = a;
                spritechangingSecond = SecondButton.GetComponent<Image>();
                spritechangingSecond.sprite = itemtextures[a];

                a = UnityEngine.Random.Range(0, itemtextures.Length);
                if (!isTaken[a] && FirstButton.GetComponent<UpgradeButton>().id != a &&
                    SecondButton.GetComponent<UpgradeButton>().id != a)
                {
                    ThreeButton.GetComponent<UpgradeButton>().id = a;
                    spritechangingThree = ThreeButton.GetComponent<Image>();
                    spritechangingThree.sprite = itemtextures[a];
                }
                else
                {
                    if (isTaken.Count(b => b == false) >= 1)
                    {
                        NewUpgrade();
                        return;
                    }
                    else
                    {
                        ThreeButton.GetComponent<UpgradeButton>().id = -1;
                        spritechangingThree = ThreeButton.GetComponent<Image>();
                        spritechangingThree.sprite = none;
                    }
                }
            }
            else
            {
                if (isTaken.Count(b => b == false) >= 2)
                {
                    NewUpgrade();
                    return;
                }
                else
                {
                    SecondButton.GetComponent<UpgradeButton>().id = -1;
                    spritechangingSecond = SecondButton.GetComponent<Image>();
                    spritechangingSecond.sprite = none;
                }
            }
        }

        else
        {
            if (isTaken.Count(b => b == false) >= 3)
            {
                a = UnityEngine.Random.Range(0, itemtextures.Length);
                NewUpgrade();
                return;
            }
            else
            {
                FirstButton.GetComponent<UpgradeButton>().id = -1;
                spritechangingFirst = FirstButton.GetComponent<Image>();
                spritechangingFirst.sprite = none;
            }
        }
    }
}