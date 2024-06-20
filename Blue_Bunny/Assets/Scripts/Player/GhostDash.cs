using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GhostDash : MonoBehaviour
{
    public float ghostDelay; // Time interval of ghost effects
    public float fadeDuration; // Time duration for Ghost Effects
    private float ghostDelayTime; // Variable To Time Check
    public GameObject ghost; // Empty Object
    public bool makeGhost; // Will I Use Ghost Effect?

    void Start()
    {
        ghostDelayTime = ghostDelay;
        makeGhost = false;
    }

    void FixedUpdate()
    {
        if (makeGhost)
        {
            if (ghostDelayTime > 0)
            {
                ghostDelayTime -= Time.deltaTime;
            }
            else
            {  

                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.transform.localScale = this.transform.localScale;

                SpriteRenderer ghostSprite = currentGhost.GetComponent<SpriteRenderer>();

                ghostSprite.sprite = currentSprite;
                ghostSprite.flipX = GetComponent<SpriteRenderer>().flipX;


                StartCoroutine(GhostBoom(currentGhost, ghostSprite));
                ghostDelayTime = ghostDelay;
                
            }
        }
    }

    IEnumerator GhostBoom(GameObject currentGhost, SpriteRenderer ghostSprite)
    {
        ghostSprite.DOFade(0.5f, 0f);
        ghostSprite.DOFade(0, fadeDuration);

        
        yield return new WaitForSeconds(fadeDuration);
        Destroy(currentGhost);
    }



}
