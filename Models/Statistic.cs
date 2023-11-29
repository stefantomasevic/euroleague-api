using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Euroleague.Models
{
    public class Statistic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int GameId { get; set; }  
        public int PlayerId { get; set; } 
        public int Points { get; set; }
        public int Fouls { get; set; }
        public int Rebounds { get; set; }
       
        public Game Game { get; set; }
        public Player Player { get; set; }
    }
}
