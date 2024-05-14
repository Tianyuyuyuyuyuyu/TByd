using TBydFramework.Examples.Repositories;
using System.IO;
using UnityEngine;
#if LITEDB
using LiteDB;
#endif

public class DataExample : MonoBehaviour
{
    void Start()
    {
#if NEWTONSOFT
        LoadFromJson();
#endif

#if LITEDB
        LoadFromLiteDB();
#endif
    }

#if NEWTONSOFT
    void LoadFromJson()
    {
        JsonEquipmentInfoRepository equipmentInfoRepository = new JsonEquipmentInfoRepository();

        var e1 = equipmentInfoRepository.GetById(1);
        Debug.LogFormat("LoadFromJson  id:{0} quality:{1} health:{2}", e1.Id, e1.Quality, e1.Health);
        var e2 = equipmentInfoRepository.GetBySign("equip001", 4);
        Debug.LogFormat("LoadFromJson  id:{0} quality:{1} health:{2}", e2.Id, e2.Quality, e2.Health);
    }
#endif

#if LITEDB
    private LiteDatabase database;

    private LiteDatabase LoadDatabase()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("LiteDB/data");
        return new LiteDatabase(new MemoryStream(textAsset.bytes));
    }

    private void OnDestroy()
    {
        if (database != null)
            database.Dispose();
    }

    void LoadFromLiteDB()
    {
        if (database == null)
            database = LoadDatabase();

        LiteDBEquipmentInfoRepository equipmentInfoRepository = new LiteDBEquipmentInfoRepository(database);

        var e1 = equipmentInfoRepository.GetById(1);
        Debug.LogFormat("LoadFromLiteDB id:{0} quality:{1} health:{2}", e1.Id, e1.Quality, e1.Health);
        var e2 = equipmentInfoRepository.GetBySign("equip001", 4);
        Debug.LogFormat("LoadFromLiteDB id:{0} quality:{1} health:{2}", e2.Id, e2.Quality, e2.Health);
    }
#endif
}
