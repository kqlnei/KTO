using UnityEngine;
using System.Collections.Generic;

public class DrawObjects : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject drawObjectPrefab;  // 置きたいオブジェクトのPrefab
    public float minDistance = 0.1f;     // オブジェクトを配置する最小距離
    private List<GameObject> drawObjects;
    private Vector3 lastPosition;
    public GameObject parentObject; // 指定された親オブジェクト

    void Start()
    {
        drawObjects = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // カメラからの適切な距離を設定
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0f;

            if (drawObjects.Count == 0 || Vector3.Distance(worldPosition, lastPosition) > minDistance)
            {
                GameObject drawObject = Instantiate(drawObjectPrefab, worldPosition, Quaternion.identity);
                drawObjects.Add(drawObject);
                lastPosition = worldPosition;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            // すべてのメッシュを一つにまとめる
            CombineMeshes(parentObject);
        }
    }

    void CombineMeshes(GameObject parent)
    {
        // 新しいオブジェクトを作成
        GameObject combinedObject = new GameObject("CombinedObject");

        // メッシュフィルターとレンダラーを追加
        MeshFilter combinedMeshFilter = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer combinedMeshRenderer = combinedObject.AddComponent<MeshRenderer>();

        // メッシュの統合
        MeshFilter[] meshFilters = new MeshFilter[drawObjects.Count];
        for (int i = 0; i < drawObjects.Count; i++)
        {
            meshFilters[i] = drawObjects[i].GetComponent<MeshFilter>();
        }

        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);
        combinedMeshFilter.mesh = combinedMesh;

        // マテリアルの設定
        combinedMeshRenderer.material = drawObjects[0].GetComponent<MeshRenderer>().sharedMaterial;

        // コライダーの統合
        MeshCollider combinedCollider = combinedObject.AddComponent<MeshCollider>();
        combinedCollider.sharedMesh = combinedMesh;
        combinedCollider.convex = true;  // Convexに設定

        // 親オブジェクトが指定されている場合
        if (parent != null)
        {
            // 親オブジェクトに設定し、ワールド座標を一致させる
            combinedObject.transform.SetParent(parent.transform, false);
            combinedObject.transform.position = parent.transform.position;
            combinedObject.transform.rotation = parent.transform.rotation;
        }
        else
        {
            Debug.LogWarning("Parent object is not specified. Combined object will be placed in the scene root.");
        }

        // 以前のオブジェクトを削除
        foreach (var obj in drawObjects)
        {
            Destroy(obj);
        }
        drawObjects.Clear();

        // 新しいオブジェクトをリストに追加
        drawObjects.Add(combinedObject);
    }
}
