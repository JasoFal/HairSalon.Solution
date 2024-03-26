using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Stylist
  {
    public int StylistId { get; set; }
    public string StylistFirstName { get; set; }
    public string StylistLastName { get; set; }
    public string Details { get; set; }
    public List<ClientStylist> JoinEntities { get; }
  }
}