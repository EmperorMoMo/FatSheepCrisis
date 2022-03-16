using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter : MonoBehaviour
{
    private static Dictionary<EventType, Delegate> m_EventTable = new Dictionary<EventType, Delegate>();
    private static void OnListenerAdding(EventType eventType,Delegate action)
    {
        if (!m_EventTable.ContainsKey(eventType))
        {
            m_EventTable.Add(eventType, null);
        }
        Delegate d = m_EventTable[eventType];
        if (d != null && d.GetType() != action.GetType())
        {
            throw new Exception(string.Format("添加监听错误：尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1}，要添加的委托类型为{2}", eventType, d.GetType(), action.GetType()));
        }
    }
    private static void OnListenerRemoving(EventType eventType,Delegate action)
    {
        if (m_EventTable.ContainsKey(eventType))
        {
            Delegate d = m_EventTable[eventType];
            if (d == null)
            {
                throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托", eventType));
            }
            else if (d.GetType() != action.GetType())
            {
                throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除不同类型的委托，当前委托类型为{1}，要移除的委托类型为{2}", eventType, d.GetType(), action.GetType()));
            }
        }
        else
        {
            throw new Exception(string.Format("移除监听错误：没有事件码{0}", eventType));
        }
    }
    private static void OnListenerRemoved(EventType eventType)
    {
        if (m_EventTable[eventType] == null)
        {
            m_EventTable.Remove(eventType);
        }
    }

    #region no parameters
    public static void AddListener(EventType eventType, Action action)
    {
        OnListenerAdding(eventType, action);
        m_EventTable[eventType] = (Action)m_EventTable[eventType] + action;
    }

    public static void RemoveListener(EventType eventType, Action action)
    {
        OnListenerRemoving(eventType, action);
        m_EventTable[eventType] = (Action)m_EventTable[eventType] - action;
        OnListenerRemoved(eventType);
    }

    public static void Broadcast(EventType eventType)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            Action action = d as Action;
            if (action != null)
            {
                action();
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应的委托具有不同的类型", eventType));
            }
        }
    }
    #endregion

    #region one parameters
    public static void AddListener<T>(EventType eventType,Action<T> action)
    {
        OnListenerAdding(eventType, action);
        m_EventTable[eventType] = (Action<T>)m_EventTable[eventType] + action;
    }

    public static void RemoveListener<T>(EventType eventType,Action<T> action)
    {
        OnListenerRemoving(eventType, action);
        m_EventTable[eventType] = (Action<T>)m_EventTable[eventType] - action;
        OnListenerRemoved(eventType);
    }

    public static void Broadcast<T>(EventType eventType,T arg)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            Action<T> action = d as Action<T>;
            if (action != null)
            {
                action(arg);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应的委托具有不同的类型", eventType));
            }
        }
    }
    #endregion

    #region two parameters
    public static void AddListener<T,X>(EventType eventType,Action<T,X> action)
    {
        OnListenerAdding(eventType, action);
        m_EventTable[eventType] = (Action<T, X>)m_EventTable[eventType] + action;
    }

    public static void RemoveListener<T,X>(EventType eventType,Action<T,X> action)
    {
        OnListenerRemoving(eventType, action);
        m_EventTable[eventType] = (Action<T, X>)m_EventTable[eventType] - action;
        OnListenerRemoved(eventType);
    }

    public static void Broadcast<T,X>(EventType eventType,T arg,X arg1)
    {
        Delegate d;
        if(m_EventTable.TryGetValue(eventType,out d))
        {
            Action<T, X> action = d as Action<T, X>;
            if (action != null)
            {
                action(arg, arg1);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应的委托具有不同的类型", eventType));
            }
        }
    }
    #endregion

    #region three parameters
    public static void AddListener<T,X,Y>(EventType eventType,Action<T,X,Y> action)
    {
        OnListenerAdding(eventType, action);
        m_EventTable[eventType] = (Action<T, X, Y>)m_EventTable[eventType] + action;
    }

    public static void RemoveListener<T,X,Y>(EventType eventType, Action<T, X, Y> action)
    {
        OnListenerRemoving(eventType, action);
        m_EventTable[eventType] = (Action<T, X, Y>)m_EventTable[eventType] - action;
        OnListenerRemoved(eventType);
    }

    public static void Broadcast<T,X,Y>(EventType eventType,T arg,X arg1,Y arg2)
    {
        Delegate d;
        if(m_EventTable.TryGetValue(eventType,out d))
        {
            Action<T, X, Y> action = d as Action<T, X, Y>;
            if (action != null)
            {
                action(arg, arg1, arg2);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应的委托具有不同的类型", eventType));
            }
        }
    }
    #endregion

    #region four parameters
    public static void AddListener<T,X,Y,Z>(EventType eventType,Action<T,X,Y,Z> action)
    {
        OnListenerAdding(eventType, action);
        m_EventTable[eventType] = (Action<T, X, Y, Z>)m_EventTable[eventType] + action;
    }

    public static void RemoveListener<T,X,Y,Z>(EventType eventType, Action<T, X, Y, Z> action)
    {
        OnListenerRemoving(eventType, action);
        m_EventTable[eventType] = (Action<T, X, Y, Z>)m_EventTable[eventType] - action;
        OnListenerRemoved(eventType);
    }

    public static void Broadcast<T,X,Y,Z>(EventType eventType,T arg,X arg1,Y arg2,Z arg3)
    {
        Delegate d;
        if(m_EventTable.TryGetValue(eventType,out d))
        {
            Action<T, X, Y, Z> action = d as Action<T, X, Y, Z>;
            if (action != null)
            {
                action(arg, arg1, arg2, arg3);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应的委托具有不同的类型", eventType));
            }
        }
    }
    #endregion
}
