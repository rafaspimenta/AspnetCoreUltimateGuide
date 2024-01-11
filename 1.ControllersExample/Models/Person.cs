using System;

namespace _1.ControllersExample.Models;

public class Person
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
}
