using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.RepositoryManager.Domain.Entities
{
    public class Repo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Url { get; set; }
        public string? Language { get; set; }
        public string Owner { get; set; }
        public int Stargazers { get; set; }
        public int Forks { get; set; }
        public int Watchers { get; set; }

        public const int MAX_LENGHT = 39;

        public Repo() { }

        public Repo(long id, string name, string description, string url, string language, string onwer, int stargazers, int forks, int watchers)
        {
            Id = id;
            Name = name;
            Description = description;
            Url = url;
            Language = language;
            Owner = onwer;
            Stargazers = stargazers;
            Forks = forks;
            Watchers = watchers;
        }
    }
}
