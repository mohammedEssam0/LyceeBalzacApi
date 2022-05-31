using System.ComponentModel.DataAnnotations;

namespace LyceeBalzacApi.data_models
{
    public class Level1
    {
        [Key]
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string Notes { get; set; }
    }
}
