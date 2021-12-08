using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rigidbody 컴포넌트가 반드시 추가된다
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    Vector3 velocity;       //플레이어가 움직일 방향 표시
    Vector3 LookPoint;      //플레이어가 쳐다볼 방향 표시
    Rigidbody myRigidbody;  //플레이어의 움직임을 위한 컴포넌트

    // Rigidbody 컴포넌트 할당
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 플레이어가 움직일 방향 설정 함수 (이후 FixedUpdate에서 움직임)
    /// </summary>
    /// <param name="_velocity">플레이어가 움직일 방향 벡터</param>
    public void Move(Vector3 _velocity, float speed)
    {
        velocity = _velocity;
    }

    /// <summary>
    /// 플레이어의 방향 설정
    /// </summary>
    /// <param name="RayPoint">플레이어가 바라볼 위치 벡터</param>
    public void LookAt(Vector3 RayPoint)
    {
        LookPoint = RayPoint;
        LookPoint.y = transform.position.y;
        transform.LookAt(LookPoint);
    }

    //플레이어의 움직임을 구현
    public void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
    }
}