using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject target; //카메라의 타겟 
    public float moveSpeed;
    private Vector3 targetPosition;// 타겟의 현 위치

    [Header("Bound")]
    public BoxCollider2D bound;
    private Vector3 minBound; //박스콜라이더 최소
    private Vector3 maxBound; //박스콜라이더 최대

    private float halfWidth; //카메가 반너비
    private float halfHeight; //카메라 반높이

    private Camera theCamera; //카메라 반너비, 반높이 구할때 쓸 변수

    void Start()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            targetPosition.Set(target.transform.position.x, this.transform.position.y, this.transform.position.z);
            //lerp는 a값과 b값 사이의 선형보간으로 중간값 리턴 ex) ( 1, 10, 0.5f) =5
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);

            this.transform.position = new Vector3(clampedX, this.transform.position.y, this.transform.position.z);
        }
    }
    public void UpCameraSpeed()
    {
        moveSpeed += 0.2f;
    }
    public void ResetCameraSpeed()
    {
        moveSpeed = 1.3f;
    }
}
