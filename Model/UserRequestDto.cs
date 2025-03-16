using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Usuario.Api.Model;

public class UserRequestDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}