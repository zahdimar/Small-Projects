using System.ComponentModel.DataAnnotations;

namespace AvorionDataConverter.Entities {
    public abstract class AbstractEntity {
        [Key]
        public int Id { get; set; }
    }
}