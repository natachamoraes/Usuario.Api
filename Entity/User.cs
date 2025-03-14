using System.ComponentModel.DataAnnotations;

namespace Usuario.Api.Entity;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public DateOnly Age { get; set; }
}