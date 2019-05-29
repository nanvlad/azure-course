using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AzureCourse.Models
{
    public class Course
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public string Title { get; set; }

        public ICollection<Module> Modules { get; set; } = new List<Module>();
    }

    public class Module
    {
        [Key]
        public string Title { get; set; }

        public ICollection<Clip> Clips { get; set; } = new List<Clip>();
    }

    public class Clip
    {
        [Key]
        public string Name { get; set; }

        public int Length { get; set; }
    }
}