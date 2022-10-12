using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouse : MonoBehaviour
{
    [SerializeField]
    private DirectionCalc DirectionCalc;

    [SerializeField]
    private MoveCube MoveCube;

    private Vector3 StartPoint;
    private Vector3 Normal;
    private Vector3 EndPoint;

    void OnMouseDown()
    {
        // プレイ中以外は無効にする
        if (GameManagerScript.status != GameManagerScript.GAME_STATUS.Play)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity);
        StartPoint = hit.point;
        Normal = hit.normal;
    }

    void OnMouseUp()
    {
        // プレイ中以外は無効にする
        if (GameManagerScript.status != GameManagerScript.GAME_STATUS.Play)
        {
            return;
        }

        // 平面を作る
        var plane = new Plane(Normal, StartPoint);

        // カメラからのRayを作成
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // rayと平面の交点を求める（交差しない可能性もある）
        if (plane.Raycast(ray, out float enter))
        {
            EndPoint = ray.GetPoint(enter);
            Debug.Log("end global result = " + EndPoint); //ワールド座標

            DirectionCalc.FlickCLC(StartPoint, EndPoint, Normal);
            DirectionCalc.FlickDirection dir = DirectionCalc.GetNowFlick();
            if (!dir.Equals(DirectionCalc.FlickDirection.NONE))
            {
                CubeEventControl.instance.EventListener(MoveCube);
                //if (CubeEventControl.instance.EventListener(MoveCube, dir))
                //{
                //    MoveCube.MoveCaller(dir);
                //}
            }
        }
        else
        {
            // rayと平面が交差しなかったので座標が取得できなかった
            EndPoint = Vector3.zero;
        }
    }
}
