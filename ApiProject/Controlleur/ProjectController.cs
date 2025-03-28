using Microsoft.AspNetCore.Mvc;
using ApiProject.Modele;
using ApiProject.Service;

namespace ApiProject.Controlleur;

[ApiController]
[Route("/api/[controller]")]
public class ProjectController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] PostProject project)
    {
        try
        {
            ProjectService.CreateProject(project);
            return Ok("Project uploaded successfully.");
        }
        catch (Exception erreur)
        {
            return BadRequest($"Error uploading project: {erreur.Message}");
        }
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var project = ProjectService.ReadProjects();
            return Ok(project.Count > 0 ? project : "No Project found.");
        }
        catch (Exception erreur)
        {
            return BadRequest($"Error getting project: {erreur.Message}");
        }
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            ProjectService.DeleteProject(id);
            return Ok("Project deleting successfully.");
        }
        catch (Exception erreur)
        {
            return BadRequest($"Error deleting project: {erreur.Message}");
        }
    }
}