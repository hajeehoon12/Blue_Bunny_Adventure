using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudBoss : MonoBehaviour
{
    public GameObject Bee;
    public GameObject Worm;
    public GameObject BlueBee;

    public GameObject ElecShockWave;

    private ParticleSystem Rain;

    public LayerMask groundLayerMask;

    SpriteRenderer spriteRenderer;

    Coroutine patterCoroutine;

    private int patternNum = 0;
    private int monsterNum = 0;

    public float bossMaxHP = 100;
    public float bossCurrentHP;

    private bool isDead = false;
    


    private void Awake()
    {
        Rain = GetComponentInChildren<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //private bool onFloor = false;

    private void Start()
    {
        AudioManager.instance.StopBGM();
        AudioManager.instance.PlayBGM("BossCloud", 0.2f);

        CameraManager.Instance.MakeCameraShake(transform.position, 2, 0.05f, 0.1f);
        AudioManager.instance.PlaySFX("Bad", 0.2f);

        patterCoroutine = StartCoroutine(CloudPattern());
        bossCurrentHP = bossMaxHP;

        Rain.Stop();


    }

    IEnumerator CloudPattern()
    {

        yield return new WaitForSeconds(2f);
        float duringTime = 3f;
        while (true)
        {
            switch (patternNum % 3)
            {
                case 0:
                    StartCoroutine(SummonPeriod());
                    break;
                case 1:
                    StartCoroutine(CloudRainMove());
                    duringTime = 6f;
                    break;
                case 2:
                    StartCoroutine(ElectricShockWave());
                    duringTime = 6f;
                    break;


                default:
                    break;

            }

            yield return new WaitForSeconds(duringTime);

            patternNum++;
        }
    }



    IEnumerator ElectricShockWave()
    {
        
        yield return new WaitForSeconds(1f);


        
        float shockWaveTime = 2f;

        Vector3 totalDirection = CharacterManager.Instance.Player.controller.transform.position - transform.position;
        float dist =Vector3.Distance(CharacterManager.Instance.Player.controller.transform.position, transform.position);
        Vector3 normalDirection = totalDirection.normalized;

        //Debug.Log(normalDirection);

        int amount = (int)dist / 1;
        int curNum = 0;

        while (curNum <= amount + 2) // 
        {
            AudioManager.instance.PlayPitchSFX("ShockWave", 0.2f);
            Debug.Log("ShockWave!!");
            GameObject shockWave = Instantiate(ElecShockWave, transform.position,Quaternion.identity);

            shockWave.transform.position += normalDirection * curNum;

            yield return new WaitForSeconds(shockWaveTime / amount);
            curNum++;
            Destroy(shockWave);
            
        }

        yield return new WaitForSeconds(1f);
    
    }


    IEnumerator CloudRainMove()
    {
        Rain.Play();
        AudioManager.instance.PlayBGM2("WindRain", 0.2f);
        float Dir = transform.position.x > 0 ? -1 : 1;

        transform.DOMoveX((CameraManager.Instance.mapSize.x - 5) * Dir, 5.9f);


        yield return new WaitForSeconds(6f);

        Rain.Stop();
        AudioManager.instance.StopBGM2();
    }



    IEnumerator SummonPeriod()
    {
        SummonServant();
        yield return new WaitForSeconds(1f);
        SummonServant();
        yield return new WaitForSeconds(1f);
        SummonServant();

        CloudPattern();

    }


    void SummonServant() // Summon Servants
    {
        AudioManager.instance.PlayPitchSFX("Summon", 0.2f);

        switch (monsterNum % 3)
        {
            case 0:
                StartCoroutine(Summon(4));
                break;
            case 1:
                StartCoroutine(Summon(5));
                break;
            default: // temp : safe code
                StartCoroutine(Summon(6));
                break;

        }

        monsterNum++;
    }

    IEnumerator Summon(int numOfMon)
    {

        bool onFloor;

        onFloor = false;
        //GameObject servant = Instantiate(mon, transform.position, Quaternion.identity);
        GameObject servant = PoolManager.Instance.Get(numOfMon);
        //servant.transform.parent = transform;
        servant.GetComponentInChildren<SpriteRenderer>().DOFade(1, 0f);
        servant.transform.position = transform.position + new Vector3(0, 1, 0);
        servant.GetComponent<Monster>().enabled = false;
        Monster monScript = servant.GetComponent<Monster>();
        Collider2D servCol = servant.GetComponent<BoxCollider2D>();

        Rigidbody2D rigid = servant.GetComponent<Rigidbody2D>();


        //rigid.gravityScale = 0f;
        // 던지기
        //float throwTime = 0;


        float distDiff;
        distDiff = (servant.transform.position.x - CharacterManager.Instance.Player.transform.position.x);
        float Dir = distDiff > 0 ? -1 : 1;

        rigid.gravityScale = 0;

        servant.transform.DOMoveY(servant.transform.position.y + Mathf.Abs(distDiff / 2) + Dir - 1, 1f).SetEase(Ease.Linear);
        servant.transform.DOMoveX(servant.transform.position.x - distDiff - Dir, 1f);

        yield return new WaitForSeconds(1f);
        rigid.gravityScale = 1f;
        while (!(distDiff < 0.5f && distDiff > -0.5f)) // in near distance
        {
            Dir = distDiff > 0 ? -1 : 1;
            servant.transform.position += new Vector3(0.2f * Dir, 0);



            distDiff = (servant.transform.position.x - CharacterManager.Instance.Player.transform.position.x);
            yield return new WaitForSeconds(0.02f);
        }


        rigid.velocity = Vector3.zero;

        while (!onFloor)
        {
            yield return new WaitForSeconds(0.003f);

            if (servant.transform.position.y - CharacterManager.Instance.Player.transform.position.y >= 1f)
            {
                continue;
            }


            RaycastHit2D hit = Physics2D.Raycast(servant.transform.position, new Vector2(0, -1), 1f, groundLayerMask);
            if (hit.collider?.name != null)
            {
                servant.transform.position = hit.point + new Vector2(0, servCol.bounds.extents.y - servCol.offset.y);//
                onFloor = true;
            }

        }

        rigid.gravityScale = 0f;
        rigid.velocity = Vector2.zero;
        //Destroy(rigid);
        monScript.enabled = true;
        //monScript.stateMachine.ChangeState(monScript.stateMachine.ChasingState);
    }


    void BossDie()
    {
        
        isDead = true;
        AudioManager.instance.StopBGM();
        AudioManager.instance.StopBGM2();
        AudioManager.instance.PlaySFX("SceneChange", 0.2f);
        CameraManager.Instance.MakeCameraShake(transform.position, 4, 0.1f, 0.2f);
        StopCoroutine(patterCoroutine);
        StopAllCoroutines();
        transform.DOScale(0, 2f).OnComplete(() =>
            {
                
                AudioManager.instance.PlayBGM("SuperMario2", 0.2f);
                //SpawnManager.
                GameManager.Instance.spawnManager.nowMap.isBossAlive = false;
                GameManager.Instance.spawnManager.ApplyAliveMonsterDeath();
                //PoolManager.Instance.DeleteAll();
                Destroy(gameObject);
            }
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Define.BULLET_TAG))
        {
            if (bossCurrentHP > CharacterManager.Instance.Player.stats.attackDamage)
            {
                bossCurrentHP -= CharacterManager.Instance.Player.stats.attackDamage;
                Debug.Log($"BOSS HP : {bossCurrentHP}");
                StartCoroutine(ColorChanged());
            }
            else
            {
                if (!isDead)
                {
                    BossDie();
                    Debug.Log("Boss Dead!!");
                }
            }
        }
    }

    IEnumerator ColorChanged()
    {
        
        spriteRenderer.DOColor(Color.red, 0.1f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.DOColor(Color.white, 0.1f);

    }

}
