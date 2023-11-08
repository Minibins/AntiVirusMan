using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public int id;
    public GameObject mainupgrader;
    private GameObject _Player;
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

    //ID 10
    //Works
    [SerializeField] private GameObject DashButton;
    //ID 10

    //ID 11
    //ID 11

    //ID 12
    //ID 12

    //ID 13
    [SerializeField] private GameObject _backStager;
    //ID 13
    
    //ID 18
    [SerializeField] private PC pc;
    //ID 18

    public void onclick()
    {
        if (gameObject.GetComponent<Image>().sprite.name == "None")
        {
            _Player = GameObject.Find("Player");
            LevelUP.IsSelected = true;
        }
        else
        {
            LevelUP lvlUp = mainupgrader.GetComponent<LevelUP>();
            lvlUp.IssTake[id] = true;
            _Player = GameObject.Find("Player");
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
                _Player.GetComponent<InstantiateWall>().canmove = true;
                break;

            case 2:
                GameObject[] starfallObjects = GameObject.FindGameObjectsWithTag("Starfall");
                foreach (GameObject starfallObject in starfallObjects)
                {
                    starfall starfallComponent = starfallObject.GetComponent<starfall>();
                    if (starfallComponent != null) starfallComponent.IsSpawn = true;
                }

                break;

            case 3:
                _Player.GetComponent<InstantiateWall>().IsOpenly = true;
                break;

            case 4:
                DownButton.SetActive(true);
                break;

            case 5:
                _Player.GetComponent<PlayerAttack>().IsSelectedBullet = true;
                break;

            case 6:
                PUSHKA = GameObject.FindGameObjectWithTag("PUSHKA");
                SpriteRenderer[] colesa = PUSHKA.GetComponentsInChildren<SpriteRenderer>();
                for (int i = 0; i < colesa.Length; i++)
                {
                    colesa[i].enabled = true;
                }

                PUSHKA.GetComponent<PUSHKA>().StartShoot();
                if (lvlUp.IssTake[1])
                {
                    for (int i = 0; i < colesa.Length; i++)
                    {
                        colesa[i].AddComponent<DRAG>();
                    }
                }

                break;

            case 7:
                gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                gameManager.StartSpawnAid();
                break;

            case 8:
                _Player.GetComponent<PlayerAttack>().isSpeedIsDamage = true;
                break;


            case 9:
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().StartSpawnPortals();
                break;

            case 10:
                DashButton.SetActive(true);
                break;

            case 11:
                SpawnerEnemy.elitePossibility[0] = true;
                break;

            case 12:
                GameObject[] AuraObjects = GameObject.FindGameObjectsWithTag("Aura");
                foreach (GameObject AuraObject in AuraObjects)
                {
                    AuraPC AuraComponent = AuraObject.GetComponent<AuraPC>();
                    if (AuraComponent != null) AuraComponent.IsStartWork = true;
                }

                break;

            case 13:
                Health.backStager = true;
                break;

            case 14:
                Enemy.isDraggable = true;
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    enemy.AddComponent<DRAG>();
                }
            }
                break;

            case 15:
                Player.isFlying = true;
                break;

            case 16:
                Enemy.AntivirusHaveBoots = true;
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<Enemy>().AddBookaComponent();
                }
            }
                break;
            
            case 17:
                PlayerAttack attacke= _Player.GetComponent<PlayerAttack>();
                attacke.IsUltraAttack = true;
                attacke.Damage = 4;
                break;

            case 18:
                pc.IsFollowing = true;
                break;
            
            case 19:
                Enemy.isEvolution = true;
                break;
        }
    }
}