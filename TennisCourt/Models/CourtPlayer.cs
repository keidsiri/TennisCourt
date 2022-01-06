namespace TennisCourt.Models
{
  
  public class CourtPlayer
  {
    public int CourtPlayerId { get; set;}
    public int PlayerId { get; set;}
    public int CourtId { get; set;}
    public virtual Player Player { get; set;}
    public virtual Court Court {get; set;}
  }
}