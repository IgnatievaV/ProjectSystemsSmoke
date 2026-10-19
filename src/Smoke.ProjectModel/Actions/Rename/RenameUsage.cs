namespace Smoke.ProjectModel.Actions.Rename;

// Rename RenameMe.cs and check that this usage is updated.
public static class RenameUsage
{
    public static string Use() => new RenameMe().Name;
}
