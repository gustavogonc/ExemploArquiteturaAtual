namespace ExemploArquiteturaAtual.Models;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Active { get; set; } = true;
    public DateTime RegistryDate { get; set; } = DateTime.Now;
}

