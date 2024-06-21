using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudBoss : MonoBehaviour
{
    public GameObject Bee;
    public GameObject Worm;
    public GameObject BlueBee;

    private ParticleSystem Rain;

    public LayerMask groundLayerMask;

    Coroutine patterCoroutine;

    private int patternNum = 0;
    private int monsterNum = 0;

    private void Awake()
    {
        Rain = GetComponentInChildren<ParticleSystem>();
    }

    //private bool onFloor = false;

    private void Start()
    {
        AudioManager.instance.StopBGM();
        AudioManager.instance.PlayBGM("BossCloud", 0.2f);
        patterCoroutine= StartCoroutine(CloudPattern());

        Rain.Stop();


    }

    IEnumerator CloudPattern()
    {
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
                    duringTime = 3f;
                    break;


                default:
                    break;
            
            }

            yield return new WaitForSeconds(duringTime);

            patternNum++;
        }
    }


    IEnumerator CloudRainMove()
    {
        Rain.Play();
        AudioManager.instance.PlayBGM2("WindRain", 0.2f);
        float Dir = transform.position.x > 0 ? -1 : 1;

        transform.DOMoveX((CameraManager.Instance.mapSize.x -5) * Dir, 6f);


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
                StartCoroutine(Summon(Bee));
                break;
            case 1:
                StartCoroutine(Summon(BlueBee));
                break;
            default: // temp : safe code
                StartCoroutine(Summon(Worm));
                break;
            
        }

        monsterNum++;
    }
    
    IEnumerator Summon(GameObject mon)
    {

        bool onFloor;
        
        onFloor = false;
        GameObject servant = Instantiate(mon, transform.position, Quaternion.identity);
        Collider2D servCol = servant.GetComponent<BoxCollider2D>();
        
        Rigidbody2D rigid = servant.GetComponent<Rigidbody2D>();

        servant.GetComponent<Monster>().enabled = false;
        //rigid.gravityScale = 0f;
        // 던지기
        //float throwTime = 0;
        

        float distDiff;
        distDiff = (servant.transform.position.x - CharacterManager.Instance.Player.transform.position.x);
        float Dir = distDiff > 0 ? -1 : 1;

        rigid.gravityScale = 0;

        servant.transform.DOMoveY(servant.transform.position.y + distDiff/3 + Dir-1, 1f).SetEase(Ease.Linear);
        servant.transform.DOMoveX(servant.transform.position.x - distDiff - Dir, 1f);

        yield return new WaitForSeconds(1f);
        rigid.gravityScale = 2f;
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


            RaycastHit2D hit = Physics2D.Raycast(servant.transform.position, new Vector2(0, -1), 0.6f, groundLayerMask);
            if (hit.collider?.name != null)
            {
                servant.transform.position = hit.point + new Vector2(0, servCol.bounds.extents.y - servCol.offset.y);
                onFloor = true;
            }
           
        }

        rigid.gravityScale = 0f;
        rigid.velocity = Vector2.zero;
        Destroy(rigid);
        servant.GetComponent<Monster>().enabled = true;
    }





    void BossDie()
    {
        StopCoroutine(patterCoroutine);
    }
}
