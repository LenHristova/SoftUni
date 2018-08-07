namespace FastFood.DataProcessor.Dto.Export
{
	using System.Xml.Serialization;

    [XmlType("Category")]
    public class CategoryStatisticsDto
    {
        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public MostPopularItemDto MostPopularItem { get; set; }
    }
}
