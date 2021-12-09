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

    private void Awake()
    {
        transform.position = new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        if (IsLocalPlayer) gameObject.AddComponent<PlayerControlGUI>(); // 로컬 플레이어에게만 GUI 생성
    }

    void Update()
    {
        if (IsLocalPlayer)  // 로컬 플레이어는 인풋을 통한 Translation 수행 후 RPC 호출
        {
            playerMovement.MoveByInput(moveSpeed);                          // 플레이어 이동 수행
            playerMovement.RotateByInput(Camera.main);                      // 플레이어 회전 수행
            SetTransformServerRpc(transform.position, transform.rotation);  // 인풋으로 갱신된 Transform을 RPC로 전달
        }
        else                // 그 외에는 네트워크 멤버를 통한 transform 동기화
        {
            transform.position = networkPosition.Value;
            transform.rotation = networkRotation.Value;
        }
    }



    /// <summary>
    /// 네트워크 멤버 동기화 RPC. 로컬 플레이어로부터 받은 위치, 회전을 네트워크 멤버에 할당한다.
    /// </summary>

    [ServerRpc]
    private void SetTransformServerRpc(Vector3 newPosition, Quaternion newRotation, ServerRpcParams rpcParams = default)
    {
        networkPosition.Value = newPosition;
        networkRotation.Value = newRotation;
    }
}