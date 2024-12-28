using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class AutoFocusGameView
{
    static AutoFocusGameView()
    {
        EditorApplication.playModeStateChanged += FocusGameView;
    }

    private static void FocusGameView(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            EditorWindow gameView = EditorWindow.GetWindow(typeof(EditorWindow).Assembly.GetType("UnityEditor.GameView"));
            gameView.Focus();
        }
    }
}
