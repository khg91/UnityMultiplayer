using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// PlayerControlGUI의 요청을 처리하기 위한 Move
    /// </summary>
    /// <param name="vector">해당 벡터로 이동(Translate)</param>
    public void Move(Vector3 vector)
    {
        transform.Translate(vector, Space.World);
    }

    /// <summary>
    /// 인풋을 통한 Player 이동 (input * moveSpeed * deltaTime)
    /// </summary>
    /// <param name="moveSpeed">이동속도</param>
    public void MoveByInput(float moveSpeed)
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));   // 인풋 벡터
        Vector3 moveVector = moveInput.normalized * moveSpeed * Time.deltaTime;                             // 실제 이동 벡터
        transform.Translate(moveVector, Space.World);
    }

    /// <summary>
    /// 인풋을 통한 Player 회전 (탑 다운 카메라로부터 마우스 레이캐스트 후 Transform.LookAt을 수행)
    /// </summary>
    /// <param name="topdownCamera">플레이어의 Y축 위로부터 아래로의 카메라</param>
    public void RotateByInput(Camera topdownCamera)
    {
        Ray mouseRay = topdownCamera.ScreenPointToRay(Input.mousePosition); // 카메라의 마우스 Ray
        Plane basePlane = new Plane(Vector3.up, transform.position);        // 레이캐스트 기준평면
        float rayDistance;                                                  // 스크린 스페이스 마우스로부터 기준평면 까지의 거리
        Vector3 point = new Vector3();

        if (basePlane.Raycast(mouseRay, out rayDistance))
        {
            point = mouseRay.GetPoint(rayDistance);
            point.y = transform.position.y;                                 // y 고정
        }

        transform.LookAt(point);
    }
}
