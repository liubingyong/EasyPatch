print("lua game start10010...")

function OnInitOK()
    print("OnInitOK12...")

    print(json.encode({ 1, 2, 3, { x = 10 } }))

    local name = "uiassets.unity3d"
    local assetName = "TestCanvas"

    local prefab = CS.EasyPatch.ResourceManager.Instance:LoadAsset(name, assetName)
    if (prefab == nil)
    then 
        return
    end

    local go = CS.UnityEngine.Object.Instantiate(prefab)
    go.name = assetName;
    -- go.layer = LayerMask.NameToLayer("Default");
    -- //go.transform.SetParent(Parent);
    -- go.transform.localScale = CS.Vector3.one;

    -- go.transform.localPosition = Vector3.zero;
    -- //go.AddComponent<LuaBehaviour>();
end