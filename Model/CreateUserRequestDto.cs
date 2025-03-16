namespace Usuario.Api.Model;

public class CreateUserRequestDto
{
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public DateOnly Age { get; set; }
}