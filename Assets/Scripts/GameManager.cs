
using BattleMgr;
using Common;
using MVC;
using Sound;
using Config;
using Timer;

/// <summary>
/// Uniformly define the managers of the game, like game manager 
/// </summary>
public class GameManager :Singleton<GameManager>
{
    public static TimerManager TimerManager;
    public static SoundManager SoundManager;
    public static ControllerManager ControllerManager;
    public static ViewManager ViewManager;
    public static ConfigManager ConfigManager;
    public static CameraManager CameraManager;
    public static EventCenter EventCenter;
    public static SceneLoader SceneLoader;
    public static FightManager FightManager;
    public static MapManager MapManager;
    public static GameDataManager GameDataManager;
    public static UserInputManager UserInputManager;
    public override void Init()
    {
        UserInputManager = new UserInputManager();
        TimerManager= new TimerManager();
        CameraManager = new CameraManager();
        SoundManager = new SoundManager();
        ConfigManager = new ConfigManager();
        ControllerManager = new ControllerManager();
        EventCenter = new EventCenter();
        SceneLoader = new SceneLoader();
        FightManager = new FightManager();
        MapManager = new MapManager();
        GameDataManager = new GameDataManager();
        ViewManager = new ViewManager();
    }

    public override void Update(float dt)
    {
        UserInputManager.OnUpdate();
        TimerManager.OnUpdate(dt);
        FightManager.OnUpdate(dt);
    }
}
