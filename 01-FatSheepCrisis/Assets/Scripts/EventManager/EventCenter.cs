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
            throw new Exception(string.Format("��Ӽ������󣺳���Ϊ�¼�{0}��Ӳ�ͬ���͵�ί�У���ǰ�¼�����Ӧ��ί����{1}��Ҫ��ӵ�ί������Ϊ{2}", eventType, d.GetType(), action.GetType()));
        }
    }
    private static void OnListenerRemoving(EventType eventType,Delegate action)
    {
        if (m_EventTable.ContainsKey(eventType))
        {
            Delegate d = m_EventTable[eventType];
            if (d == null)
            {
                throw new Exception(string.Format("�Ƴ����������¼�{0}û�ж�Ӧ��ί��", eventType));
            }
            else if (d.GetType() != action.GetType())
            {
                throw new Exception(string.Format("�Ƴ��������󣺳���Ϊ�¼�{0}�Ƴ���ͬ���͵�ί�У���ǰί������Ϊ{1}��Ҫ�Ƴ���ί������Ϊ{2}", eventType, d.GetType(), action.GetType()));
            }
        }
        else
        {
            throw new Exception(string.Format("�Ƴ���������û���¼���{0}", eventType));
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
                throw new Exception(string.Format("�㲥�¼������¼�{0}��Ӧ��ί�о��в�ͬ������", eventType));
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
                throw new Exception(string.Format("�㲥�¼������¼�{0}��Ӧ��ί�о��в�ͬ������", eventType));
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
                throw new Exception(string.Format("�㲥�¼������¼�{0}��Ӧ��ί�о��в�ͬ������", eventType));
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
                throw new Exception(string.Format("�㲥�¼������¼�{0}��Ӧ��ί�о��в�ͬ������", eventType));
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
                throw new Exception(string.Format("�㲥�¼������¼�{0}��Ӧ��ί�о��в�ͬ������", eventType));
            }
        }
    }
    #endregion
}
