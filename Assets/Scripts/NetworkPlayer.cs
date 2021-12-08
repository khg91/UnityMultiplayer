using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class NetworkPlayer : NetworkBehaviour
{
    // �ܺ� ������Ƽ
    public float moveSpeed = 7f;

    // ���� ���
    PlayerMovement playerMovement;
    
    // ��Ʈ��ũ ���
    NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>();
    NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>();

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {

        if (IsLocalPlayer)  // ���� �÷��̾�� ��ǲ�� ���� Translation ���� �� RPC ȣ��
        {
            playerMovement.MoveByInput(moveSpeed);                          // �÷��̾� �̵� ����
            playerMovement.RotateByInput(Camera.main);                                 // �÷��̾� ȸ�� ����
            SetTransformServerRpc(transform.position, transform.rotation);  // ��ǲ���� ���ŵ� Transform�� RPC�� ����
        }
        else                // �� �ܿ��� ��Ʈ��ũ ����� ���� transform ����ȭ
        {
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
}