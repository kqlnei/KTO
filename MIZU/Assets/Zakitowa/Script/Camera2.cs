using UnityEngine;

public class Camera2 : MonoBehaviour
{
    public Transform player;    // プレイヤーオブジェクトのTransform
    public Vector3 offset;      // カメラとプレイヤー間のオフセット

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
