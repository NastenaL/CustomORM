namespace SeaBattle.ORM
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class RelatesEntityAttribute : Attribute
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
    }
}
