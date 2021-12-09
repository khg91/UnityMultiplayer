using Unity.Netcode;
using UnityEngine;

public class NetworkControlGUI : MonoBehaviour
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            StartButtons();
        else
            StatusLabels();

        GUILayout.EndArea();
    }

    /// <summary>
    /// 서버를 만들거나 접속할 수 있는 GUI
    /// </summary>
    static void StartButtons()
    {
        if (GUILayout.Button("Host"))
            NetworkManager.Singleton.StartHost();

        if (GUILayout.Button("Client"))
            NetworkManager.Singleton.StartClient();

        if (GUILayout.Button("Server"))
            NetworkManager.Singleton.StartServer();
    }

    /// <summary>
    /// 서버를 만들거나 접속한 후 서버정보 간략 표시
    /// </summary>
    static void StatusLabels()
    {
        var mode = NetworkManager.Singleton.IsHost ? "Host"
            : (NetworkManager.Singleton.IsServer ? "Server" : "Client");

        GUILayout.Label("Transport: " + NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }
}