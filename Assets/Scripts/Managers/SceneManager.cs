using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public event Action<float> OnTimerChange = delegate { };
    public event Action<ERoomType> OnCameraChange = delegate { };
    public bool Freeze { get; set; } = false;

    public EPlayerState CurrentPlayerState
    {
        get
        {
            return currentPlayerState;
        }
        set
        {
            switch (value)
            {
                case EPlayerState.FPP:
                    DesktopManager.SetEnable(false);
                    Player.SetEnable(true);
                    GameManager.Instance.SetCursorState(false);
                    break;

                case EPlayerState.WindowsDisplay:
                    Player.SetEnable(true);
                    DesktopManager.SetEnable(true); 
                    GameManager.Instance.SetCursorState(true);
                    break;
            }
            currentPlayerState = value;
        }
    }

    public Player Player
    {
        get => FindObjectOfType<Player>();
    }

    public Dictionary<ERoomType, CCTVCamera> CCTVCameras
    {
        get
        {
            if(cctvCameras == null)
            {
                cctvCameras = new Dictionary<ERoomType, CCTVCamera>();
                foreach(Transform t in cctvParent)
                {
                    CCTVCamera camera;
                    if(t.TryGetComponent(out camera))
                    {
                        cctvCameras.Add(camera.RoomType, camera);
                    }
                }
            }
            return cctvCameras;
        }
    }

    public UIManager UIManager
    {
        get => FindObjectOfType<UIManager>();
    }
    
    public DesktopManager DesktopManager
    {
        get => FindObjectOfType<DesktopManager>();
    }

    public List<Aberration> Aberrations
    {
        get
        {
            if (aberrations == null || aberrations.Count == 0)
            {
                aberrations = new List<Aberration>();
                foreach (var t in GameObject.FindGameObjectsWithTag("Aberration"))
                {
                    Aberration anom;
                    if (t.transform.TryGetComponent(out anom))
                    {
                        aberrations.Add(anom);
                    }
                }
            }
            return aberrations;
        }
        set
        {
            aberrations = value;
        }
    }

    public List<Aberration> ActiveAberrations
    {
        get
        {
            List<Aberration> useList = new List<Aberration>();
            foreach (var a in Aberrations)
            {
                if (a.IsActive)
                {
                    useList.Add(a);
                }
            }
            return useList;
        }
    }

    public List<Aberration> UnactiveAberrations
    {
        get
        {
            List<Aberration> useList = new List<Aberration>();
            foreach (var a in Aberrations)
            {
                if (!a.IsActive)
                {
                    useList.Add(a);
                }
            }
            return useList;
        }
    }

    public int ActiveAberrationsCount => ActiveAberrations.Count;

    public int UnactiveAberrationsCount => UnactiveAberrations.Count;

    public int FixedAberrations { get; private set; }

    public bool IsActiveAberrationsCooldown => activeAberrationsCooldownCoroutine != null;

    [SerializeField]
    private EMapType mapType = EMapType.None;

    [SerializeField]
    private Transform cctvParent = null;

    private float realTimer;
    private float gameTimer;
    private Coroutine activeAberrationsCooldownCoroutine;
    private List<Aberration> aberrations;
    private EPlayerState currentPlayerState = EPlayerState.FPP;
    private Dictionary<ERoomType, CCTVCamera> cctvCameras;
    private int selectedCameraIndex;
    private List<string> dialogsToDisplay;
    private Action onDialogEnd = null;
    private Coroutine dialogCoroutine;

    public void Setup()
    {
        foreach (var ab in Aberrations)
        {
            ab.Setup();
        }

        UIManager.Setup();
        DesktopManager.Setup();

        GameManager.Instance.SetCursorState(false);
        GameManager.Instance.SelectedMapType = mapType;
        realTimer = 0;
        gameTimer = 0;
        FixedAberrations = 0;
        OnTimerChange(gameTimer);

        CurrentPlayerState = EPlayerState.FPP;
        selectedCameraIndex = 0;

        foreach(var c in CCTVCameras)
        {
            c.Value.Setup();
        }
        NextCamera();
    }
    
    public void NextCamera()
    {
        DisableCameras();
        selectedCameraIndex++;
        if (selectedCameraIndex > CCTVCameras.Count)
        {
            selectedCameraIndex = 1;
        }
        if (CCTVCameras[(ERoomType)selectedCameraIndex].IsBroken)
        {
            selectedCameraIndex++;
        }

        CCTVCameras[(ERoomType)selectedCameraIndex].SetEnable(true);
        OnCameraChange((ERoomType)selectedCameraIndex);
    }
    public void PrevCamera()
    {
        DisableCameras();
        selectedCameraIndex--;
        if (selectedCameraIndex <= 0)
        {
            selectedCameraIndex = CCTVCameras.Count;
        }
        if (CCTVCameras[(ERoomType)selectedCameraIndex].IsBroken)
        {
            selectedCameraIndex--;
        }

        CCTVCameras[(ERoomType)selectedCameraIndex].SetEnable(true);
        OnCameraChange((ERoomType)selectedCameraIndex);
    }

    public void StartAberration()
    {
        if(!IsActiveAberrationsCooldown)
        {
            activeAberrationsCooldownCoroutine = StartCoroutine(AberrationCoolDown(Consts.GetAnomalyCooldown()));
        }
    }

    public void ReportAnomaly(ERoomType roomType, EAberrationType anomalyType)
    {
        StartCoroutine(WaitToReport(roomType, anomalyType));
    }

    public void SetDialog(int dialogIndex, Action onDialogEnd = null)
    {
        dialogsToDisplay = new List<string>(Consts.GetDialog(dialogIndex));
        dialogCoroutine = StartCoroutine(WaitToDisplayDialog());
        this.onDialogEnd = onDialogEnd;
    }

    private void PlayAberration()
    {
        if (ActiveAberrationsCount > Consts.MaxAnomalyCount())
        {
            GameManager.Instance.SceneManager.UIManager.ShowEndPanel(false, mapType);
            return;
        }

        int chanceBucket = 0;
        List<Tuple<Aberration, int, int>> chanceList = new List<Tuple<Aberration, int, int>>();
        foreach (var a in UnactiveAberrations)
        {
            chanceList.Add(new Tuple<Aberration, int, int>(a, chanceBucket, chanceBucket + a.AberrationWeight));
            chanceBucket += a.AberrationWeight;
        }

        var att = 0;

        do
        {
            int lucyNumber = UnityEngine.Random.Range(0, chanceBucket);
            foreach (var r in chanceList)
            {
                if (lucyNumber > r.Item2 && lucyNumber < r.Item3)
                {
                    r.Item1.OnAberrationAppear();
                    return;
                }
            }

            att++;
            if(att > 10000)
            {
                Debug.LogError("Infinity loop error.");
                break;
            }
        } while (true);
    }

    private void Update()
    {
        if(IsActiveAberrationsCooldown)
        {
            realTimer += Time.deltaTime;
            gameTimer = realTimer * Consts.GetTimeMultiplier(mapType);
            OnTimerChange(gameTimer);

            if (gameTimer / 60 >= 6)
            {
                GameManager.Instance.SceneManager.UIManager.ShowEndPanel(true, mapType);
            }
        }
        

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch(CurrentPlayerState)
            {
                case EPlayerState.FPP:
                    UIManager.ShowPausePanel();
                    break;

                case EPlayerState.WindowsDisplay:
                    CurrentPlayerState = EPlayerState.FPP;
                    break;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(dialogsToDisplay != null && dialogsToDisplay.Count > 0)
            {
                UIManager.SetDialogText("");
                StopCoroutine(dialogCoroutine);
                onDialogEnd();
            }
        }
        DebugCommand();
    }

    private IEnumerator AberrationCoolDown(int timer)
    {
        Debug.Log($"Next Aberration on {timer}.");
        yield return new WaitForSeconds(timer);
        PlayAberration();
        activeAberrationsCooldownCoroutine = StartCoroutine(AberrationCoolDown(Consts.GetAnomalyCooldown()));
    }

    private IEnumerator WaitToReport(ERoomType roomType, EAberrationType anomalyType)
    {
        yield return new WaitForSeconds(Consts.GetReportingTime());
        bool anomalyFound = false;
        foreach (var a in ActiveAberrations)
        {
            if (a.AberrationPlace == roomType && a.AberrationType == anomalyType)
            {
                a.OnAberrationFixed();
                anomalyFound = true;
                FixedAberrations++;
            }
        }
        UIManager.ShowAberrationBareaker(anomalyFound);
    }

    private IEnumerator WaitToDisplayDialog()
    {
        UIManager.SetDialogText(dialogsToDisplay[0]);
        yield return new WaitForSeconds(dialogsToDisplay[0].Length * 0.1f);
        dialogsToDisplay.RemoveAt(0);
        if (dialogsToDisplay.Count == 0)
        {
            UIManager.SetDialogText("");
            onDialogEnd();
        }
        else
        {
            dialogCoroutine = StartCoroutine(WaitToDisplayDialog());
        }
    }

    private void DisableCameras()
    {
        foreach(var camera in CCTVCameras)
        {
            camera.Value.SetEnable(false);
        }
    }

    private void DebugCommand()
    {
        //Fast Win
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.SceneManager.UIManager.ShowEndPanel(true, mapType);
        }
        //Fast Loose
        if(Input.GetKeyDown(KeyCode.O))
        {
            GameManager.Instance.SceneManager.UIManager.ShowEndPanel(false, mapType);
        }
    }
}