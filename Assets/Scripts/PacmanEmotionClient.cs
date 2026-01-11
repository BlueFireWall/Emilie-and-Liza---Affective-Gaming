using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class PacmanEmotionClient : MonoBehaviour
{
    [Header("UDP Settings")]
    public string pythonIP = "127.0.0.1";
    public int pythonPort = 5005;
    public int unityPort = 5006;

    [Header("Pacman Difficulty")]
    public string currentEmotion = "neutral";
    public float difficultyMultiplier = 1.0f;

    private UdpClient client;
    private Thread receiveThread;
    private bool isRunning = false;

    // Data received from UDP, to be applied on main thread
    private float queuedMultiplier = 1f;
    private bool hasNewData = false;

    void Start()
    {
        InitializeUDP();
    }

    void InitializeUDP()
    {
        try
        {
            client = new UdpClient(unityPort);

            receiveThread = new Thread(ReceiveData);
            receiveThread.IsBackground = true;
            receiveThread.Start();

            isRunning = true;
            InvokeRepeating("RequestEmotion", 1f, 1f);

            Debug.Log("Pacman UDP client started on port " + unityPort);
        }
        catch (Exception e)
        {
            Debug.LogError("UDP init error: " + e.Message);
        }
    }

    void RequestEmotion()
    {
        if (!isRunning) return;

        try
        {
            string message = "REQUEST_EMOTION";
            byte[] data = Encoding.UTF8.GetBytes(message);
            IPEndPoint pythonEndPoint = new IPEndPoint(IPAddress.Parse(pythonIP), pythonPort);
            client.Send(data, data.Length, pythonEndPoint);
        }
        catch (Exception e)
        {
            Debug.LogError("Error sending UDP request: " + e.Message);
        }
    }

    void ReceiveData()
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        while (isRunning)
        {
            try
            {
                byte[] data = client.Receive(ref remoteEndPoint);
                string message = Encoding.UTF8.GetString(data);

                EmotionData emotionData = JsonUtility.FromJson<EmotionData>(message);

                // Store values to apply on main thread
                queuedMultiplier = emotionData.difficulty_multiplier;
                currentEmotion = emotionData.emotion;
                hasNewData = true;
            }
            catch (Exception e)
            {
                Debug.LogError("Error receiving UDP data: " + e.Message);
            }
        }
    }

    void Update()
    {
        // Only update ghost speeds on main thread
        if (hasNewData)
        {
            hasNewData = false;
            ApplyEmotionDifficulty();
        }
    }

    void ApplyEmotionDifficulty()
    {
        GhostController[] ghosts = GameObject.FindObjectsByType<GhostController>(FindObjectsSortMode.None);

        foreach (GhostController g in ghosts)
        {
            g.speed = g.NormalSpeed * queuedMultiplier;
        }

        difficultyMultiplier = queuedMultiplier;
        Debug.Log($"Emotion: {currentEmotion}, Difficulty multiplier: {difficultyMultiplier}");
    }

    void OnDestroy()
    {
        isRunning = false;
        if (receiveThread != null && receiveThread.IsAlive)
            receiveThread.Abort();
        if (client != null)
            client.Close();
    }
}
