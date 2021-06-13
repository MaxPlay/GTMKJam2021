#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.SceneManagement;

// Taken from my company GearEight Games
namespace GMTK2021.Util
{
    [Serializable]
    public class UnityScene
    {
        [SerializeField]
        private Object scene;

        [SerializeField]
        private string name;

        public string Name => name;

        public static implicit operator string(UnityScene unityScene) => unityScene.Name;

        public void LoadScene(bool additive = false)
        {
            SceneManager.LoadScene(Name, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        }

        public AsyncOperation LoadSceneAsync(bool additive = false)
        {
            return SceneManager.LoadSceneAsync(Name, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        }
    }

#if UNITY_EDITOR
    namespace Editor
    {
        [CustomPropertyDrawer(typeof(UnityScene))]
        public class UnitySceneDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                EditorGUI.BeginProperty(position, GUIContent.none, property);
                SerializedProperty asset = property.FindPropertyRelative("scene");
                SerializedProperty name = property.FindPropertyRelative("name");
                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
                if (asset != null)
                {
                    EditorGUI.BeginChangeCheck();
                    Object value = EditorGUI.ObjectField(position, asset.objectReferenceValue, typeof(SceneAsset), false);
                    if (EditorGUI.EndChangeCheck())
                    {
                        asset.objectReferenceValue = value;
                        name.stringValue = (asset.objectReferenceValue != null) ? (asset.objectReferenceValue as SceneAsset).name : null;
                    }
                }
                EditorGUI.EndProperty();
            }
        }
    }
#endif
}
