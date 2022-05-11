#r "nuget:Newtonsoft.Json, 13.0.1"
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

if (Args.Count != 2)
{
    throw new ArgumentException("Expected two arguments: <project-assets-json> <output-file-path>");
}

var projectJsonPath = Args[0];
var packagesOutputPath = Args[1];
var projectAssets = JObject.Parse(File.ReadAllText(projectJsonPath));

var project = new Project();
var itemGroupCollection = new List<ItemGroup>();
var itemGroup = new ItemGroup();
var noneCollection = new List<PackageReference>();

if (projectAssets.TryGetValue("targets", out var value))
{
    foreach (var target in value.Children<JProperty>())
    {
        foreach (var packageOrReference in target.Value.Children<JProperty>())
        {
            if (packageOrReference.Value.Value<string>("type") == "package")
            {
                var nameAndVersion = packageOrReference.Name.Split("/");

                noneCollection.Add(new PackageReference()
                {
                    Include = nameAndVersion[0],
                    Version = nameAndVersion[1]
                });
            }
        }
    }
}

itemGroup.PackageReferenceCollection = noneCollection;
itemGroupCollection.Add(itemGroup);
project.ItemGroupCollection = itemGroupCollection;

var projectJson = JsonConvert.SerializeObject(project);
var projectXmlDocument = JsonConvert.DeserializeXmlNode(projectJson, nameof(Project));

// Make sure directory for output does exist.
var directoryPath = Path.GetDirectoryName(packagesOutputPath);

if (!Directory.Exists(directoryPath))
{
    Directory.CreateDirectory(directoryPath);
}

File.WriteAllText(packagesOutputPath, projectXmlDocument.OuterXml);

class Project
{
    [JsonProperty(nameof(ItemGroup))]
    public List<ItemGroup> ItemGroupCollection { get; set; }
}

class ItemGroup
{
    [JsonProperty(nameof(PackageReference))]
    public List<PackageReference> PackageReferenceCollection { get; set; }
}

class PackageReference
{
    [JsonProperty("@Include")]
    public string Include { get; set; }
    public string Version { get; set; }
}
