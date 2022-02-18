using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> _events;

    private Dictionary<string, CustomEvent> _typedEvents;

    private static EventManager eventManager;

    public static EventManager Instance
    {
        get
        {
            if (eventManager==null)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.Log("No event Manager found!, there must be one Active GameObject with" +
                        "EventManager script attached to it.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
       
    }

    private void Init()
    {
        if (_events==null)
        {
            _events = new Dictionary<string, UnityEvent>();
        }
        if (_typedEvents==null)
        {
            _typedEvents = new Dictionary<string,CustomEvent>();
        }
    }

    //EMPTY EVENTS
    public static void AddListener(string eventName, UnityAction listener)
    {
        UnityEvent evt = null;
        if (Instance._events.TryGetValue(eventName,out evt))
        {
            evt.AddListener(listener);
        }
        else
        {
            evt = new UnityEvent();
            evt.AddListener(listener);
            Instance._events.Add(eventName, evt);
        }

    }
    public static void RemoveListener(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent evt = null;
        if (Instance._events.TryGetValue(eventName,out evt))
        {     
            evt.RemoveListener(listener);      
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent evt = null;
        if (Instance._events.TryGetValue(eventName,out evt))
        {
            evt.Invoke();
        }
    }

    //TYPED EVENTS

    public static void AddTypedListener(string eventName, UnityAction<CustomEventData> listener)
    {
       CustomEvent evt = null;
        if (Instance._typedEvents.TryGetValue(eventName,out evt))
        {
            evt.AddListener(listener);
        }
        else
        {
            evt = new CustomEvent();
            evt.AddListener(listener);
            Instance._typedEvents.Add(eventName, evt);
        }

    }

    public static void RemoveTypedListener(string eventName, UnityAction<CustomEventData> listener)
    {
        if (eventManager == null) return;
        CustomEvent evt = null;
        if (Instance._typedEvents.TryGetValue(eventName, out evt))
        {
            evt.RemoveListener(listener);
        }   
    }
    public static void TriggerTypedEvent(string eventName, CustomEventData data)
    {
       CustomEvent evt = null;
        if (Instance._typedEvents.TryGetValue(eventName, out evt))
             evt.Invoke(data);
    }
}
