using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureCourse.Models;
using Microsoft.Azure.Documents.Client;

namespace AzureCourse.Services
{
    public class CourseStore
    {
        private readonly DocumentClient _client;
        private readonly Uri _link;
        private readonly FeedOptions _options = new FeedOptions { EnableCrossPartitionQuery = true };

        public CourseStore()
        {
            var uri = new Uri("https://azure-course-nosql.documents.azure.com:443/");
            var key = "4OmNXPURCJR59JXYooNpWtw7g2ytCabJvJ7eISNFBgRfMT9nX9WKukXZJzIjsq9aOYEElUwPPwsXphjz4y9nUw==";
            _client = new DocumentClient(uri, key);
            _link = UriFactory.CreateDocumentCollectionUri("azure-course-db", "courses");
        }

        public async Task InsertCourses(IEnumerable<Course> courses)
        {
            foreach (var course in courses)
            {
                await _client.CreateDocumentAsync(_link, course);
            }
        }

        public Task<IEnumerable<Course>> GetAllCourses()
        {
            var courses = _client.CreateDocumentQuery<Course>(_link, _options)
                                .OrderBy(c => c.Title)
                                .ToArray();

            return Task.FromResult(courses.AsEnumerable());
        }
    }
}
