using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>
{
    [Header("Menus")]
    public UIMenuBase[] editorMenus;

    private Dictionary<UIMenuBase.MenuType, UIMenuBase> menus = new Dictionary<UIMenuBase.MenuType, UIMenuBase>();

    private Stack<UIMenuBase> menuStack = new Stack<UIMenuBase>();

    private List<UIMenuBase> menuOverlays = new List<UIMenuBase>();

    public UIMenuBase.MenuType initialMenu;

    public RectTransform shakeRoot;

    [Header("Canvases")]
    public Canvas gameplayCanvas;

    public Canvas screenTransitionCanvas;

    public GameObject showcaseOverlay;

    public Camera mainMenuBGCamera;

    public List<GameObject> professions = new List<GameObject>();

    [Header("Selection")]
    [SerializeField]
    public EventSystem eventSystem;

    public bool UserChangedSelection = true;

    [SerializeField]
    private float idleTimer;

    private static float idleTimeBeforeShowcase = 20f;

    private bool skipIdleFrame;

    string path = "AB/ui.bundle";
    private AssetBundle ab;

    private Dictionary<UIMenuBase.MenuType, string> editorMenusName = new Dictionary<UIMenuBase.MenuType, string>
    {
        {
            UIMenuBase.MenuType.StartMenu,
            "StartMenu"
        },
        {
            UIMenuBase.MenuType.MainMenu,
            "MainMenu"
        },
        {
            UIMenuBase.MenuType.WeaponMenu,
            "WeaponMenu"
        },
        {
            UIMenuBase.MenuType.AttributeMenu,
            "AttributeMenu"
        },
        {
            UIMenuBase.MenuType.TicketMenu,
            "TicketMenu"
        },        
        {
            UIMenuBase.MenuType.TipsMenu,
            "TipsMenu"
        },
    };

    private void Start()
    {
        ab = AssetBundle.LoadFromFile(path);
        eventSystem = EventSystem.current;
        if (menus.Count == 0)
        {
            SetupAllMenus();
        }
        UIMenuBase menu = GetMenu(initialMenu);
        if (menu != null)
        {
            PushMenu(menu);
        }
        //showcaseOverlay.SetActive(value: false);
    }

    private void SetupAllMenus()
    {
        for (int i = 0; i < editorMenus.Length; i++)
        {
            if (!menus.ContainsKey(editorMenus[i].MenuID))
            {
                menus.Add(editorMenus[i].MenuID, editorMenus[i]);
                editorMenus[i].Setup();
                //editorMenus[i].gameObject.SetActive(value: false);
            }
        }
    }

    private void InstantiateUI(GameObject obj)
    {
        GameObject temp = Instantiate(obj);
        UIMenuBase menu = temp.GetComponent<UIMenuBase>();
        if (menu != null)
        {
            RectTransform rt = temp.GetComponent<RectTransform>();
            rt.SetParent(transform);
            rt.offsetMax = Vector2.zero;
            rt.offsetMin = Vector2.zero;
            rt.anchoredPosition3D = new Vector3(0, 0, menu.startPosZ);
            rt.rotation = Quaternion.Euler(0, 0, 0);
            rt.localScale = Vector3.one;
            if (!menus.ContainsKey(menu.MenuID))
            {
                menus.Add(menu.MenuID, menu);
                menu.Setup();
                menu.gameObject.SetActive(value: false);
            }
        }
    }

    public void Init()
    {
        if (menus.Count == 0)
        {
            SetupAllMenus();
        }
        UIMenuBase menu = GetMenu(initialMenu);
        if (menu != null)
        {
            PushMenu(menu);
        }
    }

    public UIMenuBase GetMenu(UIMenuBase.MenuType menuID)
    {
        if (menus.ContainsKey(menuID))
        {
            return menus[menuID];
        }
        GameObject obj = null;
#if UNITY_EDITOR
        //obj = AssetBundleManager.Instance.LoadAssetAtPath<GameObject>("Assets/RawResources/Prefabs/", editorMenusName[menuID] + ".prefab");
        obj = XTool.LoadAssetAtPath<GameObject>("Assets/RawResources/Prefabs/", editorMenusName[menuID] + ".prefab");
#else
        //string path = "AB/ui.bundle";
        //AssetBundle ab = AssetBundle.LoadFromFile(path);
        if (ab != null)
            obj = ab.LoadAsset<GameObject>(editorMenusName[menuID] + ".prefab");
#endif
        if (obj != null)
        {
            InstantiateUI(obj);
            return menus[menuID];
        }
        UnityEngine.Debug.LogError("Menu " + menuID + " could not be found");
        return null;
    }

    public void PushMenu(UIMenuBase menu, bool disableCurrentMenu = true)
    {
        if (menuStack.Count > 0)
        {
            UIMenuBase uIMenuBase = menuStack.Peek();
            if (uIMenuBase != null)
            {
                //uIMenuBase.Suspend();
                if (disableCurrentMenu)
                {
                    uIMenuBase.gameObject.SetActive(value: false);
                }
            }
        }
        else
        {
            //Singleton<TimeKeeper>.Instance.PauseTime();
            //Time.timeScale = 0f;
        }
        menu.gameObject.SetActive(value: true);
        menu.Init();
        menuStack.Push(menu);
    }

    public void PopMenu(bool resumeLastMenu = true)
    {
        if (menuStack.Count <= 0)
        {
            UnityEngine.Debug.Log("No menus in the stack to pop!");
            return;
        }
        UIMenuBase uIMenuBase = menuStack.Peek();
        if (uIMenuBase == null)
        {
            return;
        }
        //uIMenuBase.Cleanup();
        uIMenuBase.gameObject.SetActive(value: false);
        menuStack.Pop();
        if (menuStack.Count <= 0)
        {
            //Singleton<TimeKeeper>.Instance.UnPauseTime();
        }
        else if (resumeLastMenu)
        {
            UIMenuBase uIMenuBase2 = menuStack.Peek();
            if (uIMenuBase2 != null)
            {
                uIMenuBase2.gameObject.SetActive(value: true);
                //uIMenuBase2.Resume();
            }
        }
    }

    public void PopAllMenus()
    {
        if (menuStack.Count > 0)
        {
            while (menuStack.Count > 0)
            {
                PopMenu(resumeLastMenu: false);
            }
        }
    }

    public bool IsOverlayMenu(UIMenuBase menu)
    {
        if (menu == null)
        {
            return false;
        }
        if (!menuOverlays.Contains(menu))
        {
            return false;
        }
        return true;
    }

    public bool IsMenu(UIMenuBase menu)
    {
        if (menu == null)
        {
            return false;
        }
        if (!menuStack.Contains(menu))
        {
            return false;
        }
        return true;
    }

    public void AddOverlayMenu(UIMenuBase menu)
    {
        if (!(menu == null) && !menuOverlays.Contains(menu))
        {
            if (menuOverlays.Count > 0)
            {
                //menuOverlays[menuOverlays.Count - 1].Suspend();
            }
            menu.gameObject.SetActive(value: true);
            menu.Init();
            menuOverlays.Add(menu);
        }
    }

    public void RemoveOverlayMenu(UIMenuBase menuController, bool resume = true)
    {
        if (!(menuController == null) && menuOverlays.Contains(menuController))
        {
            //menuController.Cleanup();
            menuController.gameObject.SetActive(value: false);
            menuOverlays.Remove(menuController);
            UIMenuBase currentMenu = GetCurrentMenu();
            if (currentMenu != null && resume)
            {
                //currentMenu.Resume();
            }
        }
    }
    public void RemoveAllOverlayMenu(bool resume = true)
    {
        while (menuOverlays.Count > 0)
        {
            UIMenuBase menuController = menuOverlays[menuOverlays.Count - 1];
            RemoveOverlayMenu(menuController, resume);
        }
    }

    public UIMenuBase GetCurrentMenu()
    {
        if (menuOverlays.Count > 0)
        {
            return menuOverlays[menuOverlays.Count - 1];
        }
        if (menuStack.Count == 0)
        {
            return null;
        }
        return menuStack.Peek();
    }
}
