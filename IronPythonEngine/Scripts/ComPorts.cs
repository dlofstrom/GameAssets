using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComPorts : MonoBehaviour
{
    private Dictionary<string, ComPort> comPorts;
    private Dictionary<string, GameObject> cables;
    public GameObject cablePrefab;

    public bool connected = false;

    // Use this for initialization
    void Start()
    {
        comPorts = new Dictionary<string, ComPort>();
        cables = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        connected = (comPorts.Count != 0);
    }

    public bool ConnectPort(ComPort port)
    {
        string portName = port.get_name();
        if (!comPorts.ContainsKey(portName))
        {
            comPorts.Add(portName, port);
            return true;
        }
        return false;
    }

    public void ConnectCable(ComPort port, GameObject start, GameObject end)
    {
        if (cablePrefab != null)
        {
            string portName = port.get_name();
            if (!cables.ContainsKey(portName))
            {
                GameObject cable = (GameObject)Instantiate(cablePrefab, start.transform.position, Quaternion.identity);
                cable.GetComponent<InitCable>().Connect(start, end);
                cables.Add(portName, cable);
            }
        }
    }

    public bool DisconnectPort(ComPort port)
    {
        string portName = port.get_name();
        return comPorts.Remove(portName);
    }

    public void DisconnectCable(ComPort port)
    {
        string portName = port.get_name();
        Destroy(cables[portName], 0.0f);
        cables.Remove(portName);
    }

    public void DisconnectAllPorts()
    {
        comPorts.Clear();
    }

    public bool IsConnected(ComPort port)
    {
        return comPorts.ContainsValue(port);
    }

    public string PrintPorts()
    {
        string s = "";
        foreach (string portName in comPorts.Keys)
        {
            s += portName + "\n";
        }
        return s;
    }

    public List<string> GetPorts()
    {
        List<string> l = new List<string>();
        foreach (string key in comPorts.Keys) l.Add(key);
        return l;
    }

    public ComPort GetPort(string portName)
    {
        if (comPorts.ContainsKey(portName))
        {
            return comPorts[portName];
        }
        return null;
    }

    public string PrintPortCommands(string portName)
    {
        if (comPorts.ContainsKey(portName))
        {
            return comPorts[portName].get_commands();
        }
        return "Port name " + portName + " does not exist!\n";
    }
}
