namespace LyceeBalzacApi.data_models
{
    public interface ILevel1Services
    {
        IEnumerable<Level1> GetAll();
        Level1 Get(int id);
        Level1 Add(Level1 record);
        void Remove(int id);
        bool Update(Level1 record);
    }
}
