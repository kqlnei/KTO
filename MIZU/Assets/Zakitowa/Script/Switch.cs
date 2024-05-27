using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject block; // 大きくしたいブロックオブジェクトをここにアサインします
    private bool isPressed = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPressed)
        {
            isPressed = true;
            IncreaseBlockSize();
        }
    }

    void IncreaseBlockSize()
    {
        if (block != null)
        {
            block.transform.localScale *= 2; // ブロックのサイズを2倍にします
        }
    }
}
