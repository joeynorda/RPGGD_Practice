using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NpcMgr:Singleton<NpcMgr>
{

    public Dictionary<int, Npc> AllNpc = new Dictionary<int, Npc>();

    /// <summary>
    /// 创建NPC
    /// </summary>
    /// <param name="cmd"></param>
    public static void OnCreateSomeNpc(Cmd cmd)
    {
        if (!Net.CheckCmd(cmd, typeof(CreateSomeNpcCmd)))
        {
            return;
        }
        CreateSomeNpcCmd createSomeNpcCmd = cmd as CreateSomeNpcCmd;

        Debug.Log("<color=#7FFF00><size=12>" + $"创建NPC" + "</size></color>");

        var npcDatabase = NpcTable.Instance[createSomeNpcCmd.ModelID];

        //创建NPC
        var npcObj = ResMgr.Instance.GetInstance(npcDatabase.ModelPath);
        var npc = npcObj.AddComponent<Npc>();

        npc.Init(createSomeNpcCmd, npcDatabase);

        NpcMgr.Instance.AllNpc[npc.ThisId] = npc;
    }

}
