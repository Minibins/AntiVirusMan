using UnityEngine;
using UnityEngine.UI;

public class LevelUP : MonoBehaviour
{
    [SerializeField] private GameObject FirstButton;
    [SerializeField] private GameObject SecondButton;
    [SerializeField] private GameObject ThreeButton;
    public static bool IsSelected;
    public Sprite[] itemtextures;
    public bool[] IssTake;
    private int a;
    private Image spritechangingFirst;
    private Image spritechangingSecond;
    private Image spritechangingThree;
    private void Update()
    {
        if (IsSelected)
        {
            IsSelected = false;
            gameObject.SetActive(false);
        }
    }

    public void NewUpgrade()
    {

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
                if (!IssTake[a] && FirstButton.GetComponent<UpgradeButton>().id != a && SecondButton.GetComponent<UpgradeButton>().id != a)
                {
                    ThreeButton.GetComponent<UpgradeButton>().id = a;
                    spritechangingThree = ThreeButton.GetComponent<Image>();
                    spritechangingThree.sprite = itemtextures[a];
                }
                else
                {
                    a = Random.Range(0, itemtextures.Length);
                    return;
                }
            }
            else
            {
                a = Random.Range(0, itemtextures.Length);
                return;
            }
        }

        else
        {
            a = Random.Range(0, itemtextures.Length);
            return;
        }
    }
}