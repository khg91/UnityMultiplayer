using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// PlayerControlGUI�� ��û�� ó���ϱ� ���� Move
    /// </summary>
    /// <param name="vector">�ش� ���ͷ� �̵�(Translate)</param>
    public void Move(Vector3 vector)
    {
        transform.Translate(vector, Space.World);
    }

    /// <summary>
    /// ��ǲ�� ���� Player �̵� (input * moveSpeed * deltaTime)
    /// </summary>
    /// <param name="moveSpeed">�̵��ӵ�</param>
    public void MoveByInput(float moveSpeed)
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));   // ��ǲ ����
        Vector3 moveVector = moveInput.normalized * moveSpeed * Time.deltaTime;                             // ���� �̵� ����
        transform.Translate(moveVector, Space.World);
    }

    /// <summary>
    /// ��ǲ�� ���� Player ȸ�� (ž �ٿ� ī�޶�κ��� ���콺 ����ĳ��Ʈ �� Transform.LookAt�� ����)
    /// </summary>
    /// <param name="topdownCamera">�÷��̾��� Y�� ���κ��� �Ʒ����� ī�޶�</param>
    public void RotateByInput(Camera topdownCamera)
    {
        Ray mouseRay = topdownCamera.ScreenPointToRay(Input.mousePosition); // ī�޶��� ���콺 Ray
        Plane basePlane = new Plane(Vector3.up, transform.position);        // ����ĳ��Ʈ �������
        float rayDistance;                                                  // ��ũ�� �����̽� ���콺�κ��� ������� ������ �Ÿ�
        Vector3 point = new Vector3();

        if (basePlane.Raycast(mouseRay, out rayDistance))
        {
            point = mouseRay.GetPoint(rayDistance);
            point.y = transform.position.y;                                 // y ����
        }

        transform.LookAt(point);
    }
}
