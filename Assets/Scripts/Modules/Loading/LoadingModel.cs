
namespace Model
{
    public class LoadingModel:BaseModel
    {
        public string SceneName; //the next scene name to be loaded
        public System.Action Callback; //after loading scene, call this function
    }
}