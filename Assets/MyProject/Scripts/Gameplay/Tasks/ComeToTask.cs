using UnityEngine;

public class ComeToTask : GameTask
{
    private ComeToTaskData _comeToData;
    private Transform _player;

    public ComeToTask(ComeToTaskData data, Transform player) : base(data)
    {
        _comeToData = data;
        _player = player;
    }

    public override void CheckComplete()
    {
        if (Vector3.Distance(_player.position, _comeToData.ComeToPos) < _comeToData.MaxCheckDistance)
            Complete();
    }
}
