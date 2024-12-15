    using Config;
    using Controller;

    using UnityEngine;

    //this script needs to be mounted onto a game object, so it cannot be deleted when changing scene
    public class GameScene:MonoBehaviour
    {
        private float _dt;
        public Texture2D mouseTexture; //mouse picture

        private static bool _isLoaded;
        private void Awake()
        {
            //make this script and the game object as a singleton
            if (_isLoaded)
            {
                Destroy(gameObject);
            }
            else
            {
                _isLoaded = true;
                DontDestroyOnLoad(gameObject);
                //initialize all manager classes
                GameManager.Instance.Init();
            }
        }

        private void Start()
        {
            //set cursor texture
            Cursor.SetCursor(mouseTexture,Vector2.zero, CursorMode.Auto);
            
            //load all configuration files
            RegisterConfig();
            
            //test configuration
            ConfigData temp = GameManager.ConfigManager.GetConfig("enemy");
            string name = temp.GetDataById(10001)["Name"];
            Debug.Log(name);
            //play the background music
            GameManager.SoundManager.PlayBGM("login");

            //register the controllers in the game. Before this step, in awake method, all managers has been initialized
            RegisterModule(); 
            
            InitModule();
        }

        private void Update()
        {
            _dt = Time.deltaTime;
            GameManager.Instance.Update(_dt);
        }

        private void RegisterModule()
        {
            //store each manager into the controller dictionary.
            GameManager.ControllerManager.RegisterModule(ControllerType.GameUIController, new GameUIController());
            GameManager.ControllerManager.RegisterModule(ControllerType.GameController, new GameController());
            GameManager.ControllerManager.RegisterModule(ControllerType.LoadingController, new LoadingController());
        }

        //执行所有控制器初始化
        private void InitModule()
        {
            GameManager.ControllerManager.InitAllModules();
        }

        private void RegisterConfig()
        {
            //I think this can be optimised, I can use the configuration files to manage these configuration files
            GameManager.ConfigManager.RegisterConfig("enemy", new ConfigData("enemy"));
            GameManager.ConfigManager.RegisterConfig("level", new ConfigData("level"));
            GameManager.ConfigManager.RegisterConfig("option", new ConfigData("option"));
            GameManager.ConfigManager.RegisterConfig("player", new ConfigData("player"));
            GameManager.ConfigManager.RegisterConfig("role", new ConfigData("role"));
            GameManager.ConfigManager.RegisterConfig("skill", new ConfigData("skill"));
            
            //after loading all files, then loading those data into memory
            GameManager.ConfigManager.LoadAllConfigs();
        }
    }
