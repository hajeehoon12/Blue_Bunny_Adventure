using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBoss : MonoBehaviour
{
    public GameObject Bee;
    public GameObject Worm;

    public LayerMask groundLayerMask;

    Coroutine patterCoroutine;

    private int patternNum = 0;
    private int monsterNum = 0;

    private bool onFloor = false;

    private void Start()
    {
        patterCoroutine= StartCoroutine(CloudPattern());




    }

    IEnumerator CloudPattern()
    {

        while (true)
        {
            switch (patternNum % 2)
            {
                case 0:
                    SummonServant();
                    break;




                default:
                    break;
            
            }

            yield return new WaitForSeconds(3f);

            patternNum++;
        }

        
    }


    void SummonServant() // Summon Servants
    {
        switch (monsterNum % 3)
        {
            case 0:
                StartCoroutine(Summon(Bee));
                break;
            case 1:
                StartCoroutine(Summon(Worm));
                break;
            case 2:
                break;

            default:
                break;
            
        }

        monsterNum++;
    }
    
    IEnumerator Summon(GameObject mon)
    {

        
        onFloor = false;
        GameObject servant = Instantiate(mon, transform.position, Quaternion.identity);
        Collider2D servCol = servant.GetComponent<BoxCollider2D>();
        // 던지기
        Rigidbody2D rigid = servant.GetComponent<Rigidbody2D>();

        while (!onFloor)
        {
            yield return new WaitForSeconds(0.01f);

            if (servant.transform.position.y - CharacterManager.Instance.Player.transform.position.y >= 1f)
            {
                continue;
            }


            RaycastHit2D hit = Physics2D.Raycast(servant.transform.position, new Vector2(0, -1), 0.6f, groundLayerMask);
            if (hit.collider?.name != null)
            {
                servant.transform.position = hit.point + new Vector2(0, servCol.bounds.extents.y + servCol.offset.y);
                onFloor = true;
            }
           
        }
        rigid.velocity = Vector3.zero;
        Destroy(rigid);
    }





}
