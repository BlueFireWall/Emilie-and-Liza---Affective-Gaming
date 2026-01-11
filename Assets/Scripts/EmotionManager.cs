using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

public class EmotionManager : MonoBehaviour
{
    // ===============================
    // UDP setup
    // ===============================
    private UdpClient udpClient;
    private Thread receiveThread;
    private int port = 5005;
    private string currentEmotion = "neutral";

    // ===============================
    // Emotion smoothing
    // ===============================
    private Queue<string> recentEmotions = new Queue<string>();
    private int smoothFrames = 5;

    // ===============================
    // Initialization
    // ===============================
    void Start()
    {
        udpClient = new UdpClient(port);
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
        Debug.Log("🎧 EmotionManager started, listening on UDP port " + port);
    }

    // ===============================
    // Receive data from Python
    // ===============================
    private void ReceiveData()
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
        try
        {
            while (true)
            {
                byte[] data = udpClient.Receive(ref remoteEndPoint);
                string emotion = Encoding.UTF8.GetString(data);
                currentEmotion = emotion;
            }
        }
        catch (SocketException ex)
        {
            Debug.Log("UDP receive error: " + ex.Message);
        }
    }

    // ===============================
    // Update — smooth and debug
    // ===============================
    void Update()
    {
        if (!string.IsNullOrEmpty(currentEmotion))
        {
            if (recentEmotions.Count >= smoothFrames)
                recentEmotions.Dequeue();
            recentEmotions.Enqueue(currentEmotion);
        }

        // Print every 60 frames (~1 second at 60 FPS)
        if (Time.frameCount % 60 == 0)
        {
            string smoothed = GetSmoothedEmotion();
            Debug.Log("🧠 Smoothed emotion: " + smoothed);
        }
    }

    // ===============================
    // Calculate most frequent recent emotion
    // ===============================
    private string GetSmoothedEmotion()
    {
        var grouped = new Dictionary<string, int>();
        foreach (string e in recentEmotions)
        {
            if (!grouped.ContainsKey(e))
                grouped[e] = 0;
            grouped[e]++;
        }

        string mostCommon = "neutral";
        int maxCount = 0;
        foreach (var kv in grouped)
        {
            if (kv.Value > maxCount)
            {
                mostCommon = kv.Key;
                maxCount = kv.Value;
            }
        }

        return mostCommon;
    }

    // ===============================
    // Graceful exit
    // ===============================
    void OnApplicationQuit()
    {
        if (receiveThread != null && receiveThread.IsAlive)
            receiveThread.Abort();
        if (udpClient != null)
            udpClient.Close();
        Debug.Log("🛑 EmotionManager stopped.");
    }

    // ===============================
    // Public accessor for other scripts
    // ===============================
    public string GetCurrentSmoothedEmotion()
    {
        return GetSmoothedEmotion();
    }
}
