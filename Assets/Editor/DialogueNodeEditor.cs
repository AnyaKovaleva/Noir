using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(DialogueNode))]
public class DialogueNodeEditor : NodeEditor
{
    private string[] backgroundSpriteNames = { "StoryStreetD1", "StoryStreetD2", "StoryBarD"};
    private int backgroundIndex = 0;
    private string backgroundSpritesPath = "Sprites/StorySprites/Background/";

    private string[] rightPersonSpriteNames = { "Clive"};
    private int rightPersonIndex = 0;

    private string[] leftPersonSpriteNames = { "Ernest", "Jack", "Simon", "Vladimir" };
    private int leftPersonIndex;

    private string personSpritesPath = "Images/Persons/";

    public override void OnBodyGUI()
    {
        DialogueNode dialogueNode = target as DialogueNode;

        //input port
        GUILayout.BeginVertical();
        NodeEditorGUILayout.PortField(new GUIContent("entry"), target.GetInputPort("entry"), GUILayout.MinWidth(0));
        GUILayout.EndVertical();

        //choose background
        GUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Background");
        backgroundIndex = EditorGUILayout.Popup(backgroundIndex, backgroundSpriteNames);
        dialogueNode.background = Resources.Load<Sprite>(backgroundSpritesPath + backgroundSpriteNames[backgroundIndex]);
        GUILayout.EndHorizontal();

        //draw background preview
        GUILayout.BeginVertical();
        Rect rect;

        if (dialogueNode.background != null)
        {
            rect = GUILayoutUtility.GetRect(100, 200);
            Rect backgroundRect = new Rect(new Vector2(rect.position.x + 75, rect.position.y + 10), new Vector2(120, 180));
            EditorGUI.DrawPreviewTexture(backgroundRect, dialogueNode.background.texture);
        }
        GUILayout.EndVertical();

        //choose right person
        GUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Right person");
        rightPersonIndex = EditorGUILayout.Popup(rightPersonIndex, rightPersonSpriteNames, GUILayout.Width(50));
        dialogueNode.rightPersonSprite = Resources.Load<Sprite>(personSpritesPath + rightPersonSpriteNames[rightPersonIndex]);

        //draw right person sprite preview
        rect = GUILayoutUtility.GetRect(80, 80);
        Rect rectPerson = new Rect(new Vector2(rect.position.x + 20, rect.position.y), new Vector2(80, 80));
        if (dialogueNode.rightPersonSprite != null)
        {
            EditorGUI.DrawPreviewTexture(rectPerson, dialogueNode.rightPersonSprite.texture);
        }

        GUILayout.EndHorizontal();

        //set right person text
       GUILayout.BeginVertical();

        EditorStyles.textArea.wordWrap = true;
        EditorGUILayout.LabelField("Right person text:");
        dialogueNode.rightPersonText = EditorGUILayout.TextArea(dialogueNode.rightPersonText, EditorStyles.textArea, GUILayout.MinHeight(50));

        dialogueNode.addAdditionalRightPersonText = EditorGUILayout.Toggle("Additional text", dialogueNode.addAdditionalRightPersonText);

        if(dialogueNode.addAdditionalRightPersonText)
        {
            EditorGUILayout.LabelField("additional right person text:");
            dialogueNode.additionalRightPersonText = EditorGUILayout.TextArea(dialogueNode.additionalRightPersonText, EditorStyles.textArea, GUILayout.MinHeight(50));
        }
        else
        {
            dialogueNode.additionalRightPersonText = null;
        }

        dialogueNode.addLeftPerson = EditorGUILayout.Toggle("Add left person", dialogueNode.addLeftPerson);

        GUILayout.EndVertical();

        if(dialogueNode.addLeftPerson)
        {
            //choose left person
            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Left person");
            leftPersonIndex = EditorGUILayout.Popup(leftPersonIndex, leftPersonSpriteNames, GUILayout.Width(50));
            dialogueNode.leftPersonSprite = Resources.Load<Sprite>(personSpritesPath + leftPersonSpriteNames[leftPersonIndex]);

            //draw left person sprite preview
            rect = GUILayoutUtility.GetRect(80, 80);
            rectPerson = new Rect(new Vector2(rect.position.x + 20, rect.position.y), new Vector2(80, 80));
            if (dialogueNode.rightPersonSprite != null)
            {
                EditorGUI.DrawPreviewTexture(rectPerson, dialogueNode.leftPersonSprite.texture);
            }

            GUILayout.EndHorizontal();

            //set left person text
            GUILayout.BeginVertical();
            EditorStyles.textArea.wordWrap = true;
            EditorGUILayout.LabelField("Left person text:");
            dialogueNode.leftPersonText = EditorGUILayout.TextArea(dialogueNode.leftPersonText, EditorStyles.textArea, GUILayout.MinHeight(50));

            dialogueNode.addAdditionalLeftPersonText = EditorGUILayout.Toggle("Additional text", dialogueNode.addAdditionalLeftPersonText);

            if (dialogueNode.addAdditionalLeftPersonText)
            {
                EditorGUILayout.LabelField("additional left person text:");
                dialogueNode.additionalLeftPersonText = EditorGUILayout.TextArea(dialogueNode.additionalLeftPersonText, EditorStyles.textArea, GUILayout.MinHeight(50));
            }
            else
            {
                dialogueNode.additionalLeftPersonText = null;
            }

            GUILayout.EndVertical();
        }
        else
        {
            leftPersonIndex = 0;
            dialogueNode.leftPersonSprite = null;
            dialogueNode.leftPersonText = null;
        }
        
        GUILayout.BeginVertical();

        dialogueNode.button_1_Text = EditorGUILayout.TextField("Button 1 text", dialogueNode.button_1_Text);
        dialogueNode.button_2_Text = EditorGUILayout.TextField("Button 2 text", dialogueNode.button_2_Text);
        dialogueNode.button_3_Text = EditorGUILayout.TextField("Button 3 text", dialogueNode.button_3_Text);

        NodeEditorGUILayout.PortField(new GUIContent("option_1"), target.GetOutputPort("option_1"), GUILayout.MinWidth(0));
        NodeEditorGUILayout.PortField(new GUIContent("option_2"), target.GetOutputPort("option_2"), GUILayout.MinWidth(0));
        NodeEditorGUILayout.PortField(new GUIContent("option_3"), target.GetOutputPort("option_3"), GUILayout.MinWidth(0));

        GUILayout.EndVertical();
    }

    public override Color GetTint()
    {
        Color color = Color.cyan;
        color.a = 0.4f;
        return color;
    }

    public override int GetWidth()
    {
        return 300;
    }
}
