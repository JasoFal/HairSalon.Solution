using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HairSalon.Models
{
  public class Client
  {
    public int ClientId { get; set; }
    [Required]
    public string ClientFirstName { get; set; }
    [Required]
    public string ClientLastName { get; set; }
    public List<ClientStylist> JoinEntities { get; }
  }
}