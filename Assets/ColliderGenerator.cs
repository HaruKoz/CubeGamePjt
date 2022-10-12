using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderGenerator : MonoBehaviour
{
    //接触検知用スフィアのゲームオブジェクト名
    //接触検知用スフィアはフィールドの左上から配置順に番号をふる
    //例：SphereInstance(00), SphereInstance(01), SphereInstance(02), …
    public string SphereInstanceName = "SphereInstance";

    [SerializeField]
    private GameObject Plane;

    [SerializeField]
    private GameObject CubePrefab;

    [SerializeField]
    private GameObject SpherePrefab;

    private GameObject[] Sphere;

    // Start is called before the first frame update
    void Start()
    {
        float cubeScale = CubePrefab.transform.localScale.x;
        float cubeScaleHalf = cubeScale / 2.0f;
        float sphereRadius = cubeScale / 4.0f;
        int planeScale = Mathf.FloorToInt(Plane.transform.localScale.x * 10.0f);
        Vector3 pos;
        Sphere = new GameObject[planeScale * planeScale];
        SphereCollider sphereCollider;

        //プレハブのサイズを念の為設定
        SpherePrefab.transform.localScale = new Vector3(sphereRadius * 2.0f, sphereRadius * 2.0f, sphereRadius * 2.0f);
        sphereCollider = SpherePrefab.GetComponent<SphereCollider>();
        sphereCollider.radius = sphereRadius;

        //スタート地点
        pos.x = (planeScale / -2.0f) + cubeScaleHalf;
        pos.y = 0;
        pos.z = -pos.x;

        int cnt = 0;
        for (int i = 0; i < planeScale; i++)
        {
            pos.x = (planeScale / -2.0f) + cubeScaleHalf;
            for (int j = 0; j < planeScale; j++)
            {
                Sphere[cnt] = Instantiate(SpherePrefab, pos, Quaternion.identity);
                Sphere[cnt].name = SphereInstanceName + "(" + cnt.ToString().PadLeft(2, '0') + ")";
                pos.x += cubeScale;
                cnt++;
            }
            pos.z -= cubeScale;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void GetCubeByOrderPlacement()
    {

    }
}
