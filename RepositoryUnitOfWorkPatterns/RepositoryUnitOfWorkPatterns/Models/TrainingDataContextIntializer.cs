using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RepositoryUnitOfWorkPatterns.Models
{
    public class TrainingDataContextIntializer : DropCreateDatabaseAlways<TrainingDataContext>
    {
        protected override void Seed(TrainingDataContext dataContext)
        {
            var trainingCategories = new List<TrainingCategory>()
            {
                new TrainingCategory() {CategoryName = "Web Components", DateAdded = DateTime.Now, CategoryId = Guid.NewGuid()},
                new TrainingCategory() {CategoryName = "Utilities", DateAdded = DateTime.Now, CategoryId = Guid.NewGuid()},
                new TrainingCategory() {CategoryName = "Database Structures", DateAdded = DateTime.Now, CategoryId = Guid.NewGuid()}
            };

            trainingCategories.ForEach(tc => dataContext.TrainingCategory.Add(tc));
            dataContext.SaveChanges();

            Guid WebCategoryId = dataContext.TrainingCategory.Where(c => c.CategoryName == "Web Components").Select(c => c.CategoryId).SingleOrDefault();
            Guid UtilCategoryId = dataContext.TrainingCategory.Where(c => c.CategoryName == "Utilities").Select(c => c.CategoryId).SingleOrDefault();

            #region AssigningContextValues

            string GitHubContext = "GitHub is a Git repository web-based hosting service which offers all of the distributed revision control and source code management (SCM) functionality of Git as well as adding its own features." +
                                   " Unlike Git, which is strictly a command-line tool, GitHub provides a web-based graphical interface and desktop as well as mobile integration. " +
                                   "It also provides access control and several collaboration features such as wikis, task management, and bug tracking and feature requests for every project.";

            string WebAPIContext = "A server-side web API is a programmatic interface to a defined request-response message system, typically expressed in JSON or XML, which is exposed via the web—most commonly by means of an HTTP-based web server. " +
                                   "Mashups are web applications which combine the use of multiple such web APIs.[1] Webhooks are server-side web APIs that take as input a URI that is designed to be used like a remote named pipe or a type of callback such that the server acts as a client to dereference the provided URI and trigger an event on another server which handles this event thus providing a type of peer-to-peer IPC." +
                                   "While web API in this context is sometimes considered a synonym for web service, Web 2.0 web applications have moved away from SOAP-based web services towards more cohesive collections of RESTful web resources.[2] These RESTful web APIs are accessible via standard HTTP methods by a variety of HTTP clients including browsers and mobile devices.";

            string NugetContext = "NuGet is a free and open source package manager for the .NET Framework (formerly known as NuPack).[1][2] NuGet is distributed as a Visual Studio extension, integrated with SharpDevelop, and included in the C# code snippet tool LINQPad. " +
                                  "NuGet can be used from the command line and automated via scripts. NuGet supports native packages written in C++. [3] Since its introduction in 2010, NuGet has evolved into a larger ecosystem of tools and services.";

            #endregion

            var topics = new List<TrainingTopic>()
            {
                new TrainingTopic() {TopicName = "GitHub", TopicContext = GitHubContext, DateAdded = DateTime.Now, CategoryId = UtilCategoryId, Order = 1, TopicId = Guid.NewGuid()},
                new TrainingTopic() {TopicName = "WebAPI", TopicContext = WebAPIContext, DateAdded = DateTime.Now, CategoryId = WebCategoryId, Order = 1, TopicId = Guid.NewGuid()},
                new TrainingTopic() {TopicName = "Nuget", TopicContext = NugetContext, DateAdded = DateTime.Now, CategoryId = UtilCategoryId, Order = 2, TopicId = Guid.NewGuid()}
            };

            topics.ForEach(t => dataContext.TrainingTopic.Add(t));
            dataContext.SaveChanges();

        }
    }
}