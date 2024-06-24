using UnityEngine;

public class StorePortal : Portal
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Define.PLAYER_TAG))
        {
            if (!GameManager.Instance.isInStore)
            {
                Debug.Log("상점으로 이동합니다");
                AudioManager.instance.PlaySFX("Warp", 0.2f);
                GameManager.Instance.GotoStoreMap();
            }
            else
            {
                Debug.Log("원래 맵으로 돌아갑니다.");
                AudioManager.instance.PlaySFX("Warp", 0.2f);
                GameManager.Instance.ReturnToMap();
            }
        }
    }
}
