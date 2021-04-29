using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exchange.Data.Models
{
    [Table("Logs")]
    public class Log
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public string LogType { get; set; }
    }
}
