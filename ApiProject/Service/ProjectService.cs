using ApiProject.Modele;
using ApiProject._Sql;
using ApiProject.Query;
using Npgsql;

namespace ApiProject.Service;

public class ProjectService
{
    public static void CreateProject(PostProject project)
    {
        using (var connection = _Sql.Connection.GetConnection())
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            using (var commande = new NpgsqlCommand(ProjectQuery.TableProject, connection))
            {
                commande.ExecuteNonQuery();
            }
            
            using (var commande = new NpgsqlCommand(ProjectQuery.TableAssociationProject, connection))
            {
                commande.ExecuteNonQuery();
            }

            int idProject;

            using (var commande = new NpgsqlCommand(ProjectQuery.CreateProject, connection))
            {
                commande.Parameters.AddWithValue("@title", project.Title);
                commande.Parameters.AddWithValue("@description", project.Description);
                commande.Parameters.AddWithValue("@imageUrl", project.ImageUrl);
                commande.Parameters.AddWithValue("@githubUrl", project.GithubUrl);
                commande.Parameters.AddWithValue("@liveUrl", project.LiveUrl);
                commande.Parameters.AddWithValue("@date", project.Date);

                idProject = Convert.ToInt32(commande.ExecuteScalar());
            }
            
            Console.WriteLine(idProject);

            foreach (var language in project.ProgrammingLanguage)
            {
                Console.WriteLine("GORS CACAPROUT");
                using (var commande = new NpgsqlCommand(ProjectQuery.CreateProjectAssociation, connection))
                {
                    commande.Parameters.AddWithValue("@projectId", idProject);
                    commande.Parameters.AddWithValue("@programmingLanguageName", language.Name);
                    commande.Parameters.AddWithValue("@programmingLanguageUrl", language.Url);
                    
                    Console.WriteLine("GORS CACA");
                    commande.ExecuteNonQuery();
                }
            }

        }
    }

    public static List<ProjectModele> ReadProjects()
    {
        using (var connection = _Sql.Connection.GetConnection())
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            
            List<ProjectModele> projects = new List<ProjectModele>();
            
            using (var commande = new NpgsqlCommand(ProjectQuery.ReadProject, connection))
            using (var reader = commande.ExecuteReader())
            {
                while (reader.Read())
                {
                    var projectSql = new ProjectModele
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Description = reader.GetString(2),
                        ImageUrl = reader.GetString(3),
                        GithubUrl = reader.GetString(4),
                        LiveUrl = reader.IsDBNull(5) ? null : reader.GetString(5),
                        Date = reader.GetDateTime(6),
                    };

                    if (reader.FieldCount > 7 && !reader.IsDBNull(7))
                    {
                        var programmingLanguages = reader.GetFieldValue<string[]>(7);

                        if (programmingLanguages != null)
                        {
                            foreach (var language in programmingLanguages)
                            {
                                var parts = language.Split('|');
                                if (parts.Length == 2)
                                {
                                    projectSql.ProgrammingLanguage.Add(new LanguageAssociation
                                    {
                                        Name = parts[0],
                                        Url = parts[1]
                                    });
                                }
                            }
                        }
                    }
                    projects.Add(projectSql);
                }
            }
            return projects;
        }
    }

    public static void DeleteProject(int id)
    {
        using (var connection = _Sql.Connection.GetConnection())
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            using (var commande = new NpgsqlCommand(ProjectQuery.DeleteProject, connection))
            {
                commande.Parameters.AddWithValue("@id", id);
                
                commande.ExecuteNonQuery();
            }
        }
    }
}