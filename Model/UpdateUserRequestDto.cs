namespace Usuario.Api.Model;

public class UpdateUserRequestDto
{
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Email { get; set; }
    
    public DateOnly Age { get; set; }
}