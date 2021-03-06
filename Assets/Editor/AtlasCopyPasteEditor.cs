using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;


//Copy and paste atlas settings to another atlas editor
public class AtlasCopyPasteEditor : EditorWindow
{

    public Texture2D copyFrom;           //Sprite Atlas to copy from settings
    public Texture2D pasteTo;           //Sprite atlas where to paste settings

    private Sprite[] _sprites;           //Collection of sprites from source texture for faster referencing
    private Sprite[] _sprites2;

    [MenuItem("Window/Atlas CopyPaste Editor")]
    static void Init()
    {
        // Window Set-Up
        AtlasCopyPasteEditor window = EditorWindow.GetWindow(typeof(AtlasCopyPasteEditor), false, "Atlas Editor", true) as AtlasCopyPasteEditor;
        window.minSize = new Vector2(260, 170); window.maxSize = new Vector2(260, 170);
        window.Show();
    }

    //Show UI
    void OnGUI()
    {

        copyFrom = (Texture2D)EditorGUILayout.ObjectField("Copy From", copyFrom, typeof(Texture2D), true);
        pasteTo = (Texture2D)EditorGUILayout.ObjectField("Paste To", pasteTo, typeof(Texture2D), true);

        EditorGUILayout.Space();

        if (GUILayout.Button("Copy Paste"))
        {
            if (copyFrom != null && pasteTo != null)
                CopyPaste();
            else
                Debug.LogWarning("Forgot to set the textures?");
        }

        Repaint();
    }

    //Do the copy paste
    private void CopyPaste()
    {
        if (copyFrom.width != pasteTo.width || copyFrom.height != pasteTo.height)
        {
            //Better a warning if textures doesn't match than a crash or error
            Debug.LogWarning("Unable to proceed, textures size doesn't match.");
            return;
        }

        if (!IsAtlas(copyFrom))
        {
            Debug.LogWarning("Unable to proceed, the source texture is not a sprite atlas.");
            return;
        }

        //Proceed to read all sprites from CopyFrom texture and reassign to a TextureImporter for the end result
        UnityEngine.Object[] _objects = AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetDatabase.GetAssetPath(copyFrom));

        if (_objects != null && _objects.Length > 0)
            _sprites = new Sprite[_objects.Length];

        for (int i = 0; i < _objects.Length; i++)
            _sprites[i] = _objects[i] as Sprite;

        //to keep old and add new ones
        UnityEngine.Object[] _objects2 = AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetDatabase.GetAssetPath(pasteTo));

        if (_objects2 != null && _objects2.Length > 0)
            _sprites2 = new Sprite[_objects2.Length];

        for (int i = 0; i < _objects2.Length; i++)
            _sprites2[i] = _objects2[i] as Sprite;

        Sprite[] combined = new Sprite[_sprites.Length + _sprites2.Length];
        Array.Copy(_sprites2, combined, _sprites2.Length);
        Array.Copy(_sprites, 0, combined, _sprites2.Length, _sprites.Length);
        _sprites = combined;


        //Get Texture Importer of pasteTo texture for assigning sprite variables (pixxelsToUnit is not counted)
        string _path = AssetDatabase.GetAssetPath(pasteTo);
        TextureImporter _importer = AssetImporter.GetAtPath(_path) as TextureImporter;
        _importer.isReadable = true;// to add new sprites by overriding

        //Force settings to Sprite and Multiple just to be sure
        _importer.textureType = TextureImporterType.Sprite;
        _importer.spriteImportMode = SpriteImportMode.Multiple;

        //Reassigning to new atlas
        List<SpriteMetaData> _data = new List<SpriteMetaData>();

        TextureImporterSettings texImporterSettings = new TextureImporterSettings();
        _importer.ReadTextureSettings(texImporterSettings);
        texImporterSettings.spriteAlignment = (int)SpriteAlignment.Custom;
        _importer.SetTextureSettings(texImporterSettings);

        //texImporterSettings.spritePixelsPerUnit = 32;
        //texImporterSettings.spritePivot = new Vector2(0.5f, 0.08f); ;
        //texImporterSettings.spriteMode = 1;
        //texImporterSettings.spriteExtrude = 1;
        //texImporterSettings.spriteMeshType = SpriteMeshType.FullRect;
        //texImporterSettings.filterMode = FilterMode.Point;
        //texImporterSettings.wrapMode = TextureWrapMode.Clamp;
        //texImporterSettings.textureType = TextureImporterType.Sprite;
        _importer.SetTextureSettings(texImporterSettings);

        for (int i = 0; i < _sprites.Length; i++)
        {
            SpriteMetaData _meta = new SpriteMetaData();
            //Debug.Log("pivot " + _sprites[i].pivot.normalized);
            _meta.alignment = 9;
            _meta.pivot = _sprites[i].pivot.normalized;
            _meta.name = _sprites[i].name;
            _meta.rect = _sprites[i].rect;
            _meta.border = _sprites[i].border;


            _data.Add(_meta);
        }

        //Add MetaData back to spriteshet from List to Array
        Debug.Log("CopyPaste1 " + _importer.spritesheet.Length);
        _importer.spritesheet = _data.ToArray();

        //Debug.Log("i2 " + _importer.spritesheet[2].pivot.x);
        Debug.Log("CopyPaste2 " + _importer.spritesheet.Length);

        //Rebuild asset
        _importer.isReadable = false;// to add new sprites - !important!
        //EditorUtility.CopySerialized(_importer, _importer);
        AssetDatabase.ImportAsset(_path, ImportAssetOptions.ForceUpdate);

        // _importer.isReadable = true;//---?
        Debug.Log("COMPLETE slicing " + _importer.spritesheet.Length);
    }

    //Check that the texture is an actual atlas and not a normal texture
    private bool IsAtlas(Texture2D tex)
    {
        string _path = AssetDatabase.GetAssetPath(tex);
        TextureImporter _importer = AssetImporter.GetAtPath(_path) as TextureImporter;

        return _importer.textureType == TextureImporterType.Sprite && _importer.spriteImportMode == SpriteImportMode.Multiple;
    }
}