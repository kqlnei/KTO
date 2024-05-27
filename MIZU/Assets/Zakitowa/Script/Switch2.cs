using UnityEngine;
using System.Collections;

public class SwitchPlatform : MonoBehaviour
{
    public GameObject platform; // 大きくしたい足場オブジェクトをここにアサインします
    private Vector3 originalScale;
    public Vector3 enlargedScale = new Vector3(2, 2, 2); // 大きくしたいサイズを設定します
    public float shrinkDelay = 2.0f; // 足場が元のサイズに戻るまでの遅延時間

    private Coroutine shrinkCoroutine;

    void Start()
    {
        if (platform != null)
        {
            originalScale = platform.transform.localScale;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (shrinkCoroutine != null)
            {
                StopCoroutine(shrinkCoroutine);
            }
            platform.transform.localScale = enlargedScale;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shrinkCoroutine = StartCoroutine(ShrinkPlatformAfterDelay());
        }
    }

    IEnumerator ShrinkPlatformAfterDelay()
    {
        yield return new WaitForSeconds(shrinkDelay);
        platform.transform.localScale = originalScale;
    }
}
