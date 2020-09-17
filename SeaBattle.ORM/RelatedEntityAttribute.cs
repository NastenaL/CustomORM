namespace SeaBattle.ORM
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class RelatedEntityAttribute : Attribute
    {
        public string PropertyName { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
    }
}
