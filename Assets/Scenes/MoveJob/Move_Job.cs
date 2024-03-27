using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

public class Move_Job : MonoBehaviour
{
    public int SpawnSize = 10000;

    [SerializeField] GameObject prefab;

    TransformAccessArray transformArray;

    void Awake()
    {
        transformArray = new TransformAccessArray(SpawnSize);

        for (int i = 0; i < SpawnSize; i++)
        {
            GameObject go = Instantiate(prefab);
            transformArray.Add(go.transform.GetInstanceID());
        }
    }
    void OnDestroy()
    {
        transformArray.Dispose();
    }

    void Update()
    {
        var sinMoveJob = new SinMoveJob()
        {
            time = Time.time,
        };

        var sinMoveJobHandle = sinMoveJob.Schedule(transformArray);

        var cosMoveJob = new CosMoveJob()
        {
            time = Time.time,
        };

        var cosMoveJobHandle = cosMoveJob.Schedule(transformArray, sinMoveJobHandle);

        var zMoveJob = new ZMoveJob()
        {
            time = Time.time,
        };

        zMoveJob.Schedule(transformArray, cosMoveJobHandle);
    }
}

/// <summary>
/// Special Job to run in parallel for manipulation transforms
/// </summary>
/// 
[BurstCompile]
public struct SinMoveJob : IJobParallelForTransform
{
    //Inputs
    public float time;

    //Interface
    public void Execute(int index, TransformAccess transform)
    {
        var pos = transform.position;

        //Move
        var y = math.sin(time + index) * math.sqrt(index);
        pos = new UnityEngine.Vector3(0, y, 0);

        transform.position = pos;
    }
}

/// <summary>
/// Special Job to run in parallel for manipulation transforms
/// </summary>
[BurstCompile]
public struct CosMoveJob : IJobParallelForTransform
{
    //Inputs
    public float time;

    //Interface
    public void Execute(int index, TransformAccess transform)
    {
        var pos = transform.position;

        //Move
        var x = math.cos(time + index) * math.sqrt(index);
        pos = new UnityEngine.Vector3(x, pos.y, 0);
        transform.position = pos;
    }
}

/// <summary>
/// Special Job to run in parallel for manipulation transforms
/// </summary>
[BurstCompile]
public struct ZMoveJob : IJobParallelForTransform
{
    //Inputs
    public float time;

    //Interface
    public void Execute(int index, TransformAccess transform)
    {
        var pos = transform.position;

        //Move
        float2 fx = new float2(pos.x, 0);
        float2 fy = new float2(0, pos.y);

        var dist = math.distance(fx, fy);
        var z = math.abs(dist + math.log(index + 1));
        pos = new UnityEngine.Vector3(pos.x, pos.y, z);

        transform.position = pos;
    }
}