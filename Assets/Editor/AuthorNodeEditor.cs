using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(AuthorNode))]
public class AuthorNodeEditor : NodeEditor
{
    private string[] backgroundSpriteNames = { "StoryStreetW1", "StoryBarW" };
    private int spriteArrayIndex = 0;
    private string backgroundSpritesPath = "Sprites/StorySprites/Background/";

    public override void OnBodyGUI()
    {
        AuthorNode authorNode = target as AuthorNode;

        GUILayout.BeginVertical();
        NodeEditorGUILayout.PortField(new GUIContent("entry"), target.GetInputPort("entry"), GUILayout.MinWidth(0));
        GUILayout.EndVertical();

        GUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Background");
        spriteArrayIndex = EditorGUILayout.Popup(spriteArrayIndex, backgroundSpriteNames);
        authorNode.background = Resources.Load<Sprite>(backgroundSpritesPath + backgroundSpriteNames[spriteArrayIndex]);
        GUILayout.EndHorizontal();


        GUILayout.BeginVertical();

        if(authorNode.background != null)
        {
            Rect rect = GUILayoutUtility.GetRect(100, 200);
            Rect backgroundRect = new Rect(new Vector2(rect.position.x + 70, rect.position.y + 15), new Vector2(120, 180));
            EditorGUI.DrawPreviewTexture(backgroundRect, authorNode.background.texture);
        }

        EditorStyles.textArea.wordWrap = true;
        EditorGUILayout.LabelField("Author text:");
        authorNode.authorText = EditorGUILayout.TextArea(authorNode.authorText, EditorStyles.textArea , GUILayout.MinHeight(80));

        authorNode.button_1_Text = EditorGUILayout.TextField("Button 1 text", authorNode.button_1_Text);
        authorNode.button_2_Text = EditorGUILayout.TextField("Button 2 text", authorNode.button_2_Text);
        authorNode.button_3_Text = EditorGUILayout.TextField("Button 3 text", authorNode.button_3_Text);

        NodeEditorGUILayout.PortField(new GUIContent("option_1"), target.GetOutputPort("option_1"), GUILayout.MinWidth(0));
        NodeEditorGUILayout.PortField(new GUIContent("option_2"), target.GetOutputPort("option_2"), GUILayout.MinWidth(0));
        NodeEditorGUILayout.PortField(new GUIContent("option_3"), target.GetOutputPort("option_3"), GUILayout.MinWidth(0));

        GUILayout.EndVertical();
    }

    public override Color GetTint()
    {
        Color color = Color.red;
        color.a = 0.4f;
        return color;
    }

    public override int GetWidth()
    {
        return 300;
    }

}
