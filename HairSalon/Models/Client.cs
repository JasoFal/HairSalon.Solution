namespace HairSalon.Models
{
  public class Client
  {
    public int ClientId { get; set; }
    public string ClientFirstName { get; set; }
    public string ClientLastName { get; set; }
    public int StylistId { get; set; }
    public Stylist Stylist { get; set; }
  }
}