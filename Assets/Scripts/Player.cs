using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    // 외부 프로퍼티
    public float moveSpeed = 7f;
    
    // 네트워크 멤버
    NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>();
    NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>();

    void Update()
    {

        if (IsLocalPlayer)  // 로컬 플레이어는 인풋을 통한 Translation 수행 후 RPC 호출
        {
            MoveByInput();                                                  // 플레이어 이동 수행
            RotateByInput();                                                // 플레이어 회전 수행
            SetTransformServerRpc(transform.position, transform.rotation);  // 인풋으로 갱신된 Transform을 RPC로 전달
        }
        else                // 그 외에는 네트워크 멤버를 통한 transform 동기화
        {
            networkPosition.IsDirty();
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



    
    /// <summary>
    /// 인풋을 통한 Player 이동
    /// </summary>
    void MoveByInput()
    {
        transform.Translate(GetMoveVectorFromInput(), Space.World);
    }

    /// <summary>
    /// 인풋을 통한 Player 회전
    /// </summary>
    void RotateByInput()
    {
        transform.LookAt(GetLookAtPointFromInput());
    }
    
    /// <summary>
    /// 인풋을 통해 이동벡터 구하기
    /// </summary>
    /// <returns>input * moveSpeed * deltaTime</returns>
    private Vector3 GetMoveVectorFromInput()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));   // 인풋 벡터
        Vector3 moveVector = moveInput.normalized * moveSpeed * Time.deltaTime;                             // 실제 이동 벡터
        return moveVector;
    }

    /// <summary>
    /// 탑 다운 카메라로부터 마우스 레이캐스트 후 Transform.LookAt을 수행할 포인트 계산
    /// </summary>
    /// <returns>LookAt 포인트</returns>
    private Vector3 GetLookAtPointFromInput()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);   // 카메라(메인)의 마우스 Ray
        Plane basePlane = new Plane(Vector3.up, Vector3.zero);              // 레이캐스트 기준바닥
        float rayDistance;                                                  // 스크린 스페이스 마우스로부터 기준바닥 까지의 거리
        Vector3 point = new Vector3();

        if (basePlane.Raycast(mouseRay, out rayDistance))
        {
            point = mouseRay.GetPoint(rayDistance);
            point.y = transform.position.y;                                 // y 고정
        }
        
        return point;
    }
}