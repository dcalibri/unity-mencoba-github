using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MouseToggle : MonoBehaviour
{
    [Header("Key Settings")]
    public string Key;

    [Header("Event Settings")]
    public UnityEvent OnEvent;
    public UnityEvent OffEvent;
    private Toggle CurrentToggle;

    int ToInt(bool aValue)
    {
        if (aValue) return 1; 
               else return 0;
    }

    bool ToBool(int aValue)
    {
        if (aValue == 1) return true; else return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        CurrentToggle = GetComponent<Toggle>();

        if (PlayerPrefs.HasKey(Key))
        {
            int temp = PlayerPrefs.GetInt(Key);
            CurrentToggle.isOn = ToBool(temp);
            InvokeToggle();
        }

        CurrentToggle.onValueChanged.AddListener((value) =>
        {
            PlayerPrefs.SetInt(Key, ToInt(value));
            if (value)
            {
                OnEvent.Invoke();
            }
            else
            {
                OffEvent.Invoke();
            }
        });
    }

    public void InvokeToggle()
    {
        int temp = PlayerPrefs.GetInt(Key);
        PlayerPrefs.SetInt(Key, temp);
        if (temp == 1)
        {
            OnEvent.Invoke();
        }
        else
        {
            OffEvent.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
