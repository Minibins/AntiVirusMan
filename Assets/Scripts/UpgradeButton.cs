using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public int id;
    public GameObject mainupgrader;
    private GameObject Player;
    private GameManager gameManager;


    //ID 0
    //Works
    private GameObject Laser;
    //ID 0

    //ID 1
    //Works
    //ID 1

    //ID 2
    //Works
    //ID 2

    //ID 3
    //Works
    //ID 3

    //ID 4
    //Works
    [SerializeField] private GameObject DownButton;
    //ID 4

    //ID 5
    //Works
    //ID 5

    //ID 6
    //Works
    private GameObject PUSHKA;
    //ID 6

    //ID 7
    //Works
    //ID 7

    //ID 8
    //Works
    //ID 8

    //ID 9
    //Works
    //ID 9

    public void onclick()
    {
        if (gameObject.GetComponent<Image>().sprite.name == "None")
        {
            Player = GameObject.Find("Player");
            LevelUP.IsSelected = true;
        }
        else
        {
            LevelUP lvlUp = mainupgrader.GetComponent<LevelUP>();
            lvlUp.IssTake[id] = true;
            Player = GameObject.Find("Player");
            ChooseUpgrade(lvlUp);
            LevelUP.IsSelected = true;
        }
    }

    private void ChooseUpgrade(LevelUP lvlUp)
    {

        switch (id)
        {
            case 0:
                Laser = GameObject.FindGameObjectWithTag("LaserGun");
                Laser.GetComponent<LaserGun>().StartShoot();
                if (lvlUp.IssTake[1])
                {
                    Laser.AddComponent<DRAG>();
                }
                break;
            case 1:
                GameObject.FindGameObjectWithTag("LaserGun").AddComponent<DRAG>();
                GameObject.FindGameObjectWithTag("PC").AddComponent<DRAG>();
                GameObject.FindGameObjectWithTag("PUSHKA").AddComponent<DRAG>();
                Player.GetComponent<InstantiateWall>().canmove = true;
                break;
            case 2:
                GameObject.FindGameObjectWithTag("Starfall").GetComponent<starfall>().IsSpawn = true;
                break;
            case 3:
                Player.GetComponent<InstantiateWall>().IsOpenly = true;
                break;
            case 4:
                DownButton.SetActive(true);
                break;
            case 5:
                Player.GetComponent<PlayerAttack>().IsSelectedBullet = true;
                break;
            case 6:
                PUSHKA = GameObject.FindGameObjectWithTag("PUSHKA");
                PUSHKA.GetComponent<PUSHKA>().StartShoot();
                if (lvlUp.IssTake[1])
                {
                    PUSHKA.AddComponent<DRAG>();
                }
                break;

            case 7:
                gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                gameManager.StartSpawnAid();
                break;

            case 8:
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>().isSpeedIsDamage = true;
                break;

            case 9:
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().StartSpawnPortals();
                break;
        }
    }
}
