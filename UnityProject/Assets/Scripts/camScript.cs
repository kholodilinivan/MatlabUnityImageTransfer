using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using SimpleFileBrowser;

public class camScript : MonoBehaviour
{    
    private Camera SelectedCamera;
    RenderTexture CameraRender;
    byte[] EncodedPng, EncodedJpg;
    public int Height = 240;
    public int Width = 240;
    bool ScreenshotDone;

    string CompleteFilePath;

    // for Canvas Settings
    public InputField BrowsePath, imgName;

    [SerializeField]
    private int b;

    // for changing state between canvases
    public Dropdown ImgExtent;

    // Use this for initialization
    void Start()
    {
        SelectedCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {       
        CameraRender = new RenderTexture(Width, Height, 16);
    }

    public void CaptureImage(string FilePath, string Filename, string CameraSelect)
    {
        ScreenshotDone = false;

        CompleteFilePath = FilePath + "/" + Filename;
        Debug.Log("File was Saved at:" + CompleteFilePath + ".png");
        StartCoroutine(GetRender(SelectedCamera));
    }

    public void BrowseLocation()
    {
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    public void SaveSnapshot()
    {
        if (ImgExtent.value == 0)
        {
            b = 0;
        }
        else if (ImgExtent.value == 1)
        {
            b = 1;
        }
        CompleteFilePath = BrowsePath.text + "/" + imgName.text;        
        StartCoroutine(GetRender(SelectedCamera));
    }

    public bool CaptureDone()
    {
        return ScreenshotDone;
    }

    public byte[] ReturnCaptureBytes()
    {
        ScreenshotDone = false;
        return EncodedPng;
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.ShowLoadDialog((path) =>
        {
            Debug.Log("Selected: " + path);
            BrowsePath.text = path;
        },
        () => { Debug.Log("Canceled"); },
        true, null, "Select Folder", "Select");
    }

    IEnumerator GetRender(Camera cam)
    {
        // yield return new WaitForEndOfFrame();
        ScreenshotDone = false;
        Texture2D tempTex = new Texture2D(CameraRender.width, CameraRender.height, TextureFormat.RGB24, false);                
        cam.rect = new Rect(new Vector2(0, 0), new Vector2(1, 1)); // Without this, the screenshot will have the wrong size
        cam.targetTexture = CameraRender;
        cam.Render();
        RenderTexture.active = CameraRender;//Sets the Render
        tempTex.ReadPixels(new Rect(0, 0, CameraRender.width, CameraRender.height), 0, 0);
        tempTex.Apply();
        if (b == 0)
        {
            EncodedJpg = tempTex.EncodeToJPG();
            File.WriteAllBytes(CompleteFilePath + ".jpg", tempTex.EncodeToJPG());
        }
        else if (b == 1)
        {
            EncodedPng = tempTex.EncodeToPNG();
            File.WriteAllBytes(CompleteFilePath + ".png", tempTex.EncodeToPNG());
        }
        cam.targetTexture = null;
        Destroy(tempTex);
        yield return null;
        ScreenshotDone = true;
    }
}
