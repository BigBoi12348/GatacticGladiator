using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    private static ViewManager _vMInstance;
    [SerializeField] private View intialStartingView;
    [SerializeField] private View newStartingView;
    [SerializeField] private View[] _views;
    [SerializeField] private View[] _subViews;
    
    
    [Header("Current Views being Displayed")]
    [SerializeField]private View currentView;
    [SerializeField]private View subCurrentView;

    [SerializeField]private bool _hidingCurrentView;
    private readonly Stack<View> _history = new Stack<View>();

    private void Awake() => _vMInstance = this;

    public static void Show<T>(float timeDelay, bool remember = true) where T : View
    {
        for (int i = 0; i < _vMInstance._views.Length; i++)
        {
            if(_vMInstance._views[i] is T)
            {
                if(_vMInstance.currentView != null)
                {
                    if(remember)
                    {
                        _vMInstance._history.Push(_vMInstance.currentView);
                    }
                    _vMInstance._hidingCurrentView = true;
                }
                
                if(timeDelay > 0)
                {
                    if(_vMInstance._hidingCurrentView)
                    {
                        _vMInstance.StartCoroutine(_vMInstance.ExecuteDelayedShowAndHide(_vMInstance._views[i], _vMInstance.currentView, timeDelay));
                    }
                    else
                    {
                        _vMInstance.StartCoroutine(_vMInstance.ExecuteDelayedShowAndHide(_vMInstance._views[i], null, timeDelay));
                    }
                }
                else
                {
                    if(_vMInstance._hidingCurrentView)
                    {
                        _vMInstance.currentView.Hide();
                    }
                    _vMInstance._views[i].Show();
                }

                _vMInstance.currentView = _vMInstance._views[i];
            }
        }
    }

    public static void Show(View view, bool remember = true)
    {
        if(_vMInstance.currentView != null)
        {
            if(remember)
            {
                _vMInstance._history.Push(_vMInstance.currentView);
            }
            
            _vMInstance.currentView.Hide();
        }

        view.Show();

        _vMInstance.currentView = view;
    }

    public static void ShowLast()
    {
        if(_vMInstance._history.Count != 0)
        {
            Show(_vMInstance._history.Pop(), false);
        }
    }

    public static void ShowSubView<T>(float timeDelay) where T : View
    {
        for (int i = 0; i < _vMInstance._subViews.Length; i++)
        {
            if(_vMInstance._subViews[i] is T)
            {
                _vMInstance.StartCoroutine(_vMInstance.ExecuteDelayedShowAndHide(_vMInstance._subViews[i], null, timeDelay));
                //_vMInstance._subViews[i].Show();

                _vMInstance.subCurrentView = _vMInstance._subViews[i];
            }
        }
    }

    public static void HideSubView<T>(float timeDelay) where T : View
    {
        for (int i = 0; i < _vMInstance._subViews.Length; i++)
        {
            if(_vMInstance._subViews[i] is T)
            {
                _vMInstance.StartCoroutine(_vMInstance.ExecuteDelayedShowAndHide(null, _vMInstance._subViews[i], timeDelay));
                //_vMInstance._subViews[i].Hide();
            }
        }
    }

    IEnumerator ExecuteDelayedShowAndHide(View targetView, View currentView, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        if(currentView != null)
        {
            //Debug.Log("Hiding");
            currentView.Hide();
        }
        if(targetView != null)
        {
            targetView.Show();
        }
    }

    private void Start() 
    {
        for (int i = 0; i < _views.Length; i++)
        {
            _views[i].Initialize();

            _views[i].Hide();
        }

        //SUB VIEWS INITIALIZE
        for (int i = 0; i < _subViews.Length; i++)
        {
            _subViews[i].Initialize();
            //_subViews[i].Hide();
        }
        
        if(_views.Length > 0)
        {
            if(GameManager.Instance.LastSceneID == 0)
            {
                Show(intialStartingView, true);
            }
            else
            {
                Show(newStartingView, true);
            }
        }
    }

    public static T GetView<T>() where T : View
    {
        for (int i = 0; i < _vMInstance._views.Length; i++)
        {
            if(_vMInstance._views[i] is T tView)
            {
                return tView;
            }
        }
        return null;
    }

    public static T GetSubView<T>() where T : View
    {
        for (int i = 0; i < _vMInstance._subViews.Length; i++)
        {
            if(_vMInstance._subViews[i] is T tView)
            {
                return tView;
            }
        }
        return null;
    }
}
