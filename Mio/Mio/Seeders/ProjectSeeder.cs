namespace Mio.Seeders;

public class ProjectSeeder
{
    public List<ProjectModel> Projects { get; set; }

    public ProjectSeeder()
    {
        Projects = new List<ProjectModel>
        {
            new ProjectModel
            {
                Id = 1,
                Title = "Takamine",
                ProjectSpec = "React JS",
                Dependencies = "Mongo DB",
                Desc = "Post anonymous confessions without being identified. Moderators are present to manage discourse! Anonymous login like reddit.",
                ShortDesc = "Post anonymous confessions without being identified. Moderators are present to manage discourse! Anonymous login like reddit."
            },
            new ProjectModel
            {
                Id = 2,
                Title = "Ayane",
                ProjectSpec = "Swing",
                Dependencies = "AyaneApi",
                Desc = "Realtime videocall app designed for java open ended project.I couldnt finish it though back then!",
                ShortDesc = "Realtime videocall app"
            },
            new ProjectModel
            {
                Id = 3,
                Title = "Futaba",
                ProjectSpec = "Swing",
                Dependencies = "FutabaApi",
                Desc = "Basically google books, but with library inventory management!",
                ShortDesc ="Basically google books, but with library inventory management!"
            },
            new ProjectModel
            {
                Id = 4,
                Title = "Kiyone",
                ProjectSpec = "Swing",
                Dependencies = "KiyoneApi",
                Desc = "Doctor booking app with the power of SWING!",
                ShortDesc ="Doctor booking app with the power of SWING!"
            },
            new ProjectModel
            {
                Id = 5,
                Title = "Kaede",
                ProjectSpec = "Android",
                Dependencies = "GithubApi",
                Desc = "Todo app backed by Github. Store and retrieve notes via github. Made for use in old android tablets!",
                ShortDesc ="Todo app backed by Github. Store and retrieve notes via github. Made for use in old android tablets!"
            },
        };
    }

    public List<ProjectModel> Generate()
        => Projects;
}
