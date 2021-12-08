using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class NetworkPlayer : NetworkBehaviour
{
    // 외부 프로퍼티
    public float moveSpeed = 7f;

    // 내부 멤버
    PlayerMovement playerMovement;
    
    // 네트워크 멤버
    NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>();
    NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>();

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {

        if (IsLocalPlayer)  // 로컬 플레이어는 인풋을 통한 Translation 수행 후 RPC 호출
        {
            playerMovement.MoveByInput(moveSpeed);                          // 플레이어 이동 수행
            playerMovement.RotateByInput(Camera.main);                                 // 플레이어 회전 수행
            SetTransformServerRpc(transform.position, transform.rotation);  // 인풋으로 갱신된 Transform을 RPC로 전달
        }
        else                // 그 외에는 네트워크 멤버를 통한 transform 동기화
        {
            transform.position = networkPosition.Value;
            transform.rotation = networkRotation.Value;
        }
    }



    /// <summary>
    /// 네트워크 멤버 동기화 RPC
    /// </summary>
    /// <param name="newPosition">로컬플레이어로부터 받은 새 position</param>
    /// <param name="newRotation">로컬플레이어로부터 받은 새 rotation</param>
    /// <param name="rpcParams">RPC 파라미터</param>

    [ServerRpc]
    void SetTransformServerRpc(Vector3 newPosition, Quaternion newRotation, ServerRpcParams rpcParams = default)
    {
        networkPosition.Value = newPosition;
        networkRotation.Value = newRotation;
    }
}