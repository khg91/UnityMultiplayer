using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    // �ܺ� ������Ƽ
    public float moveSpeed = 7f;
    
    // ��Ʈ��ũ ���
    NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>();
    NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>();

    void Update()
    {

        if (IsLocalPlayer)  // ���� �÷��̾�� ��ǲ�� ���� Translation ���� �� RPC ȣ��
        {
            MoveByInput();                                                  // �÷��̾� �̵� ����
            RotateByInput();                                                // �÷��̾� ȸ�� ����
            SetTransformServerRpc(transform.position, transform.rotation);  // ��ǲ���� ���ŵ� Transform�� RPC�� ����
        }
        else                // �� �ܿ��� ��Ʈ��ũ ����� ���� transform ����ȭ
        {
            networkPosition.IsDirty();
            transform.position = networkPosition.Value;
            transform.rotation = networkRotation.Value;
        }
    }



    /// <summary>
    /// ��Ʈ��ũ ��� ����ȭ RPC
    /// </summary>
    /// <param name="newPosition">�����÷��̾�κ��� ���� �� position</param>
    /// <param name="newRotation">�����÷��̾�κ��� ���� �� rotation</param>
    /// <param name="rpcParams">RPC �Ķ����</param>

    [ServerRpc]
    void SetTransformServerRpc(Vector3 newPosition, Quaternion newRotation, ServerRpcParams rpcParams = default)
    {
        networkPosition.Value = newPosition;
        networkRotation.Value = newRotation;
    }



    
    /// <summary>
    /// ��ǲ�� ���� Player �̵�
    /// </summary>
    void MoveByInput()
    {
        transform.Translate(GetMoveVectorFromInput(), Space.World);
    }

    /// <summary>
    /// ��ǲ�� ���� Player ȸ��
    /// </summary>
    void RotateByInput()
    {
        transform.LookAt(GetLookAtPointFromInput());
    }
    
    /// <summary>
    /// ��ǲ�� ���� �̵����� ���ϱ�
    /// </summary>
    /// <returns>input * moveSpeed * deltaTime</returns>
    private Vector3 GetMoveVectorFromInput()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));   // ��ǲ ����
        Vector3 moveVector = moveInput.normalized * moveSpeed * Time.deltaTime;                             // ���� �̵� ����
        return moveVector;
    }

    /// <summary>
    /// ž �ٿ� ī�޶�κ��� ���콺 ����ĳ��Ʈ �� Transform.LookAt�� ������ ����Ʈ ���
    /// </summary>
    /// <returns>LookAt ����Ʈ</returns>
    private Vector3 GetLookAtPointFromInput()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);   // ī�޶�(����)�� ���콺 Ray
        Plane basePlane = new Plane(Vector3.up, Vector3.zero);              // ����ĳ��Ʈ ���عٴ�
        float rayDistance;                                                  // ��ũ�� �����̽� ���콺�κ��� ���عٴ� ������ �Ÿ�
        Vector3 point = new Vector3();

        if (basePlane.Raycast(mouseRay, out rayDistance))
        {
            point = mouseRay.GetPoint(rayDistance);
            point.y = transform.position.y;                                 // y ����
        }
        
        return point;
    }
}