using System;
using System.Collections.Generic;
using Common.Def;
using Controller;
using View;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public class ViewInfo
    {
        public string PrefabName;
        public Transform ParentTransform;
        public BaseController Controller;  //the controller this view belongs to
        public int SortingOrder;
    }
    public class ViewManager
    {
        private readonly Transform _canvasTransform = GameObject.Find("Canvas").transform;
        private readonly Transform _worldCanvasTransform=GameObject.Find("WorldCanvas").transform;
        readonly Dictionary<int, IBaseView> _openedViews = new Dictionary<int, IBaseView>();
        readonly Dictionary<int, IBaseView> _viewsCache = new Dictionary<int, IBaseView>();
        readonly Dictionary<int, ViewInfo> _viewInfos = new Dictionary<int, ViewInfo>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewType">预制体的名字和脚本的名字和枚举的名字必须统一，一模一样</param>
        /// <param name="controller"></param>
        /// <param name="sortingOrder"></param>
        /// <param name="isGlobal"></param>
        public void RegisterView(ViewType viewType,  BaseController controller, int sortingOrder=0, bool isGlobal=false)
        {
            // Debug.Log(viewType.ToString());
            int viewId = (int)viewType;
            ViewInfo viewInfo = new ViewInfo()
            {
                PrefabName = viewType.ToString(),
                SortingOrder = sortingOrder,
                Controller = controller,
                ParentTransform = isGlobal? _worldCanvasTransform:_canvasTransform,
            };
            _viewInfos.TryAdd(viewId, viewInfo);
        }
        public void RegisterView(int viewId, ViewInfo viewInfo)
        {

            _viewInfos.TryAdd(viewId, viewInfo);
            // if (!_viewInfos.ContainsKey(viewId))
            // {
            //     _viewInfos.Add(viewId, viewInfo);
            // }
        }

        public void RegisterView(ViewType viewType, ViewInfo viewInfo)
        {
            RegisterView((int)viewType, viewInfo);
        }

        public void Unregister(int viewId)
        {
            if (_viewInfos.ContainsKey(viewId))
            {
                _viewInfos.Remove(viewId);
            }
        }
        
        //remove panel
        public void RemoveView(int viewId)
        {
            _viewInfos.Remove(viewId);
            _viewsCache.Remove(viewId);
            _openedViews.Remove(viewId);
        }

        public void RemoveViewByController(BaseController controller)
        {
            foreach (var item in _viewInfos)
            {
                if (item.Value.Controller == controller)
                {
                    RemoveView(item.Key);
                }
            }
        }

        public bool IsOpened(int viewId)
        {
            return _openedViews.ContainsKey(viewId);
        }

        public IBaseView GetView(int viewId)
        {

            if (_openedViews.TryGetValue(viewId, out var view))
            {
                return view;
            }

            return _viewsCache.GetValueOrDefault(viewId);
        }

        public T GetView<T>(int viewId) where T : class, IBaseView
        {
            IBaseView view = GetView(viewId);
            if (view != null)
            {
                return view as T;
            }
            return null;
        }

        public void DestroyView(int viewId)
        {
            IBaseView oldView = GetView(viewId);
            if (oldView != null)
            {
                Unregister(viewId);
                oldView.DestroyView();
                _viewsCache.Remove(viewId);
            }
        }

        public void CloseView(int viewId, params object[] args)
        {
            if (!IsOpened(viewId))
            {
                return;
            }
            IBaseView view = GetView(viewId);
            // Debug.Log(view);
            if (view != null)
            {
                _openedViews.Remove(viewId);
                view.Close(args);
                _viewInfos[viewId].Controller.CloseView(view);
            }
        }

        public void OpenView(int viewId, params object[] args)
        {
            IBaseView localView = GetView(viewId);
            ViewInfo viewInfo = _viewInfos[viewId];

            //if local isn't created before, then create the view
            if (localView == null)
            {
                //if view does not exist, then load this resource of the view
                // string type = $"MVC.View.{((ViewType)viewId).ToString()}";
                
                //the type of view should be the same with the script filename
                string type =$"View.{((ViewType)viewId).ToString()}"; 
                GameObject uiObj = UnityEngine.Object.Instantiate(Resources.Load($"View/{viewInfo.PrefabName}"),viewInfo.ParentTransform) as GameObject;
                if (uiObj != null)
                {
                    Canvas canvas = uiObj.GetComponent<Canvas>();
                    if (canvas == null)
                    {
                        canvas = uiObj.AddComponent<Canvas>();
                    }

                    //
                    if (uiObj.GetComponent<GraphicRaycaster>()==null)
                    {
                        uiObj.AddComponent<GraphicRaycaster>();
                    }
                    canvas.overrideSorting = true; // can set canvas.sortingOrder = 1;
                    canvas.sortingOrder = viewInfo.SortingOrder;
                }

                // Debug.Log(type);
                if (uiObj != null)
                {
                    localView = uiObj.AddComponent(Type.GetType(type)) as IBaseView; // for adding the corresponding script
                }
                if (localView != null)
                {
                    localView.ViewId = viewId;
                    localView.Controller = viewInfo.Controller;
                    //to add the view to the cache
                    _viewsCache.Add(viewId, localView);
                    viewInfo.Controller.OnLoadView(localView);
                }
            }
            
            //add the view to opened view dictionary
            if (!_openedViews.TryAdd(viewId, localView))
            {
                return;
            }

            if (localView != null)
            {
                // if (localView.IsInitialized())
                // {
                //     localView.SetVisible(true);
                //
                // }
                // else
                // {
                //     //first time to load
                //     localView.InitUI();
                //     localView.InitData(); //set the initialized variable as true
                //
                // }
                if (!localView.IsInitialized())
                {
                    localView.InitUI();
                    localView.InitData();
                }
                localView.ShowView(args);
                viewInfo.Controller.OpenView(localView);
            }
        }

        public void OpenView(ViewType viewType, params object[] args)
        {
            OpenView((int)viewType, args);
        }
    }
}