using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HairSalon.Models
{
  public class Stylist
  {
    public int StylistId { get; set; }
    [Required]
    public string StylistFirstName { get; set; }
    [Required]
    public string StylistLastName { get; set; }
    public string Details { get; set; }
    public List<ClientStylist> JoinEntities { get; }
  }
}