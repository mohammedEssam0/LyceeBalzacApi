namespace LyceeBalzacApi.data_models
{
    public class Level1Services : ILevel1Services
    {
        private List<Level1> _level1Data = new List<Level1>();
        private int _nextId = 1;

        public Level1Services()
        {
            Add(new Level1 { Id = 1, ArabicName = "أحمد", EnglishName = "Ahmed", Notes = "adding new user" });
            Add(new Level1 { Id = 2, ArabicName = "أحمد", EnglishName = "Ahmed", Notes = "adding new user" });
            Add(new Level1 { Id = 3, ArabicName = "أحمد", EnglishName = "Ahmed", Notes = "adding new user" });
        }

        public Level1 Add(Level1 record)
        {
            if(record == null)
            {
                throw new ArgumentNullException("id");
            }
            record.Id = _nextId++;
            _level1Data.Add(record);
            return record;
        }

        public Level1 Get(int id)
        {
            return _level1Data.Find(x => x.Id == id);
        }

        public IEnumerable<Level1> GetAll()
        {
            return _level1Data;
        }

        public void Remove(int id)
        {
            _level1Data.RemoveAll(x => x.Id == id);
        }

        public bool Update(Level1 record)
        {
            if(record == null)
            {
                throw new ArgumentNullException("zz");

            }
            int index = _level1Data.FindIndex(x => x.Id == record.Id);
            if(index == -1)
            {
                return false;
            }
            _level1Data.RemoveAt(index);
            _level1Data.Add(record);
            return true;
        }
    }
}
