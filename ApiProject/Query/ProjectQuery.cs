namespace ApiProject.Query;

public class ProjectQuery
{
    public static string CreateProject = """
                                         INSERT INTO Project (title, description, imageUrl, githubUrl, liveUrl, dateProject)
                                         VALUES (@title, @description, @imageUrl, @githubUrl, @liveUrl, @date)
                                         RETURNING id;
                                         """;
    
    public static string CreateProjectAssociation = """
                                         INSERT INTO ProjectProgrammingLanguages (projectId, programmingLanguageName, programmingLanguageUrl)
                                         VALUES (@projectId, @programmingLanguageName, @programmingLanguageUrl);
                                         """;

    public static string ReadProject = """
                                       SELECT 
                                           p.id, 
                                           p.title, 
                                           p.description, 
                                           p.imageUrl, 
                                           p.githubUrl, 
                                           p.liveUrl, 
                                           p.dateProject,
                                           ARRAY_AGG(pl.programmingLanguageName || '|' || pl.programmingLanguageUrl) as languages
                                       FROM 
                                           Project p
                                       LEFT JOIN 
                                           ProjectProgrammingLanguages pl ON p.id = pl.projectId
                                       GROUP BY 
                                           p.id, p.title, p.description, p.imageUrl, p.githubUrl, p.liveUrl, p.dateProject
                                       ORDER BY 
                                           p.dateProject DESC;
                                       """;

    public static string DeleteProject = """
                                         DELETE FROM Project WHERE id = @id;
                                         """;
    
    
    
    public static string TableProject = """
                                      CREATE TABLE IF NOT EXISTS Project (
                                        id SERIAL PRIMARY KEY,
                                        title VARCHAR(255),
                                        description TEXT,
                                        imageUrl VARCHAR(255),
                                        githubUrl VARCHAR(255),
                                        liveUrl VARCHAR(255) NULL,
                                        dateProject DATE
                                      );
                                      """;
  
    public static string TableAssociationProject = """
                                      CREATE TABLE IF NOT EXISTS ProjectProgrammingLanguages (
                                        projectId INT,
                                        programmingLanguageName VARCHAR(255),
                                        programmingLanguageUrl VARCHAR(255),
                                        FOREIGN KEY (projectId) REFERENCES Project(id) ON DELETE CASCADE
                                      );
                                      """;
}