using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rigidbody ������Ʈ�� �ݵ�� �߰��ȴ�
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    Vector3 velocity;       //�÷��̾ ������ ���� ǥ��
    Vector3 LookPoint;      //�÷��̾ �Ĵٺ� ���� ǥ��
    Rigidbody myRigidbody;  //�÷��̾��� �������� ���� ������Ʈ

    // Rigidbody ������Ʈ �Ҵ�
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// �÷��̾ ������ ���� ���� �Լ� (���� FixedUpdate���� ������)
    /// </summary>
    /// <param name="_velocity">�÷��̾ ������ ���� ����</param>
    public void Move(Vector3 _velocity, float speed)
    {
        velocity = _velocity;
    }

    /// <summary>
    /// �÷��̾��� ���� ����
    /// </summary>
    /// <param name="RayPoint">�÷��̾ �ٶ� ��ġ ����</param>
    public void LookAt(Vector3 RayPoint)
    {
        LookPoint = RayPoint;
        LookPoint.y = transform.position.y;
        transform.LookAt(LookPoint);
    }

    //�÷��̾��� �������� ����
    public void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
    }
}