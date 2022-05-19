using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class GOLController : MonoBehaviour
{
    public ComputeShader GOL;
    public Material mat;
    public RenderTexture rt;
    public RenderTexture rt1;
    private bool step = true;

    [Header("control")]
    public Texture startTexture;
    [ReadOnly] public float fps;
    public bool paused = false;
    [Range(0, 1)] public float waitBetweenStep = .0166667f;
    public Vector2Int size = new Vector2Int(16,16);
    

    private int kernal = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (rt == null)
        {
            rt = new RenderTexture(size.x, size.y, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB)
            { //depth of 0 to fix wrapMode not working (otherwise 24)
                enableRandomWrite = true,
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Repeat
            };
            //Graphics.Blit(startTexture, rt);
            rt.Create();
            Graphics.Blit(startTexture, rt);

            kernal = GOL.FindKernel("PrepareOut");
            
            GOL.SetTexture(kernal, "Out", rt);
            GOL.SetTexture(kernal, "From", rt);
            GOL.SetVector("rtSize", new Vector4(rt.width, rt.height));
            //GOL.Dispatch(kernal, Mathf.RoundToInt(rt.width *.125f), Mathf.RoundToInt(rt.height *.125f), 1);
        }
        if (rt1 == null)
        {
            rt1 = new RenderTexture(size.x, size.y, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB)
            { //depth of 0 to fix wrapMode not working (otherwise 24)
                enableRandomWrite = true,
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Repeat
            };
            rt1.Create();
        }
        mat.mainTexture = rt;
        
        kernal = GOL.FindKernel("GameOfLife");
    }

    void doComputer(RenderTexture target, RenderTexture from)
    {
        GOL.SetTexture(kernal, "Out", target);
        GOL.SetTexture(kernal, "From", from);
        GOL.SetVector("rtSize", new Vector4(target.width, target.height));
        GOL.Dispatch(kernal, Mathf.RoundToInt(target.width *.125f), Mathf.RoundToInt(target.height *.125f), 1);

        mat.mainTexture = target; 
    }

    private float waitedTime = 0;
    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            if (waitedTime > waitBetweenStep)
            {
                if (step)
                {
                    doComputer(rt1, rt);
                    step = !step;
                }
                else
                {
                    doComputer(rt, rt1);
                    step = !step;
                }

                waitedTime = 0;
            }

            waitedTime += Time.deltaTime;
        }

        fps = 1 / Time.deltaTime;
    }
}
