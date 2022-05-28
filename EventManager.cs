using System.Collections.Generic;
using UnityEngine;
public static class EventManager
{
    public delegate void EventReceiver(params object[] parameters);

    static Dictionary<string, EventReceiver> events = new Dictionary<string, EventReceiver>();

    public static void Suscribe(string eventType, EventReceiver function)
    {
        if (events.ContainsKey(eventType))
            events[eventType] += function;
        else
            events.Add(eventType, function);
    }

    public static void UnSuscribe(string eventType, EventReceiver function)
    {
        if (events.ContainsKey(eventType))
        {
            events[eventType] -= function;

            if (events[eventType] == null)
            {
                events.Remove(eventType);
            }
        }
    }

    public static void Trigger(string eventType, params object[] parameters)
    {
        if (events.ContainsKey(eventType))
        {
            Debug.Log("Se ejecuto el evento: " + eventType);

            events[eventType](parameters);
        }
        else
            Debug.LogWarning("Evento sin suscripciones");
    }

    public static void UnSuscribeAll()
    {
        events.Clear();
    }
}
