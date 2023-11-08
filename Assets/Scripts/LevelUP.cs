using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUP : MonoBehaviour
{
    [SerializeField] private GameObject FirstButton;
    [SerializeField] private GameObject SecondButton;
    [SerializeField] private GameObject ThreeButton;
    [SerializeField] private Sprite none;
    public static bool IsSelected;
    public Sprite[] itemtextures;
    public bool[] IssTake;
    public int a;
    private Image spritechangingFirst;
    private Image spritechangingSecond;
    private Image spritechangingThree;

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
        a = Random.Range(0, itemtextures.Length);
        if (!IssTake[a])
        {
            FirstButton.GetComponent<UpgradeButton>().id = a;
            spritechangingFirst = FirstButton.GetComponent<Image>();
            spritechangingFirst.sprite = itemtextures[a];

            a = Random.Range(0, itemtextures.Length);
            if (!IssTake[a] && FirstButton.GetComponent<UpgradeButton>().id != a)
            {
                SecondButton.GetComponent<UpgradeButton>().id = a;
                spritechangingSecond = SecondButton.GetComponent<Image>();
                spritechangingSecond.sprite = itemtextures[a];

                a = Random.Range(0, itemtextures.Length);
                if (!IssTake[a] && FirstButton.GetComponent<UpgradeButton>().id != a &&
                    SecondButton.GetComponent<UpgradeButton>().id != a)
                {
                    ThreeButton.GetComponent<UpgradeButton>().id = a;
                    spritechangingThree = ThreeButton.GetComponent<Image>();
                    spritechangingThree.sprite = itemtextures[a];
                }
                else
                {
                    if (IssTake.Count(b => b == false) >= 1)
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
                if (IssTake.Count(b => b == false) >= 2)
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
            if (IssTake.Count(b => b == false) >= 3)
            {
                a = Random.Range(0, itemtextures.Length);
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