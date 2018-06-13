using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
using Basis.Pattern;

namespace EasyPatch
{
    public class LuaManager : Singleton<LuaManager>
    {
        public static LuaEnv luaEnv = new LuaEnv();

        public void InitStart()
        {
            luaEnv.AddLoader((ref string filename) =>
            {
                if (!filename.EndsWith(".lua"))
                    filename = string.Format("{0}.lua", filename);

                string uri = Util.DataPath + "/lua/" + filename;

                return File.Exists(uri) ? File.ReadAllBytes(uri) : null;
            });

            luaEnv.DoString("require 'main'");
        }
    }
}
