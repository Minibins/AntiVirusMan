using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private GameObject DashButton;
    [SerializeField] private GameObject DownButton;
    private GameObject _Player;
    private GameManager gameManager;
    private GameObject Laser;
    private GameObject PUSHKA;
    public GameObject mainupgrader;
    public int id;

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
            lvlUp.isTaken[id] = true;
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
                if (lvlUp.isTaken[1])
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
                byte delay = 0;
                foreach (GameObject starfallObject in starfallObjects)
                {
                    starfall starfallComponent = starfallObject.GetComponent<starfall>();
                    if (starfallComponent != null) Invoke(StartStarfall(starfallComponent), ((float) delay++) / 10);
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
                if (lvlUp.isTaken[1])
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
                PlayerAttack attacke = _Player.GetComponent<PlayerAttack>();
                attacke.IsUltraAttack = true;
                attacke.Damage = 4;
                break;
            case 18:
                Enemy.isEvolution = true;
                break;

            case 19:
                PC.IsFollowing = true;
                break;
        }

        string StartStarfall(starfall starfallComponent)
        {
            starfallComponent.StartCoroutine(starfallComponent.spawnStars());
            return "";
        }
    }
}