using Controller;

namespace Model
{
    public class BaseModel
    {
        public BaseController Controller;

        // public BaseModel(BaseController controller)
        // {
        //     Controller = controller;
        // }
        
        public BaseModel(){}
        
        public virtual void Init(){}
    }
}

