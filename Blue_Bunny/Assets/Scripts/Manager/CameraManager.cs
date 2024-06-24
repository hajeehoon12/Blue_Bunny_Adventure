using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{


    public static CameraManager Instance;

    public Transform _player;
    public MeshRenderer render;

    public GameObject map;

    private Vector2 _firstPos;

    [SerializeField]
    Vector3 cameraPosition;

    [SerializeField]
    Vector2 center;
    
    public Vector2 mapSize;

    float screenHeight;
    float screenWidth;

    // 진동할 카메라의 transform
    public Transform shakeCamera;
    // 회전시킬 것인지를 판단할 변수
    public bool shakeRotate = false;
    // 초기 좌표와 회전값을 저장할 변수
    public Vector3 originPos;
    public Quaternion originRot;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        shakeCamera = transform;
    }

    private void Start()
    {
        _firstPos = _player.position;

        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Screen.width / Screen.height;

        //mapSize = map.GetComponent<Collider2D>().bounds.extents + new Vector3(0, 2, 0);

        


    }

    private void Update()
    {

        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 3f, -10);
        render.material.mainTextureOffset = new Vector2((_firstPos.x - _player.position.x) / 300, 0);

        LimitCameraArea();
    }

    void LimitCameraArea()
    {
        
        float borderx = mapSize.x - screenWidth;
        float clampX = Mathf.Clamp(transform.position.x, -borderx + center.x, borderx + center.x);

        float bordery = mapSize.y - screenHeight;
        float clampY = Mathf.Clamp(transform.position.y, -bordery + center.y, bordery + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }

    public void MakeCameraShake(Vector3 cameraPos, float duration, float Position, float Rotation)
    {
        StartCoroutine(Shake(cameraPos, duration, Position, Rotation));
    }


    public IEnumerator Shake(Vector3 cameraPos, float duration = 5f, float magnitudePos = 0.03f, float magnitudeRot = 0.1f)
    {
   
        originPos = transform.position;
        originRot = transform.rotation;

        // 지나간 시간을 누적할 변수
        float passTime = 0.0f;
        // 진동시간동안 루프 돌림
        while (passTime < duration)
        {
            // 불규칙한 위치를 산출
            Vector3 shakePos = Random.insideUnitCircle;
            // 카메라의 위치를 변경
            shakePos.z = originPos.z / magnitudePos;
            shakeCamera.localPosition = shakePos * magnitudePos + cameraPos;

            // 불규칙한 회전을 사용할 경우
            if (shakeRotate)
            {
                // 펄린노이즈함수로 불규칙한 회전값 생성
                Vector3 shakeRot = new Vector3(0, 0, Mathf.PerlinNoise(Time.time * magnitudeRot, 0.0f));
                // 카메라 회전값 변경
                shakeCamera.localRotation = Quaternion.Euler(shakeRot);
            }

            // 진동시간 누적
            passTime += Time.deltaTime;
            yield return null;
        }
        // 진동 후 원상복구
        shakeCamera.localPosition = originPos;
        shakeCamera.localRotation = originRot;

    }

}
