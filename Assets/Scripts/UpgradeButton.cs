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
                Laser.transform.position = new Vector3(Random.Range(-6, 6), -3.2f, 0);
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
                Player.GetComponent<Move>().IsDownSelected = true;
                DownButton.SetActive(true);
                break;
            case 14:
                Player.GetComponent<PlayerAttack>().IsSelectedBullet = true;
                break;
            case 28:
                PUSHKA = GameObject.FindGameObjectWithTag("PUSHKA");
                PUSHKA.transform.position = new Vector3(Random.Range(-7, 7), -3.317f, 0);
                PUSHKA.GetComponent<PUSHKA>().StartShoot();
                break;
        }
    }
}
