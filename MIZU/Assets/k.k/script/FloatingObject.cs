using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    // 波の高さ
    public float waveHeight = 0.5f;
    // 波の速さ
    public float waveSpeed = 1.0f;
    // 波の長さ
    public float waveLength = 1.0f;

    // オブジェクトの初期位置
    private Vector3 startPosition;

    void Start()
    {
        // 初期位置を保存
        startPosition = transform.position;
    }

    void Update()
    {
        // 波の動きの計算
        float newY = startPosition.y + Mathf.Sin(Time.time * waveSpeed + transform.position.x / waveLength) * waveHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
