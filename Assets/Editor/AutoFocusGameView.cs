using UnityEditor;
using UnityEngine;

// This attribute ensures the class is initialized as soon as the editor loads (no need to attach to GameObjects)
[InitializeOnLoad]
public class AutoFocusGameView
{
    // Static constructor is called automatically when the editor starts or compiles
    static AutoFocusGameView()
    {
        // Subscribe to the play mode state change event to detect when Play mode is entered
        EditorApplication.playModeStateChanged += FocusGameView;
    }

    // Method to focus the Game view when entering Play mode
    private static void FocusGameView(PlayModeStateChange state)
    {
        // Check if the editor has just entered Play mode
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            // Get a reference to the Game view window and focus on it
            EditorWindow gameView = EditorWindow.GetWindow(typeof(EditorWindow).Assembly.GetType("UnityEditor.GameView"));
            gameView.Focus();  // Bring the Game view to the front
        }
    }
}
