using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerControlGUI : MonoBehaviour
{
    PlayerMovement playerMovement;

    private void OnGUI()
    {
        const int sizeX = 300;
        const int sizeY = 30;
        int posX = (Screen.width - sizeX) / 2;
        int posY = Screen.height - sizeY;


        GUILayout.BeginArea(new Rect(posX, posY, sizeX, sizeY));
        GUILayout.BeginHorizontal();
        // �� ��ư�� ���� PlayerMovement.Move ����
        if (GUILayout.Button("��")) playerMovement.Move(new Vector3(0f, 0f, 0.5f));
        if (GUILayout.Button("��")) playerMovement.Move(new Vector3(0f, 0f, -0.5f));
        if (GUILayout.Button("��")) playerMovement.Move(new Vector3(-0.5f, 0f, 0f));
        if (GUILayout.Button("��")) playerMovement.Move(new Vector3(0.5f, 0f, 0f));
        // NetworkPlayer���� RPC�� �������ֱ� ������ ���⼭ ���� ������ ��ġ����ȭ�� ���� ����
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
    
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
}
