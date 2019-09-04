namespace DbSchemaToPoco.Mssql.Cli.Schema.Models
{
    public enum ConstraintType
    {
        PrimaryKey = 0,
        ForeignKey = 1,
        Unique = 2,
        Check = 3,
        Default = 4
    }
}