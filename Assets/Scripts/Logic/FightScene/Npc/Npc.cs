using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{
    public int ThisId { get => _serverData.ThisID; }

    protected NavMeshAgent _agent;

    //引用  只读不写 不要修改
    CreateSomeNpcCmd _serverData; //服务器传递的角色index
    NpcDataBase _tableData;

    public void Init(CreateSomeNpcCmd createSomeNpcCmd, NpcDataBase npcDatabase)
    {
        this._serverData = createSomeNpcCmd;
        this._tableData = npcDatabase;


        _agent = gameObject.AddComponent<NavMeshAgent>();
        _agent.stoppingDistance = GameSetting.STOP_DISTANCE;
        _agent.speed = 5f;
        _agent.angularSpeed = float.MaxValue;       //角速度
        _agent.acceleration = float.MaxValue;

        transform.position = _serverData.Pos;
        transform.name = _serverData.Name;
        
    }
}
