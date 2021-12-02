using UnityEngine;
using UnityEditor;

// AudioEvent and editor idea inspired by: Unite 2016 - 'Overthrowing the MonoBehaviour Tyranny in a Glorious Scriptable Object Revolution' youtube video

[CustomEditor(typeof(AudioEventSO), true)] //[Notes] true value on the second parameter means that the custom editor will also be applied on child classes of the observed class
public class AudioEventEditor : Editor
{
    [SerializeField] private AudioSource _previewer;

    public void OnEnable()
    {
        //[Notes] create an invisible game object not attached to the hierarchy or the scene and attach an AudioSource on it
        _previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
    }

    public void OnDisable()
    {
        //[Notes] destroy invisible gameobject 
        DestroyImmediate(_previewer.gameObject); 
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); //[Notes] implement the GUI that the inspector usually presents, then code below will add better GUI on top of it

        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects); //not sure of the purpose of this, research more on this later

        EditorGUILayout.Space();
        if (GUILayout.Button("Preview"))
        {
            ((AudioEventSO)target).Play(_previewer);
        }

        EditorGUI.EndDisabledGroup();
    }
}
