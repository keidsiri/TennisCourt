using System.Collections.Generic;
using System;

namespace TennisCourt.Models
{

  public class Player
  {

    public Player()
    {
      this.JoinEntities = new HashSet<CourtPlayer>();
    }

    public int PlayerId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Location { get; set; }

    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<CourtPlayer> JoinEntities { get; }
  }
}