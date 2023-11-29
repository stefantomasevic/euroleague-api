using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Euroleague.Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("HomeTeam")]
        public int HomeId { get; set; }
        public Team HomeTeam { get; set; }

        [ForeignKey("GuestTeam")]
        public int GuestId { get; set; }

        public Team GuestTeam { get; set; }

        public int HomeScore { get; set; }

        public int GuestScore { get; set; }

        public ICollection<Statistic> Statistics { get; set; }
    }
}
