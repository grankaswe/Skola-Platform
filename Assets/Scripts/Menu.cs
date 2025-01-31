using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ButtonEvent
{
    [SerializeField] string _buttonName = "";
    [SerializeField] UnityEvent _unityEvent;
    Button _button;

    // Activates the button's event listener
    public void Activate(UIDocument document)
    {
        if (_button == null)
        {
            // Try to find the button by name in the document's visual tree
            _button = document.rootVisualElement.Q<Button>(_buttonName);
        }

        if (_button != null)
        {
            // Register the callback for the button click event
            _button.RegisterCallback<ClickEvent>(evt => _unityEvent.Invoke());
        }
        else
        {
            Debug.LogWarning($"Button with name '{_buttonName}' not found!");
        }
    }

    // Deactivates the button's event listener
    public void Deactivate(UIDocument document)
    {
        if (_button != null)
        {
            // Unregister the callback for the button click event
            _button.UnregisterCallback<ClickEvent>(evt => _unityEvent.Invoke());
        }
    }
}

public class Menu : MonoBehaviour
{
    [SerializeField] UIDocument _document;                   // The UI document containing the buttons
    [SerializeField] List<ButtonEvent> _buttonEvents;         // List of ButtonEvent objects

    // Called when the script is enabled, sets up all button events
    private void OnEnable()
    {
        // Activate each button's event listener
        _buttonEvents.ForEach(button => button.Activate(_document));
    }

    // Called when the script is disabled, removes all button events
    private void OnDisable()
    {
        // Deactivate each button's event listener
        _buttonEvents.ForEach(button => button.Deactivate(_document));
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
