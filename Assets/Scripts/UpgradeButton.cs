using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    //ID 0
    private GameObject Laser;
    //ID 0
    //ID 5
    //ID 12
    //ID 12
    //ID 13
    [SerializeField] private GameObject DownButton;
    //ID 13
    //ID 14
    //ID 14
    //ID 28
    private GameObject PUSHKA;
    //ID 28

    public int id;
    public GameObject mainupgrader;
    private GameObject Player;

    public void onclick()
    {
        LevelUP lvlUp = mainupgrader.GetComponent<LevelUP>();
        lvlUp.IssTake[id] = true;
        Player = GameObject.Find("Player");
        ChooseUpgrade();
        LevelUP.IsSelected = true;
    }

    private void ChooseUpgrade()
    {
        switch (id)
        {
            case 0:
                Laser = GameObject.FindGameObjectWithTag("LaserGun");
                Laser.transform.position = new Vector2(Random.Range(25, 35), -23.725f);
                Laser.GetComponent<LaserGun>().StartShoot();
                break;
            case 5:
                GameObject.FindGameObjectWithTag("Mathyrspawn").GetComponent<SpawnerEnemy>().StopOrStartSpawn();
                break;
            case 7:
                GameObject.FindGameObjectWithTag("LaserGun").AddComponent<DRAG>();
                GameObject.FindGameObjectWithTag("PC").AddComponent<DRAG>();
                GameObject.FindGameObjectWithTag("PC").AddComponent<Rigidbody2D>();
                break;
            case 11:
                GameObject.FindGameObjectWithTag("Starfall").GetComponent<starfall>().IsSpawn = true;
                break;
            case 12:
                Player.GetComponent<InstantiateWall>().IsOpenly = true;
                break;
            case 13:
                Player.GetComponent<Player>().IsDownSelected = true;
                DownButton.SetActive(true);
                break;
            case 14:
                Player.GetComponent<PlayerAttack>().IsSelectedBullet = true;
                break;
            case 28:
                PUSHKA = GameObject.FindGameObjectWithTag("PUSHKA");
                PUSHKA.transform.position = new Vector2(Random.Range(47, 51), 4.55f);
                PUSHKA.GetComponent<PUSHKA>().StartShoot();
                break;
        }
    }
}
