using Newtonsoft.Json;

namespace Test;


public class GetUserResponseModel
{
    public string AboutMe { get; set; }

    public int? Age { get; set; }

    public DateTime CreationDate { get; set; }

    public string DisplayName { get; set; }

    public int DownVotes { get; set; }

    public int UpVotes { get; set; }

    public int Views { get; set; }

    public IEnumerable<Badge> Badges { get; set; } = new List<Badge>();

    public class Badge
    {
        public string Name { get; set; }
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

