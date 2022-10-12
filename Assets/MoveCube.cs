using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCube : MonoBehaviour
{
	//public CubeEventControl CubeEventControl;

	[SerializeField]
	DirectionCalc DirectionCalc;

	[SerializeField]
	JudgePattern JudgePtn;

	//[SerializeField]
 //   private GameObject testOBJ;

	[SerializeField]
    private float cubeAngle = 16f; //ここを変えると回転速度が変わる

	public DirectionCalc.FlickDirection FlickDir
    {
		get
        {
			return DirectionCalc.GetNowFlick();
        }
    }

	private int cnt;

	Vector3 rotatePoint = Vector3.zero;  //回転の中心
	Vector3 rotateAxis = Vector3.zero;   //回転軸
	//float cubeAngle = 0f;                //回転角度

	float cubeSizeHalf;                  //キューブの大きさの半分
	bool isRotate = false;               //回転中に立つフラグ。回転中は入力を受け付けない

	void Start()
	{
		cubeSizeHalf = transform.localScale.x / 2f;
		Application.targetFrameRate = 30; // 30FPSに設定
	}

	// Update is called once per frame
	void Update()
	{
		//DirectionCalc.FlickDirection dir = directionCalc.GetNowFlick();
  //      if (!dir.Equals(DirectionCalc.FlickDirection.NONE))
  //      {
		//	CubeEventControl.instance.EventListener(dir);
		//	MoveCaller(dir);
		//}

		//      // 文字表示処理
		//      _testText.text = "Flick:" + Input.GetNowFlick().ToString();
		//      _testText.text += "\nSwipe:" + Input.GetNowSwipe().ToString();
		//      _testText.text += "\nRange:" + Input.GetSwipeRange();

		//_testText_dir.text = "Direction_UP:" + transform.up;
		//_testText_dir.text += "\nDirection_FW:" + transform.forward;
		//_testText_dir.text += "\nDirection_RG:" + transform.right;

		//_testText_ptn.text = "Pattern:" + JudgePtn.GetPattern().ToString();
	}

	public void Exec()
    {
		MoveCaller(FlickDir);
	}

	private void MoveCaller(DirectionCalc.FlickDirection flickDirection)
	{
		//回転中は入力を受け付けない
		if (isRotate)
			return;

		Debug.Log("flickDirection: " + flickDirection);
        switch (flickDirection)
        {
            case DirectionCalc.FlickDirection.UP:
				rotatePoint = transform.position + new Vector3(0f, -cubeSizeHalf, cubeSizeHalf);
				rotateAxis = new Vector3(1, 0, 0);
				//transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + _work);
				//transform.Rotate(0, 90.0f, 0, Space.World);
				break;
            case DirectionCalc.FlickDirection.DOWN:
				rotatePoint = transform.position + new Vector3(0f, -cubeSizeHalf, -cubeSizeHalf);
                rotateAxis = new Vector3(-1, 0, 0);
				//transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - _work);
				break;
            case DirectionCalc.FlickDirection.LEFT:
				rotatePoint = transform.position + new Vector3(-cubeSizeHalf, -cubeSizeHalf, 0f);
                rotateAxis = new Vector3(0, 0, 1);
				//transform.localPosition = new Vector3(transform.localPosition.x - _work, transform.localPosition.y);
				break;
            case DirectionCalc.FlickDirection.RIGHT:
				rotatePoint = transform.position + new Vector3(cubeSizeHalf, -cubeSizeHalf, 0f);
                rotateAxis = new Vector3(0, 0, -1);
				//transform.localPosition = new Vector3(transform.localPosition.x + _work, transform.localPosition.y);
				break;
			case DirectionCalc.FlickDirection.VERT_LEFT:
				rotatePoint = transform.position;
				rotateAxis = new Vector3(0, 1, 0);
				//transform.localPosition = new Vector3(transform.localPosition.x - _work, transform.localPosition.y);
				break;
			case DirectionCalc.FlickDirection.VERT_RIGHT:
				rotatePoint = transform.position;
				rotateAxis = new Vector3(0, -1, 0);
				//transform.localPosition = new Vector3(transform.localPosition.x - _work, transform.localPosition.y);
				break;
			default:
				return;
		}

		// 入力がない時はコルーチンを呼び出さないようにする
		//if (rotatePoint == Vector3.zero)
		//	return;
		StartCoroutine(Move());
	}

	IEnumerator Move()
	{
		//// 文字表示処理
		//_testText.text = "Flick:" + Input.GetNowFlick().ToString();
		//_testText.text += "\nSwipe:" + Input.GetNowSwipe().ToString();
		//_testText.text += "\nRange:" + Input.GetSwipeRange();

		//_testText_ptn.text = "Pattern:" + JudgePtn.GetPattern().ToString();

		//回転中のフラグを立てる
		isRotate = true;

		//回転処理
		float sumAngle = 0f; //angleの合計を保存
		while (sumAngle < 90f)
		{
			//cubeAngle = 15f; //ここを変えると回転速度が変わる
			sumAngle += cubeAngle;

			// 90度以上回転しないように値を制限
			if (sumAngle > 90f)
			{
				cubeAngle -= sumAngle - 90f;
			}
			transform.RotateAround(rotatePoint, rotateAxis, cubeAngle);

			//yield return new WaitForSeconds(3);
			yield return null;
		}

		//回転中のフラグを倒す
		isRotate = false;
		rotatePoint = Vector3.zero;
		rotateAxis = Vector3.zero;

		//JudgePtn.Judge();

		yield break;
	}

	public bool IsRotate()
    {
		return isRotate;
    }
}
