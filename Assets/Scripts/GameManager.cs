
using Common;
using MVC;
using Sound;
using Config;
/// <summary>
/// Uniformly define the managers of the game, like game manager 
/// </summary>
public class GameManager :Singleton<GameManager>
{
    public static SoundManager SoundManager;
    public static ControllerManager ControllerManager;
    public static ViewManager ViewManager;
    public static ConfigManager ConfigManager;
    public static CameraManager CameraManager;
    public static EventCenter EventCenter;
    public override void Init()
    {
        CameraManager = new CameraManager();
        SoundManager = new SoundManager();
        ConfigManager = new ConfigManager();
        ControllerManager = new ControllerManager();
        ViewManager = new ViewManager();
        EventCenter = new EventCenter();
    }
}
