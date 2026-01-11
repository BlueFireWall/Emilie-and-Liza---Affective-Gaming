using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class EmotionData
{
    public string emotion;
    public float difficulty_multiplier;
}

public class EmotionUDPClient : MonoBehaviour
{
    [Header("UDP Settings")]
    public string pythonIP = "127.0.0.1";
    public int pythonPort = 5005;
    public int unityPort = 5006;
    
    [Header("Game Difficulty")]
    public float currentDifficultyMultiplier = 1.0f;
    public string currentEmotion = "unknown";
    
    private UdpClient client;
    private Thread receiveThread;
    private bool isRunning = false;
    
    void Start()
    {
        InitializeUDP();
    }
    
    void InitializeUDP()
    {
        try
        {
            client = new UdpClient(unityPort);
            
            receiveThread = new Thread(new ThreadStart(ReceiveData));
            receiveThread.IsBackground = true;
            receiveThread.Start();
            
            isRunning = true;
            Debug.Log($"UDP Client started on port {unityPort}");
            
            InvokeRepeating("SendRequest", 1f, 2f);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error initializing UDP: {e.Message}");
        }
    }
    
    void SendRequest()
    {
        if (!isRunning) return;
        
        try
        {
            string message = "REQUEST_EMOTION";
            byte[] data = Encoding.UTF8.GetBytes(message);
            
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(pythonIP), pythonPort);
            client.Send(data, data.Length, remoteEndPoint);
            
            Debug.Log("Sent emotion request to Python");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error sending data: {e.Message}");
        }
    }
    
    void ReceiveData()
    {
        while (isRunning)
        {
            try
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref remoteEndPoint);
                
                string message = Encoding.UTF8.GetString(data);
                Debug.Log($"Received from Python: {message}");
                
                EmotionData emotionData = JsonUtility.FromJson<EmotionData>(message);
                
                UnityMainThreadDispatcher dispatcher = UnityMainThreadDispatcher.Instance();
                if (dispatcher != null)
                {
                    dispatcher.Enqueue(() =>
                    {
                        currentEmotion = emotionData.emotion;
                        currentDifficultyMultiplier = emotionData.difficulty_multiplier;
                        OnEmotionUpdated(emotionData);
                    });
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error receiving data: {e.Message}");
            }
        }
    }
    
    void OnEmotionUpdated(EmotionData data)
    {
        Debug.Log($"Emotion: {data.emotion}, Difficulty: {data.difficulty_multiplier}");
        ApplyDifficultyToGame(data.difficulty_multiplier);
    }
    
void ApplyDifficultyToGame(float multiplier)
{
    // Skip neutral emotions - keep whatever speed is currently set
    if (currentEmotion == "neutral")
    {
        Debug.Log($"Neutral emotion - maintaining current speed");
        return;
    }
    
    // Only adjust for non-neutral emotions
    Debug.Log($"Non-neutral emotion detected: {currentEmotion} - Adjusting difficulty to {multiplier}x");
    
    Movement movement = FindFirstObjectByType<Movement>();
    if (movement != null)
    {
        movement.emotionMultiplier = multiplier;
        Debug.Log($"Tetris speed adjusted! Emotion: {currentEmotion}, Multiplier: {multiplier}");
    }
    else
    {
        Debug.LogError("Movement script not found!");
    }
}
    
    void OnDestroy()
    {
        isRunning = false;
        
        if (receiveThread != null && receiveThread.IsAlive)
        {
            receiveThread.Abort();
        }
        
        if (client != null)
        {
            client.Close();
        }
        
        Debug.Log("UDP Client stopped");
    }
    
    void OnApplicationQuit()
    {
        OnDestroy();
    }
}