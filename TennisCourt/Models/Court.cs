using System.Collections.Generic;
using System;

namespace TennisCourt.Models
{

  public class Court
  {
    public Court()
    {
      this.JoinEntities = new HashSet<CourtPlayer>();
    }

    public int CourtId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string SurfaceType { get; set; }
    public bool Public { get; set; } = false;
    public bool Private { get; set; } = false;
    public virtual ICollection<CourtPlayer> JoinEntities { get; set; }
  }
}