using System.ComponentModel;
using ModelContextProtocol.Server;


[McpServerToolType]
public static class EcoTool
{
    [McpServerTool, Description("Classifies an item into a waste category.")]
public static string ClassifyWaste(string item)
{
    return item.ToLower() switch
    {
        "banana peel" => "Organic Waste 🍌",
        "newspaper" => "Paper Recycling 📰",
        "plastic bottle" => "Plastic Recycling ♻️",
        "glass bottle" => "Glass Recycling 🍾",
        _ => "Unknown item. Please check local recycling guidelines."
    };
}


[McpServerTool, Description("Suggests a greener alternative to a common product.")]
public static string GreenAlternative(string item)
{
    return item.ToLower() switch
    {
        "plastic bag" => "Use a reusable cloth bag.",
        "plastic bottle" => "Carry a reusable steel or glass bottle.",
        "plastic straw" => "Use a steel or bamboo straw.",
        "paper cup" => "Carry a reusable coffee mug.",
        _ => "Try to choose reusable products whenever possible."
    };
}
}