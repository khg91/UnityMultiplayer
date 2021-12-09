using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NetworkPlayer))]
public class PlayerControlGUI : MonoBehaviour
{
    private void OnGUI()
    {
        const int sizeX = 300;
        const int sizeY = 30;
        int posX = (Screen.width - sizeX) / 2;
        int posY = Screen.height - sizeY;


        GUILayout.BeginArea(new Rect(posX, posY, sizeX, sizeY));
        GUILayout.BeginHorizontal();
        GUILayout.Button("ก่");
        GUILayout.Button("ก้");
        GUILayout.Button("ก็");
        GUILayout.Button("กๆ");
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
