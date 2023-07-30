using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArduinoBluetoothAPI;
using System;
using System.Threading.Tasks;


public class move3 : MonoBehaviour
{
    BluetoothHelper bluetoothHelper;
    string deviceName = "dpop"; // Replace with your HC-06 Bluetooth module name
    public Text receivedText;
    public float speed = 3.0f;
    public string receivedData;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            bluetoothHelper = BluetoothHelper.GetInstance(deviceName);
            //bluetoothHelper.setLengthBasedStream();
            bluetoothHelper.setTerminatorBasedStream("\n");
            bluetoothHelper.OnConnected += OnConnected;
            bluetoothHelper.OnConnectionFailed += OnConnectionFailed;
            bluetoothHelper.OnDataReceived += OnDataReceived;
            bluetoothHelper.Connect();

            Debug.Log("Start()");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float speed_delta = speed * Time.deltaTime;

        if (!bluetoothHelper.isConnected())
        {
            Debug.Log("Disconnected. Attempting to reconnect...");
            try
            {
                bluetoothHelper.Connect();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }


        if (Input.GetKey("left"))
        {
            transform.Translate(-speed_delta, 0, 0);
        }

        if (Input.GetKey("right"))
        {
            transform.Translate(speed_delta, 0, 0);
        }

        if (Input.GetKey("up"))
        {
            transform.Translate(0, speed_delta, 0);
        }

        if (Input.GetKey("down"))
        {
            transform.Translate(0, -speed_delta, 0);
        }
    }

    void OnDataReceived(BluetoothHelper helper)
    {
        if (bluetoothHelper == null)
            return;

        float speed_delta = speed * Time.deltaTime;
        Debug.Log("OnDataReceived()");
        receivedData = helper.Read();
        Debug.Log(receivedData);

        if (receivedData == "4")
        {
            transform.Translate(-speed_delta, 0, 0);
        }

        if (receivedData == "3")
        {
            transform.Translate(speed_delta, 0, 0);
        }

        if (receivedData == "2")
        {
            transform.Translate(0, speed_delta, 0);
        }

        if (receivedData == "1")
        {
            transform.Translate(0, -speed_delta, 0);
        }
    }

    void OnConnected(BluetoothHelper helper)
    {
        Debug.Log("OnConnected()");
        Debug.Log("CONNTECT  " + helper.getId() + " / " + bluetoothHelper.getId());
        try
        {
            helper.StartListening();
            Debug.Log("Bluetooth listening started!");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    void OnConnectionFailed(BluetoothHelper helper)
    {
        Debug.Log("OnConnectionFailed()");
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy()");
        if (bluetoothHelper != null)
        {
            bluetoothHelper.OnDataReceived -= OnDataReceived; // 이벤트 처리 중지
            bluetoothHelper.Disconnect();
        }
    }
}
