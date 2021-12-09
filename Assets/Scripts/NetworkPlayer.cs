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

    private void Awake()
    {
        transform.position = new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        if (IsLocalPlayer) gameObject.AddComponent<PlayerControlGUI>(); // ���� �÷��̾�Ը� GUI ����
    }

    void Update()
    {
        if (IsLocalPlayer)  // ���� �÷��̾�� ��ǲ�� ���� Translation ���� �� RPC ȣ��
        {
            playerMovement.MoveByInput(moveSpeed);                          // �÷��̾� �̵� ����
            playerMovement.RotateByInput(Camera.main);                      // �÷��̾� ȸ�� ����
            SetTransformServerRpc(transform.position, transform.rotation);  // ��ǲ���� ���ŵ� Transform�� RPC�� ����
        }
        else                // �� �ܿ��� ��Ʈ��ũ ����� ���� transform ����ȭ
        {
            transform.position = networkPosition.Value;
            transform.rotation = networkRotation.Value;
        }
    }



    /// <summary>
    /// ��Ʈ��ũ ��� ����ȭ RPC. ���� �÷��̾�κ��� ���� ��ġ, ȸ���� ��Ʈ��ũ ����� �Ҵ��Ѵ�.
    /// </summary>

    [ServerRpc]
    private void SetTransformServerRpc(Vector3 newPosition, Quaternion newRotation, ServerRpcParams rpcParams = default)
    {
        networkPosition.Value = newPosition;
        networkRotation.Value = newRotation;
    }
}