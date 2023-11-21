using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private GameObject DashButton;
    [SerializeField] private GameObject DownButton,Drone;
    private GameManager gameManager;
    private GameObject Laser, PUSHKA,_Player;
    public GameObject mainupgrader;
    public int id;
   
    public void onclick()
    {
        if (gameObject.GetComponent<Image>().sprite.name == "None")
        {
            _Player = GameObject.Find("Player");
            Level.IsSelected = true;
        }
        else
        {
            LevelUP lvlUp = mainupgrader.GetComponent<LevelUP>();
            LevelUP.isTaken[id] = true;
            _Player = GameObject.Find("Player");
            ChooseUpgrade(lvlUp);
            Level.IsSelected = true;
        }
    }

    private void ChooseUpgrade(LevelUP lvlUp)
    {
        switch (id)
        {
            case 0://лазер
                Laser = GameObject.FindGameObjectWithTag("LaserGun");
                Laser.GetComponent<LaserGun>().StartShoot();
                if (LevelUP.isTaken[1])
                {
                    Laser.AddComponent<DRAG>();
                }

                break;

            case 1://Перетаскивание турелей

                GameObject.FindGameObjectWithTag("LaserGun").AddComponent<DRAG>();
                GameObject.FindGameObjectWithTag("PC").AddComponent<DRAG>();
                GameObject.FindGameObjectWithTag("PUSHKA").AddComponent<DRAG>();
                _Player.GetComponent<InstantiateWall>().canmove = true;
                break;

            case 2: //звездопад
            GameObject[] starfallObjects = GameObject.FindGameObjectsWithTag("Starfall");
                byte delay = 0;
                foreach (GameObject starfallObject in starfallObjects)
                {
                    starfall starfallComponent = starfallObject.GetComponent<starfall>();
                    if (starfallComponent != null) Invoke(StartStarfall(starfallComponent), ((float) delay++) / 10);
                }
                break;

            case 3://Строительство стен
            _Player.GetComponent<InstantiateWall>().IsOpenly = true;
                break;

            case 4://Пике
            DownButton.SetActive(true);
                break;

            case 5://Стрельба в небо
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
                if (LevelUP.isTaken[1])
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
            _Player.GetComponent<Player>().dashRange *= 2;
                break;
            //Айди 11 значит элитная бяка
            case 12:
                GameObject[] AuraObjects = GameObject.FindGameObjectsWithTag("Aura");
                foreach (GameObject AuraObject in AuraObjects)
                {
                    AuraPC AuraComponent = AuraObject.GetComponent<AuraPC>();
                    if (AuraComponent != null) AuraComponent.IsStartWork = true;
                }
                break;

            //Айди 13 значит удар в спину

            case 14:
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    enemy.AddComponent<DRAG>();
                }
            }
                break;

            //Айди 15 значит полет

            case 16://Антивирус может прыгать на врагах
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
                attacke.AttackType = PlayerAttack.attackTypes.Ultra;
                attacke.Damage = 4;
                attacke._timeReload /=2;
                Level.EnemyNeedToUpLVL /= 1.5f;
                break;
            //Айди 18 значит эволюцию вирусов

            case 19:
                PC.IsFollowing = true;
            break;
            case 20:
                Instantiate(Drone,_Player.transform.position,Quaternion.identity,_Player.transform.parent);
                break;
            case 21:
                NumberOfObjectsIsDamage.tagAlloved[0] = true;
                break; 
            case 22:
            GameObject.Find("Carma").GetComponent<Image>().color = Color.white;
            break;
        }

        
    }
    private string StartStarfall(starfall starfallComponent)
    {
        starfallComponent.StartCoroutine(starfallComponent.spawnStars());
        return "";
    }
}