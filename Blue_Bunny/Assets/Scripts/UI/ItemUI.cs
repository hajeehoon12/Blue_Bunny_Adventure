using System.Collections;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    public GameObject MenuUI;//Test
    private bool isMenuOn = false;//Test

    private bool isOn = false;
    private bool isMoving = false;


    private void Update()
    {
        //Test
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuOn = !isMenuOn;
            MenuUI.SetActive(isMenuOn);

            if (isMenuOn) Time.timeScale = 0.0f;
            else Time.timeScale = 1.0f;
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!isMoving) isMoving = true;
            isOn = !isOn;
        }

        if(isMoving)
        {
            if(isOn)
            {
                gameObject.transform.position = Vector2.Lerp(gameObject.transform.position,
                    new Vector2(0.0f, gameObject.transform.position.y), Time.deltaTime * 10.0f);
            }
            else
            {
                gameObject.transform.position = Vector2.Lerp(gameObject.transform.position,
                    new Vector2(-500.0f, gameObject.transform.position.y), Time.deltaTime * 10.0f);
            }
        }

        if (gameObject.transform.position.x <= -499.0f || gameObject.transform.position.x >= -1.0f) isMoving = false;
    }
}
