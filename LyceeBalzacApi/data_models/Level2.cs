using System.ComponentModel.DataAnnotations;

namespace LyceeBalzacApi.data_models
{
    public class Level2
    {
        [Key]
        public int Level2Id { get; set; }
        public int Id { get; set; }
        public string Level2_Name_A { get; set; }
        public string Level2_Name_E { get; set; }
        public string? Notes { get; set; }
        public int? Entry_ID { get; set; }
    }
}
