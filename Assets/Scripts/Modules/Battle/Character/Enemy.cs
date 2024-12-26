namespace Character
{
    public class Enemy:CharacterBase
    {
        protected override void Start()
        {
            base.Start();
            DataFromConfig = GameManager.ConfigManager.GetConfig("enemy").GetDataById(Id);
            
            type = int.Parse(DataFromConfig["Type"]);
            attack = int.Parse(DataFromConfig["Attack"]);
            step = int.Parse(DataFromConfig["Step"]);
            maxHp = int.Parse(DataFromConfig["Hp"]);
            currentHp = maxHp;
        }
    }
}