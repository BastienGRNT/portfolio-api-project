namespace ApiProject.Modele;

public class ProjectModele
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public List<LanguageAssociation> ProgrammingLanguage { get; set; } = new();
    public string GithubUrl { get; set; }
    public string? LiveUrl { get; set; }
    public DateTime Date { get; set; }
}

public class LanguageAssociation
{
    public string Name { get; set; }
    public string Url { get; set; }
}

public class PostProject
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public List<LanguageAssociation> ProgrammingLanguage { get; set; } = new();
    public string GithubUrl { get; set; }
    public string? LiveUrl { get; set; }
    public DateTime Date { get; set; }
}

