using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f; // プレイヤーの移動速度

    void Update()
    {
        // 水平方向の入力（AとD）
        float horizontal = Input.GetAxis("Horizontal");
        // 垂直方向の入力（WとS）
        float vertical = Input.GetAxis("Vertical");

        // 入力に基づいて移動方向を計算
        Vector2 direction = new Vector2(horizontal, vertical).normalized;

        // 移動処理
        if (direction.magnitude >= 0.1f)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
