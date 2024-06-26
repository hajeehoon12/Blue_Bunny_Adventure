
/// <summary>
/// 데이터를 동기화 하기 위해 필요한 인터페이스
/// ** Monobehavoir 붙어있는 클래스에다가만 붙히기 **
/// </summary>
public interface IDataPersistence
{
    void LoadData(GameData data);
    void SaveData(GameData data);
}
