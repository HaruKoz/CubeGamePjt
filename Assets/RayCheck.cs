using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCheck : MonoBehaviour
{
    //[SerializeField]
    //private GameObject cube;

    private Collider col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
    }

    public bool GetStartPosition(Vector3 mousePosition, out RaycastHit hit, out Vector3 pos)
    {
        bool beRay;
        Ray ray = new Ray();
        ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity) && hit.collider == col)
        {
            beRay = true;
            Debug.Log("start global hitPoint = " + hit.point); //ワールド座標
            pos = hit.point;
        }
        else
        {
            beRay = false;
            pos = Vector3.zero;
        }

        return beRay;
    }

    public bool GetEndPosition(Vector3 mousePosition, Vector3 origin, Vector3 normal, out Vector3 result)
    {
        // 平面を作る
        var plane = new Plane(normal, origin);
        // カメラからのRayを作成
        var ray = Camera.main.ScreenPointToRay(mousePosition);
        // rayと平面の交点を求める（交差しない可能性もある）
        if (plane.Raycast(ray, out float enter))
        {
            result = ray.GetPoint(enter);
            Debug.Log("end global result = " + result); //ワールド座標
            return true;
        }
        else
        {
            // rayと平面が交差しなかったので座標が取得できなかった
            result = Vector3.zero;
            return false;
        }
    }
}
